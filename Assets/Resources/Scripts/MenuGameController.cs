using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static GameRules;

public class MenuGameController : MonoBehaviour
{
    #region ConstRegionMenuController
    const string PathToCreppa = "Creepa  Subsets - Downpour";
    const string PathToDOCTORVPX = "DOCTOR VOX - Death";
    const string PathToEricRodriguezIll = "Eric Rodriguez - Illuminati";
    const string PathToEricRodriguezSunlight = "Eric Rodriguez - Sunlight";
    const string PathToFiASKO = "FiASKO - Nu Demon";
    const string PathToMAY = "MAY - Terminus";
    const string PathToMOUNTSWIFT = "MOUNTSWIFT - Amethyst";
    const string PathToNeffex = "NEFFEX - Blow Up";
    const string PathToTokyoMachine = "Tokyo Machine - Spooky";
    const string PathToYusufAlev = "Yusuf Alev - Yoros";
    const string PathToZEDION = "ZEDION - Radiance";
    #endregion

    [SerializeField] internal string PathToMusic = "";
    [SerializeField] internal GameRules gameRules;
    [SerializeField] protected PlayerProfille playerProfille;
    [SerializeField] protected ServerManager serverManager;
    [SerializeField] protected Animator BookMenu;
    [SerializeField] bool isStatusOpen = false;
    [SerializeField] internal Text textMyMusicSelected, textMyMusicSelectedIsNotReadyOrReady;
    [SerializeField] Button skipButton,buttonSelectMusic;

    [SerializeField] internal GameObject[] UIGameModes;

    [SerializeField] internal AudioClip SelectedMusic;
    [SerializeField] internal AudioClip[] SoundsTracks;
    [SerializeField] AudioSource music;
    WWW www;


    private string LinkGameStore = "https://play.google.com/store/apps/details?id=com.KJPGames.Classic";


