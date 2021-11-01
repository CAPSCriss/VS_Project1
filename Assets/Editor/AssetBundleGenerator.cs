<<<<<<< Updated upstream
﻿using System.Collections;
=======
using System.Collections;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

<<<<<<< Updated upstream
public class AssetBundleBuilder
{
    struct BuildTargetInfo {
        public BuildTarget buildTarget;
        public string folderName;
    }

    static string outputPath;

    [MenuItem("Assets/XR Masters/Build AssetBundles")]
    static void BuildAllAssetBundles() {

        if (string.IsNullOrEmpty(outputPath))
            outputPath = PlayerPrefs.GetString("ab-output-path");

        outputPath = EditorUtility.OpenFolderPanel("Asset Bundle Output Path", outputPath, "");

        if (string.IsNullOrEmpty(outputPath)) {
            Debug.Log("Output path not specified");
            return;
        }
        else
            PlayerPrefs.SetString("ab-output-path", outputPath);

        var buildTargets = new List<BuildTargetInfo> {
            new BuildTargetInfo{buildTarget = BuildTarget.Android, folderName = "android"},
            new BuildTargetInfo{buildTarget = BuildTarget.iOS, folderName = "ios"},
            //new BuildTargetInfo{buildTarget = BuildTarget.WSAPlayer, folderName = "uwp"},
        };

        var outputFolderGUID = GUID.Generate().ToString();
        foreach(var platform in buildTargets) {
            if (!IsBuildTargetSupported(platform.buildTarget)) {
                Debug.Log($"{platform.buildTarget} skipped because build modules missing. Install unity build support modules for {platform.buildTarget} to generate asset bundles for {platform.buildTarget}");
                continue;
            }
            string assetBundleDirectory = $"{outputPath}/{outputFolderGUID}/{platform.folderName}";
            if (!Directory.Exists(assetBundleDirectory)) {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            platform.buildTarget);
        }

        var assetBundleNames = AssetDatabase.GetAllAssetBundleNames();

        foreach(var bundleName in assetBundleNames) {
            foreach(var platform in buildTargets) {
                var oldPath = $"{outputPath}/{outputFolderGUID}/{platform.folderName}";
                var oldFile = oldPath + "/" + bundleName;

                if (!File.Exists(oldFile)) continue;

                var newPath = $"{outputPath}/AssetBundles/{bundleName}/{platform.folderName}";
                var newFile = newPath + "/bundle";

                if (!Directory.Exists(oldPath))
                    Directory.CreateDirectory(oldPath);
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);


                if (File.Exists(newFile))
                    File.Delete(newFile);

                File.Move(oldFile, newFile);
            }
        }

        Directory.Delete($"{outputPath}/{outputFolderGUID}", true);
		Debug.Log($"Asset bundles created in {outputPath}");
    }

    static bool IsBuildTargetSupported(BuildTarget target) {
        var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
        var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

        return (bool)isPlatformSupportLoaded.Invoke(null, new object[] { (string)getTargetStringFromBuildTarget.Invoke(null, new object[] { target }) });
    }
}

=======
public class AssetBundleGenerator
{
   [MenuItem("Assets/Build Asset Bundles")]
    static void CreateAssetBundles() {

        var directoryPath = EditorUtility.OpenFolderPanel("Select bundle location", "", "");

        var platforms = new List<BuildTarget> { BuildTarget.Android, BuildTarget.iOS };

        if (!string.IsNullOrEmpty(directoryPath)) {
            foreach(var platform in platforms) {
                Debug.Log("Building asset bundles for platform " + platform);
                var finalPath = directoryPath + "/" + platform.ToString();
                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);
                BuildPipeline.BuildAssetBundles(finalPath, BuildAssetBundleOptions.None, platform);
            }
        }
    }

}
>>>>>>> Stashed changes
