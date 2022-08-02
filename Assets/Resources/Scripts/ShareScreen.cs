using System.Collections;
using UnityEngine;

public class ShareScreen : MonoBehaviour
{
#if UNITY_ANDROID
    private bool isFocus = false;
    private string shareSubject, shareMessage;
    internal bool isProcessing = false;
    private string screenshotName;
    [SerializeField] private PlayerProfille profille;


    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButtonClick()
    {
        screenshotName = "Screen.png";
        shareSubject = "Share you'r high score";
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
                shareMessage = Translate.NameTextsChina[92];
                break;
            case SystemLanguage.ChineseSimplified:
                shareMessage = Translate.NameTextsChina[92];
                break;
            case SystemLanguage.ChineseTraditional:
                shareMessage = Translate.NameTextsChina[92];
                break;
            case SystemLanguage.Danish:
                shareMessage = Translate.NameTextsDanish[92];
                break;
            case SystemLanguage.Dutch:
                shareMessage = Translate.NameTextsDutch[92];
                break;
            case SystemLanguage.English:
                shareMessage = Translate.NameTextsEng[92];
                break;
            case SystemLanguage.Finnish:
                shareMessage = Translate.NameTextsFinnish[92];
                break;
            case SystemLanguage.French:
                shareMessage = Translate.NameTextsFrench[92];
                break;
            case SystemLanguage.German:
                shareMessage = Translate.NameTextsGerman[92];
                break;
            case SystemLanguage.Italian:
                shareMessage = Translate.NameTextsItalian[92];
                break;
            case SystemLanguage.Norwegian:
                shareMessage = Translate.NameTextsNorwegian[92];
                break;
            case SystemLanguage.Portuguese:
                shareMessage = Translate.NameTextsPortuguese[92];
                break;
            case SystemLanguage.Russian:
                shareMessage = Translate.NameTextsRU[92];
                break;
            case SystemLanguage.Spanish:
                shareMessage = Translate.NameTextsSpanishSpain[92];
                break;
            case SystemLanguage.Swedish:
                shareMessage = Translate.NameTextsSwedish[92];
                break;
        }
        ShareScreenshot();
    }

    private void ShareScreenshot()
    {
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshotInAnroid());
        }
    }


    public IEnumerator ShareScreenshotInAnroid()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(0.25f);

        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        ScreenCapture.CaptureScreenshot(screenshotName, 1);
        yield return new WaitForSecondsRealtime(0.25f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareSubject);
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
        profille.SaveDataPlayer();
        Application.LoadLevel(0);
    }
#endif
}