    void Start()
    {
        ListMusicGetting sounds = GameObject.Find("SoundTracks").GetComponent<ListMusicGetting>();
        SoundsTracks = new AudioClip[11];
        for(int i = 0; i < sounds.audioClips.Count; i++)
        {
            if(sounds.audioClips[i].name == PathToTokyoMachine)
                SoundsTracks[0] = sounds.audioClips[i];
            
            else if(sounds.audioClips[i].name == PathToDOCTORVPX)
                SoundsTracks[1] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToEricRodriguezIll)
                SoundsTracks[2] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToEricRodriguezSunlight)
                SoundsTracks[3] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToFiASKO)
                SoundsTracks[4] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToYusufAlev)
                SoundsTracks[5] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToMAY)
                SoundsTracks[6] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToMOUNTSWIFT)
                SoundsTracks[7] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToNeffex)
                SoundsTracks[8] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToZEDION)
                SoundsTracks[9] = sounds.audioClips[i];
            
            else if (sounds.audioClips[i].name == PathToCreppa)
                SoundsTracks[10] = sounds.audioClips[i];
        }
        music.clip = SoundsTracks[10];
        music.Play();
    }

    public void SetMusic()
    {
        music.clip = SoundsTracks[10];
        music.Play();
    }

    internal IEnumerator LoadMusic(string pathToClip)
    {
        //Use the first audio index found in the directory
        string audioPath = "file:///" + pathToClip;
        Debug.Log(audioPath);

        using (WWW www = new WWW(audioPath))
        {
            yield return www;

            //Set the AudioClip to the loaded one
            AudioClip clip = www.GetAudioClip(false, false);
            music.clip = clip;
            music.Play();
        }
    }

    public void OpenCloseBookMenu()
    {
        isStatusOpen = !isStatusOpen;
        BookMenu.SetBool("Open", isStatusOpen);
    }
    public void OpenURLBookMenu(string url)
    {
        Application.OpenURL(url);
        Application.Quit();
    }
    public void OpenURLGameStore()
    {
        Application.OpenURL(LinkGameStore);
        Application.Quit();
    }
    public void PlaySoundTrackGame(int index)
    {
        if (music != null)
        {
            SetSelectedMusic(SoundsTracks[index]);
            music.clip = SoundsTracks[index];
            music.Play();
        }
    }

    internal void SetSelectedMusic(AudioClip audio)
    {
        SelectedMusic = audio;
    }
    public void StopSoundTrackGame()
    {
        SetMusic();
    }

    public void OpenMenu(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void CloseMenu(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SpawnSound(AudioClip clip)
    {
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = clip;
        Sound.GetComponent<AudioSource>().Play();
        Sound.AddComponent<DestoryGameObject>();
    }

    public void ScrollingAnimation(Animator anim)
    {
        anim.Play("MenuScrolling", 0, 0);
    }

    public void SkipStartAnimation(Animator anim)
    {
        anim.Play("CutScene", 0, 0.98f);
        skipButton.gameObject.SetActive(false);
    }

    /// <param name="indexMode">0 - Survival 1 - BlueShift 2 - MatchMaking</param>
    public void GameModeSelecting(int indexMode)
    {
        switch (indexMode)
        {
            case 0:
                gameRules.currentGameModeSelected = GameMode.Survival;
                buttonSelectMusic.interactable = true;
                break;
            case 1:
                gameRules.currentGameModeSelected = GameMode.BlueShift;
                buttonSelectMusic.interactable = true;
                break;
            case 2:
                gameRules.currentGameModeSelected = GameMode.MatchMaking;
                buttonSelectMusic.interactable = false;
                switch (playerProfille.GetLanguageID())
                {
                    case 0:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsChina[19];
                        break;
                    case 1:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsDanish[19];
                        break;
                    case 2:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsDutch[19];
                        break;
                    case 3:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsEng[19];
                        break;
                    case 4:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsFinnish[19];
                        break;
                    case 5:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsFrench[19];
                        break;
                    case 6:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsGerman[19];
                        break;
                    case 7:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsItalian[19];
                        break;
                    case 8:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsNorwegian[19];
                        break;
                    case 9:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsPortuguese[19];
                        break;
                    case 10:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsRU[19];
                        break;
                    case 11:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsSpanishSpain[19];
                        break;
                    case 12:
                        textMyMusicSelectedIsNotReadyOrReady.text = Translate.NameTextsSwedish[19];
                        break;
                }
                break;
        }
    }

    public void StartGame()
    {
        UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].SetActive(true);
        if(gameRules.currentGameModeSelected == GameMode.Survival)
        {
            if (playerProfille.GetIsSposobnosty())
            {
                UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].GetComponent<Animator>().Play("SurvivalPreStartSposobnosty", 0, 0);
                Debug.Log("IsSposobnosty");
            }
            else
                UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].GetComponent<Animator>().Play("SurvivalPreStart", 0, 0);
        }
        else if(gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking)
        {
            if (playerProfille.GetIsSposobnosty())
            {
                UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].GetComponent<Animator>().Play("BlueShiftModePreStartSposobnosty", 0, 0);
                Debug.Log("IsSposobnosty");
            }
                
            else
                UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].GetComponent<Animator>().Play("BlueShiftPreStart", 0, 0);
        }
        Text[] UIGame = PreparingUIAndMusicData();
        Animator bloodScreen = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("BloodScreen").GetComponent<Animator>();
        Animator CountSongsSurvive = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("CountSongsSurvive").GetComponent<Animator>();
        Animator GameOverVstavka = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("GameOverVstavka").GetComponent<Animator>();
        Image CurrentDifficulyImg = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("CurrentDifficulyImg").GetComponent<Image>();
        Text ComboCounterText = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("ComboCounterText").GetComponent<Text>();
        Button SposobnostyButton = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("SposobnostyText").GetChild(0).GetComponent<Button>();
        Text SposobnostyText = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("SposobnostyText").GetChild(0).GetChild(0).GetComponent<Text>();
        Text WeaponModeText = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("WeaponMode").Find("Background").GetChild(0).GetComponent<Text>();
        GameObject WeaponMode = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("WeaponMode").gameObject;
        WeaponMode.SetActive(false);
        GameObject FreezeScreen = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("FreezeScreen").gameObject;
        gameRules.GameModeUIAndMusicObject(UIGame, ComboCounterText, GameOverVstavka, CountSongsSurvive, bloodScreen, CurrentDifficulyImg, music, playerProfille.GetIDPlayer(), playerProfille.GetTimeSurivePlayer(), playerProfille.GetRecordScorePlayer(), UIGameModes[gameRules.currentGameModeSelected.GetHashCode()], playerProfille.GetAdsPlayer(), playerProfille.GetLanguageID(), FreezeScreen, WeaponMode, playerProfille.GetIsSposobnosty(), SposobnostyText, WeaponModeText, playerProfille.GetCurrentBulletsAWP(), playerProfille.GetMaxBulletsAWP(), SposobnostyButton);
    }
    public void PreRestart()
    {
        UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].SetActive(false);
        UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("PauseMenu").GetComponent<Button>().interactable = false;
        gameRules.MethodIsGameChanged(false);
        StartGame();
    }

    private Text[] PreparingUIAndMusicData()
    {
        Text[] UIGame = new Text[5];
        UIGame[0] = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("ScoreText").GetComponent<Text>();
        UIGame[1] = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.GetChild(0).GetChild(0).GetComponent<Text>();
        UIGame[2] = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("MoneyText").GetComponent<Text>();
        UIGame[3] = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("CurrentDifficulyImg").GetChild(0).GetComponent<Text>();
        UIGame[4] = UIGameModes[gameRules.currentGameModeSelected.GetHashCode()].transform.Find("CurrentMusicSelectionText").GetComponent<Text>();
        if (SelectedMusic == null && string.IsNullOrEmpty(PathToMusic))
        {
            int index = Random.Range(0, SoundsTracks.Length-2);
            gameRules.SetMusicID(index);
        }
        else if (!string.IsNullOrEmpty(PathToMusic))
        {
            www = new WWW("file://"+PathToMusic);
            music.clip = SelectedMusic = www.GetAudioClip(false, true, AudioType.MPEG);
        }
        if (music.isPlaying)
            music.Stop();
        return UIGame;
    }
}
