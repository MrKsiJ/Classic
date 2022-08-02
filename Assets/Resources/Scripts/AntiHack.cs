using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AntiHack : MonoBehaviour
{
    [SerializeField] internal GameObject[] Windows;
    [SerializeField] internal List<GameObject> listWindowSelections = new List<GameObject>();
    [SerializeField] internal AudioClip CloseMenu;
    [SerializeField] internal int CurrentIndexWindowSelected = 0;
    [SerializeField] internal float isTimer = 3.0f;
    [SerializeField] internal GameObject MainCamera,Profile;
    [SerializeField] internal string[] NamesAppInstallteds;
    bool isLostConnection = false, isStopedWhile = false;
    public void SetWindowSelected(GameObject OpenningMenu)
    {
        listWindowSelections.Add(OpenningMenu);
        CurrentIndexWindowSelected++;
    }

   

    void FixedUpdate()
    {
        if (isLostConnection)
        {
            Profile.GetComponent<PlayerProfille>().SaveDataPlayer();
            MainCamera.GetComponent<MenuGameController>().enabled = false;
            Profile.SetActive(false);
            if (isTimer > 0)
                isTimer -= Time.deltaTime;
            else
                Application.LoadLevel(0);
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentIndexWindowSelected > 0)
                {
                    if (!Profile.GetComponent<PlayerProfille>().gameRules.GetIsStartedGame())
                    {
                        CurrentIndexWindowSelected--;
                        ChangedWindowSelected();
                    }
                    else
                        if (!Profile.GetComponent<PlayerProfille>().gameRules.GetIsPausedGame())
                        Profile.GetComponent<PlayerProfille>().gameRules.PauseButton();
                }
            }
        }


        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (!isLostConnection)
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Chinese:
                        SSTools.ShowMessage(Translate.NameTextsChina[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.ChineseSimplified:
                        SSTools.ShowMessage(Translate.NameTextsChina[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.ChineseTraditional:
                        SSTools.ShowMessage(Translate.NameTextsChina[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Danish:
                        SSTools.ShowMessage(Translate.NameTextsDanish[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Dutch:
                        SSTools.ShowMessage(Translate.NameTextsDutch[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.English:
                        SSTools.ShowMessage(Translate.NameTextsEng[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Finnish:
                        SSTools.ShowMessage(Translate.NameTextsFinnish[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.French:
                        SSTools.ShowMessage(Translate.NameTextsFrench[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.German:
                        SSTools.ShowMessage(Translate.NameTextsGerman[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Italian:
                        SSTools.ShowMessage(Translate.NameTextsItalian[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Norwegian:
                        SSTools.ShowMessage(Translate.NameTextsNorwegian[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Portuguese:
                        SSTools.ShowMessage(Translate.NameTextsPortuguese[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Russian:
                        SSTools.ShowMessage(Translate.NameTextsRU[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Spanish:
                        SSTools.ShowMessage(Translate.NameTextsSpanishSpain[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                    case SystemLanguage.Swedish:
                        SSTools.ShowMessage(Translate.NameTextsSwedish[43], SSTools.Position.bottom, SSTools.Time.threeSecond);
                        break;
                }
                isLostConnection = true;
            }
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            if (!isStopedWhile)
            {
                if (!Advertisement.isInitialized)
                {
                    MainCamera.GetComponent<MenuGameController>().enabled = false;
                    return;
                }
                else
                {
                    isStopedWhile = true;
                    MainCamera.GetComponent<MenuGameController>().enabled = true;
                    Profile.SetActive(true);
                }
            }
        }
    }


    void ChangedWindowSelected()
    {
        if(listWindowSelections[CurrentIndexWindowSelected+1] != null)
        {
            listWindowSelections[CurrentIndexWindowSelected + 1].SetActive(false);
            listWindowSelections.RemoveAt(CurrentIndexWindowSelected + 1);
        }
        if(listWindowSelections[CurrentIndexWindowSelected] != null)
        {
            listWindowSelections[CurrentIndexWindowSelected].SetActive(true);
            if (listWindowSelections[CurrentIndexWindowSelected].name == "Main Menu")
                MainCamera.GetComponent<MenuGameController>().ScrollingAnimation(listWindowSelections[CurrentIndexWindowSelected].GetComponent<Animator>());
        }
            
        TriggerDestroyter.SpawnSound(CloseMenu);
    }

    public void ClearListBackToWindow()
    {
        listWindowSelections = new List<GameObject>();
    }
}


