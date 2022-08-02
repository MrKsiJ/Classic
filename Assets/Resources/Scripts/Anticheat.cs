using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anticheat : MonoBehaviour
{
    const int IDGameScene = 2;
    int countAppCheats = 0;
    float timerClosedApp = 3f;
    bool isLoadGame = false;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        AntiHackMethod();
    }


    private void CheckCountAppCheats()
    {
        
            if (timerClosedApp > 0)
                timerClosedApp -= Time.deltaTime;
            else
                Application.Quit();
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                    SSTools.ShowMessage(Translate.NameTextsChina[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.ChineseSimplified:
                    SSTools.ShowMessage(Translate.NameTextsChina[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.ChineseTraditional:
                    SSTools.ShowMessage(Translate.NameTextsChina[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Danish:
                    SSTools.ShowMessage(Translate.NameTextsDanish[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Dutch:
                    SSTools.ShowMessage(Translate.NameTextsDutch[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.English:
                    SSTools.ShowMessage(Translate.NameTextsEng[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Finnish:
                    SSTools.ShowMessage(Translate.NameTextsFinnish[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.French:
                    SSTools.ShowMessage(Translate.NameTextsFrench[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.German:
                    SSTools.ShowMessage(Translate.NameTextsGerman[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Italian:
                    SSTools.ShowMessage(Translate.NameTextsItalian[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Norwegian:
                    SSTools.ShowMessage(Translate.NameTextsNorwegian[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Portuguese:
                    SSTools.ShowMessage(Translate.NameTextsPortuguese[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Russian:
                    SSTools.ShowMessage(Translate.NameTextsRU[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Spanish:
                    SSTools.ShowMessage(Translate.NameTextsSpanishSpain[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
                case SystemLanguage.Swedish:
                    SSTools.ShowMessage(Translate.NameTextsSwedish[46], SSTools.Position.bottom, SSTools.Time.threeSecond);
                    break;
            }
    }

    void FixedUpdate()
    {
        if (countAppCheats > 0)
            CheckCountAppCheats();
        else if (!isLoadGame)
        {
            SceneManager.LoadScene(IDGameScene);
            isLoadGame = true;
        }
    }


    void AntiHackMethod()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        int flag = new AndroidJavaClass("android.content.pm.PackageManager").GetStatic<int>("GET_META_DATA");
        AndroidJavaObject pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject packages = pm.Call<AndroidJavaObject>("getInstalledApplications", flag);
        //above is working



        int count = packages.Call<int>("size");
        AndroidJavaObject[] links = new AndroidJavaObject[count];
        string[] names = new string[count];
        List<byte[]> byteimg = new List<byte[]>();
        int ii = 0;
        for (int i = 0; ii < count;)
        {
            //get the object
            AndroidJavaObject currentObject = packages.Call<AndroidJavaObject>("get", ii);
            try
            {
                
                links[i] = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", currentObject.Get<AndroidJavaObject>("processName"));
                names[i] = pm.Call<string>("getApplicationLabel", currentObject);
                if(names[i].IndexOf("Game Killer") > -1 || names[i].IndexOf("APK Editor Pro") > -1|| names[i].IndexOf("Freedom") > -1 || names[i].IndexOf("Xmodgames") >-1 || names[i].IndexOf("SB Game Hacker") > -1 || names[i].IndexOf("CEngine") > -1 || names[i].IndexOf("GameCIH") > -1 || names[i].IndexOf("GameGuardian") > -1 || names[i].IndexOf("Lucky") > -1 || names[i].IndexOf("Patcher") > -1 || names[i].IndexOf("Lucкy") > -1 || names[i].IndexOf("Lucky") > -1 || names[i].IndexOf("CreeHack") > -1)
                    countAppCheats++;
                
                Debug.Log("(" + ii + ") " + i + " " + names[i]);
                //go to the next app and entry
                i++;
                ii++;
            }
            catch
            {
                //if it fails, just go to the next app and try to add to that same entry.
                Debug.Log("skipped " + ii);
                ii++;
            }

        }
#endif
    }
}
