using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using static GameRules;

public class ServerManager : MonoBehaviour
{
    const string NameMatchMaking = "SliderTargetNumber#1";
    private FirebaseApp app;
    private DatabaseReference dbRef;
    [SerializeField] internal GameObject InizializationUI, MainCamera, Profile;
    [SerializeField] protected List<Profile> profiles;
    [SerializeField] protected GameObject PrefabPlayerStats;
    [SerializeField] protected Transform ContentLeaberBoard, TextNoLoad;
    [SerializeField] internal bool isSortedProfiels = false, isGameOver = false, isButtonClicked = false, isInizializationGame = false, isFirstLaunch = false, isLoadingOK = false, isLoadingResources = false;

    [SerializeField] GameRules gameRules;
    [SerializeField] Image RewardAdsImg;
    [SerializeField] int IDPlayer;
    [SerializeField] GameObject ResutGameUI;
    [SerializeField] float CurrentGameRoundTime;
    [SerializeField] int CountMusicSurvial;
    [SerializeField] int indexDifficuly;
    [SerializeField] float ScorePlayerRound;
    [SerializeField] float RecordPlayer;
    [SerializeField] float MoneyPlayer;
    [SerializeField] int RecordComboPlayer;
    [SerializeField] int GetLanguageID;
    [SerializeField] float currentBulletsInAWP, maxBulletsInAWP;
    [SerializeField] Image DiffuculyIcon;

    [SerializeField] float GettingMoneyPlayer;
    int length;
    Sprite[] Array;

    private string dbLink = "https://classic-a0487.firebaseio.com/";

