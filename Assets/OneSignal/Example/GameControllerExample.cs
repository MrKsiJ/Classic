/**
 * Modified MIT License
 * 
 * Copyright 2016 OneSignal
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * 1. The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * 2. All copies of substantial portions of the Software may only be used in connection
 * with services provided by OneSignal.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections.Generic;

using OneSignalPush.MiniJSON;
using System;

public class GameControllerExample : MonoBehaviour {

    private static string extraMessage;
    public string email = "Email Address";
    public string externalId = "External User ID";

    private static bool requiresUserPrivacyConsent = false;

    

    public void Inizialize()
    {
        extraMessage = null;

        // Enable line below to debug issues with setuping OneSignal. (logLevel, visualLogLevel)
        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);

        // If you set to true, the user will have to provide consent
        // using OneSignal.UserDidProvideConsent(true) before the
        // SDK will initialize
        OneSignal.SetRequiresUserPrivacyConsent(requiresUserPrivacyConsent);

        // The only required method you need to call to setup OneSignal to receive push notifications.
        // Call before using any other methods on OneSignal (except setLogLevel or SetRequiredUserPrivacyConsent)
        // Should only be called once when your app is loaded.
        // OneSignal.Init(OneSignal_AppId);
        OneSignal.StartInit("7e5be357-7543-4530-a375-a540e5eef7a3")
            .HandleNotificationReceived(HandleNotificationReceived)
            .HandleNotificationOpened(HandleNotificationOpened)
            .HandleInAppMessageClicked(HandlerInAppMessageClicked)
            .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        OneSignal.permissionObserver += OneSignal_permissionObserver;
        OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
        OneSignal.emailSubscriptionObserver += OneSignal_emailSubscriptionObserver;
        var pushState = OneSignal.GetPermissionSubscriptionState();
        OneSignal.SendTag("nobot", "1");
    }


    // Examples of using OneSignal External User Id
    private void OneSignalExternalUserIdCallback(Dictionary<string, object> results)
    {
        // The results will contain push and email success statuses
        Console.WriteLine("External user id updated with results: " + Json.Serialize(results));

        // Push can be expected in almost every situation with a success status, but
        // as a pre-caution its good to verify it exists
        if (results.ContainsKey("push"))
        {
            Dictionary<string, object> pushStatusDict = results["push"] as Dictionary<string, object>;
            if (pushStatusDict.ContainsKey("success"))
            {
                Console.WriteLine("External user id updated for push with results: " + pushStatusDict["success"] as string);
            }
        }

        // Verify the email is set or check that the results have an email success status
        if (results.ContainsKey("email"))
        {
            Dictionary<string, object> emailStatusDict = results["email"] as Dictionary<string, object>;
            if (emailStatusDict.ContainsKey("success"))
            {
                Console.WriteLine("External user id updated for email with results: " + emailStatusDict["success"] as string);
            }
        }
    }

    private void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges) {
	    Debug.Log("SUBSCRIPTION stateChanges: " + stateChanges);
	    Debug.Log("SUBSCRIPTION stateChanges.to.userId: " + stateChanges.to.userId);
	    Debug.Log("SUBSCRIPTION stateChanges.to.subscribed: " + stateChanges.to.subscribed);
   }

   private void OneSignal_permissionObserver(OSPermissionStateChanges stateChanges) {
	    Debug.Log("PERMISSION stateChanges.from.status: " + stateChanges.from.status);
	    Debug.Log("PERMISSION stateChanges.to.status: " + stateChanges.to.status);
   }

    private void OneSignal_emailSubscriptionObserver(OSEmailSubscriptionStateChanges stateChanges) {
	    Debug.Log("EMAIL stateChanges.from.status: " + stateChanges.from.emailUserId + ", " + stateChanges.from.emailAddress);
	    Debug.Log("EMAIL stateChanges.to.status: " + stateChanges.to.emailUserId + ", " + stateChanges.to.emailAddress);
    }

    // Called when your app is in focus and a notificaiton is recieved.
    // The name of the method can be anything as long as the signature matches.
    // Method must be static or this object should be marked as DontDestroyOnLoad
    private static void HandleNotificationReceived(OSNotification notification) {
        OSNotificationPayload payload = notification.payload;
        string message = payload.body;

        print("GameControllerExample:HandleNotificationReceived: " + message);
        print("displayType: " + notification.displayType);
        extraMessage = "Notification received with text: " + message;

        Dictionary<string, object> additionalData = payload.additionalData;
        if (additionalData == null) 
            Debug.Log ("[HandleNotificationReceived] Additional Data == null");
        else
            Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Json.Serialize(additionalData) as string);
    }

    // Called when a notification is opened.
    // The name of the method can be anything as long as the signature matches.
    // Method must be static or this object should be marked as DontDestroyOnLoad
    public static void HandleNotificationOpened(OSNotificationOpenedResult result) {
        OSNotificationPayload payload = result.notification.payload;
        string message = payload.body;
        string actionID = result.action.actionID;

        print("GameControllerExample:HandleNotificationOpened: " + message);
        extraMessage = "Notification opened with text: " + message;
      
        Dictionary<string, object> additionalData = payload.additionalData;
        if (additionalData == null) 
            Debug.Log ("[HandleNotificationOpened] Additional Data == null");
        else
            Debug.Log("[HandleNotificationOpened] message " + message + ", additionalData: " + Json.Serialize(additionalData) as string);

        if (actionID != null) {
            // actionSelected equals the id on the button the user pressed.
            // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
            extraMessage = "Pressed ButtonId: " + actionID;
        }
    }

    public static void HandlerInAppMessageClicked(OSInAppMessageAction action) {
        String logInAppClickEvent = "In-App Message Clicked: " +
            "\nClick Name: " + action.clickName +
            "\nClick Url: " + action.clickUrl +
            "\nFirst Click: " + action.firstClick +
            "\nCloses Message: " + action.closesMessage;

        print(logInAppClickEvent);
        extraMessage = logInAppClickEvent;
    }
}
