using UnityEngine;
using System;
using static CardMechanic;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;

public class GameRules : MonoBehaviour
{
    const int StartHealthes = 3;
    const float DifficulyUpTimer = 60f;
    const float TimerOffSchansConst = 10f;
    const int DefaultUpDifficuly = 15;
    const float StartTimeGame = 30.0f;
    const int StartDifficuly = 0;
    const int minClicked = 1, maxClicked = 10;
    const float TimerFreezedConst = 5.0f;
    protected int minMoneyClicked = 1, maxMoneyClicked = 10;
    const string NameBackground = "Background";
    const string NameFireButton = "FireButton";
    const string NameReloadButton = "ReloadButton";
    const string NameTimerText = "TimerText";
    const string NameMatchMaking = "SliderTargetNumber#1";
    const string NameAWP= "AWP";
    internal enum GameMode
    {
        Survival,
        BlueShift,
        MatchMaking,
    }
    [Header("__GAME RULES__")]
    [SerializeField] protected bool IsStartedGame, IsGamePaused, IsGameFreezed, isAudioClipFreezePlay,AWPOn, Fire, Reload,isSposobnsty,GameOver,SchansGame;
    [SerializeField] protected float currentbulletsingun, maxbulletsingun;
    [SerializeField] protected int Health = 3;
    [SerializeField] protected float TimerGame = 10.0f;
    [SerializeField] protected float CurrentGameRoundTime = 0.0f;
    [SerializeField] protected float MoneyPlayer = 0.0f;
    [SerializeField] protected float TimerGameDifficulyCheck = DifficulyUpTimer;
    [SerializeField] protected float TimerFrezzed = TimerFreezedConst;
    [SerializeField] protected int indexDifficuly;
    [SerializeField] protected float ScorePlayerRound = 0.0f;
    [SerializeField] protected float RecordPlayer = 0.0f;
    [SerializeField] protected int ComboConnect = 0,RecordComboGame;
    [SerializeField] protected int IDPlayer = 0,IDGetLanguage = 0;
    [SerializeField] protected bool Ads = false;
    [SerializeField] protected float RecordTimerSurvive = 0;
    [SerializeField] protected int CountMusicSurvial = 0;
    [SerializeField] protected int minCountClicked, maxCountClicked;
    [SerializeField] protected int minCountMoneyClicked, maxCountMoneyClicked;
    [SerializeField] internal GameMode currentGameModeSelected;
    [SerializeField] internal AudioClip DifficulyUp,SurviveMusicCounter,ScoreClicks,FreezeSound,AWPFire,AWPReload,NullBullets;
    [SerializeField] protected ServerManager serverManager;
    [SerializeField] protected MenuGameController menuGameController;
    [SerializeField] internal ChangeSpawnCard[] changeSpawnCards;
    [SerializeField] internal Button SponobsnostyAWP;
    [SerializeField] internal Sprite[] DifficulySprites;
    [SerializeField] protected Text[] Gametexts;
    [SerializeField] protected Text TextCustomMusicName;
    [SerializeField] protected Animator CountSongsSurvive;
    [SerializeField] protected Animator GameOverVstavka;
    [SerializeField] protected Animator BloodScreen;
    [SerializeField] protected Image DiffuculyIcon;
    [SerializeField] protected Text ComboCounterText,BulletsSposobnosty,BulletsWeaponMode;
    [SerializeField] protected GameObject BackgroundMusic;
    [SerializeField] protected GameObject ModeSelected;
    [SerializeField] protected GameObject PauseUI;
    [SerializeField] protected GameObject AdsBuyUI;
    [SerializeField] internal GameObject GameSchansUI;
    [SerializeField] protected GameObject ResutGameUI;
    [SerializeField] protected GameObject FreezeScreen;
    [SerializeField] protected GameObject WeaponMode;
    [SerializeField] protected GameObject Crosshair;
    [SerializeField] internal GameObject MatchMakingTimer;

    [SerializeField] internal AudioSource music;

    [SerializeField] bool LiveChecker = false;
    [SerializeField] float TimerOffMoney = 3.0f;
    [SerializeField] float TimerOffSchans = 10f;

    [SerializeField] protected int CountDeads = 0;

    private string gameID = "3653556";
    private string VideoID = "video";
    private string rewardedID = "rewardedVideo";

    // 0 - xScale 1 - yScale 2 - GravityScale
    float[] samples = new float[512];
    float oldSample;
    [SerializeField] internal List<Profile> PlayerProfiles = new List<Profile>();
    [SerializeField] protected int IDMusic = -1;
    [SerializeField] internal event Action MagnitCardEvent;

    void Start()
    {
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(gameID, false);
    }


    internal void MethodMoneyPlayer(float count,char symbolMoney)
    {
        switch (symbolMoney)
        {
            case '+':
                MoneyPlayer += count;
                Gametexts[2].GetComponent<Animator>().SetBool("On", true);
                switch (IDGetLanguage)
                {
                    case 0:
                        Gametexts[2].text = Translate.NameTextsChina[21] + MoneyPlayer;
                        break;
                    case 1:
                        Gametexts[2].text = Translate.NameTextsDanish[21] + MoneyPlayer;
                        break;
                    case 2:
                        Gametexts[2].text = Translate.NameTextsDutch[21] + MoneyPlayer;
                        break;
                    case 3:
                        Gametexts[2].text = Translate.NameTextsEng[21] + MoneyPlayer;
                        break;
                    case 4:
                        Gametexts[2].text = Translate.NameTextsFinnish[21] + MoneyPlayer;
                        break;
                    case 5:
                        Gametexts[2].text = Translate.NameTextsFrench[21] + MoneyPlayer;
                        break;
                    case 6:
                        Gametexts[2].text = Translate.NameTextsGerman[21] + MoneyPlayer;
                        break;
                    case 7:
                        Gametexts[2].text = Translate.NameTextsItalian[21] + MoneyPlayer;
                        break;
                    case 8:
                        Gametexts[2].text = Translate.NameTextsNorwegian[21] + MoneyPlayer;
                        break;
                    case 9:
                        Gametexts[2].text = Translate.NameTextsPortuguese[21] + MoneyPlayer;
                        break;
                    case 10:
                        Gametexts[2].text =  Translate.NameTextsRU[21] + MoneyPlayer;
                        break;
                    case 11:
                        Gametexts[2].text = Translate.NameTextsSpanishSpain[21] + MoneyPlayer;
                        break;
                    case 12:
                        Gametexts[2].text = Translate.NameTextsSwedish[21] + MoneyPlayer;
                        break;
                }
                
                break;
            case '-':
                MoneyPlayer -= count;
                break;
            case '0':
                MoneyPlayer = count;
                break;
        }
    }
    internal GameObject GetWeaponMode() { return WeaponMode; }
    internal bool GetisSposobnosty() { return isSposobnsty; }
    internal bool GetFire() { return Fire; }
    internal bool GetReload() { return Reload; }
    internal void SetFire(bool set) { Fire = set; }

