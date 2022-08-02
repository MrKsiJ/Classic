using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadScript : MonoBehaviour
{
    private string bundleURL = "https://drive.google.com/uc?export=download&id=1F1JvsZL8yK0plLxNk54YjUkYFmLqz4rK";
    private int version = 0;
    bool isLoadingScene = false;
    [SerializeField] Text PreLoadResources;
    [SerializeField] List<AudioClip> soundtracks = new List<AudioClip>();
    AssetBundle assetBundle;
    WWW www;
    IEnumerable<AssetBundle> assetBundles;
    AssetBundleRequest assets;
    void InternetErrorConnection()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
                PreLoadResources.text = Translate.NameTextsChina[43];
                break;
            case SystemLanguage.ChineseSimplified:
                PreLoadResources.text = Translate.NameTextsChina[43];
                break;
            case SystemLanguage.ChineseTraditional:
                PreLoadResources.text = Translate.NameTextsChina[43];
                break;
            case SystemLanguage.Danish:
                PreLoadResources.text = Translate.NameTextsDanish[43];
                break;
            case SystemLanguage.Dutch:
                PreLoadResources.text = Translate.NameTextsDutch[43];
                break;
            case SystemLanguage.English:
                PreLoadResources.text = Translate.NameTextsEng[43];
                break;
            case SystemLanguage.Finnish:
                PreLoadResources.text = Translate.NameTextsFinnish[43];
                break;
            case SystemLanguage.French:
                PreLoadResources.text = Translate.NameTextsFrench[43];
                break;
            case SystemLanguage.German:
                PreLoadResources.text = Translate.NameTextsGerman[43];
                break;
            case SystemLanguage.Italian:
                PreLoadResources.text = Translate.NameTextsItalian[43];
                break;
            case SystemLanguage.Norwegian:
                PreLoadResources.text = Translate.NameTextsNorwegian[43];
                break;
            case SystemLanguage.Portuguese:
                PreLoadResources.text = Translate.NameTextsPortuguese[43];
                break;
            case SystemLanguage.Russian:
                PreLoadResources.text = Translate.NameTextsRU[43];
                break;
            case SystemLanguage.Spanish:
                PreLoadResources.text = Translate.NameTextsSpanishSpain[43];
                break;
            case SystemLanguage.Swedish:
                PreLoadResources.text = Translate.NameTextsSwedish[43];
                break;
        }
    }

    void LanguagePreLoad()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
                    PreLoadResources.text = Translate.NameTextsChina[48];
                break;
            case SystemLanguage.ChineseSimplified:
                    PreLoadResources.text = Translate.NameTextsChina[48];
                break;
            case SystemLanguage.ChineseTraditional:
                    PreLoadResources.text = Translate.NameTextsChina[48];
                break;
            case SystemLanguage.Danish:
                    PreLoadResources.text = Translate.NameTextsDanish[48];
                break;
            case SystemLanguage.Dutch:
                    PreLoadResources.text = Translate.NameTextsDutch[48];
                break;
            case SystemLanguage.English:
                    PreLoadResources.text = Translate.NameTextsEng[48];
                break;
            case SystemLanguage.Finnish:
                    PreLoadResources.text = Translate.NameTextsFinnish[48];
                break;
            case SystemLanguage.French:
                    PreLoadResources.text = Translate.NameTextsFrench[48];
                break;
            case SystemLanguage.German:
                    PreLoadResources.text = Translate.NameTextsGerman[48];
                break;
            case SystemLanguage.Italian:
                    PreLoadResources.text = Translate.NameTextsItalian[48];
                break;
            case SystemLanguage.Norwegian:
                    PreLoadResources.text = Translate.NameTextsNorwegian[48];
                break;
            case SystemLanguage.Portuguese:
                    PreLoadResources.text = Translate.NameTextsPortuguese[48];
                break;
            case SystemLanguage.Russian:
                    PreLoadResources.text = Translate.NameTextsRU[48];
                break;
            case SystemLanguage.Spanish:
                    PreLoadResources.text = Translate.NameTextsSpanishSpain[48];
                break;
            case SystemLanguage.Swedish:
                    PreLoadResources.text = Translate.NameTextsSwedish[48];
                break;
        }
    }

    IEnumerator DownloadAssetBundle()
    {
        while (!Caching.ready)
            yield return null;

        www = WWW.LoadFromCacheOrDownload(bundleURL, version);
        yield return www;
        if(!string.IsNullOrEmpty(www.error))
            Debug.LogError(www.error);
        assetBundle = www.assetBundle;
        StartCoroutine(LoadingMusic());
    }

    IEnumerator LoadingMusic()
    {
        assets = assetBundle.LoadAllAssetsAsync();
        yield return assets;
        UnityEngine.Object[] assetsobj = assets.allAssets;
        for (int i = 0; i < assetsobj.Length; i++)
            soundtracks.Add(assetsobj[i] as AudioClip);

        GameObject sounds = new GameObject();
        sounds.name = "SoundTracks";
        sounds.AddComponent<ListMusicGetting>();
        sounds.GetComponent<ListMusicGetting>().audioClips = soundtracks;
        DontDestroyOnLoad(sounds);
    }
  

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (Application.internetReachability == NetworkReachability.NotReachable)
            InternetErrorConnection();
        else
        {
            assetBundles = AssetBundle.GetAllLoadedAssetBundles();
            if (assetBundles.Count() <= 0)
            {
                AssetBundle.UnloadAllAssetBundles(true);
                LanguagePreLoad();
                StartCoroutine(DownloadAssetBundle());
            }
            else
            {
                Debug.LogError("Зашёл!");
                foreach (AssetBundle AB in assetBundles)
                {
                    Debug.LogError("Найден Ассет загружаю!");
                    assetBundle = AB;
                    break;
                }
                StartCoroutine(LoadingMusic());
            }
        }  
    }

    void FixedUpdate()
    {
            if (soundtracks.Count == 11 && !isLoadingScene)
            {
                isLoadingScene = true;
                Application.LoadLevel(1);
            }
    }

   
}

  
