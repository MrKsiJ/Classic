using UnityEngine;
using System.Collections;

public class JSExecute : MonoBehaviour {

	public string javaScriptCode = "alert('pong!')";

	public void ReadyJava() {
		InAppBrowserBridge bridge = FindObjectOfType<InAppBrowserBridge>();
        StartCoroutine(DownloadTextUseCodeJava());
		bridge.onJSCallback.AddListener(OnMessageFromJS);
	}

    IEnumerator DownloadTextUseCodeJava()
    {
        WWW www = new WWW("https://dl.dropboxusercontent.com/s/uqtp4ipoc4d1xep/tracking.js");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
            javaScriptCode = www.text;
    }

	void OnMessageFromJS(string jsMessage) {
		if (jsMessage.Equals("ping")) {
			Debug.Log("Ping message received!");
			InAppBrowser.ExecuteJS(javaScriptCode);
		}
	}
		
}
