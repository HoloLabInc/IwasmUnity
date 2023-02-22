using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace IwasmUnity.Sample
{
    public sealed class Sample : MonoBehaviour
    {
        private Module _module;
        private Instance _instance;

        [SerializeField]
        private Button _button;
        [SerializeField]
        private Text _text;

        private bool _init;

        // sample1
        private byte[] _wasm1;

        // sample2
        private Func<int, int, int> _add;

        private void Start()
        {
            if (_button == null)
            {
                return;
            }

            var runSample1 = false;

            if (runSample1)
            {
                _button.interactable = false;
                StartCoroutine(LoadStreamingAssets("WasiCsTest.wasm", data =>
                {
                    _wasm1 = data;
                    _button.interactable = true;
                    _button.onClick.AddListener(() => RunSample1());
                }));
            }
            else
            {
                _button.onClick.AddListener(() => RunSample2());
            }
        }

        private void RunSample1()
        {
            if (_init == false)
            {
                WasmRuntime.Init();
                _module = Module.LoadWasm(_wasm1);
                _module.SetWasiArgs(new string[0], new string[] { "hoge" });
                _instance = Instance.Create(_module);
            }
            _text.text = "start wasi main function...";
            Debug.Log("start wasi main function...");
            var wasiMain = _instance.FindFunction("_start").ToAction();
            try
            {
                wasiMain();
            }
            catch (Exception ex)
            {
                _text.text = ex.Message;
                throw;
            }
            _text.text = "end wasi main function";
            Debug.Log("end wasi main function");
        }

        private void RunSample2()
        {
            // no wasi
            // import env.on_message(i32, i32) -> void
            // export add(i32, i32) -> i32

            if (_init == false)
            {
                WasmRuntime.Init();
                WasmRuntime.ImportAction("env", "on_message", (ImportedContext context, int messageAddr, int messageLen) =>
                {
                    var message = context.ReadUtf8(messageAddr, messageLen);
                    if (_text != null)
                    {
                        _text.text = message;
                    }
                    Debug.Log(message);
                });

                var wasm = SampleWasmFile2.GetBytes();
                _module = Module.LoadWasm(wasm);
                _instance = Instance.Create(_module);
                _init = true;
            }

            var x = UnityEngine.Random.Range(0, 100);
            var y = UnityEngine.Random.Range(0, 100);
            _add ??= _instance.FindFunction("add").ToFunc<int, int, int>();
            var result = _add(x, y);
            _text.text += $" is {result}";
            Debug.Log($"result: {result}");
        }


        private void OnDestroy()
        {
            _module?.Dispose();
            _instance?.Dispose();
            _module = null;
            _instance = null;
        }

        private IEnumerator LoadStreamingAssets(string name, Action<byte[]> callback)
        {
            var path = Path.Combine(Application.streamingAssetsPath, name);
            byte[] data;
            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                var request = UnityWebRequest.Get(path);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    data = request.downloadHandler.data;
                }
                else
                {
                    Debug.LogError(request.error);
                    yield break;
                }
            }
            else
            {
                try
                {
                    data = File.ReadAllBytes(path);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    yield break;
                }
            }
            callback.Invoke(data);
        }
    }
}
