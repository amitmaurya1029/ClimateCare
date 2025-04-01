using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    
    void Start()
    {
      //  LoadAssetsLocally();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadAssetsLocally();
        }

    } 

    private void LoadAssetsLocally()
    {
         var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "testassetbundle"));

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Rock 1");
        Instantiate(prefab);
        Debug.Log("prefab gets instanciated : ");
    }
}
