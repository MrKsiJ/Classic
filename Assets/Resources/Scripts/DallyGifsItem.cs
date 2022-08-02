using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class DallyGifsItem : MonoBehaviour
{
    private DallyGifsController controller;

    void OnEnable()
    {
#if UNITY_EDITOR
        Debug.Log("OnEnable");
        if(day == null)
            day = transform.Find("HeaderCurrentDay").GetComponent<Text>();

        if (giftInfo == null)
            giftInfo = transform.Find("HeaderCountText").GetComponent<Text>();

        if (GiftedPanel == null)
            GiftedPanel = gameObject;

        if (GiftedText == null)
            GiftedText = transform.Find("HeaderTextCountet").gameObject;

        if (GetGiftButton == null)
            GetGiftButton = transform.Find("GiftHappButton").gameObject;
#endif
    }

    public Text day;
    public Text giftInfo;

    public GameObject GiftedPanel;
    public GameObject GiftedText;

    public GameObject GetGiftButton;

    public void SetInfo(DallyGifsController controller,string dayText,string gifInfo,string gifttext,string pickupgift,short status = 0)
    {
        this.controller = controller;
        day.text = dayText;
        giftInfo.text = gifInfo;
        GiftedText.GetComponent<Text>().text = gifttext;
        GetGiftButton.transform.GetChild(0).GetComponent<Text>().text = pickupgift;

        if(status == 0)
        {
            Status0();
        }else if(status == 1)
        {
            Status1();
        }else if(status == 2)
        {
            Status2();
        }
    }

    public void OnClickGetGift()
    {
        controller.GetGift();
    }

    void Status0()
    {
        GiftedPanel.GetComponent<Button>().interactable = true;
        GiftedText.SetActive(false);
        GetGiftButton.SetActive(false);
    }

    private void Status1()
    {
        GiftedPanel.GetComponent<Button>().interactable = true;
        GiftedText.SetActive(false);
        GetGiftButton.SetActive(true);
    }
    private void Status2()
    {
        GiftedPanel.GetComponent<Button>().interactable = false;
        GiftedText.SetActive(true);
        GetGiftButton.SetActive(false);
    }
}