    private void Awake()
    {
        InizializationUI.SetActive(true);
        switch (System.Globalization.CultureInfo.InstalledUICulture.Name)
        {
            case "zh-CN":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[45];
                break;
            case "zh-CHS":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[45];
                break;
            case "zh-CHT":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[45];
                break;
            case "da":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDanish[45];
                break;
            case "da-DK":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDanish[45];
                break;
            case "nl":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDutch[45];
                break;
            case "nl-BE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDutch[45];
                break;
            case "nl-NL":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDutch[45];
                break;
            case "en":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsEng[45];
                break;
            case "en-US":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsEng[45];
                break;
            case "fi":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFinnish[45];
                break;
            case "fi-FI":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFinnish[45];
                break;
            case "fr":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-BE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-CA":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-FR":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-LU":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-MC":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "fr-CH":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[45];
                break;
            case "de":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "de-AT":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "de-DE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "de-LI":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "de-LU":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "de-CH":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[45];
                break;
            case "it":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsItalian[45];
                break;
            case "it-IT":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsItalian[45];
                break;
            case "it-CH":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsItalian[45];
                break;
            case "no":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsNorwegian[45];
                break;
            case "nb-NO":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsNorwegian[45];
                break;
            case "nn-NO":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsNorwegian[45];
                break;
            case "pt":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsPortuguese[45];
                break;
            case "pt-BR":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsPortuguese[45];
                break;
            case "pt-PT":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsPortuguese[45];
                break;
            case "ru-RU":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsRU[45];
                break;
            case "ru":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsRU[45];
                break;
            case "es":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-AR":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-BO":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-CL":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-CO":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-CR":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-DO":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-EC":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-SV":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-GT":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-HN":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-MX":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-NI":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-PA":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-PY":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-PE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-PR":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-ES":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-UY":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "es-VE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[45];
                break;
            case "sv":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSwedish[45];
                break;
            case "sv-FI":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSwedish[45];
                break;
            case "sv-SE":
                InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSwedish[45];
                break;
        }
        MainCamera.GetComponent<MenuGameController>().enabled = false;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                InitF8();
                GetPlayers();
                isFirstLaunch = true;
                isLoadingResources = true;
            }
            else
                Debug.LogError(string.Format("Не удалось разрешить все зависимости Firebase: {0}", dependencyStatus));

        });
    }

    internal void SetGameRules(GameRules gameRules)
    {
        this.gameRules = gameRules;
    }
    internal void SetIDPlayer(int IDPlayer)
    {
        this.IDPlayer = IDPlayer;
    }
    internal void SetResultGameUI(GameObject ResultGameUI)
    {
        ResutGameUI = ResultGameUI;
    }
    internal void SetCurrentGameRoundTime(float CurrentGameRoundTime)
    {
        this.CurrentGameRoundTime = CurrentGameRoundTime;
    }
    internal void SetCountMusicSurvial(int CountMusicSurvial)
    {
        this.CountMusicSurvial = CountMusicSurvial;
    }
    internal void SetIndexDifficuly(int indexDifficuly)
    {
        this.indexDifficuly = indexDifficuly;
    }
    internal void SetScorePlayerRound(float ScorePlayerRound)
    {
        this.ScorePlayerRound = ScorePlayerRound;
    }
    internal void SetRecordPlayer(float RecordPlayer)
    {
        this.RecordPlayer = RecordPlayer;
    }
    internal void SetMoneyPlayer(float MoneyPlayer)
    {
        this.MoneyPlayer = MoneyPlayer;
    }
    internal void SetDifficulyIcon(Image DifficulyIcon)
    {
        DiffuculyIcon = DifficulyIcon;
    }
    internal void SetRecordComboPlayer(int RecordComboPlayer)
    {
        this.RecordComboPlayer = RecordComboPlayer;
    }
    internal void SetLanguageID(int LanguageID)
    {
        GetLanguageID = LanguageID;
    }

    internal void SetCurrentBulletsInAWP(float set)
    {
        currentBulletsInAWP = set;
    }
    internal void SetMaxBulletsInAWP(float set)
    {
        maxBulletsInAWP = set;
    }



    internal bool GetSortedProfiels() { return isSortedProfiels; }

    internal void AddPlayer(int IDPlayer, string NickNamePlayer)
    {
        Profile profile = new Profile(IDPlayer, NickNamePlayer, 0.0f,0.0f);
        string json = JsonUtility.ToJson(profile);
        dbRef.Child(IDPlayer.ToString()).SetRawJsonValueAsync(json);
    }

    internal void SetPlayerValueSurvived(int IDPlayer,float TimerSurvive)
    {
        dbRef.Child(IDPlayer.ToString()).Child("TimerSurvive").SetValueAsync(TimerSurvive);
    }

    internal void SetPlayerValueMoney(int IDPlayer, float MoneyPlayer, bool prokladka)
    {
        if(prokladka)
            Profile.GetComponent<PlayerProfille>().MoneyChanged(MoneyPlayer, '+');
        dbRef.Child(IDPlayer.ToString()).Child("MoneyPlayer").SetValueAsync(Profile.GetComponent<PlayerProfille>().GetMoneyPlayer());
    }
    internal void GetMoneyPlayerServer(int IDPlayer)
    {
        dbRef.Child(IDPlayer.ToString()).Child("MoneyPlayer").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
                UnityEngine.Debug.LogError(task.Exception.Message);
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                GettingMoneyPlayer = float.Parse(snapshot.Value.ToString());
            }
        });
    }


    public void LeaderBoardButton()
    {
        isButtonClicked = true;
        LeaberBoardClear();
        GetPlayers();
    }

    private void GetPlayers()
    {
        profiles = new List<Profile>();
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
        {
                if (task.IsFaulted)
                    UnityEngine.Debug.LogError(task.Exception.Message);
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot user in snapshot.Children)
                    {
                        IDictionary dictUser = (IDictionary)user.Value;
                        Profile profile = new Profile(int.Parse(dictUser["IDPlayer"].ToString()), dictUser["NickNamePlayer"].ToString(), float.Parse(dictUser["TimerSurvive"].ToString()), float.Parse(dictUser["MoneyPlayer"].ToString()));
                        profiles.Add(profile);
                    }
                    profiles.Sort();
                    isSortedProfiels = true;
                    isInizializationGame = true;
                    isLoadingOK = true;
                }

           
        });
    }

    internal void PreGameOver(bool isStartMatchMaking)
    {
        profiles = new List<Profile>();
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
        {
                if (task.IsFaulted)
                UnityEngine.Debug.LogError(task.Exception.Message);
                else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Profile profile = new Profile(int.Parse(dictUser["IDPlayer"].ToString()), dictUser["NickNamePlayer"].ToString(), float.Parse(dictUser["TimerSurvive"].ToString()), float.Parse(dictUser["MoneyPlayer"].ToString()));
                    profiles.Add(profile);
                }
                profiles.Sort();
                if (!isStartMatchMaking)
                    isGameOver = true;
            }
        });
    }


    private void GameOver(GameRules gameRules, int IDPlayer, GameObject ResutGameUI, float CurrentGameRoundTime, int CountMusicSurvial, int indexDifficuly, float ScorePlayerRound, float RecordPlayer, float MoneyPlayer, Image DiffuculyIcon,int RecordComboPlayer)
    {
        if (gameRules.currentGameModeSelected == GameMode.MatchMaking)
        {
            int NumberMM = 0;
            for (int i = 0; i < profiles.Count; i++)
                if (profiles[i].IDPlayer == IDPlayer)
                {
                    NumberMM = (i + 1);
                    break;
                }
            ResutGameUI.GetComponent<ResultGame>().DataRefresh(NumberMM, CurrentGameRoundTime, CountMusicSurvial, Translate.NameDifficulyEng[indexDifficuly], ScorePlayerRound, RecordPlayer, MoneyPlayer, DiffuculyIcon.sprite, true, RecordComboPlayer, GetLanguageID,IDPlayer,currentBulletsInAWP,maxBulletsInAWP);
        }
    }

    internal List<Profile> GetPlayersList()
    {
        return profiles;
    }
    private void LeaberBoardClear()
    {
        for (int i = 0; i < ContentLeaberBoard.childCount; i++)
            Destroy(ContentLeaberBoard.GetChild(i).gameObject);
        TextNoLoad.gameObject.SetActive(true);
    }
    private void LeaberBoardCreate()
    {
        for (int i = 0; i < profiles.Count; i++)
        {
            GameObject prefabStatPlayer = Instantiate(PrefabPlayerStats);
            prefabStatPlayer.transform.SetParent(ContentLeaberBoard);
            prefabStatPlayer.transform.localScale = new Vector3(1, 1, 1);
            prefabStatPlayer.transform.GetChild(0).GetComponent<Text>().text = "#" + (i + 1);
            float timerSeconds = profiles[i].TimerSurvive;
            int timerMinuts = (int)timerSeconds / 60;
            int timerHours = (int)timerMinuts / 60;
            if (timerHours < 10 && timerMinuts < 10 && timerSeconds < 10)
                prefabStatPlayer.transform.GetChild(1).GetComponent<Text>().text = string.Format("0{0}:0{1}:0{2:#.###}", timerHours, timerMinuts, timerSeconds);
            else if (timerHours >= 10 && timerMinuts >= 10 && timerSeconds >= 10)
                prefabStatPlayer.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}:{1}:{2:#.###}", timerHours, timerMinuts, timerSeconds);
            else if (timerHours < 10 && timerMinuts < 10 && timerSeconds >= 10)
                prefabStatPlayer.transform.GetChild(1).GetComponent<Text>().text = string.Format("0{0}:0{1}:{2:#.###}", timerHours, timerMinuts, timerSeconds);
            else if (timerHours < 10 && timerMinuts >= 10 && timerSeconds >= 10)
                prefabStatPlayer.transform.GetChild(1).GetComponent<Text>().text = string.Format("0{0}:{1}:{2:#.###}", timerHours, timerMinuts, timerSeconds);
            else if (timerHours >= 10 && timerMinuts >= 10 && timerSeconds >= 10)
                prefabStatPlayer.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}:{1}:{2:#.###}", timerHours, timerMinuts, timerSeconds);
            prefabStatPlayer.transform.GetChild(2).GetComponent<Text>().text = profiles[i].NickNamePlayer;
        }
        TextNoLoad.gameObject.SetActive(false);
    }

    private void InitF8()
    {
        app.SetEditorDatabaseUrl(dbLink);
        dbRef = FirebaseDatabase.DefaultInstance.RootReference.Child("Players");
        dbRef.ValueChanged += PlayersValueChanged;
    }

    void FixedUpdate()
    {
        if(GettingMoneyPlayer != -1)
        {
            Profile.GetComponent<PlayerProfille>().MoneyChanged(GettingMoneyPlayer, '=');
            GettingMoneyPlayer = -1;
        }
    }
    void Update()
    {
        if (isSortedProfiels && isButtonClicked)
        {
            LeaberBoardCreate();
            isSortedProfiels = false;
        }
        if (isInizializationGame && isFirstLaunch)
        {
            MainCamera.GetComponent<MenuGameController>().enabled = true;
            Profile.SetActive(true);
            InizializationUI.GetComponent<Animator>().Play("IntoMainMenu", 0, 0);
            RewardAdsImg.sprite = Profile.GetComponent<PlayerProfille>().GetRewardsImages()[13];
            StartCoroutine(Profile.GetComponent<PlayerProfille>().VersionChecker());
            isFirstLaunch = false;
        }
        if (isGameOver)
        {
            GameOver(gameRules, IDPlayer, ResutGameUI, CurrentGameRoundTime, CountMusicSurvial, indexDifficuly, ScorePlayerRound, RecordPlayer, MoneyPlayer, DiffuculyIcon, RecordComboPlayer);
            isGameOver = false;
        }
        if (isLoadingResources)
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[48];
                    break;
                case SystemLanguage.ChineseSimplified:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[48];
                    break;
                case SystemLanguage.ChineseTraditional:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsChina[48];
                    break;
                case SystemLanguage.Danish:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDanish[48];
                    break;
                case SystemLanguage.Dutch:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsDutch[48];
                    break;
                case SystemLanguage.English:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsEng[48];
                    break;
                case SystemLanguage.Finnish:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFinnish[48];
                    break;
                case SystemLanguage.French:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsFrench[48];
                    break;
                case SystemLanguage.German:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsGerman[48];
                    break;
                case SystemLanguage.Italian:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsItalian[48];
                    break;
                case SystemLanguage.Norwegian:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsNorwegian[48];
                    break;
                case SystemLanguage.Portuguese:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsPortuguese[48];
                    break;
                case SystemLanguage.Russian:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsRU[48];
                    break;
                case SystemLanguage.Spanish:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSpanishSpain[48];
                    break;
                case SystemLanguage.Swedish:
                    InizializationUI.transform.Find("ConnectionServersKJPGames").GetComponent<Text>().text = Translate.NameTextsSwedish[48];
                    break;
            }
            isLoadingResources = false;
        }
    }

    private void PlayersValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            UnityEngine.Debug.LogError(e.DatabaseError.Message);
            return;
        }



    }
}