    public void ReloadButton()
    {
        if(maxbulletsingun > 0)
        {
            if (!WeaponMode.transform.Find(NameAWP).gameObject.activeSelf)
                WeaponMode.transform.Find(NameAWP).gameObject.SetActive(true);
            WeaponMode.transform.Find(NameAWP).GetComponent<Animator>().Play("Reload", 0, 0);
            SetFire(false);
        }

    }

    public void ReloadAWP()
    {
        float reason = 10 - currentbulletsingun;
        if(maxbulletsingun >= reason)
        {
            maxbulletsingun = maxbulletsingun - reason;
            currentbulletsingun = 10;
        }
        else
        {
            currentbulletsingun = currentbulletsingun + maxbulletsingun;
            maxbulletsingun = 0;
        }
    }

    internal int GetMusicID() { return IDMusic; }
    internal void SetMusicID(int IDMusic) { this.IDMusic = IDMusic; }
    internal void MethodScorePlayer(float count,bool isNull)
    {
        if (isNull)
            ScorePlayerRound = 0.0f;
        else
            ScorePlayerRound += count;
        
    }
    internal void ResetRecordComboGame()
    {
        RecordComboGame = 0;
    }
    internal void MethodComboConnectPlayer(bool isNull,bool lostCombo)
    {
        if (isNull)
        {
            ComboConnect = 0;
            if (lostCombo)
            {
                switch (IDGetLanguage)
                {
                    case 0:
                        ComboCounterText.text = Translate.NameTextsChina[22] + ComboConnect + Translate.NameTextsChina[23];
                        break;
                    case 1:
                        ComboCounterText.text = Translate.NameTextsDanish[22] + ComboConnect + Translate.NameTextsDanish[23];
                        break;
                    case 2:
                        ComboCounterText.text = Translate.NameTextsDutch[22] + ComboConnect + Translate.NameTextsDutch[23];
                        break;
                    case 3:
                        ComboCounterText.text = Translate.NameTextsEng[22] + ComboConnect + Translate.NameTextsEng[23];
                        break;
                    case 4:
                        ComboCounterText.text = Translate.NameTextsFinnish[22] + ComboConnect + Translate.NameTextsFinnish[23];
                        break;
                    case 5:
                        ComboCounterText.text = Translate.NameTextsFrench[22] + ComboConnect + Translate.NameTextsFrench[23];
                        break;
                    case 6:
                        ComboCounterText.text = Translate.NameTextsGerman[22] + ComboConnect + Translate.NameTextsGerman[23];
                        break;
                    case 7:
                        ComboCounterText.text = Translate.NameTextsItalian[22] + ComboConnect + Translate.NameTextsItalian[23];
                        break;
                    case 8:
                        ComboCounterText.text = Translate.NameTextsNorwegian[22] + ComboConnect + Translate.NameTextsNorwegian[23];
                        break;
                    case 9:
                        ComboCounterText.text = Translate.NameTextsPortuguese[22] + ComboConnect + Translate.NameTextsPortuguese[23];
                        break;
                    case 10:
                        ComboCounterText.text = Translate.NameTextsRU[22] + ComboConnect + Translate.NameTextsRU[23];
                        break;
                    case 11:
                        ComboCounterText.text = Translate.NameTextsSpanishSpain[22] + ComboConnect + Translate.NameTextsSpanishSpain[23];
                        break;
                    case 12:
                        ComboCounterText.text = Translate.NameTextsSwedish[22] + ComboConnect + Translate.NameTextsSwedish[23];
                        break;
                }
                
                var scroll = ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().colors;
                scroll.normalColor = Color.red;
                scroll.disabledColor = Color.red;
                ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().colors = scroll;
            }
        }
        else
        {
            switch (IDGetLanguage)
            {
                case 0:
                    ComboCounterText.text = Translate.NameTextsChina[22] + ComboConnect + Translate.NameTextsChina[23];
                    break;
                case 1:
                    ComboCounterText.text = Translate.NameTextsDanish[22] + ComboConnect + Translate.NameTextsDanish[23];
                    break;
                case 2:
                    ComboCounterText.text = Translate.NameTextsDutch[22] + ComboConnect + Translate.NameTextsDutch[23];
                    break;
                case 3:
                    ComboCounterText.text = Translate.NameTextsEng[22] + ComboConnect + Translate.NameTextsEng[23];
                    break;
                case 4:
                    ComboCounterText.text = Translate.NameTextsFinnish[22] + ComboConnect + Translate.NameTextsFinnish[23];
                    break;
                case 5:
                    ComboCounterText.text = Translate.NameTextsFrench[22] + ComboConnect + Translate.NameTextsFrench[23];
                    break;
                case 6:
                    ComboCounterText.text = Translate.NameTextsGerman[22] + ComboConnect + Translate.NameTextsGerman[23];
                    break;
                case 7:
                    ComboCounterText.text = Translate.NameTextsItalian[22] + ComboConnect + Translate.NameTextsItalian[23];
                    break;
                case 8:
                    ComboCounterText.text = Translate.NameTextsNorwegian[22] + ComboConnect + Translate.NameTextsNorwegian[23];
                    break;
                case 9:
                    ComboCounterText.text = Translate.NameTextsPortuguese[22] + ComboConnect + Translate.NameTextsPortuguese[23];
                    break;
                case 10:
                    ComboCounterText.text = Translate.NameTextsRU[22] + ComboConnect + Translate.NameTextsRU[23];
                    break;
                case 11:
                    ComboCounterText.text = Translate.NameTextsSpanishSpain[22] + ComboConnect + Translate.NameTextsSpanishSpain[23];
                    break;
                case 12:
                    ComboCounterText.text = Translate.NameTextsSwedish[22] + ComboConnect + Translate.NameTextsSwedish[23];
                    break;
            }
            ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().size = 1f;
            var scroll = ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().colors;
            scroll.normalColor = Color.green;
            scroll.disabledColor = Color.green;
            ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().colors = scroll;
            if (!LiveChecker)
                ComboCounterText.GetComponent<Animator>().Play("ComboOn", 0, 0);
            ComboConnect++;
            LiveChecker = true;
        }
    }
    internal GameObject GetModeSelected() { return ModeSelected; }
    internal bool GetGameFrezzed() { return IsGameFreezed; }
    internal void SetGameFrezzed(bool isFrezze) { IsGameFreezed = isFrezze; }

