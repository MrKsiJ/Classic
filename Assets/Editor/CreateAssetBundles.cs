using UnityEditor;

public class CreateAssetBundles 
{
    [MenuItem("Assets/BuildAssetBundels")]
   static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundels", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
