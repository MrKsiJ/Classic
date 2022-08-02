using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class BuyNoAds : MonoBehaviour
{
    const string PathToRewardPrefab = "Prefabs/IconSelectionBackgroundInGame";
    protected string xmlPublicKey = "<RSAKeyValue><Modulus>r+nYgIe/tmIbNZXkfgKuGxKY8H1oD6cWFOSEKOdpRbnqjpWQTozGYHkyiCuKDx7LbKnOnxd8S3qtBXS2IrDFdWztdHucM1pxZH9fSe89xTmMvhVhtejQvM11E55PDamSF3xEW4Tjk1XFeetjG3r169Mc0mb1Ycz4AgtConFfV6zAtGmp92Jlr9OzwqlNsJHcgjXgG5tfXcFJLtfaA7TlXpXNx0cGmVFtoD6uHstXlLWoV0UOxt/TB/qEYADm2U+NzWCO3gb5r6dblSTK3q2EAE6rv1WeufQr/OXWeiCZ1U5C9NJzVpF4T5DTXa4IUKS+XJyCIsViWDlIA+TnvJuNtQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    [SerializeField] PlayerProfille playerProfille;
    [SerializeField] GameObject RewardUI,ResultGameUI;
    [SerializeField] Transform ContentReward;

   void Start()
    {
        PurchaseManager.OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
    }

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        GooglePurchaseData data = new GooglePurchaseData(args.purchasedProduct.receipt);
        if (Verify(data.inAppPurchaseData, data.inAppDataSignature))
        {
            playerProfille.SetAdsPlayer(false);
            AddRewardInUI();
            if (ResultGameUI.activeSelf)
                ResultGameUI.SetActive(false);
            RewardUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Verify Google Play purchase. Protect you app against hack via Freedom. More info: http://mrtn.me/blog/2012/11/15/checking-google-play-signatures-on-net/
    /// </summary>
    /// <param name="purchaseJson">Purchase JSON string</param>
    /// <param name="base64Signature">Purchase signature string</param>
    /// <param name="xmlPublicKey">XML public key. Use http://superdry.apphb.com/tools/online-rsa-key-converter to convert RSA public key from Developer Console</param>
    /// <returns></returns>
    public bool Verify(string purchaseJson, string base64Signature)
    {
        using (var provider = new RSACryptoServiceProvider())
        {
            try
            {
                provider.FromXmlString(xmlPublicKey);

                var signature = Convert.FromBase64String(base64Signature);
                var sha = new SHA1Managed();
                var data = System.Text.Encoding.UTF8.GetBytes(purchaseJson);

                return provider.VerifyData(data, sha, signature);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
            }

            return false;
        }
    }


private void AddRewardInUI()
    {
        GameObject reward = Instantiate(Resources.Load<GameObject>(PathToRewardPrefab));
        reward.transform.SetParent(ContentReward);
        reward.transform.localScale = new Vector3(1, 1, 1);
        reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsRU[13];
        reward.transform.GetComponent<Image>().sprite = playerProfille.GetRewardsImages()[13];
        playerProfille.AddItemInInventory(13);
    }
}

public class GooglePurchaseData
{
    // INAPP_PURCHASE_DATA
    public string inAppPurchaseData;
    // INAPP_DATA_SIGNATURE
    public string inAppDataSignature;

    [System.Serializable]
    private struct GooglePurchaseReceipt
    {
        public string Payload;
    }
    [System.Serializable]
    private struct GooglePurchasePayload
    {
        public string json;
        public string signature;
    }

    public GooglePurchaseData(string receipt)
    {
        try
        {
            var purchaseReceipt = JsonUtility.FromJson<GooglePurchaseReceipt>(receipt);
            var purchasePayload = JsonUtility.FromJson<GooglePurchasePayload>(purchaseReceipt.Payload);
            inAppPurchaseData = purchasePayload.json;
            inAppDataSignature = purchasePayload.signature;
        }
        catch
        {
            Debug.Log("Could not parse receipt: " + receipt);
            inAppPurchaseData = "";
            inAppDataSignature = "";
        }
    }
}
