using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AB : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetsBudles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }      

}