    internal void MethodCardEvent()
    {
        MagnitCardEvent?.Invoke();
    }
    internal void SetPauseGame(bool changed) 
    {
        IsGamePaused = changed;
    }
    internal void MethodCounterMusicSurvial(bool isNull)
    {
        if (isNull)
            CountMusicSurvial = 0;
        else
            CountMusicSurvial++;
    }
    internal void MethodIsGameChanged(bool currentGameChangedValue)
    {
        IsStartedGame = currentGameChangedValue;
    }

    /// <param name="count"></param>
    /// <param name="Smitch">0 - Обнулить начать игру заново 1 - Прибавить -1 - Отнять</param>
    internal void MethodHealthPlayer(int count,int Smitch)
    {
        switch (Smitch)
        {
            case -1:
                Health -= count;
                Gametexts[1].text = Health.ToString();
                break;
            case 0:
                Health = StartHealthes;
                Gametexts[1].text = Health.ToString();
                if (menuGameController.SelectedMusic != null)
                    music.Play();
                GameOver = false;
                music.pitch = 1f;
                indexDifficuly = StartDifficuly;
                break;
            case 1:
                Health += count;
                Gametexts[1].text = Health.ToString();
                break;
        }
    }

    internal void UpdateTimerFreezed()
    {
        TimerFrezzed = TimerFreezedConst;
    }
    /// <param name="count"></param>
    /// <param name="Smitch">0 - Обнулить начать игру заново 1 - Прибавить -1 - Отнять</param>
    internal void MethodTimerPlayer(float count, int Smitch)
    {
        switch (Smitch)
        {
            case -1:
                TimerGame -= count;
                Gametexts[1].text = Mathf.RoundToInt(TimerGame).ToString();
                break;
            case 0:
                TimerGame = StartTimeGame;
                Gametexts[1].text = Mathf.RoundToInt(TimerGame).ToString();
                if (menuGameController.SelectedMusic != null)
                    music.Play();
                music.pitch = 1f;
                GameOver = false;
                indexDifficuly = StartDifficuly;
                break;
            case 1:
                TimerGame += count;
                Gametexts[1].text = Mathf.RoundToInt(TimerGame).ToString();
                break;
        }
    }

    /// <param name="Smitch">0 - Обнулить 1 - Увеличить на 2</param>
    internal void MethodRandomClicked(int Smitch)
    {
        switch (Smitch)
        {
            case 0:
                minCountClicked = minClicked;
                maxCountClicked = maxClicked;
                minCountMoneyClicked = minMoneyClicked;
                maxCountMoneyClicked = maxMoneyClicked;
                break;
            case 1:
                minCountClicked += 3;
                maxCountClicked += 3;
                break;
        }
    }

    public void SchansButton()
    {
        if (Advertisement.IsReady(rewardedID) && Advertisement.isInitialized)
        {
            Advertisement.Show(rewardedID);
            if (SchansGame)
                SchansGame = false;
            switch (currentGameModeSelected)
            {
                case GameMode.BlueShift:
                    MethodTimerPlayer(30, 1);
                    break;
                case GameMode.Survival:
                    MethodHealthPlayer(3, 1);
                    break;
            }
            GameSchansUI.SetActive(false);
            music.Play();
        }
    }

