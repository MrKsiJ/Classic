using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ResultGame : MonoBehaviour
{
    const int minRndMM = -5, maxRndMM = 5;
    const int minRndOther = -50, maxRndOther = 50;
    const string PathToRewardPrefab = "Prefabs/IconSelectionBackgroundInGame";
    [SerializeField] Text MatchMakingNumberText, PlayTimerTheOverText, SongsSurviveText, DifficulyTextName, CountScoreText, CountMoneyText,CountComboText;
    [SerializeField] GameObject SelectGameMode, RewardsGameUI;
    [SerializeField] Image DifficulyIcon;
    [SerializeField] Transform ContentRewards;
    [SerializeField] internal PlayerProfille playerProfille;
    [SerializeField] internal ServerManager serverManager;
    [SerializeField] internal MenuGameController SoundTracks;
    [SerializeField] internal GameRules gameRules;
    [SerializeField] internal AudioSource MusicCloundGame;
    [SerializeField] internal AudioClip ResultPick, RewardPick;
    [SerializeField] internal int RewardPickUp = 0;
    [SerializeField] internal int CountRewards = 0;
    [SerializeField] internal bool isStopedGame = false;
    [SerializeField] private Button MultiplayButton;
    private string rewardedID = "rewardedVideo";
    private string gameID = "3653556";

    float CountMoneyPlayerGame = 0.0f;
    int IDPlayer = 0;
  

    internal void SetIsStopedGame(bool changed)
    {
        isStopedGame = changed;
    }
    internal void StopedGame()
    {
        RewardPickUp = 0;
        CountRewards = 0;
    }


    internal void DataRefresh(int NumberMatchMaking,float secondsSurvive,int SongsCountSurvive,string NameDifficuly,float CountScore,float RecordScore,float CountMoney,Sprite DifficulyIcon,bool isMatchMaking,int ComboCountRecordPlayer,int GetLanguageID,int IDPlayer,float currentBulletsGun,float maxBulletsGun)
    {
        MatchMakingNumberText.text = "#" + NumberMatchMaking;
        CountMoneyPlayerGame = CountMoney;
        this.IDPlayer = IDPlayer;
        TimerSurvive(secondsSurvive);
        SongsSurviveText.text = SongsCountSurvive.ToString();
        DifficulyTextName.text = NameDifficuly;
        CountScoreText.text = Mathf.RoundToInt(CountScore).ToString();
        CountMoneyText.text = Mathf.RoundToInt(CountMoney).ToString();
        CountComboText.text = ComboCountRecordPlayer.ToString();
        this.DifficulyIcon.sprite = DifficulyIcon;

        serverManager.SetPlayerValueMoney(IDPlayer, CountMoney,true);
        playerProfille.SetCurrentBulletsAWP(currentBulletsGun,'=');
        playerProfille.SetMaxBulletsAWP(maxBulletsGun,'=');
        playerProfille.SetIsSposobnosty(maxBulletsGun <= 0);
        if(playerProfille.GetRecordScorePlayer() < RecordScore)
            playerProfille.RecordScoreChanged(RecordScore);
        if (playerProfille.GetRecordComboPlayer() < ComboCountRecordPlayer)
            playerProfille.RecordComboChanged(ComboCountRecordPlayer);
        if (isMatchMaking)
        {
            if (playerProfille.GetTimeSurivePlayer() < secondsSurvive)
                playerProfille.RecordTimerSurviveChanged(secondsSurvive);

            if (!isStopedGame)
            {
                RewardPickUp = UnityEngine.Random.Range(minRndMM, maxRndMM);
                if (RewardPickUp > 0)
                    PickUpRewards(GetLanguageID);
            }

        }
        else
        {
            if (!isStopedGame)
            {
                RewardPickUp = UnityEngine.Random.Range(minRndOther, maxRndOther);
                if (RewardPickUp > 0)
                    PickUpRewards(GetLanguageID);
            }
        }
        playerProfille.SaveDataPlayer();
    }

    public void SelectGameModesOrRewards()
    {
        MusicCloundGame.clip = SoundTracks.SoundsTracks[10];
        MusicCloundGame.Play();
        MusicCloundGame.loop = true;
        gameRules.SetDifficuly(0);
        gameRules.MatchMakingTimer.GetComponent<Text>().text = "0:0.000";
        if (RewardPickUp > 0 && CountRewards > 0)
        {
            RewardsGameUI.SetActive(true);
            TriggerDestroyter.SpawnSound(RewardPick);
        }
        else
        {
            playerProfille.VersionCheckerMethod();
            SelectGameMode.SetActive(true);
        }

        IDPlayer = 0;
        CountMoneyPlayerGame = 0;
        if (!MultiplayButton.gameObject.activeSelf)
            MultiplayButton.gameObject.SetActive(true);
    }

    public void ExitForRewardUI()
    {
        ClearRewards();
        playerProfille.VersionCheckerMethod();
    }

    private void PickUpRewards(int GetLanguageID)
    {
        CountRewards = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < CountRewards; i++)
        {
            GameObject reward = Instantiate(Resources.Load<GameObject>(PathToRewardPrefab));
            int IDReward = UnityEngine.Random.Range(0, playerProfille.GetRewardsSpriteCount());
            reward.transform.SetParent(ContentRewards);
            reward.transform.localScale = new Vector3(1, 1, 1);
            switch (GetLanguageID)
            {
                case 0:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsChina[IDReward];
                    break;
                case 1:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsDanish[IDReward];
                    break;
                case 2:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsDutch[IDReward];
                    break;
                case 3:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsEng[IDReward];
                    break;
                case 4:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsFinnish[IDReward];
                    break;
                case 5:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsFrench[IDReward];
                    break;
                case 6:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsGerman[IDReward];
                    break;
                case 7:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsItalian[IDReward];
                    break;
                case 8:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsNorwegian[IDReward];
                    break;
                case 9:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsPortuguese[IDReward];
                    break;
                case 10:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsRU[IDReward];
                    break;
                case 11:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsSpanishSpain[IDReward];
                    break;
                case 12:
                    reward.transform.GetChild(0).GetComponent<Text>().text = reward.GetComponent<ChangedBackgroundGame>().nameItem = Translate.NameRewardsSwedish[IDReward];
                    break;
            }
            
            reward.transform.GetComponent<Image>().sprite = playerProfille.GetRewardsImages()[IDReward];
            playerProfille.AddItemInInventory(IDReward);
        }
    }

    private void ClearRewards()
    {
        for (int i = 0; i < ContentRewards.childCount; i++)
            Destroy(ContentRewards.GetChild(i).gameObject);
    }

    private void TimerSurvive(float secondsSurvive)
    {
        string timerMinuts = ((int)secondsSurvive / 60).ToString();
        string timerSeconds = (secondsSurvive % 60).ToString("f3");
        string timerHours = (int.Parse(timerMinuts) / 60).ToString();
        PlayTimerTheOverText.text = timerHours + ":" + timerMinuts + ":" + timerSeconds;
    }

    public void MultiplayMoneyInGame()
    {
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(gameID, false);

        if (Advertisement.IsReady(rewardedID) && Advertisement.isInitialized)
        {
            Advertisement.Show(rewardedID);
            serverManager.SetPlayerValueMoney(IDPlayer, CountMoneyPlayerGame, true);
            CountMoneyText.text = Mathf.RoundToInt(CountMoneyPlayerGame * 2).ToString();
            MultiplayButton.gameObject.SetActive(false);
        }
    }


    public void SpawnSound()
    {
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = ResultPick;
        Sound.GetComponent<AudioSource>().Play();
        Sound.AddComponent<DestoryGameObject>();
    }
}
