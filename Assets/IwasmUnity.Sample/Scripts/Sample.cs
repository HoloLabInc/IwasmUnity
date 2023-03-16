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
        private Engine _engine;
        private Store _store;

        [SerializeField]
        private Button _button;
        [SerializeField]
        private Text _text;

        private byte[] _wasm;

        private void HelloSample()
        {
            try
            {
                if (_engine == null)
                {
                    _engine = new Engine();
                    _store = new Store(_engine);
                }
                using var module = Module.CreateFromWasm(_store, _wasm);
                var imports = module.CreateImports();
                imports.ImportAction("env", "hello", context =>
                {
                    var now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                    var message = $"hello from C# ({now})";
                    Debug.Log(message);
                    _text.text = message;
                });
                using var instance = module.CreateInstance(imports);
                var run = instance.Exports.GetFunction("run").ToAction();
                run.Invoke();
            }
            catch (Exception ex)
            {
                _text.text = ex.ToString();
                throw;
            }
        }

        private void Start()
        {
            if (_button == null) { return; }
            _button.interactable = false;
            StartCoroutine(LoadStreamingAssets("hello.wasm", wasm =>
            {
                _wasm = wasm;
                _button.interactable = true;
                _button.onClick.AddListener(HelloSample);
            }));
        }

        private void OnDestroy()
        {
            _store?.Dispose();
            _engine?.Dispose();
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