    internal void SelectedMusicCurrentText()
    {
        if(music != null)
        {
            if (!string.IsNullOrEmpty(music.clip.name))
            {
                switch (IDGetLanguage)
                {
                    case 0:
                        Gametexts[4].text = Translate.NameTextsChina[41] + music.clip.name;
                        break;
                    case 1:
                        Gametexts[4].text = Translate.NameTextsDanish[41] + music.clip.name;
                        break;
                    case 2:
                        Gametexts[4].text = Translate.NameTextsDutch[41] + music.clip.name;
                        break;
                    case 3:
                        Gametexts[4].text = Translate.NameTextsEng[41] + music.clip.name;
                        break;
                    case 4:
                        Gametexts[4].text = Translate.NameTextsFinnish[41] + music.clip.name;
                        break;
                    case 5:
                        Gametexts[4].text = Translate.NameTextsFrench[41] + music.clip.name;
                        break;
                    case 6:
                        Gametexts[4].text = Translate.NameTextsGerman[41] + music.clip.name;
                        break;
                    case 7:
                        Gametexts[4].text = Translate.NameTextsItalian[41] + music.clip.name;
                        break;
                    case 8:
                        Gametexts[4].text = Translate.NameTextsNorwegian[41] + music.clip.name;
                        break;
                    case 9:
                        Gametexts[4].text = Translate.NameTextsPortuguese[41] + music.clip.name;
                        break;
                    case 10:
                        Gametexts[4].text = Translate.NameTextsRU[41] + music.clip.name;
                        break;
                    case 11:
                        Gametexts[4].text = Translate.NameTextsSpanishSpain[41] + music.clip.name;
                        break;
                    case 12:
                        Gametexts[4].text = Translate.NameTextsSwedish[41] + music.clip.name;
                        break;
                }
            }
            else
            {
                switch (IDGetLanguage)
                {
                    case 0:
                        Gametexts[4].text = Translate.NameTextsChina[41] + TextCustomMusicName.text;
                        break;
                    case 1:
                        Gametexts[4].text = Translate.NameTextsDanish[41] + TextCustomMusicName.text;
                        break;
                    case 2:
                        Gametexts[4].text = Translate.NameTextsDutch[41] + TextCustomMusicName.text;
                        break;
                    case 3:
                        Gametexts[4].text = Translate.NameTextsEng[41] + TextCustomMusicName.text;
                        break;
                    case 4:
                        Gametexts[4].text = Translate.NameTextsFinnish[41] + TextCustomMusicName.text;
                        break;
                    case 5:
                        Gametexts[4].text = Translate.NameTextsFrench[41] + TextCustomMusicName.text;
                        break;
                    case 6:
                        Gametexts[4].text = Translate.NameTextsGerman[41] + TextCustomMusicName.text;
                        break;
                    case 7:
                        Gametexts[4].text = Translate.NameTextsItalian[41] + TextCustomMusicName.text;
                        break;
                    case 8:
                        Gametexts[4].text = Translate.NameTextsNorwegian[41] + TextCustomMusicName.text;
                        break;
                    case 9:
                        Gametexts[4].text = Translate.NameTextsPortuguese[41] + TextCustomMusicName.text;
                        break;
                    case 10:
                        Gametexts[4].text = Translate.NameTextsRU[41] + TextCustomMusicName.text;
                        break;
                    case 11:
                        Gametexts[4].text = Translate.NameTextsSpanishSpain[41] + TextCustomMusicName.text;
                        break;
                    case 12:
                        Gametexts[4].text = Translate.NameTextsSwedish[41] + TextCustomMusicName.text;
                        break;
                }
            }
   
            Gametexts[4].GetComponent<Animator>().Play("Move", 0, 0);
        }
    }


