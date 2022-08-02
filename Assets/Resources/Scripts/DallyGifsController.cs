using System;
using System.Collections.Generic;
using UnityEngine;

public class DallyGifsController : MonoBehaviour
{
    public DateTime LastGiftDateTime;
    [SerializeField] internal PlayerProfille profille;
    [SerializeField] internal ServerManager serverManager;
    public int DaysCount;

    private int MaxDayCount;

    private bool CanUserGetGift;

    public Action OnUserWantGift;

    private List<DallyGifsItem> items;

    void Awake()
    {
        Load();
        items = new List<DallyGifsItem>(transform.GetComponentsInChildren<DallyGifsItem>());
        Start(OnClick);
    }

    void OnClick()
    {
        switch (DaysCount)
        {
            case 0:
                serverManager.SetPlayerValueMoney(profille.GetIDPlayer(), 10, true);
                break;
            case 1:
                serverManager.SetPlayerValueMoney(profille.GetIDPlayer(), 20, true);
                break;
            case 2:
                serverManager.SetPlayerValueMoney(profille.GetIDPlayer(), 30, true);
                break;
            case 3:
                serverManager.SetPlayerValueMoney(profille.GetIDPlayer(), 40, true);
                break;
            case 4:
                serverManager.SetPlayerValueMoney(profille.GetIDPlayer(), 50, true);
                break;
        }
    }

    void Save()
    {
        profille.SetDaysCount(DaysCount);
        profille.SetLastTimeGiftDateTime(LastGiftDateTime.ToShortDateString());
    }

    void Load()
    {
        try
        {
            DaysCount = profille.GetDaysCount();
            LastGiftDateTime = DateTime.Parse(profille.GetLastTimeGiftDateTime());
        }
        catch(FormatException e)
        {
            DaysCount = 0;
            LastGiftDateTime = new DateTime();
        }
  
    }

    private void SetInfos()
    {
        for (int i = 0; i < items.Count; i++)
        {
            short status = 0;

            if (DaysCount == i && CanUserGetGift)
                status = 1;
            else if (DaysCount > i)
                status = 2;
            switch (profille.GetLanguageID())
            {
                case 0:
                    items[i].SetInfo(this, string.Format("{0} {1}", i+1, Translate.NameTextsChina[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsChina[95]),Translate.NameTextsChina[96],Translate.NameTextsChina[98], status);
                    break;
                case 1:
                    items[i].SetInfo(this, string.Format("{0} {1}", i+1, Translate.NameTextsDanish[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsDanish[95]), Translate.NameTextsDanish[96], Translate.NameTextsDanish[98], status);
                    break;
                case 2:
                    items[i].SetInfo(this, string.Format("{0} {1}", i+1, Translate.NameTextsDutch[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsDutch[95]), Translate.NameTextsDutch[96], Translate.NameTextsDutch[98], status);
                    break;
                case 3:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsEng[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsEng[95]), Translate.NameTextsEng[96], Translate.NameTextsEng[98], status);
                    break;
                case 4:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsFinnish[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsFinnish[95]), Translate.NameTextsFinnish[96], Translate.NameTextsFinnish[98], status);
                    break;
                case 5:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsFrench[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsFrench[95]), Translate.NameTextsFrench[96], Translate.NameTextsFrench[98], status);
                    break;
                case 6:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsGerman[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsGerman[95]), Translate.NameTextsGerman[96], Translate.NameTextsGerman[98], status); ;
                    break;
                case 7:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsItalian[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsItalian[95]), Translate.NameTextsItalian[96], Translate.NameTextsItalian[98], status);
                    break;
                case 8:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsNorwegian[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsNorwegian[95]), Translate.NameTextsNorwegian[96], Translate.NameTextsNorwegian[98], status);
                    break;
                case 9:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsPortuguese[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsPortuguese[95]), Translate.NameTextsPortuguese[96], Translate.NameTextsPortuguese[98], status);
                    break;
                case 10:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsRU[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsRU[95]), Translate.NameTextsRU[96], Translate.NameTextsRU[98], status);
                    break;
                case 11:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsSpanishSpain[94]), string.Format("{0} {1}", (i + 1) * 10, Translate.NameTextsSpanishSpain[95]), Translate.NameTextsSpanishSpain[96], Translate.NameTextsSpanishSpain[98], status);
                    break;
                case 12:
                    items[i].SetInfo(this, string.Format("{0} {1}", i + 1, Translate.NameTextsSwedish[94]), string.Format("{0} {1}", (i+1) * 10, Translate.NameTextsSwedish[95]), Translate.NameTextsSwedish[96], Translate.NameTextsSwedish[98], status);
                    break;
            }
        }
    }

    public void Start(Action callback, int _MaxDayCount = 5)
    {
        MaxDayCount = _MaxDayCount;
        if (callback != null)
        {
            OnUserWantGift = callback;
        }
        if (DateTime.Now.AddDays(-1).Day == LastGiftDateTime.Day && DateTime.Now.AddDays(-1).Month == LastGiftDateTime.Month)
        {
            CanUserGetGift = true;
        }
        else if(DateTime.Now.Day == LastGiftDateTime.Day && DateTime.Now.Month == LastGiftDateTime.Month)
            CanUserGetGift = false;
        else
        {
            DaysCount = 0;
            CanUserGetGift = true;
        }

        SetInfos();
    }

    internal void GetGift()
    {
        if (CanUserGetGift)
        {
            if (OnUserWantGift != null)
                OnUserWantGift();
            DaysCount++;

            if(DaysCount == MaxDayCount)
            {
                DaysCount = 0;
            }

            LastGiftDateTime = DateTime.Now;
            CanUserGetGift = false;
        }
        Save();
        SetInfos();
    }
}
