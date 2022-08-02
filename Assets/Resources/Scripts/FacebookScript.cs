using Facebook.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookScript: MonoBehaviour
{
    [SerializeField]internal string Facebookdeeplink = "";
    [SerializeField] internal BrowserOpener browser;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                    FacebookLogin();
                }
                else
                    Debug.LogError("Не удалось инициализировать плагин FaceBook");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
        {
            FB.ActivateApp();
            FacebookLogin();
        }
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
        FacebookDeepLink();
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion

    public void FacebookDeepLink()
    {
        FB.GetAppLink(DeepLinkCallback);
        browser.OnButtonClicked();
    }

    void DeepLinkCallback(IAppLinkResult result)
    {
        if (!string.IsNullOrEmpty(result.Url))
        {
            var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
            if (index != -1)
            {
                Facebookdeeplink = result.Url;
            }
        }
    }

}