    internal void GameModeUIAndMusicObject(Text[] texts,Text ComboCounterText,Animator GameOverVstavka, Animator ContSoungsSurvive,Animator bloodScreen,Image DifficulyIcon,AudioSource music,int IDPlayer,float RecordTimerSurvive,float RecordScore,GameObject ModeSelected,bool Ads,int GetLanguage,GameObject FreezeScreen,GameObject WeaponMode,bool isSposobnosty,Text SposobnostyBulletText,Text BulletsWeaponMode,float currentGunBullets,float maxGunBullets,Button SposonbostyAWP)
    {
        Gametexts = texts;
        this.Ads = Ads;
        IDGetLanguage = GetLanguage;
        this.ModeSelected = ModeSelected;
        this.ComboCounterText = ComboCounterText;
        this.GameOverVstavka = GameOverVstavka;
        this.IDPlayer = IDPlayer;
        this.RecordTimerSurvive = RecordTimerSurvive;
        this.FreezeScreen = FreezeScreen;
        this.WeaponMode = WeaponMode;
        this.BulletsWeaponMode = BulletsWeaponMode;
        SponobsnostyAWP = SposonbostyAWP;
        currentbulletsingun = currentGunBullets;
        maxbulletsingun = maxGunBullets;
        isSposobnsty = isSposobnosty;
        BulletsSposobnosty = SposobnostyBulletText;
        RecordPlayer = RecordScore;
        CountSongsSurvive = ContSoungsSurvive;
        BloodScreen = bloodScreen;
        DiffuculyIcon = DifficulyIcon;
        music.loop = false;
        SchansGame = true;
        MatchMakingTimer.SetActive(false);
        TimerOffSchans = TimerOffSchansConst;
        if (AWPOn)
            WeaponModeChanged();
        switch (IDGetLanguage)
        {
            case 0:
                texts[0].text = Translate.NameTextsChina[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsChina[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 1:
                texts[0].text = Translate.NameTextsDanish[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsDanish[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 2:
                texts[0].text = Translate.NameTextsDutch[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsDutch[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 3:
                texts[0].text = Translate.NameTextsEng[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsEng[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 4:
                texts[0].text = Translate.NameTextsFinnish[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsFinnish[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 5:
                texts[0].text = Translate.NameTextsFrench[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsFrench[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 6:
                texts[0].text = Translate.NameTextsGerman[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsGerman[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 7:
                texts[0].text = Translate.NameTextsItalian[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsItalian[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 8:
                texts[0].text = Translate.NameTextsNorwegian[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsNorwegian[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 9:
                texts[0].text = Translate.NameTextsPortuguese[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsPortuguese[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 10:
                texts[0].text = Translate.NameTextsRU[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsRU[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 11:
                texts[0].text = Translate.NameTextsSpanishSpain[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsSpanishSpain[64] + currentGunBullets + " / " + maxGunBullets;
                break;
            case 12:
                texts[0].text = Translate.NameTextsSwedish[20] + 0 + " / " + RecordPlayer;
                BulletsSposobnosty.text = BulletsWeaponMode.text = Translate.NameTextsSwedish[64] + currentGunBullets + " / " + maxGunBullets;
                break;
        }
        if (currentGameModeSelected == GameMode.BlueShift || currentGameModeSelected == GameMode.MatchMaking)
            texts[1].text = StartTimeGame.ToString();
        else if (currentGameModeSelected == GameMode.Survival)
            texts[1].text = StartHealthes.ToString();
        switch (IDGetLanguage)
        {
            case 0:
                texts[3].text = Translate.NameTextsChina[24] + Translate.NameDifficulyChina[indexDifficuly];
                break;
            case 1:
                texts[3].text = Translate.NameTextsDanish[24] + Translate.NameDifficulyDanish[indexDifficuly];
                break;
            case 2:
                texts[3].text = Translate.NameTextsDutch[24] + Translate.NameDifficulyDutch[indexDifficuly];
                break;
            case 3:
                texts[3].text = Translate.NameTextsEng[24] + Translate.NameDifficulyEng[indexDifficuly];
                break;
            case 4:
                texts[3].text = Translate.NameTextsFinnish[24] + Translate.NameDifficulyFinnish[indexDifficuly];
                break;
            case 5:
                texts[3].text = Translate.NameTextsFrench[24] + Translate.NameDifficulyFrench[indexDifficuly];
                break;
            case 6:
                texts[3].text = Translate.NameTextsGerman[24] + Translate.NameDifficulyGerman[indexDifficuly];
                break;
            case 7:
                texts[3].text = Translate.NameTextsItalian[24] + Translate.NameDifficulyItalian[indexDifficuly];
                break;
            case 8:
                texts[3].text = Translate.NameTextsNorwegian[24] + Translate.NameDifficulyNorwegian[indexDifficuly];
                break;
            case 9:
                texts[3].text = Translate.NameTextsPortuguese[24] + Translate.NameDifficulyPortuguese[indexDifficuly];
                break;
            case 10:
                texts[3].text = Translate.NameTextsRU[24] + Translate.NameDifficulyRU[indexDifficuly];
                break;
            case 11:
                texts[3].text = Translate.NameTextsSpanishSpain[24] + Translate.NameDifficulySpanishSpain[indexDifficuly];
                break;
            case 12:
                texts[3].text = Translate.NameTextsSwedish[24] + Translate.NameDifficulySwedish[indexDifficuly];
                break;
        }
        this.music = music;
    }
    internal int GetMinClickedNumber() { return minCountClicked; }
    internal int GetMaxClickedNumber() { return maxCountClicked; }

    internal void SetMinMoneyClickedNumber(int min,int max) { minMoneyClicked = min; maxMoneyClicked = max; }

    internal GameObject GetResultGameUI() { return ResutGameUI; }

    internal int GetMinMoneyClickedNumber() { return minCountMoneyClicked; }
    internal int GetMaxMoneyClickedNumber() { return maxCountMoneyClicked; }

    internal bool GetIsStartedGame() { return IsStartedGame; }

    internal bool GetIsPausedGame() { return IsGamePaused; }

    internal int GetHealthPlayer() { return Health; }

    internal GameObject GetGameOverVstavka() { return GameOverVstavka.gameObject; }

    internal float GetTimerDifficulyCheck() { return TimerGameDifficulyCheck; }

    internal float GetTimerPlayer() { return TimerGame; }
    void FixedUpdate()
    {
        if (GetIsStartedGame() && !GetIsPausedGame())
        {
            SetupScoreLanguage();
            BackgroundMusicChanged();
            MoneyAnimOffMethod();
            BloodScreenMechanic();
            DifficulyTimer();
            CheckMusicPlaying();
            ComboCheckerLive();
            FreezeGame();
            SchansGameTime();
            MatchMakingGameTimeAndPlace();
            WeaponModeMethod();
        }

    }

    private void MatchMakingGameTimeAndPlace()
    {
        if(currentGameModeSelected == GameMode.MatchMaking)
        {
            if (!MatchMakingTimer.activeSelf)
            {
                serverManager.PreGameOver(true);
                MatchMakingTimer.SetActive(true);
            }
            if (PlayerProfiles.Count > 0)
            {
                if (!MatchMakingTimer.transform.Find(NameMatchMaking).gameObject.activeSelf)
                    MatchMakingTimer.transform.Find(NameMatchMaking).gameObject.SetActive(true);

                if (MatchMakingTimer.transform.Find(NameMatchMaking).GetComponent<Slider>().maxValue != PlayerProfiles[0].TimerSurvive)
                    MatchMakingTimer.transform.Find(NameMatchMaking).GetComponent<Slider>().maxValue = PlayerProfiles[0].TimerSurvive;
            }
            else
            {
                    PlayerProfiles = serverManager.GetPlayersList();
                if (MatchMakingTimer.transform.Find(NameMatchMaking).gameObject.activeSelf)
                    MatchMakingTimer.transform.Find(NameMatchMaking).gameObject.SetActive(false);
            }
            if(!GameOver)
                TimerSurvive(CurrentGameRoundTime);
        }
    }

    private void TimerSurvive(float secondsSurvive)
    {
        string timerMinuts = ((int)secondsSurvive / 60).ToString();
        string timerSeconds = (secondsSurvive % 60).ToString("f3");
        string timerHours = (int.Parse(timerMinuts) / 60).ToString();
        MatchMakingTimer.GetComponent<Text>().text = timerHours + ":" + timerMinuts + ":" + timerSeconds;

        if (MatchMakingTimer.transform.Find(NameMatchMaking).gameObject.activeSelf)
        {
            if (MatchMakingTimer.transform.Find(NameMatchMaking).GetComponent<Slider>().value < MatchMakingTimer.transform.Find(NameMatchMaking).GetComponent<Slider>().maxValue)
                MatchMakingTimer.transform.Find(NameMatchMaking).GetComponent<Slider>().value = CurrentGameRoundTime;
        }
            
    }

    private void SchansGameTime()
    {
        if (GameSchansUI.activeSelf)
        {
            if (TimerOffSchans > 0)
            {
                TimerOffSchans -= Time.deltaTime;
                GameSchansUI.transform.Find(NameBackground).Find(NameTimerText).GetComponent<Text>().text = Mathf.RoundToInt(TimerOffSchans).ToString();
            }   
            else
            {
                GameOverStayResults();
                SchansGame = false;
                GameSchansUI.SetActive(SchansGame);
                GameOver = true;
            }
        }
    }

    private void SetupScoreLanguage()
    {
        if (Gametexts[0] != null)
        {
            switch (IDGetLanguage)
            {
                case 0:
                    Gametexts[0].text = Translate.NameTextsChina[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 1:
                    Gametexts[0].text = Translate.NameTextsDanish[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 2:
                    Gametexts[0].text = Translate.NameTextsDutch[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 3:
                    Gametexts[0].text = Translate.NameTextsEng[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 4:
                    Gametexts[0].text = Translate.NameTextsFinnish[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 5:
                    Gametexts[0].text = Translate.NameTextsFrench[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 6:
                    Gametexts[0].text = Translate.NameTextsGerman[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 7:
                    Gametexts[0].text = Translate.NameTextsItalian[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 8:
                    Gametexts[0].text = Translate.NameTextsNorwegian[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 9:
                    Gametexts[0].text = Translate.NameTextsPortuguese[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 10:
                    Gametexts[0].text = Translate.NameTextsRU[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 11:
                    Gametexts[0].text = Translate.NameTextsSpanishSpain[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
                case 12:
                    Gametexts[0].text = Translate.NameTextsSwedish[20] + ScorePlayerRound + " / " + RecordPlayer;
                    break;
            }

        }
        if (ScorePlayerRound > RecordPlayer)
            RecordPlayer = ScorePlayerRound;
    }

    private void WeaponModeMethod()
    {
        SponobsnostyAWP.interactable = isSposobnsty;
        if (isSposobnsty)
        {
            switch (IDGetLanguage)
            {
                case 0:
                    BulletsSposobnosty.text = Translate.NameTextsChina[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 1:
                    BulletsSposobnosty.text = Translate.NameTextsDanish[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 2:
                    BulletsSposobnosty.text = Translate.NameTextsDutch[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 3:
                    BulletsSposobnosty.text = Translate.NameTextsEng[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 4:
                    BulletsSposobnosty.text = Translate.NameTextsFinnish[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 5:
                    BulletsSposobnosty.text = Translate.NameTextsFrench[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 6:
                    BulletsSposobnosty.text = Translate.NameTextsGerman[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 7:
                    BulletsSposobnosty.text = Translate.NameTextsItalian[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 8:
                    BulletsSposobnosty.text = Translate.NameTextsNorwegian[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 9:
                    BulletsSposobnosty.text = Translate.NameTextsPortuguese[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 10:
                    BulletsSposobnosty.text = Translate.NameTextsRU[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 11:
                    BulletsSposobnosty.text = Translate.NameTextsSpanishSpain[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
                case 12:
                    BulletsSposobnosty.text = Translate.NameTextsSwedish[64] + currentbulletsingun + " / " + maxbulletsingun;
                    break;
            }
            
            BulletsWeaponMode.text = currentbulletsingun + " / " + maxbulletsingun;
            WeaponMode.transform.Find(NameFireButton).GetComponent<Button>().interactable = Fire;
            WeaponMode.transform.Find(NameReloadButton).GetComponent<Button>().interactable = Reload;
            Reload = currentbulletsingun < 10 && maxbulletsingun != 0 && Fire;

            if (currentbulletsingun <= 0 && maxbulletsingun <= 0)
                isSposobnsty = false;
        }
        else
        {
            if (AWPOn)
                WeaponModeChanged();
        }
    }

    public void WeaponModeChanged()
    {
        if (GetIsStartedGame() && !GetIsPausedGame())
        {
            Debug.Log("Cross");
            AWPOn = !AWPOn;
            Debug.Log("AWPOn:" +AWPOn);
            if (AWPOn)
            {
                WeaponMode.SetActive(AWPOn);
                SetFire(false);
                WeaponMode.transform.Find(NameAWP).GetComponent<Animator>().Play("On", 0, 0);
            }

            if (!AWPOn)
            {
                if (Crosshair.GetComponent<CrosshairMechanic>().isFire)
                    Crosshair.GetComponent<CrosshairMechanic>().isFire = AWPOn;
                SetFire(false);
                WeaponMode.transform.Find(NameAWP).GetComponent<Animator>().Play("Off", 0, 0);
                WeaponMode.SetActive(AWPOn);
            }    
        }    
    }

    internal bool GetAWPOn() { return AWPOn; }

    public void FireMethodAWP()
    {
        if (currentbulletsingun > 0)
        {
            WeaponMode.transform.Find(NameAWP).GetComponent<Animator>().Play("Fire", 0, 0);
            TriggerDestroyter.SpawnSound(AWPFire);
            currentbulletsingun--;
            Crosshair.GetComponent<CrosshairMechanic>().isFire = true;
            SetFire(false);
        }
        else if (maxbulletsingun > 0)
            ReloadButton();
        else
        {
            TriggerDestroyter.SpawnSound(NullBullets);
            WeaponModeChanged();
        }
            
    }


    private void FreezeGame()
    {
        if(FreezeScreen != null)
            FreezeScreen.GetComponent<Animator>().SetBool("On", IsGameFreezed);
        if (IsGameFreezed)
        {
            if (!isAudioClipFreezePlay)
            {
                TriggerDestroyter.SpawnSound(FreezeSound);
                isAudioClipFreezePlay = true;
            }
            if (music.pitch > 0)
                music.pitch -= Time.deltaTime;
            else
            {
                if(music.isPlaying)
                    music.Pause();
            }

            if (TimerFrezzed > 0)
                TimerFrezzed -= Time.deltaTime;
            else
            {
                music.pitch = 1f;
                TimerFrezzed = TimerFreezedConst;
                isAudioClipFreezePlay = false;
                IsGameFreezed = false;
            }
        }
    }

    internal int GetDifficulyIndex() { return indexDifficuly; }

    public void ResumeButton()
    {
        menuGameController.PlaySoundTrackGame(IDMusic);
        IsGamePaused = false;
    }
    public void PauseButton()
    {
        PauseUI.SetActive(true);
        IsGamePaused = true;
        music.Pause();
    }
    public void StopGameButton()
    {
        SchansGame = false;
        if(currentGameModeSelected == GameMode.BlueShift || currentGameModeSelected == GameMode.MatchMaking)
            TimerGame = 0;
        else if(currentGameModeSelected == GameMode.Survival)
            Health = 0;
        ResutGameUI.GetComponent<ResultGame>().SetIsStopedGame(true);
        ResutGameUI.GetComponent<ResultGame>().StopedGame();
        SetPauseGame(false);
    }
    internal void CheckMusicPlaying()
    {
        if (GetIsStartedGame() && !GetIsPausedGame())
        {
            if (music != null)
            {
                if (!music.isPlaying && !IsGameFreezed && !GameSchansUI.activeSelf)
                {
                    music.clip = menuGameController.SoundsTracks[UnityEngine.Random.Range(0, menuGameController.SoundsTracks.Length-1)];
                    music.Play();
                    MethodCounterMusicSurvial(false);
                    SelectedMusicCurrentText();
                    if (CountMusicSurvial > 1)
                    {
                        if (CountSongsSurvive != null)
                            CountSongsSurvive.Play("SongsSurvive", 0, 0);
                        switch (IDGetLanguage)
                        {
                            case 0:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsChina[25] + CountMusicSurvial + Translate.NameTextsChina[26];
                                break;
                            case 1:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsDanish[25] + CountMusicSurvial + Translate.NameTextsDanish[26];
                                break;
                            case 2:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsDutch[25] + CountMusicSurvial + Translate.NameTextsDutch[26];
                                break;
                            case 3:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsEng[25] + CountMusicSurvial + Translate.NameTextsEng[26];
                                break;
                            case 4:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsFinnish[25] + CountMusicSurvial + Translate.NameTextsFinnish[26];
                                break;
                            case 5:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsFrench[25] + CountMusicSurvial + Translate.NameTextsFrench[26];
                                break;
                            case 6:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsGerman[25] + CountMusicSurvial + Translate.NameTextsGerman[26];
                                break;
                            case 7:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsItalian[25] + CountMusicSurvial + Translate.NameTextsItalian[26];
                                break;
                            case 8:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsNorwegian[25] + CountMusicSurvial + Translate.NameTextsNorwegian[26];
                                break;
                            case 9:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsPortuguese[25] + CountMusicSurvial + Translate.NameTextsPortuguese[26];
                                break;
                            case 10:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsRU[25] + CountMusicSurvial + Translate.NameTextsRU[26];
                                break;
                            case 11:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsSpanishSpain[25] + CountMusicSurvial + Translate.NameTextsSpanishSpain[26];
                                break;
                            case 12:
                                CountSongsSurvive.transform.GetChild(0).GetComponent<Text>().text = Translate.NameTextsSwedish[25] + CountMusicSurvial + Translate.NameTextsSwedish[26];
                                break;
                        }
                        TriggerDestroyter.SpawnSound(SurviveMusicCounter);
                    }
                }
            }
        }
    }
    internal void RestartMusicForRestartGame()
    {
        music.Stop();
        music.Play();
    }

    private void BackgroundMusicChanged()
    {
        if(BackgroundMusic != null)
        {
            if (music != null)
                music.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        }
    }

    private void MoneyAnimOffMethod()
    {
        if (Gametexts[2] != null)
            if (Gametexts[2].GetComponent<Animator>().GetBool("On"))
            {
                if (TimerOffMoney > 0)
                    TimerOffMoney -= Time.deltaTime;
                else
                {
                    Gametexts[2].GetComponent<Animator>().SetBool("On", false);
                    TimerOffMoney = 3.0f;
                }
            }
    }

    private void BloodScreenMechanic()
    {
        if (currentGameModeSelected == GameMode.BlueShift || currentGameModeSelected == GameMode.MatchMaking)
        {
            if (GetIsStartedGame() && !GetIsPausedGame())
            {
                if (TimerGame > 0 && !IsGameFreezed && !GameSchansUI.activeSelf)
                    MethodTimerPlayer(Time.deltaTime, -1);
                else if(TimerGame > 0 && IsGameFreezed && !GameSchansUI.activeSelf)
                    MethodTimerPlayer(Mathf.Epsilon, -1);
                else
                {
                    TimerGame = 0;
                    if (!GameOver)
                    {
                        if (!SchansGame || currentGameModeSelected == GameMode.MatchMaking)
                        {
                            GameOverStayResults();
                            GameOver = true;
                        }
                        else
                        {
                            GameSchansUI.SetActive(true);
                            music.Pause();
                        }
                      
                    }
                   
                }
                    


                if (BloodScreen != null)
                    BloodScreen.SetBool("On", TimerGame < 5);
            }
        }
        else if (currentGameModeSelected == GameMode.Survival)
        {
            if (GetIsStartedGame() && !GetIsPausedGame())
            {
                if (Health <= 0)
                {
                    Health = 0;
                    if (!GameOver)
                    {
                        GameOverStayResults();
                        GameOver = true;
                    }
                }
                    
                if (BloodScreen != null)
                    BloodScreen.SetBool("On", Health <= 1);
            }
        }
    }

    


    public void GameOverStayResults()
    {
        if (!GameOver)
            GameOver = true;
        if (AWPOn)
            WeaponModeChanged();

        if (CountDeads < 2 && Ads)
            CountDeads++;
        else if(Ads)
        {
            if (Advertisement.IsReady(VideoID))
            {
                Advertisement.Show(VideoID);
                AdsBuyUI.SetActive(true);
                CountDeads = 0;
            }
        }
        MethodIsGameChanged(false);
        float reason = 10 - currentbulletsingun;
        if (maxbulletsingun >= reason)
        {
            maxbulletsingun = maxbulletsingun - reason;
            currentbulletsingun = 10;
        }
        else
        {
            currentbulletsingun = currentbulletsingun + maxbulletsingun;
            maxbulletsingun = 0;
        }
        menuGameController.SelectedMusic = null;
        BackgroundMusic.GetComponent<Image>().color = Color.white;
        if (music != null)
            music.pitch = 1f;
        TimerGameDifficulyCheck = DifficulyUpTimer;
        BloodScreen.gameObject.SetActive(false);
        ComboCounterText.GetComponent<Animator>().Play("ComboOff", 0, 0);
        GameOverVstavka.Play("GameOver", 0, 0);
        GameOverVstavka.GetComponent<AudioSource>().Play();
        music.Stop();
        if (currentGameModeSelected == GameMode.MatchMaking)
        {
            if (CurrentGameRoundTime > RecordTimerSurvive)
                serverManager.SetPlayerValueSurvived(IDPlayer, CurrentGameRoundTime);
            serverManager.SetGameRules(this);
            serverManager.SetIDPlayer(IDPlayer);
            serverManager.SetResultGameUI(ResutGameUI);
            serverManager.SetCurrentGameRoundTime(CurrentGameRoundTime);
            serverManager.SetCountMusicSurvial(CountMusicSurvial);
            serverManager.SetIndexDifficuly(indexDifficuly);
            serverManager.SetScorePlayerRound(ScorePlayerRound);
            serverManager.SetRecordPlayer(RecordPlayer);
            serverManager.SetMoneyPlayer(MoneyPlayer);
            serverManager.SetDifficulyIcon(DiffuculyIcon);
            serverManager.SetRecordComboPlayer(RecordComboGame);
            serverManager.SetLanguageID(IDGetLanguage);
            serverManager.SetCurrentBulletsInAWP(currentbulletsingun);
            serverManager.SetMaxBulletsInAWP(maxbulletsingun);
            serverManager.PreGameOver(false);
        }
        else if (currentGameModeSelected == GameMode.BlueShift || currentGameModeSelected == GameMode.Survival)
            ResutGameUI.GetComponent<ResultGame>().DataRefresh(-1, CurrentGameRoundTime, CountMusicSurvial, Translate.NameDifficulyEng[indexDifficuly], ScorePlayerRound, RecordPlayer, MoneyPlayer, DiffuculyIcon.sprite, false, RecordComboGame,IDGetLanguage,IDPlayer,currentbulletsingun,maxbulletsingun);

        CurrentGameRoundTime = 0;
        CountMusicSurvial = 0;
        music.Stop();
    }

    internal void SetDifficuly(int indexDifficuly)
    {
        DiffuculyIcon.sprite = DifficulySprites[indexDifficuly];
        this.indexDifficuly = indexDifficuly;
        ChangedDifficulyText();
    }

    private void DifficulyTimer()
    {
        if (TimerGameDifficulyCheck > 0)
        {
            if (!IsGameFreezed)
            {
                TimerGameDifficulyCheck -= Time.deltaTime;
                CurrentGameRoundTime += Time.deltaTime;
            }
            else
            {
                TimerGameDifficulyCheck -= Mathf.Epsilon;
                CurrentGameRoundTime += Mathf.Epsilon;
            }
                
        }
        else
        {
            if (currentGameModeSelected == GameMode.BlueShift || currentGameModeSelected == GameMode.MatchMaking)
                changeSpawnCards[4].currentChange -= DefaultUpDifficuly;
            else if (currentGameModeSelected == GameMode.Survival)
                changeSpawnCards[5].currentChange -= DefaultUpDifficuly;
            if (DiffuculyIcon != null)
            {
                DiffuculyIcon.GetComponent<Animator>().Play("DifficulyUp", 0, 0);
                TriggerDestroyter.SpawnSound(DifficulyUp);
                SetDifficuly(indexDifficuly);
            }
            MethodRandomClicked(1);
            indexDifficuly++;
            TimerGameDifficulyCheck = DifficulyUpTimer;
        }
    }

    private void ChangedDifficulyText()
    {
        if (Gametexts[3] != null)
        {
            switch (IDGetLanguage)
            {
                case 0:
                    Gametexts[3].text = Translate.NameTextsChina[24] + Translate.NameDifficulyChina[indexDifficuly];
                    break;
                case 1:
                    Gametexts[3].text = Translate.NameTextsDanish[24] + Translate.NameDifficulyDanish[indexDifficuly];
                    break;
                case 2:
                    Gametexts[3].text = Translate.NameTextsDutch[24] + Translate.NameDifficulyDutch[indexDifficuly];
                    break;
                case 3:
                    Gametexts[3].text = Translate.NameTextsEng[24] + Translate.NameDifficulyEng[indexDifficuly];
                    break;
                case 4:
                    Gametexts[3].text = Translate.NameTextsFinnish[24] + Translate.NameDifficulyFinnish[indexDifficuly];
                    break;
                case 5:
                    Gametexts[3].text = Translate.NameTextsFrench[24] + Translate.NameDifficulyFrench[indexDifficuly];
                    break;
                case 6:
                    Gametexts[3].text = Translate.NameTextsGerman[24] + Translate.NameDifficulyGerman[indexDifficuly];
                    break;
                case 7:
                    Gametexts[3].text = Translate.NameTextsItalian[24] + Translate.NameDifficulyItalian[indexDifficuly];
                    break;
                case 8:
                    Gametexts[3].text = Translate.NameTextsNorwegian[24] + Translate.NameDifficulyNorwegian[indexDifficuly];
                    break;
                case 9:
                    Gametexts[3].text = Translate.NameTextsPortuguese[24] + Translate.NameDifficulyPortuguese[indexDifficuly];
                    break;
                case 10:
                    Gametexts[3].text = Translate.NameTextsRU[24] + Translate.NameDifficulyRU[indexDifficuly];
                    break;
                case 11:
                    Gametexts[3].text = Translate.NameTextsSpanishSpain[24] + Translate.NameDifficulySpanishSpain[indexDifficuly];
                    break;
                case 12:
                    Gametexts[3].text = Translate.NameTextsSwedish[24] + Translate.NameDifficulySwedish[indexDifficuly];
                    break;
            }
        }
    }

    void ComboCheckerLive()
    {
        if (ComboCounterText != null)
        {
            if (LiveChecker)
            {
                if (ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().size > 0)
                {
                    ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().size -= Time.deltaTime;
                }
                else
                {
                    ComboCounterText.GetComponent<Animator>().Play("ComboOff", 0, 0);
                    if (RecordComboGame < ComboConnect)
                        RecordComboGame = ComboConnect;
                    if (ComboConnect > 0)
                        TriggerDestroyter.SpawnSound(ScoreClicks);
                    MethodScorePlayer(ComboConnect, false);
                    MethodComboConnectPlayer(true,false);
                    ComboCounterText.transform.GetChild(0).GetComponent<Scrollbar>().size = 1f;
                    LiveChecker = false;
                }

            }
        }
    }

    internal Text GetComboCounterText() { return ComboCounterText; }
}

[Serializable]
internal class ChangeSpawnCard
{
    [SerializeField] internal string NameSpawnCard;
    [Range(0,100)]
    [SerializeField] internal int currentChange;
    [SerializeField] internal CardType typeCard;
}
