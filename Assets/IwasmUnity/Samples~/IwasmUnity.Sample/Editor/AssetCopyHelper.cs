#nullable enable
#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IwasmUnity.Sample
{
    internal sealed class AssetCopyHelper
    {
        private static readonly (GUID Guid, string Name)[] _assets =
        {
            (new GUID("48331536284bb2c4e8ab3f4ad8af9367"), "hello.wat"),
            (new GUID("065708f7ad6220e4dbd876847f38d569"), "hello.wasm"),
        };

        [InitializeOnLoadMethod]
        public static void Run()
        {
            CopyAssets();
        }

        private static void CopyAssets()
        {
            var copied = false;
            foreach (var (guid, name) in _assets)
            {
                var streamingAssetsDir = Application.streamingAssetsPath;
                if (Directory.Exists(streamingAssetsDir) == false)
                {
                    Directory.CreateDirectory(streamingAssetsDir);
                }
                var dest = $"{streamingAssetsDir}/{name}";
                if (File.Exists(dest)) { continue; }
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (string.IsNullOrEmpty(assetPath))
                {
                    Debug.LogError($"asset not found: {guid}");
                    continue;
                }
                var newAssetPath = new Uri(Application.dataPath).MakeRelativeUri(new Uri(dest)).ToString();
                AssetDatabase.CopyAsset(assetPath, newAssetPath);
                copied = true;
            }

            if (copied)
            {
                AssetDatabase.Refresh();
            }
        }
    }
}

#endif
