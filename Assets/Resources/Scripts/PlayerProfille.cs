using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using static GameRules;

public class RsaEnc
{
    private static string key = "dofkrfacsrdedofkrfaosrdedofsrfao";

    private static string IV = "zxcvbnmdfrasdfgh";

    internal static string Encrypt(string text)
    {
        byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
        aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
        crypto.Dispose();
        return Convert.ToBase64String(encrypted);
    }

    internal static string Decrypt(string encrypted)
    {
        byte[] encryptedbytes = Convert.FromBase64String(encrypted);
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
        aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
        crypto.Dispose();
        return System.Text.ASCIIEncoding.ASCII.GetString(secret);
    }
}

public class PlayerProfille : MonoBehaviour
{

    const int IDBombSpawnLvL = 0, IDMagnitSpawnLvL = 1, IDMoneySpawnLvL = 2,IDBulletsAWP = 3;
    const float BulletBuy = 5;
    const float InAWPBulletsCurrentMax = 10;
    const float BombSpawnLvL2 = 500, BombSpawnLvL3 = 1500, BombSpawnLvL4 = 3500, BombSpawnLvL5 = 5000;
    const float MagnitSpawnLvL2 = 500, MagnitSpawnLvL3 = 1750, MagnitSpawnLvL4 = 3750, MagnitSpawnLvL5 = 4499;
    const float MoneySpawnLvL2 = 1000, MoneySpawnLvL3 = 4000, MoneySpawnLvL4 = 8000, MoneySpawnLvL5 = 12000;

    const int SchansCharactheristikBombSpawnLvL2 = 50, SchansCharactheristikBombSpawnLvL3 = 45, SchansCharactheristikBombSpawnLvL4 = 30, SchansCharactheristikBombSpawnLvL5 = 5;
    const int SchansCharactheristikMagnitSpawnLvL2 = 10, SchansCharactheristikMagnitSpawnLvL3 = 30, SchansCharactheristikMagnitSpawnLvL4 = 50, SchansCharactheristikMagnitSpawnLvL5 = 70;
    const int SchansCharactheristikMoneyMinLvL2 = 2, SchansCharactheristikMoneyMaxLvL2 = 20, SchansCharactheristikMoneyMinLvL3 = 4, SchansCharactheristikMoneyMaxLvL3 = 30, SchansCharactheristikMoneyMinLvL4 = 8, SchansCharactheristikMoneyMaxLvL4 = 40, SchansCharactheristikMoneyMinLvL5 = 9, SchansCharactheristikMoneyMaxLvL5 = 40;

    const float SizeScrollBar = 5f;
    const int minIDPlayer = 0, maxIDPlayer = int.MaxValue;
    const int MaxCountLanguages = 12;

    const string PathToBackgroundInDesktop = "Mods/Backgrounds";
    const string PahToMusicInDesktop = "Mods/Music";
    const string PathToAWPSkinsInDesktop = "Mods/AWPSkins";

    [Header("__PlayerProfile__")]

    [SerializeField] internal PlayerInventory playerInventory;
    [SerializeField] internal CrosshairSettingsMain crosshairSettings;
    [SerializeField] internal GameRules gameRules;
    [SerializeField] internal ServerManager serverManager;
    [SerializeField] internal InizializationSuccessWelcomeScreen inizializationSuccessWelcomeScreen;
    [SerializeField] internal Scrollbar[] Bleedings;
    [SerializeField] internal Button[] BuyButtons;
    [SerializeField] internal GameObject UpdateUIObject;
    [SerializeField] internal Text[] Buys;
    [SerializeField] internal Text[] LvLs;
    [SerializeField] internal Text currentMoneyPlayer;

    [SerializeField] internal Sprite[] RewardsSprite,LanguagesSprite;
    [SerializeField] internal Dropdown dropdownlistWorkshop;
    [SerializeField] internal Button EnteringNickNameButton;
    [SerializeField] internal GameObject BackgroundMusic;
    [SerializeField] internal Transform ContentInventory;
    [SerializeField] internal GameObject InventoryItemSchblon;
    [SerializeField] internal Text[] TextsInGame;
    [SerializeField] internal Image CurrentLanguageImgCountry;

    private string LinkVersionServer = "https://raw.githubusercontent.com/MrKsi/Classic/master/versionGame.txt";
    [SerializeField] private string PathToModsFloderMusic,PathToModsFloderBackgrounds, PathToModsFloderAWPSkins;
    [SerializeField] private SaveDataGame dataPlayer = new SaveDataGame();
    private string pathToSave;
    [SerializeField] internal Material AWPMat;
    [SerializeField] internal Texture2D DefaultTexture;
    [SerializeField] internal bool isLoad = false,isInventoryLoad = false;


    //Mods
    [SerializeField] internal int CurrentFilterInventory = 0;
    [SerializeField] internal Text[] CurrentTextsFiltersInventory;
    [SerializeField] internal Sprite DefaultBackground;
    [SerializeField] internal Transform ContentWorkshopMusic,ContentScrollView;
    [SerializeField] internal GameObject MusicWorkshopText, DefaultMusicText;
    [SerializeField] internal GameObject MusicScablonButton;
    [SerializeField] internal MenuGameController menugameController;


    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        pathToSave = Path.Combine(Application.persistentDataPath, "Data.json");
#else
        pathToSave = Path.Combine(Application.dataPath, "Data.json");
#endif
        CreateModsFloders();
    }
    
    void FixedUpdate()
    {
        if (inizializationSuccessWelcomeScreen.EnteringNickName.activeSelf)
            EnteringNickNameButton.interactable = inizializationSuccessWelcomeScreen.EnteringNickName.transform.Find("Background").Find("EnteringNickNamePlayerInGame").GetComponent<InputField>().text.Length > 2;
    }
    public void RefreshInventoryMap(int CurrentFilterSelected)
    {
        CurrentFilterInventory = CurrentFilterSelected;
        UIChangedFilter();
        switch (CurrentFilterInventory)
        {
            case 0:
                RefreshRewardsListInventory();
                break;
            case 1:
                RefreshBackgroundWorkshopInventory();
                break;
            case 2:
                RefreshSkinsAWPWorkshopInventory();
                break;
        }
    }


    public void EnteringNickName(InputField inputField)
    {
        dataPlayer.NickNamePlayer = inputField.text;
        dataPlayer.isTraningMode = false;
        IDPlayerGenerated();
        Profile ID = SearchUserInDataBase(dataPlayer.IDPlayer, serverManager.GetPlayersList());
        while (ID != null)
        {
            IDPlayerGenerated();
            ID = SearchUserInDataBase(dataPlayer.IDPlayer, serverManager.GetPlayersList());
        }
        serverManager.AddPlayer(dataPlayer.IDPlayer, dataPlayer.NickNamePlayer);
        serverManager.GetMoneyPlayerServer(dataPlayer.IDPlayer);
    }

    private void RefreshSkinsAWPWorkshopInventory()
    {
        ClearScrollViewList();
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().ClearList();
#if UNITY_ANDROID && !UNITY_EDITOR
      
#else
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + '/' + PathToAWPSkinsInDesktop);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        int countMods = directoryInfo.GetFiles().Length;
        for (int i = 0; i < countMods; i++)
        {
            if (!fileInfos[i].FullName.Contains(".meta") && fileInfos[i].FullName.Contains("Icon"))
            {
                GameObject newItem = Instantiate(InventoryItemSchblon);
                byte[] downloadAsbyteArray = File.ReadAllBytes(fileInfos[i].FullName);
                Texture2D tex = new Texture2D(640, 1136);
                tex.LoadImage(downloadAsbyteArray);
                newItem.GetComponent<ChangedBackgroundGame>().ModSelection(tex, fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 8), fileInfos[i].FullName, true);
                newItem.transform.GetChild(0).GetComponent<Text>().text = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 8);
                newItem.GetComponent<ChangedBackgroundGame>().ParentWorkshopUncheker = ContentInventory;
                newItem.transform.SetParent(ContentInventory);
                newItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }
#endif
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().AddedListItems();
    }

    private void RefreshBackgroundWorkshopInventory()
    {
        ClearScrollViewList();
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().ClearList();
#if UNITY_ANDROID && !UNITY_EDITOR
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath  + '/' + PathToBackgroundInDesktop);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        int countMods = directoryInfo.GetFiles().Length;
        for (int i = 0; i < countMods; i++)
        {
            if(!fileInfos[i].FullName.Contains(".meta"))
            {
                GameObject newItem = Instantiate(InventoryItemSchblon);
                byte[] downloadAsbyteArray = File.ReadAllBytes(fileInfos[i].FullName);
                Texture2D tex = new Texture2D(640, 1136);
                tex.LoadImage(downloadAsbyteArray);
                newItem.GetComponent<ChangedBackgroundGame>().ModSelection(tex, fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4), fileInfos[i].FullName,false);
                newItem.transform.GetChild(0).GetComponent<Text>().text = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4);
                newItem.GetComponent<ChangedBackgroundGame>().ParentWorkshopUncheker = ContentInventory;
                newItem.transform.SetParent(ContentInventory);
                newItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }
#else
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + '/' + PathToBackgroundInDesktop);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        int countMods = directoryInfo.GetFiles().Length;
        for (int i = 0; i < countMods; i++)
        {
            if(!fileInfos[i].FullName.Contains(".meta"))
            {
                GameObject newItem = Instantiate(InventoryItemSchblon);
                byte[] downloadAsbyteArray = File.ReadAllBytes(fileInfos[i].FullName);
                Texture2D tex = new Texture2D(640, 1136);
                tex.LoadImage(downloadAsbyteArray);
                newItem.GetComponent<ChangedBackgroundGame>().ModSelection(tex, fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4), fileInfos[i].FullName,false);
                newItem.transform.GetChild(0).GetComponent<Text>().text = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4);
                newItem.GetComponent<ChangedBackgroundGame>().ParentWorkshopUncheker = ContentInventory;
                newItem.transform.SetParent(ContentInventory);
                newItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }
#endif
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().AddedListItems();
    }

    public void LoadMusicWorkshop()
    {
        ClearListWorkshopMusic();
#if UNITY_ANDROID && !UNITY_EDITOR
        if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + '/' + PahToMusicInDesktop);
            int countMusicFiles = directoryInfo.GetFiles().Length;
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            if (countMusicFiles > 0)
            {
                MusicWorkshopText.SetActive(true);
                for (int i = 0; i < countMusicFiles; i++)
                {
                    if (!fileInfos[i].FullName.Contains(".meta"))
                    {
                        GameObject schablonMusicButton = Instantiate(MusicScablonButton);
                        schablonMusicButton.transform.SetParent(MusicWorkshopText.transform);
                        schablonMusicButton.transform.localScale = new Vector3(1.0032f, 1.0032f, 1.0032f);
                        schablonMusicButton.GetComponent<Button>().onClick.AddListener(menugameController.StopSoundTrackGame);
                        schablonMusicButton.transform.GetChild(0).GetComponent<Text>().text = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4);
                        StartCoroutine(GetAudioClip(schablonMusicButton, fileInfos[i].FullName));
                    }
                }
                DefaultMusicText.SetActive(true);
                if(DefaultMusicText.transform.parent != MusicWorkshopText.transform)
                 DefaultMusicText.transform.SetParent(MusicWorkshopText.transform);
            }
            else
            {
                if (DefaultMusicText.transform.parent == MusicWorkshopText.transform)
                    DefaultMusicText.transform.SetParent(ContentScrollView.transform);
                MusicWorkshopText.SetActive(false);
                DefaultMusicText.SetActive(true);
            }
                
        }
        else
        {
            if (DefaultMusicText.transform.parent == MusicWorkshopText.transform)
                DefaultMusicText.transform.SetParent(ContentScrollView.transform);
            MusicWorkshopText.SetActive(false);
            DefaultMusicText.SetActive(true);
        }    
#else
        if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + '/' + PahToMusicInDesktop);
            int countMusicFiles = directoryInfo.GetFiles().Length;
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            if (countMusicFiles > 0)
            {
                MusicWorkshopText.SetActive(true);
                for (int i = 0; i < countMusicFiles; i++)
                {
                    if (!fileInfos[i].FullName.Contains(".meta"))
                    {
                        GameObject schablonMusicButton = Instantiate(MusicScablonButton);
                        schablonMusicButton.transform.SetParent(MusicWorkshopText.transform);
                        schablonMusicButton.transform.localScale = new Vector3(1.0032f, 1.0032f, 1.0032f);
                        schablonMusicButton.GetComponent<Button>().onClick.AddListener(menugameController.StopSoundTrackGame);
                        schablonMusicButton.transform.GetChild(0).GetComponent<Text>().text = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 4);
                        StartCoroutine(GetAudioClip(schablonMusicButton, fileInfos[i].FullName));
                    }
                }
                DefaultMusicText.SetActive(true);
                if(DefaultMusicText.transform.parent != MusicWorkshopText.transform)
                 DefaultMusicText.transform.SetParent(MusicWorkshopText.transform);
            }
            else
            {
                if (DefaultMusicText.transform.parent == MusicWorkshopText.transform)
                    DefaultMusicText.transform.SetParent(ContentScrollView.transform);
                MusicWorkshopText.SetActive(false);
                DefaultMusicText.SetActive(true);
            }  
        }
        else
        {
            if (DefaultMusicText.transform.parent == MusicWorkshopText.transform)
                DefaultMusicText.transform.SetParent(ContentScrollView.transform);
            MusicWorkshopText.SetActive(false);
            DefaultMusicText.SetActive(true);
        }    
#endif
    }

    void ClearListWorkshopMusic()
    {
        if (DefaultMusicText.transform.parent == MusicWorkshopText.transform)
            DefaultMusicText.transform.SetParent(ContentScrollView.transform);
        for (int i = 0; i < ContentWorkshopMusic.childCount; i++)
            Destroy(ContentWorkshopMusic.GetChild(i).gameObject);
    }

    IEnumerator GetAudioClip(GameObject schablonMusicButton,string pathToClip)
    {
        //Use the first audio index found in the directory
        string audioPath = "file:///" + pathToClip;
        Debug.Log(audioPath);

        using (WWW www = new WWW(audioPath))
        {
            yield return www;

            //Set the AudioClip to the loaded one
            AudioClip clip = www.GetAudioClip(false, false);
            schablonMusicButton.transform.GetChild(1).gameObject.AddComponent<WorkshopMusicPlayButton>();
            schablonMusicButton.transform.GetChild(1).gameObject.GetComponent<WorkshopMusicPlayButton>().clip = clip;
        }
    }


        public void ResetFilter()
    {
        CurrentFilterInventory = 0;
        UIChangedFilter();
    }

    private void UIChangedFilter()
    {
        foreach (Text B in CurrentTextsFiltersInventory)
        {
            if (B.name == CurrentTextsFiltersInventory[CurrentFilterInventory].name)
                B.color = new Color(0.895999f, 0, 1, 1);
            else
                B.color = new Color(0.895999f, 0, 1, 0.5019608f);
        }
    }


    private void CreateModsFloders()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        PathToModsFloderMusic = Path.Combine(Application.persistentDataPath, "Mods", "Music");
        PathToModsFloderBackgrounds = Path.Combine(Application.persistentDataPath, "Mods", "Backgrounds");
        PathToModsFloderAWPSkins = Path.Combine(Application.persistentDataPath, "Mods", "AWPSkins");
        DirectoryInfo mods = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Mods"));
        if (!mods.Exists)
        {
            mods.Create();
            mods = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Mods", "Music"));
            if (!mods.Exists)
            {
                mods.Create();
                mods = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Mods", "Backgrounds"));
                if (!mods.Exists)
                {
                    mods.Create();
                    mods = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Mods", "AWPSkins"));
                    if (!mods.Exists)
                    {
                        mods.Create();
                        string pathDefault = Application.persistentDataPath + '/' + "Mods" + '/' + "DefaultAWP" + ".png";
                        string pathDefaultIcon = Application.persistentDataPath + '/' + "Mods" + '/' + "DefaultAWPIcon" + ".png";
                        File.WriteAllBytes(pathDefault, GetComponent<DefaultSkinAWPByte>().GetFromDefaultAWPTexture);
                        File.WriteAllBytes(pathDefaultIcon, GetComponent<DefaultSkinAWPByte>().GetFromIconDefaultAWP);
                        mods = null;
                    }
                }
            }
        }

        if (!File.Exists(Application.persistentDataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWP" + ".png"))
        {
            string pathDefault = Application.persistentDataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWP" + ".png";
            File.WriteAllBytes(pathDefault, GetComponent<DefaultSkinAWPByte>().GetFromDefaultAWPTexture);
        }

        if (!File.Exists(Application.persistentDataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWPIcon" + ".png"))
        {
            string pathDefaultIcon = Application.persistentDataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWPIcon" + ".png";
            File.WriteAllBytes(pathDefaultIcon, GetComponent<DefaultSkinAWPByte>().GetFromIconDefaultAWP);
        }
#else
        PathToModsFloderMusic = Path.Combine(Application.dataPath, "Mods", "Music");
        PathToModsFloderBackgrounds = Path.Combine(Application.dataPath, "Mods", "Backgrounds");
        PathToModsFloderAWPSkins = Path.Combine(Application.dataPath, "Mods", "AWPSkins");
        DirectoryInfo mods = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods"));
        if (!mods.Exists)
        {
            mods.Create();
            mods = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods", "Music"));
            if (!mods.Exists)
            {
                mods.Create();
                mods = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods", "Backgrounds"));
                if (!mods.Exists)
                {
                    mods.Create();
                    mods = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods", "AWPSkins"));
                    if (!mods.Exists)
                    {
                        mods.Create();
                        mods = null;
                    }
                }
            }
        }
        if (!File.Exists(Application.dataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWP" + ".png"))
        {
            string pathDefault = Application.dataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWP" + ".png";
            File.WriteAllBytes(pathDefault, GetComponent<DefaultSkinAWPByte>().GetFromDefaultAWPTexture);
        }
        if (!File.Exists(Application.dataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWPIcon" + ".png"))
        {
            string pathDefaultIcon = Application.dataPath + '/' + "Mods" + "/AWPSkins/" + "DefaultAWPIcon" + ".png";
            File.WriteAllBytes(pathDefaultIcon, GetComponent<DefaultSkinAWPByte>().GetFromIconDefaultAWP);
        }
#endif
    }



    internal IEnumerator VersionChecker()
    {
        WWW www = new WWW(LinkVersionServer);
        yield return www;
        if(www.text != Application.version)
            UpdateUIObject.SetActive(true);
    }

    internal void VersionCheckerMethod()
    {
        StartCoroutine(VersionChecker());
    }
    void Update()
    {
        if (serverManager.isLoadingOK)
        {
            LoadPlayerData();
            ChangeBleedingUI();
            serverManager.isLoadingOK = false;
        }
        if (!isInventoryLoad && isLoad)
        {
            LoadInventoryRewards();
            if (playerInventory.items.Count > 0)
                UpdateNamesItemsInventoryLanguage();
            if (dataPlayer.currentIDSelectedBackground >= 0 && dataPlayer.currentIDSelectedBackground < RewardsSprite.Length)
                LoadBackgroundSelected();
            else if(dataPlayer.PathToBackgroundMod != "")
            {
                if (File.Exists(dataPlayer.PathToBackgroundMod))
                {
                    byte[] data = File.ReadAllBytes(dataPlayer.PathToBackgroundMod);
                    Texture2D tex = new Texture2D(640, 1136);
                    tex.LoadImage(data);
                    Sprite Icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    BackgroundMusic.GetComponent<Image>().sprite = Icon;
                }
                else
                {
                    dataPlayer.currentIDSelectedBackground = -1;
                    dataPlayer.PathToBackgroundMod = "";
                    BackgroundMusic.GetComponent<Image>().sprite = DefaultBackground;
                }
            }
            isInventoryLoad = true;
        }


    }

    void LoadPlayerData()
    {
        if (!File.Exists(pathToSave))
        {
            CreateNewSaveFile();
            if (GetTraningMode())
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Chinese:
                        SetLanguageID(0);
                        break;
                    case SystemLanguage.ChineseSimplified:
                        SetLanguageID(0);
                        break;
                    case SystemLanguage.ChineseTraditional:
                        SetLanguageID(0);
                        break;
                    case SystemLanguage.Danish:
                        SetLanguageID(1);
                        break;
                    case SystemLanguage.Dutch:
                        SetLanguageID(2);
                        break;
                    case SystemLanguage.English:
                        SetLanguageID(3);
                        break;
                    case SystemLanguage.Finnish:
                        SetLanguageID(4);
                        break;
                    case SystemLanguage.French:
                        SetLanguageID(5);
                        break;
                    case SystemLanguage.German:
                        SetLanguageID(6);
                        break;
                    case SystemLanguage.Italian:
                        SetLanguageID(7);
                        break;
                    case SystemLanguage.Norwegian:
                        SetLanguageID(8);
                        break;
                    case SystemLanguage.Portuguese:
                        SetLanguageID(9);
                        break;
                    case SystemLanguage.Russian:
                        SetLanguageID(10);
                        break;
                    case SystemLanguage.Spanish:
                        SetLanguageID(11);
                        break;
                    case SystemLanguage.Swedish:
                        SetLanguageID(12);
                        break;
                }
            }
        }
        else
        {
            if (!isLoad)
            {

                string Decode = File.ReadAllText(pathToSave, Encoding.Default).Replace("\n", " ");
                if (Decode.Length > 0)
                {
                    var plainTetx = RsaEnc.Decrypt(Decode);
                    File.WriteAllText(pathToSave, plainTetx, Encoding.Default);
                    dataPlayer = JsonUtility.FromJson<SaveDataGame>(File.ReadAllText(pathToSave));
                    Debug.Log("DecryptText: \n" + plainTetx);
                    crosshairSettings.LoadSettings(dataPlayer.Thickness, dataPlayer.Distance, dataPlayer.OutLine, dataPlayer.Red, dataPlayer.Blue, dataPlayer.Green, dataPlayer.TObrazz);

#if UNITY_ANDROID && !UNITY_EDITOR
                    if (!string.IsNullOrEmpty(dataPlayer.NameAWPSkinCurrentSelected))
                    {
                        byte[] data = File.ReadAllBytes(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + dataPlayer.NameAWPSkinCurrentSelected + ".png");
                        Texture2D AWPSelectedSkin = new Texture2D(1024, 1024);
                        AWPSelectedSkin.LoadImage(data);
                        AWPMat.mainTexture = AWPSelectedSkin;
                    }
#else
                    if (!string.IsNullOrEmpty(dataPlayer.NameAWPSkinCurrentSelected))
                    {
                        byte[] data = File.ReadAllBytes(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + dataPlayer.NameAWPSkinCurrentSelected + ".png");
                        Texture2D AWPSelectedSkin = new Texture2D(1024, 1024);
                        AWPSelectedSkin.LoadImage(data);
                        AWPMat.mainTexture = AWPSelectedSkin;
                    }
#endif
                    serverManager.GetMoneyPlayerServer(dataPlayer.IDPlayer);
                    ChangeLanguage();
                    isLoad = true;
                }
                else
                    CreateNewSaveFile();  
            }
        }
    }

    private void CreateNewSaveFile()
    {
        isLoad = true;
        File.AppendAllText(pathToSave, "");
        crosshairSettings.LoadSettings(dataPlayer.Thickness, dataPlayer.Distance, dataPlayer.OutLine, dataPlayer.Red, dataPlayer.Blue, dataPlayer.Green, dataPlayer.TObrazz);
        dataPlayer.isTraningMode = true;
    }

    internal void LoadPlayerDataMethod(byte[] data)
    {
        LoadPlayerData(Encoding.UTF8.GetString(data, 0, data.Length));
    }
      
    void LoadPlayerData(string data)
    {
        if (!File.Exists(pathToSave))
        {
            isLoad = true;
            File.AppendAllText(pathToSave, "");
            dataPlayer.isTraningMode = true;
            if (!File.Exists(pathToSave))
            {
                CreateNewSaveFile();
                if (GetTraningMode())
                {
                    switch (Application.systemLanguage)
                    {
                        case SystemLanguage.Chinese:
                            SetLanguageID(0);
                            break;
                        case SystemLanguage.ChineseSimplified:
                            SetLanguageID(0);
                            break;
                        case SystemLanguage.ChineseTraditional:
                            SetLanguageID(0);
                            break;
                        case SystemLanguage.Danish:
                            SetLanguageID(1);
                            break;
                        case SystemLanguage.Dutch:
                            SetLanguageID(2);
                            break;
                        case SystemLanguage.English:
                            SetLanguageID(3);
                            break;
                        case SystemLanguage.Finnish:
                            SetLanguageID(4);
                            break;
                        case SystemLanguage.French:
                            SetLanguageID(5);
                            break;
                        case SystemLanguage.German:
                            SetLanguageID(6);
                            break;
                        case SystemLanguage.Italian:
                            SetLanguageID(7);
                            break;
                        case SystemLanguage.Norwegian:
                            SetLanguageID(8);
                            break;
                        case SystemLanguage.Portuguese:
                            SetLanguageID(9);
                            break;
                        case SystemLanguage.Russian:
                            SetLanguageID(10);
                            break;
                        case SystemLanguage.Spanish:
                            SetLanguageID(11);
                            break;
                        case SystemLanguage.Swedish:
                            SetLanguageID(12);
                            break;
                    }
                }
            }
        }
        else
        {
            if (!isLoad)
            {
                string Decode = data;
                var plainTetx = RsaEnc.Decrypt(Decode);
                File.WriteAllText(pathToSave, plainTetx, Encoding.Default);
                dataPlayer = JsonUtility.FromJson<SaveDataGame>(data);
                crosshairSettings.LoadSettings(GetThickness(), GetDistance(), GetOutLine(), GetRedColor(), GetBlueColor(), GetGreenColor(), GetTObrazz());
#if UNITY_ANDROID && !UNITY_EDITOR
                    if (!string.IsNullOrEmpty(dataPlayer.NameAWPSkinCurrentSelected))
                    {
                        byte[] dataawp = File.ReadAllBytes(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + dataPlayer.NameAWPSkinCurrentSelected + ".png");
                        Texture2D AWPSelectedSkin = new Texture2D(1024, 1024);
                        AWPSelectedSkin.LoadImage(dataawp);
                        AWPMat.mainTexture = AWPSelectedSkin;
                    }
#else
                if (!string.IsNullOrEmpty(dataPlayer.NameAWPSkinCurrentSelected))
                {
                    byte[] dataawp = File.ReadAllBytes(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + dataPlayer.NameAWPSkinCurrentSelected + ".png");
                    Texture2D AWPSelectedSkin = new Texture2D(1024, 1024);
                    AWPSelectedSkin.LoadImage(dataawp);
                    AWPMat.mainTexture = AWPSelectedSkin;
                }
#endif
                ChangeLanguage();
                Debug.Log("DecryptText: \n" + plainTetx);
                isLoad = true;
            }
        }
    }

    internal void SetImgInSpriteReward(Sprite sprite,int index)
    {
        RewardsSprite[index] = sprite;
        RewardsSprite[index].name = Translate.NameRewardsRU[index];
    }
    internal void SetCurrentAWPSkinSelected(string name)
    {
        dataPlayer.NameAWPSkinCurrentSelected = name;
    }
    internal string GetCurrentAWPSkinSelected() { return dataPlayer.NameAWPSkinCurrentSelected; }
    internal void AddItemInInventory(int IDItem)
    {
        Item item = new Item();
        item.ID = IDItem;
        item.Image = RewardsSprite[IDItem];
        switch (GetLanguageID())
        {
            case 0:
                item.nameItem = Translate.NameRewardsChina[IDItem];
                break;
            case 1:
                item.nameItem = Translate.NameRewardsDanish[IDItem];
                break;
            case 2:
                item.nameItem = Translate.NameRewardsDutch[IDItem];
                break;
            case 3:
                item.nameItem = Translate.NameRewardsEng[IDItem];
                break;
            case 4:
                item.nameItem = Translate.NameRewardsFinnish[IDItem];
                break;
            case 5:
                item.nameItem = Translate.NameRewardsFrench[IDItem];
                break;
            case 6:
                item.nameItem = Translate.NameRewardsGerman[IDItem];
                break;
            case 7:
                item.nameItem = Translate.NameRewardsItalian[IDItem];
                break;
            case 8:
                item.nameItem = Translate.NameRewardsNorwegian[IDItem];
                break;
            case 9:
                item.nameItem = Translate.NameRewardsPortuguese[IDItem];
                break;
            case 10:
                item.nameItem = Translate.NameRewardsRU[IDItem];
                break;
            case 11:
                item.nameItem = Translate.NameRewardsSpanishSpain[IDItem];
                break;
            case 12:
                item.nameItem = Translate.NameRewardsSwedish[IDItem];
                break;

        }
        playerInventory.items.Add(item);
        if(isLoad && isInventoryLoad)
         dataPlayer.IDRewardsInInventory.Add(IDItem);
    }
    private void UpdateNamesItemsInventoryLanguage()
    {
        for(int i = 0; i < dataPlayer.IDRewardsInInventory.Count; i++)
        {
            switch (GetLanguageID())
            {
                case 0:
                    playerInventory.items[i].nameItem = Translate.NameRewardsChina[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 1:
                    playerInventory.items[i].nameItem = Translate.NameRewardsDanish[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 2:
                    playerInventory.items[i].nameItem = Translate.NameRewardsDutch[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 3:
                    playerInventory.items[i].nameItem = Translate.NameRewardsEng[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 4:
                    playerInventory.items[i].nameItem = Translate.NameRewardsFinnish[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 5:
                    playerInventory.items[i].nameItem = Translate.NameRewardsFrench[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 6:
                    playerInventory.items[i].nameItem = Translate.NameRewardsGerman[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 7:
                    playerInventory.items[i].nameItem = Translate.NameRewardsItalian[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 8:
                    playerInventory.items[i].nameItem = Translate.NameRewardsNorwegian[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 9:
                    playerInventory.items[i].nameItem = Translate.NameRewardsPortuguese[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 10:
                    playerInventory.items[i].nameItem = Translate.NameRewardsRU[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 11:
                    playerInventory.items[i].nameItem = Translate.NameRewardsSpanishSpain[dataPlayer.IDRewardsInInventory[i]];
                    break;
                case 12:
                    playerInventory.items[i].nameItem = Translate.NameRewardsSwedish[dataPlayer.IDRewardsInInventory[i]];
                    break;
            }
        }
    }
    internal GameObject GetBackgroundMusicObject () { return BackgroundMusic; }
    void LoadInventoryRewards()
    {
        for(int i = 0; i < dataPlayer.IDRewardsInInventory.Count; i++)
            AddItemInInventory(dataPlayer.IDRewardsInInventory[i]);
    }
    void LoadBackgroundSelected()
    {
            BackgroundMusic.GetComponent<Image>().sprite = RewardsSprite[dataPlayer.currentIDSelectedBackground];
    }
    public void RefreshRewardsListInventory()
    {
        ClearScrollViewList();
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().ClearList();
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            GameObject newItem = Instantiate(InventoryItemSchblon);
            newItem.GetComponent<ChangedBackgroundGame>().SetIDBackground(playerInventory.items[i].ID);
            newItem.GetComponent<ChangedBackgroundGame>().ParentWorkshopUncheker = ContentInventory;
            newItem.transform.SetParent(ContentInventory);
            newItem.transform.localScale = new Vector3(1, 1, 1);
            newItem.transform.GetChild(0).GetComponent<Text>().text = newItem.GetComponent<ChangedBackgroundGame>().nameItem = playerInventory.items[i].nameItem;
            if (playerInventory.items[i].Image != null)
                newItem.transform.GetComponent<Image>().sprite = playerInventory.items[i].Image;
            else if (playerInventory.items[i].runtimeAnimatorGIF != null)
                newItem.transform.GetComponent<Animator>().runtimeAnimatorController = playerInventory.items[i].runtimeAnimatorGIF;
        }
        ContentInventory.GetComponent<WorkshopSelectedItemsUnCheker>().AddedListItems();
    }

    private void ClearScrollViewList()
    {
        for (int i = 0; i < ContentInventory.childCount; i++)
            Destroy(ContentInventory.GetChild(i).gameObject);
    }

    internal void SetCurrentIDBackground(int IDSelectedBackground)
    {
        dataPlayer.currentIDSelectedBackground = IDSelectedBackground;
        dataPlayer.PathToBackgroundMod = "";
    }
    internal void SetCurrentIDBackground(string pathToBackgroundMod)
    {
        dataPlayer.PathToBackgroundMod = pathToBackgroundMod;
        dataPlayer.currentIDSelectedBackground = -1;
    }
    internal void MoneyChanged(float current, char thisCount)
    {
        switch (thisCount)
        {
            case '+':
                if (dataPlayer != null)
                    dataPlayer.MoneyPlayer += current;
                break;

            case '-':
                if (dataPlayer != null)
                    dataPlayer.MoneyPlayer -= current;
                break;

            case '=':
                if (dataPlayer != null)
                    dataPlayer.MoneyPlayer = current;
                break;
        }
    }
    internal float GetMoneyPlayer() { return dataPlayer.MoneyPlayer; }
    internal int GetRewardsSpriteCount() { return RewardsSprite.Length; }
    internal Sprite[] GetRewardsImages() { return RewardsSprite; }
    internal void RecordScoreChanged(float currentRecord)
    {
        dataPlayer.RecordScorePlayer = currentRecord;
    }
    internal void RecordTimerSurviveChanged(float currentRecord)
    {
        dataPlayer.RecordTimerSurvive = currentRecord;
    }
    internal void RecordComboChanged(int ComboChanged)
    {
        dataPlayer.RecordComboPlayer = ComboChanged;
    }
    void LvLBleedingChanged(int IDBleeding)
    {
        if (dataPlayer != null)
        {
            dataPlayer.LvLsBleeding[IDBleeding]++;
            ChangeBleedingUI();
        }
            
    }
    public void BuyButton(int IDBleeding)
    {
        switch (IDBleeding)
        {
            case 0:
                int currentLvLBombSpawn = dataPlayer.LvLsBleeding[IDBombSpawnLvL];
                switch (currentLvLBombSpawn)
                {
                    case 1:
                        dataPlayer.MoneyPlayer -= BombSpawnLvL2;
                        ChangedValueMoneyInServer();
                        break;
                    case 2:
                        dataPlayer.MoneyPlayer -= BombSpawnLvL3;
                        ChangedValueMoneyInServer();
                        break;
                    case 3:
                        dataPlayer.MoneyPlayer -= BombSpawnLvL4;
                        ChangedValueMoneyInServer();
                        break;
                    case 4:
                        dataPlayer.MoneyPlayer -= BombSpawnLvL5;
                        ChangedValueMoneyInServer();
                        break;
                }
                break;
            case 1:
                int currentLvLMagnitSpawn = dataPlayer.LvLsBleeding[IDMagnitSpawnLvL];
                switch (currentLvLMagnitSpawn)
                {
                    case 1:
                        dataPlayer.MoneyPlayer -= MagnitSpawnLvL2;
                        ChangedValueMoneyInServer();
                        break;
                    case 2:
                        dataPlayer.MoneyPlayer -= MagnitSpawnLvL3;
                        ChangedValueMoneyInServer();
                        break;
                    case 3:
                        dataPlayer.MoneyPlayer -= MagnitSpawnLvL4;
                        ChangedValueMoneyInServer();
                        break;
                    case 4:
                        dataPlayer.MoneyPlayer -= MagnitSpawnLvL5;
                        ChangedValueMoneyInServer();
                        break;
                }
                break;
            case 2:
                int currentLvLMoneySpawn = dataPlayer.LvLsBleeding[IDMoneySpawnLvL];
                switch (currentLvLMoneySpawn)
                {
                    case 1:
                        dataPlayer.MoneyPlayer -= MoneySpawnLvL2;
                        ChangedValueMoneyInServer();
                        break;
                    case 2:
                        dataPlayer.MoneyPlayer -= MoneySpawnLvL3;
                        ChangedValueMoneyInServer();
                        break;
                    case 3:
                        dataPlayer.MoneyPlayer -= MoneySpawnLvL4;
                        ChangedValueMoneyInServer();
                        break;
                    case 4:
                        dataPlayer.MoneyPlayer -= MoneySpawnLvL5;
                        ChangedValueMoneyInServer();
                        break;
                }
                break;
            case 3:
                dataPlayer.MoneyPlayer -= BulletBuy;
                ChangedValueMoneyInServer();
                if(GetMaxBulletsAWP() >= InAWPBulletsCurrentMax && GetCurrentBulletsAWP() < InAWPBulletsCurrentMax)
                {
                    SetCurrentBulletsAWP(InAWPBulletsCurrentMax,'=');
                    SetMaxBulletsAWP(InAWPBulletsCurrentMax,'-');
                    SetMaxBulletsAWP(1, '+');
                }
                else
                    SetMaxBulletsAWP(1, '+');
                ChangeBleedingUI();
                break;
        }
        if(IDBleeding != 3)
        LvLBleedingChanged(IDBleeding);
    }

    private void ChangedValueMoneyInServer()
    {
        serverManager.SetPlayerValueMoney(dataPlayer.IDPlayer, dataPlayer.MoneyPlayer,false);
    }

    internal void SetNickNamePlayer(string newNickName)
    {
        dataPlayer.NickNamePlayer = newNickName;
        List<Profile> profiles = serverManager.GetPlayersList();
        while (SearchUserInDataBase(dataPlayer.IDPlayer,profiles) != null)
            IDPlayerGenerated();
        serverManager.AddPlayer(dataPlayer.IDPlayer, dataPlayer.NickNamePlayer);
    }
    internal string GetNickNamePlayer()
    {
        return dataPlayer.NickNamePlayer;
    }

    private void IDPlayerGenerated()
    {
        dataPlayer.IDPlayer = UnityEngine.Random.Range(minIDPlayer, maxIDPlayer);
    }
    internal int GetIDPlayer() { return dataPlayer.IDPlayer; }

    internal bool GetAdsPlayer() { return dataPlayer.Ads; }

    internal void SetAdsPlayer(bool changed)
    {
       dataPlayer.Ads = changed;
    }

    internal float GetRecordScorePlayer() { return dataPlayer.RecordScorePlayer; }

    internal int GetRecordComboPlayer() { return dataPlayer.RecordComboPlayer; }

    internal float GetTimeSurivePlayer() { return dataPlayer.RecordTimerSurvive; }
    private Profile SearchUserInDataBase(int IDUser,List<Profile> profiles)
    {
        for(int i = 0; i < profiles.Count; i++)
        {
            if (profiles[i].IDPlayer == IDUser)
                return profiles[i];
        }
        return null;
    }
    internal int GetLanguageID() { return dataPlayer.CurrentIDLanguage; }
    internal bool GetTraningMode() { return dataPlayer.isTraningMode; }
    internal void SetTraningMode(bool changed)
    {
        dataPlayer.isTraningMode = changed;
    }
    internal void SetTimerSurvive(float current)
    {
        dataPlayer.RecordTimerSurvive = current;
    }
    public void LanguageIDNext()
    {
        if (dataPlayer.CurrentIDLanguage < MaxCountLanguages)
            dataPlayer.CurrentIDLanguage++;
        else
            dataPlayer.CurrentIDLanguage = 0;
        ChangeLanguage();
        ChangeBleedingUI();
        if (playerInventory.items.Count > 0)
            UpdateNamesItemsInventoryLanguage();
    }
    internal void SetLanguageID(int IDLanguage)
    {
        dataPlayer.CurrentIDLanguage = IDLanguage;
        ChangeLanguage();
        ChangeBleedingUI();
    }

    public void SelectedGameModeUpdateBleedingUI()
    {
        ChangeLanguage();
        ChangeBleedingUI();
        if(playerInventory.items.Count > 0)
            UpdateNamesItemsInventoryLanguage();
    }
    void ChangeLanguage()
    {
        CurrentLanguageImgCountry.sprite = LanguagesSprite[dataPlayer.CurrentIDLanguage];

        switch (dataPlayer.CurrentIDLanguage)
        {
            case 0:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicChina = new Dropdown.OptionData(Translate.NameTextsChina[71]);
                Dropdown.OptionData optionDataBackgroundChina = new Dropdown.OptionData(Translate.NameTextsChina[72]);
                Dropdown.OptionData optionDataSkinAWPChina = new Dropdown.OptionData(Translate.NameTextsChina[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicChina);
                dropdownlistWorkshop.options.Add(optionDataBackgroundChina);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPChina);
                TextsInGame[0].text = Translate.NameTextsChina[0];
                TextsInGame[1].text = Translate.NameTextsChina[1];
                TextsInGame[2].text = Translate.NameTextsChina[2];
                TextsInGame[3].text = Translate.NameTextsChina[3];
                TextsInGame[4].text = Translate.NameTextsChina[4];
                TextsInGame[5].text = Translate.NameTextsChina[5];
                TextsInGame[6].text = Translate.NameTextsChina[6];
                TextsInGame[7].text = Translate.NameTextsChina[7];
                TextsInGame[8].text = Translate.NameTextsChina[8];
                TextsInGame[9].text = Translate.NameTextsChina[9];
                TextsInGame[10].text = Translate.NameTextsChina[10];
                TextsInGame[11].text = Translate.NameTextsChina[11];
                TextsInGame[12].text = Translate.NameTextsChina[14];
                TextsInGame[13].text = Translate.NameTextsChina[12];
                TextsInGame[14].text = Translate.NameTextsChina[11];
                TextsInGame[15].text = Translate.NameTextsChina[14];
                TextsInGame[16].text = Translate.NameTextsChina[13];
                TextsInGame[17].text = Translate.NameTextsChina[11];
                TextsInGame[18].text = Translate.NameTextsChina[14];
                TextsInGame[19].text = Translate.NameTextsChina[16];
                TextsInGame[20].text = Translate.NameTextsChina[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsChina[18];
                TextsInGame[22].text = Translate.NameTextsChina[27];
                TextsInGame[23].text = Translate.NameTextsChina[28];
                TextsInGame[24].text = Translate.NameTextsChina[29];
                TextsInGame[25].text = Translate.NameTextsChina[30];
                TextsInGame[26].text = Translate.NameTextsChina[31];
                TextsInGame[27].text = Translate.NameTextsChina[32];
                TextsInGame[28].text = Translate.NameTextsChina[33];
                TextsInGame[29].text = Translate.NameTextsChina[34];
                TextsInGame[30].text = Translate.NameTextsChina[35];
                TextsInGame[31].text = Translate.NameTextsChina[36];
                TextsInGame[32].text = Translate.NameTextsChina[37];
                TextsInGame[33].text = Translate.NameTextsChina[38];
                TextsInGame[34].text = Translate.NameTextsChina[39];
                TextsInGame[35].text = Translate.NameTextsChina[39];
                TextsInGame[36].text = Translate.NameTextsChina[40];
                TextsInGame[37].text = Translate.NameTextsChina[49];
                TextsInGame[38].text = Translate.NameTextsChina[50];
                TextsInGame[39].text = Translate.NameTextsChina[51];
                TextsInGame[40].text = Translate.NameTextsChina[52];
                TextsInGame[41].text = Translate.NameTextsChina[53];
                TextsInGame[43].text = Translate.NameTextsChina[55];
                TextsInGame[44].text = Translate.NameTextsChina[56];
                TextsInGame[45].text = Translate.NameTextsChina[57];
                TextsInGame[46].text = Translate.NameTextsChina[58];
                TextsInGame[47].text = Translate.NameTextsChina[59];
                TextsInGame[48].text = Translate.NameTextsChina[60];
                TextsInGame[49].text = Translate.NameTextsChina[61];
                TextsInGame[50].text = Translate.NameTextsChina[62];
                TextsInGame[51].text = Translate.NameTextsChina[63];
                TextsInGame[52].text = Translate.NameTextsChina[63];
                TextsInGame[53].text = Translate.NameTextsChina[63];

                TextsInGame[54].text = Translate.NameTextsChina[35];
                TextsInGame[55].text = Translate.NameTextsChina[72];
                TextsInGame[56].text = Translate.NameTextsChina[69];
                TextsInGame[57].text = Translate.NameTextsChina[93];
                TextsInGame[58].text = Translate.NameTextsChina[67];
                TextsInGame[59].text = Translate.NameTextsChina[71];
                TextsInGame[60].text = Translate.NameTextsChina[72];
                TextsInGame[61].text = Translate.NameTextsChina[69];
                TextsInGame[62].text = Translate.NameTextsChina[68];
                TextsInGame[63].text = Translate.NameTextsChina[74];
                TextsInGame[64].text = Translate.NameTextsChina[75];
                TextsInGame[65].text = Translate.NameTextsChina[76];
                TextsInGame[66].text = Translate.NameTextsChina[77];
                TextsInGame[67].text = Translate.NameTextsChina[78];
                TextsInGame[68].text = Translate.NameTextsChina[79];
                TextsInGame[69].text = Translate.NameTextsChina[79];
                TextsInGame[70].text = Translate.NameTextsChina[80];
                TextsInGame[71].text = Translate.NameTextsChina[70];
                TextsInGame[72].text = Translate.NameTextsChina[97];
                TextsInGame[73].text = Translate.NameTextsChina[91];
                TextsInGame[74].text = Translate.NameTextsChina[90];
                TextsInGame[75].text = Translate.NameTextsChina[99];
                TextsInGame[76].text = Translate.NameTextsChina[100];
                break;
            case 1:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicDanish = new Dropdown.OptionData(Translate.NameTextsDanish[71]);
                Dropdown.OptionData optionDataBackgroundDanish = new Dropdown.OptionData(Translate.NameTextsDanish[72]);
                Dropdown.OptionData optionDataSkinAWPDanish = new Dropdown.OptionData(Translate.NameTextsDanish[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicDanish);
                dropdownlistWorkshop.options.Add(optionDataBackgroundDanish);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPDanish);
                TextsInGame[0].text = Translate.NameTextsDanish[0];
                TextsInGame[1].text = Translate.NameTextsDanish[1];
                TextsInGame[2].text = Translate.NameTextsDanish[2];
                TextsInGame[3].text = Translate.NameTextsDanish[3];
                TextsInGame[4].text = Translate.NameTextsDanish[4];
                TextsInGame[5].text = Translate.NameTextsDanish[5];
                TextsInGame[6].text = Translate.NameTextsDanish[6];
                TextsInGame[7].text = Translate.NameTextsDanish[7];
                TextsInGame[8].text = Translate.NameTextsDanish[8];
                TextsInGame[9].text = Translate.NameTextsDanish[9];
                TextsInGame[10].text = Translate.NameTextsDanish[10];
                TextsInGame[11].text = Translate.NameTextsDanish[11];
                TextsInGame[12].text = Translate.NameTextsDanish[14];
                TextsInGame[13].text = Translate.NameTextsDanish[12];
                TextsInGame[14].text = Translate.NameTextsDanish[11];
                TextsInGame[15].text = Translate.NameTextsDanish[14];
                TextsInGame[16].text = Translate.NameTextsDanish[13];
                TextsInGame[17].text = Translate.NameTextsDanish[11];
                TextsInGame[18].text = Translate.NameTextsDanish[14];
                TextsInGame[19].text = Translate.NameTextsDanish[16];
                TextsInGame[20].text = Translate.NameTextsDanish[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsDanish[18];
                TextsInGame[22].text = Translate.NameTextsDanish[27];
                TextsInGame[23].text = Translate.NameTextsDanish[28];
                TextsInGame[24].text = Translate.NameTextsDanish[29];
                TextsInGame[25].text = Translate.NameTextsDanish[30];
                TextsInGame[26].text = Translate.NameTextsDanish[31];
                TextsInGame[27].text = Translate.NameTextsDanish[32];
                TextsInGame[28].text = Translate.NameTextsDanish[33];
                TextsInGame[29].text = Translate.NameTextsDanish[34];
                TextsInGame[30].text = Translate.NameTextsDanish[35];
                TextsInGame[31].text = Translate.NameTextsDanish[36];
                TextsInGame[32].text = Translate.NameTextsDanish[37];
                TextsInGame[33].text = Translate.NameTextsDanish[38];
                TextsInGame[34].text = Translate.NameTextsDanish[39];
                TextsInGame[35].text = Translate.NameTextsDanish[39];
                TextsInGame[36].text = Translate.NameTextsDanish[40];
                TextsInGame[37].text = Translate.NameTextsDanish[49];
                TextsInGame[38].text = Translate.NameTextsDanish[50];
                TextsInGame[39].text = Translate.NameTextsDanish[51];
                TextsInGame[40].text = Translate.NameTextsDanish[52];
                TextsInGame[41].text = Translate.NameTextsDanish[53];
                TextsInGame[43].text = Translate.NameTextsDanish[55];
                TextsInGame[44].text = Translate.NameTextsDanish[56];
                TextsInGame[45].text = Translate.NameTextsDanish[57];
                TextsInGame[46].text = Translate.NameTextsDanish[58];
                TextsInGame[47].text = Translate.NameTextsDanish[59];
                TextsInGame[48].text = Translate.NameTextsDanish[60];
                TextsInGame[49].text = Translate.NameTextsDanish[61];
                TextsInGame[50].text = Translate.NameTextsDanish[62];
                TextsInGame[51].text = Translate.NameTextsDanish[63];
                TextsInGame[52].text = Translate.NameTextsDanish[63];
                TextsInGame[53].text = Translate.NameTextsDanish[63];

                TextsInGame[54].text = Translate.NameTextsDanish[35];
                TextsInGame[55].text = Translate.NameTextsDanish[72];
                TextsInGame[56].text = Translate.NameTextsDanish[69];
                TextsInGame[57].text = Translate.NameTextsDanish[93];
                TextsInGame[58].text = Translate.NameTextsDanish[67];
                TextsInGame[59].text = Translate.NameTextsDanish[71];
                TextsInGame[60].text = Translate.NameTextsDanish[72];
                TextsInGame[61].text = Translate.NameTextsDanish[69];
                TextsInGame[62].text = Translate.NameTextsDanish[68];
                TextsInGame[63].text = Translate.NameTextsDanish[74];
                TextsInGame[64].text = Translate.NameTextsDanish[75];
                TextsInGame[65].text = Translate.NameTextsDanish[76];
                TextsInGame[66].text = Translate.NameTextsDanish[77];
                TextsInGame[67].text = Translate.NameTextsDanish[78];
                TextsInGame[68].text = Translate.NameTextsDanish[79];
                TextsInGame[69].text = Translate.NameTextsDanish[79];
                TextsInGame[70].text = Translate.NameTextsDanish[80];
                TextsInGame[71].text = Translate.NameTextsDanish[70];
                TextsInGame[72].text = Translate.NameTextsDanish[97];
                TextsInGame[73].text = Translate.NameTextsDanish[91];
                TextsInGame[74].text = Translate.NameTextsDanish[90];
                TextsInGame[75].text = Translate.NameTextsDanish[99];
                TextsInGame[76].text = Translate.NameTextsDanish[100];
                break;
            case 2:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicDutch = new Dropdown.OptionData(Translate.NameTextsDutch[71]);
                Dropdown.OptionData optionDataBackgroundDutch = new Dropdown.OptionData(Translate.NameTextsDutch[72]);
                Dropdown.OptionData optionDataSkinAWPDutch = new Dropdown.OptionData(Translate.NameTextsDutch[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicDutch);
                dropdownlistWorkshop.options.Add(optionDataBackgroundDutch);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPDutch);
                TextsInGame[0].text = Translate.NameTextsDutch[0];
                TextsInGame[1].text = Translate.NameTextsDutch[1];
                TextsInGame[2].text = Translate.NameTextsDutch[2];
                TextsInGame[3].text = Translate.NameTextsDutch[3];
                TextsInGame[4].text = Translate.NameTextsDutch[4];
                TextsInGame[5].text = Translate.NameTextsDutch[5];
                TextsInGame[6].text = Translate.NameTextsDutch[6];
                TextsInGame[7].text = Translate.NameTextsDutch[7];
                TextsInGame[8].text = Translate.NameTextsDutch[8];
                TextsInGame[9].text = Translate.NameTextsDutch[9];
                TextsInGame[10].text = Translate.NameTextsDutch[10];
                TextsInGame[11].text = Translate.NameTextsDutch[11];
                TextsInGame[12].text = Translate.NameTextsDutch[14];
                TextsInGame[13].text = Translate.NameTextsDutch[12];
                TextsInGame[14].text = Translate.NameTextsDutch[11];
                TextsInGame[15].text = Translate.NameTextsDutch[14];
                TextsInGame[16].text = Translate.NameTextsDutch[13];
                TextsInGame[17].text = Translate.NameTextsDutch[11];
                TextsInGame[18].text = Translate.NameTextsDutch[14];
                TextsInGame[19].text = Translate.NameTextsDutch[16];
                TextsInGame[20].text = Translate.NameTextsDutch[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsDutch[18];
                TextsInGame[22].text = Translate.NameTextsDutch[27];
                TextsInGame[23].text = Translate.NameTextsDutch[28];
                TextsInGame[24].text = Translate.NameTextsDutch[29];
                TextsInGame[25].text = Translate.NameTextsDutch[30];
                TextsInGame[26].text = Translate.NameTextsDutch[31];
                TextsInGame[27].text = Translate.NameTextsDutch[32];
                TextsInGame[28].text = Translate.NameTextsDutch[33];
                TextsInGame[29].text = Translate.NameTextsDutch[34];
                TextsInGame[30].text = Translate.NameTextsDutch[35];
                TextsInGame[31].text = Translate.NameTextsDutch[36];
                TextsInGame[32].text = Translate.NameTextsDutch[37];
                TextsInGame[33].text = Translate.NameTextsDutch[38];
                TextsInGame[34].text = Translate.NameTextsDutch[39];
                TextsInGame[35].text = Translate.NameTextsDutch[39];
                TextsInGame[36].text = Translate.NameTextsDutch[40];
                TextsInGame[37].text = Translate.NameTextsDutch[49];
                TextsInGame[38].text = Translate.NameTextsDutch[50];
                TextsInGame[39].text = Translate.NameTextsDutch[51];
                TextsInGame[40].text = Translate.NameTextsDutch[52];
                TextsInGame[41].text = Translate.NameTextsDutch[53];
                TextsInGame[43].text = Translate.NameTextsDutch[55];
                TextsInGame[44].text = Translate.NameTextsDutch[56];
                TextsInGame[45].text = Translate.NameTextsDutch[57];
                TextsInGame[46].text = Translate.NameTextsDutch[58];
                TextsInGame[47].text = Translate.NameTextsDutch[59];
                TextsInGame[48].text = Translate.NameTextsDutch[60];
                TextsInGame[49].text = Translate.NameTextsDutch[61];
                TextsInGame[50].text = Translate.NameTextsDutch[62];
                TextsInGame[51].text = Translate.NameTextsDutch[63];
                TextsInGame[52].text = Translate.NameTextsDutch[63];
                TextsInGame[53].text = Translate.NameTextsDutch[63];

                TextsInGame[54].text = Translate.NameTextsDutch[35];
                TextsInGame[55].text = Translate.NameTextsDutch[72];
                TextsInGame[56].text = Translate.NameTextsDutch[69];
                TextsInGame[57].text = Translate.NameTextsDutch[93];
                TextsInGame[58].text = Translate.NameTextsDutch[67];
                TextsInGame[59].text = Translate.NameTextsDutch[71];
                TextsInGame[60].text = Translate.NameTextsDutch[72];
                TextsInGame[61].text = Translate.NameTextsDutch[69];
                TextsInGame[62].text = Translate.NameTextsDutch[68];
                TextsInGame[63].text = Translate.NameTextsDutch[74];
                TextsInGame[64].text = Translate.NameTextsDutch[75];
                TextsInGame[65].text = Translate.NameTextsDutch[76];
                TextsInGame[66].text = Translate.NameTextsDutch[77];
                TextsInGame[67].text = Translate.NameTextsDutch[78];
                TextsInGame[68].text = Translate.NameTextsDutch[79];
                TextsInGame[69].text = Translate.NameTextsDutch[79];
                TextsInGame[70].text = Translate.NameTextsDutch[80];
                TextsInGame[71].text = Translate.NameTextsDutch[70];
                TextsInGame[72].text = Translate.NameTextsDutch[97];
                TextsInGame[73].text = Translate.NameTextsDutch[91];
                TextsInGame[74].text = Translate.NameTextsDutch[90];
                TextsInGame[75].text = Translate.NameTextsDutch[99];
                TextsInGame[76].text = Translate.NameTextsDutch[100];
                break;
            case 3:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicEng = new Dropdown.OptionData(Translate.NameTextsEng[71]);
                Dropdown.OptionData optionDataBackgroundEng = new Dropdown.OptionData(Translate.NameTextsEng[72]);
                Dropdown.OptionData optionDataSkinAWPEng = new Dropdown.OptionData(Translate.NameTextsEng[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicEng);
                dropdownlistWorkshop.options.Add(optionDataBackgroundEng);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPEng);
                TextsInGame[0].text = Translate.NameTextsEng[0];
                TextsInGame[1].text = Translate.NameTextsEng[1];
                TextsInGame[2].text = Translate.NameTextsEng[2];
                TextsInGame[3].text = Translate.NameTextsEng[3];
                TextsInGame[4].text = Translate.NameTextsEng[4];
                TextsInGame[5].text = Translate.NameTextsEng[5];
                TextsInGame[6].text = Translate.NameTextsEng[6];
                TextsInGame[7].text = Translate.NameTextsEng[7];
                TextsInGame[8].text = Translate.NameTextsEng[8];
                TextsInGame[9].text = Translate.NameTextsEng[9];
                TextsInGame[10].text = Translate.NameTextsEng[10];
                TextsInGame[11].text = Translate.NameTextsEng[11];
                TextsInGame[12].text = Translate.NameTextsEng[14];
                TextsInGame[13].text = Translate.NameTextsEng[12];
                TextsInGame[14].text = Translate.NameTextsEng[11];
                TextsInGame[15].text = Translate.NameTextsEng[14];
                TextsInGame[16].text = Translate.NameTextsEng[13];
                TextsInGame[17].text = Translate.NameTextsEng[11];
                TextsInGame[18].text = Translate.NameTextsEng[14];
                TextsInGame[19].text = Translate.NameTextsEng[16];
                TextsInGame[20].text = Translate.NameTextsEng[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsEng[18];
                TextsInGame[22].text = Translate.NameTextsEng[27];
                TextsInGame[23].text = Translate.NameTextsEng[28];
                TextsInGame[24].text = Translate.NameTextsEng[29];
                TextsInGame[25].text = Translate.NameTextsEng[30];
                TextsInGame[26].text = Translate.NameTextsEng[31];
                TextsInGame[27].text = Translate.NameTextsEng[32];
                TextsInGame[28].text = Translate.NameTextsEng[33];
                TextsInGame[29].text = Translate.NameTextsEng[34];
                TextsInGame[30].text = Translate.NameTextsEng[35];
                TextsInGame[31].text = Translate.NameTextsEng[36];
                TextsInGame[32].text = Translate.NameTextsEng[37];
                TextsInGame[33].text = Translate.NameTextsEng[38];
                TextsInGame[34].text = Translate.NameTextsEng[39];
                TextsInGame[35].text = Translate.NameTextsEng[39];
                TextsInGame[36].text = Translate.NameTextsEng[40];
                TextsInGame[37].text = Translate.NameTextsEng[49];
                TextsInGame[38].text = Translate.NameTextsEng[50];
                TextsInGame[39].text = Translate.NameTextsEng[51];
                TextsInGame[40].text = Translate.NameTextsEng[52];
                TextsInGame[41].text = Translate.NameTextsEng[53];
                TextsInGame[43].text = Translate.NameTextsEng[55];
                TextsInGame[44].text = Translate.NameTextsEng[56];
                TextsInGame[45].text = Translate.NameTextsEng[57];
                TextsInGame[46].text = Translate.NameTextsEng[58];
                TextsInGame[47].text = Translate.NameTextsEng[59];
                TextsInGame[48].text = Translate.NameTextsEng[60];
                TextsInGame[49].text = Translate.NameTextsEng[61];
                TextsInGame[50].text = Translate.NameTextsEng[62];
                TextsInGame[51].text = Translate.NameTextsEng[63];
                TextsInGame[52].text = Translate.NameTextsEng[63];
                TextsInGame[53].text = Translate.NameTextsEng[63];

                TextsInGame[54].text = Translate.NameTextsEng[35];
                TextsInGame[55].text = Translate.NameTextsEng[72];
                TextsInGame[56].text = Translate.NameTextsEng[69];
                TextsInGame[57].text = Translate.NameTextsEng[93];
                TextsInGame[58].text = Translate.NameTextsEng[67];
                TextsInGame[59].text = Translate.NameTextsEng[71];
                TextsInGame[60].text = Translate.NameTextsEng[72];
                TextsInGame[61].text = Translate.NameTextsEng[69];
                TextsInGame[62].text = Translate.NameTextsEng[68];
                TextsInGame[63].text = Translate.NameTextsEng[74];
                TextsInGame[64].text = Translate.NameTextsEng[75];
                TextsInGame[65].text = Translate.NameTextsEng[76];
                TextsInGame[66].text = Translate.NameTextsEng[77];
                TextsInGame[67].text = Translate.NameTextsEng[78];
                TextsInGame[68].text = Translate.NameTextsEng[79];
                TextsInGame[69].text = Translate.NameTextsEng[79];
                TextsInGame[70].text = Translate.NameTextsEng[80];
                TextsInGame[71].text = Translate.NameTextsEng[70];
                TextsInGame[72].text = Translate.NameTextsEng[97];
                TextsInGame[73].text = Translate.NameTextsEng[91];
                TextsInGame[74].text = Translate.NameTextsEng[90];
                TextsInGame[75].text = Translate.NameTextsEng[99];
                TextsInGame[76].text = Translate.NameTextsEng[100];
                break;
            case 4:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicFinnish = new Dropdown.OptionData(Translate.NameTextsFinnish[71]);
                Dropdown.OptionData optionDataBackgroundFinnish = new Dropdown.OptionData(Translate.NameTextsFinnish[72]);
                Dropdown.OptionData optionDataSkinAWPFinnish = new Dropdown.OptionData(Translate.NameTextsFinnish[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicFinnish);
                dropdownlistWorkshop.options.Add(optionDataBackgroundFinnish);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPFinnish);
                TextsInGame[0].text = Translate.NameTextsFinnish[0];
                TextsInGame[1].text = Translate.NameTextsFinnish[1];
                TextsInGame[2].text = Translate.NameTextsFinnish[2];
                TextsInGame[3].text = Translate.NameTextsFinnish[3];
                TextsInGame[4].text = Translate.NameTextsFinnish[4];
                TextsInGame[5].text = Translate.NameTextsFinnish[5];
                TextsInGame[6].text = Translate.NameTextsFinnish[6];
                TextsInGame[7].text = Translate.NameTextsFinnish[7];
                TextsInGame[8].text = Translate.NameTextsFinnish[8];
                TextsInGame[9].text = Translate.NameTextsFinnish[9];
                TextsInGame[10].text = Translate.NameTextsFinnish[10];
                TextsInGame[11].text = Translate.NameTextsFinnish[11];
                TextsInGame[12].text = Translate.NameTextsFinnish[14];
                TextsInGame[13].text = Translate.NameTextsFinnish[12];
                TextsInGame[14].text = Translate.NameTextsFinnish[11];
                TextsInGame[15].text = Translate.NameTextsFinnish[14];
                TextsInGame[16].text = Translate.NameTextsFinnish[13];
                TextsInGame[17].text = Translate.NameTextsFinnish[11];
                TextsInGame[18].text = Translate.NameTextsFinnish[14];
                TextsInGame[19].text = Translate.NameTextsFinnish[16];
                TextsInGame[20].text = Translate.NameTextsFinnish[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsFinnish[18];
                TextsInGame[22].text = Translate.NameTextsFinnish[27];
                TextsInGame[23].text = Translate.NameTextsFinnish[28];
                TextsInGame[24].text = Translate.NameTextsFinnish[29];
                TextsInGame[25].text = Translate.NameTextsFinnish[30];
                TextsInGame[26].text = Translate.NameTextsFinnish[31];
                TextsInGame[27].text = Translate.NameTextsFinnish[32];
                TextsInGame[28].text = Translate.NameTextsFinnish[33];
                TextsInGame[29].text = Translate.NameTextsFinnish[34];
                TextsInGame[30].text = Translate.NameTextsFinnish[35];
                TextsInGame[31].text = Translate.NameTextsFinnish[36];
                TextsInGame[32].text = Translate.NameTextsFinnish[37];
                TextsInGame[33].text = Translate.NameTextsFinnish[38];
                TextsInGame[34].text = Translate.NameTextsFinnish[39];
                TextsInGame[35].text = Translate.NameTextsFinnish[39];
                TextsInGame[36].text = Translate.NameTextsFinnish[40];
                TextsInGame[37].text = Translate.NameTextsFinnish[49];
                TextsInGame[38].text = Translate.NameTextsFinnish[50];
                TextsInGame[39].text = Translate.NameTextsFinnish[51];
                TextsInGame[40].text = Translate.NameTextsFinnish[52];
                TextsInGame[41].text = Translate.NameTextsFinnish[53];
                TextsInGame[43].text = Translate.NameTextsFinnish[55];
                TextsInGame[44].text = Translate.NameTextsFinnish[56];
                TextsInGame[45].text = Translate.NameTextsFinnish[57];
                TextsInGame[46].text = Translate.NameTextsFinnish[58];
                TextsInGame[47].text = Translate.NameTextsFinnish[59];
                TextsInGame[48].text = Translate.NameTextsFinnish[60];
                TextsInGame[49].text = Translate.NameTextsFinnish[61];
                TextsInGame[50].text = Translate.NameTextsFinnish[62];
                TextsInGame[51].text = Translate.NameTextsFinnish[63];
                TextsInGame[52].text = Translate.NameTextsFinnish[63];
                TextsInGame[53].text = Translate.NameTextsFinnish[63];

                TextsInGame[54].text = Translate.NameTextsFinnish[35];
                TextsInGame[55].text = Translate.NameTextsFinnish[72];
                TextsInGame[56].text = Translate.NameTextsFinnish[69];
                TextsInGame[57].text = Translate.NameTextsFinnish[93];
                TextsInGame[58].text = Translate.NameTextsFinnish[67];
                TextsInGame[59].text = Translate.NameTextsFinnish[71];
                TextsInGame[60].text = Translate.NameTextsFinnish[72];
                TextsInGame[61].text = Translate.NameTextsFinnish[69];
                TextsInGame[62].text = Translate.NameTextsFinnish[68];
                TextsInGame[63].text = Translate.NameTextsFinnish[74];
                TextsInGame[64].text = Translate.NameTextsFinnish[75];
                TextsInGame[65].text = Translate.NameTextsFinnish[76];
                TextsInGame[66].text = Translate.NameTextsFinnish[77];
                TextsInGame[67].text = Translate.NameTextsFinnish[78];
                TextsInGame[68].text = Translate.NameTextsFinnish[79];
                TextsInGame[69].text = Translate.NameTextsFinnish[79];
                TextsInGame[70].text = Translate.NameTextsFinnish[80];
                TextsInGame[71].text = Translate.NameTextsFinnish[70];
                TextsInGame[72].text = Translate.NameTextsFinnish[97];
                TextsInGame[73].text = Translate.NameTextsFinnish[91];
                TextsInGame[74].text = Translate.NameTextsFinnish[90];
                TextsInGame[75].text = Translate.NameTextsFinnish[99];
                TextsInGame[76].text = Translate.NameTextsFinnish[100];
                break;
            case 5:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicFrench = new Dropdown.OptionData(Translate.NameTextsFrench[71]);
                Dropdown.OptionData optionDataBackgroundFrench = new Dropdown.OptionData(Translate.NameTextsFrench[72]);
                Dropdown.OptionData optionDataSkinAWPFrench = new Dropdown.OptionData(Translate.NameTextsFrench[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicFrench);
                dropdownlistWorkshop.options.Add(optionDataBackgroundFrench);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPFrench);
                TextsInGame[0].text = Translate.NameTextsFrench[0];
                TextsInGame[1].text = Translate.NameTextsFrench[1];
                TextsInGame[2].text = Translate.NameTextsFrench[2];
                TextsInGame[3].text = Translate.NameTextsFrench[3];
                TextsInGame[4].text = Translate.NameTextsFrench[4];
                TextsInGame[5].text = Translate.NameTextsFrench[5];
                TextsInGame[6].text = Translate.NameTextsFrench[6];
                TextsInGame[7].text = Translate.NameTextsFrench[7];
                TextsInGame[8].text = Translate.NameTextsFrench[8];
                TextsInGame[9].text = Translate.NameTextsFrench[9];
                TextsInGame[10].text = Translate.NameTextsFrench[10];
                TextsInGame[11].text = Translate.NameTextsFrench[11];
                TextsInGame[12].text = Translate.NameTextsFrench[14];
                TextsInGame[13].text = Translate.NameTextsFrench[12];
                TextsInGame[14].text = Translate.NameTextsFrench[11];
                TextsInGame[15].text = Translate.NameTextsFrench[14];
                TextsInGame[16].text = Translate.NameTextsFrench[13];
                TextsInGame[17].text = Translate.NameTextsFrench[11];
                TextsInGame[18].text = Translate.NameTextsFrench[14];
                TextsInGame[19].text = Translate.NameTextsFrench[16];
                TextsInGame[20].text = Translate.NameTextsFrench[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsFrench[18];
                TextsInGame[22].text = Translate.NameTextsFrench[27];
                TextsInGame[23].text = Translate.NameTextsFrench[28];
                TextsInGame[24].text = Translate.NameTextsFrench[29];
                TextsInGame[25].text = Translate.NameTextsFrench[30];
                TextsInGame[26].text = Translate.NameTextsFrench[31];
                TextsInGame[27].text = Translate.NameTextsFrench[32];
                TextsInGame[28].text = Translate.NameTextsFrench[33];
                TextsInGame[29].text = Translate.NameTextsFrench[34];
                TextsInGame[30].text = Translate.NameTextsFrench[35];
                TextsInGame[31].text = Translate.NameTextsFrench[36];
                TextsInGame[32].text = Translate.NameTextsFrench[37];
                TextsInGame[33].text = Translate.NameTextsFrench[38];
                TextsInGame[34].text = Translate.NameTextsFrench[39];
                TextsInGame[35].text = Translate.NameTextsFrench[39];
                TextsInGame[36].text = Translate.NameTextsFrench[40];
                TextsInGame[37].text = Translate.NameTextsFrench[49];
                TextsInGame[38].text = Translate.NameTextsFrench[50];
                TextsInGame[39].text = Translate.NameTextsFrench[51];
                TextsInGame[40].text = Translate.NameTextsFrench[52];
                TextsInGame[41].text = Translate.NameTextsFrench[53];
                TextsInGame[43].text = Translate.NameTextsFrench[55];
                TextsInGame[44].text = Translate.NameTextsFrench[56];
                TextsInGame[45].text = Translate.NameTextsFrench[57];
                TextsInGame[46].text = Translate.NameTextsFrench[58];
                TextsInGame[47].text = Translate.NameTextsFrench[59];
                TextsInGame[48].text = Translate.NameTextsFrench[60];
                TextsInGame[49].text = Translate.NameTextsFrench[61];
                TextsInGame[50].text = Translate.NameTextsFrench[62];
                TextsInGame[51].text = Translate.NameTextsFrench[63];
                TextsInGame[52].text = Translate.NameTextsFrench[63];
                TextsInGame[53].text = Translate.NameTextsFrench[63];

                TextsInGame[54].text = Translate.NameTextsFrench[35];
                TextsInGame[55].text = Translate.NameTextsFrench[72];
                TextsInGame[56].text = Translate.NameTextsFrench[69];
                TextsInGame[57].text = Translate.NameTextsFrench[93];
                TextsInGame[58].text = Translate.NameTextsFrench[67];
                TextsInGame[59].text = Translate.NameTextsFrench[71];
                TextsInGame[60].text = Translate.NameTextsFrench[72];
                TextsInGame[61].text = Translate.NameTextsFrench[69];
                TextsInGame[62].text = Translate.NameTextsFrench[68];
                TextsInGame[63].text = Translate.NameTextsFrench[74];
                TextsInGame[64].text = Translate.NameTextsFrench[75];
                TextsInGame[65].text = Translate.NameTextsFrench[76];
                TextsInGame[66].text = Translate.NameTextsFrench[77];
                TextsInGame[67].text = Translate.NameTextsFrench[78];
                TextsInGame[68].text = Translate.NameTextsFrench[79];
                TextsInGame[69].text = Translate.NameTextsFrench[79];
                TextsInGame[70].text = Translate.NameTextsFrench[80];
                TextsInGame[71].text = Translate.NameTextsFrench[70];
                TextsInGame[72].text = Translate.NameTextsFrench[97];
                TextsInGame[73].text = Translate.NameTextsFrench[91];
                TextsInGame[74].text = Translate.NameTextsFrench[90];
                TextsInGame[75].text = Translate.NameTextsFrench[99];
                TextsInGame[76].text = Translate.NameTextsFrench[100];
                break;
            case 6:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicGerman = new Dropdown.OptionData(Translate.NameTextsGerman[71]);
                Dropdown.OptionData optionDataBackgroundGerman = new Dropdown.OptionData(Translate.NameTextsGerman[72]);
                Dropdown.OptionData optionDataSkinAWPGerman = new Dropdown.OptionData(Translate.NameTextsGerman[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicGerman);
                dropdownlistWorkshop.options.Add(optionDataBackgroundGerman);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPGerman);
                TextsInGame[0].text = Translate.NameTextsGerman[0];
                TextsInGame[1].text = Translate.NameTextsGerman[1];
                TextsInGame[2].text = Translate.NameTextsGerman[2];
                TextsInGame[3].text = Translate.NameTextsGerman[3];
                TextsInGame[4].text = Translate.NameTextsGerman[4];
                TextsInGame[5].text = Translate.NameTextsGerman[5];
                TextsInGame[6].text = Translate.NameTextsGerman[6];
                TextsInGame[7].text = Translate.NameTextsGerman[7];
                TextsInGame[8].text = Translate.NameTextsGerman[8];
                TextsInGame[9].text = Translate.NameTextsGerman[9];
                TextsInGame[10].text = Translate.NameTextsGerman[10];
                TextsInGame[11].text = Translate.NameTextsGerman[11];
                TextsInGame[12].text = Translate.NameTextsGerman[14];
                TextsInGame[13].text = Translate.NameTextsGerman[12];
                TextsInGame[14].text = Translate.NameTextsGerman[11];
                TextsInGame[15].text = Translate.NameTextsGerman[14];
                TextsInGame[16].text = Translate.NameTextsGerman[13];
                TextsInGame[17].text = Translate.NameTextsGerman[11];
                TextsInGame[18].text = Translate.NameTextsGerman[14];
                TextsInGame[19].text = Translate.NameTextsGerman[16];
                TextsInGame[20].text = Translate.NameTextsGerman[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsGerman[18];
                TextsInGame[22].text = Translate.NameTextsGerman[27];
                TextsInGame[23].text = Translate.NameTextsGerman[28];
                TextsInGame[24].text = Translate.NameTextsGerman[29];
                TextsInGame[25].text = Translate.NameTextsGerman[30];
                TextsInGame[26].text = Translate.NameTextsGerman[31];
                TextsInGame[27].text = Translate.NameTextsGerman[32];
                TextsInGame[28].text = Translate.NameTextsGerman[33];
                TextsInGame[29].text = Translate.NameTextsGerman[34];
                TextsInGame[30].text = Translate.NameTextsGerman[35];
                TextsInGame[31].text = Translate.NameTextsGerman[36];
                TextsInGame[32].text = Translate.NameTextsGerman[37];
                TextsInGame[33].text = Translate.NameTextsGerman[38];
                TextsInGame[34].text = Translate.NameTextsGerman[39];
                TextsInGame[35].text = Translate.NameTextsGerman[39];
                TextsInGame[36].text = Translate.NameTextsGerman[40];
                TextsInGame[37].text = Translate.NameTextsGerman[49];
                TextsInGame[38].text = Translate.NameTextsGerman[50];
                TextsInGame[39].text = Translate.NameTextsGerman[51];
                TextsInGame[40].text = Translate.NameTextsGerman[52];
                TextsInGame[41].text = Translate.NameTextsGerman[53];
                TextsInGame[43].text = Translate.NameTextsGerman[55];
                TextsInGame[44].text = Translate.NameTextsGerman[56];
                TextsInGame[45].text = Translate.NameTextsGerman[57];
                TextsInGame[46].text = Translate.NameTextsGerman[58];
                TextsInGame[47].text = Translate.NameTextsGerman[59];
                TextsInGame[48].text = Translate.NameTextsGerman[60];
                TextsInGame[49].text = Translate.NameTextsGerman[61];
                TextsInGame[50].text = Translate.NameTextsGerman[62];
                TextsInGame[51].text = Translate.NameTextsGerman[63];
                TextsInGame[52].text = Translate.NameTextsGerman[63];
                TextsInGame[53].text = Translate.NameTextsGerman[63];

                TextsInGame[54].text = Translate.NameTextsGerman[35];
                TextsInGame[55].text = Translate.NameTextsGerman[72];
                TextsInGame[56].text = Translate.NameTextsGerman[69];
                TextsInGame[57].text = Translate.NameTextsGerman[93];
                TextsInGame[58].text = Translate.NameTextsGerman[67];
                TextsInGame[59].text = Translate.NameTextsGerman[71];
                TextsInGame[60].text = Translate.NameTextsGerman[72];
                TextsInGame[61].text = Translate.NameTextsGerman[69];
                TextsInGame[62].text = Translate.NameTextsGerman[68];
                TextsInGame[63].text = Translate.NameTextsGerman[74];
                TextsInGame[64].text = Translate.NameTextsGerman[75];
                TextsInGame[65].text = Translate.NameTextsGerman[76];
                TextsInGame[66].text = Translate.NameTextsGerman[77];
                TextsInGame[67].text = Translate.NameTextsGerman[78];
                TextsInGame[68].text = Translate.NameTextsGerman[79];
                TextsInGame[69].text = Translate.NameTextsGerman[79];
                TextsInGame[70].text = Translate.NameTextsGerman[80];
                TextsInGame[71].text = Translate.NameTextsGerman[70];
                TextsInGame[72].text = Translate.NameTextsGerman[97];
                TextsInGame[73].text = Translate.NameTextsGerman[91];
                TextsInGame[74].text = Translate.NameTextsGerman[90];
                TextsInGame[75].text = Translate.NameTextsGerman[99];
                TextsInGame[76].text = Translate.NameTextsGerman[100];
                break;
            case 7:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicItalian = new Dropdown.OptionData(Translate.NameTextsItalian[71]);
                Dropdown.OptionData optionDataBackgroundItalian = new Dropdown.OptionData(Translate.NameTextsItalian[72]);
                Dropdown.OptionData optionDataSkinAWPItalian = new Dropdown.OptionData(Translate.NameTextsItalian[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicItalian);
                dropdownlistWorkshop.options.Add(optionDataBackgroundItalian);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPItalian);
                TextsInGame[0].text = Translate.NameTextsItalian[0];
                TextsInGame[1].text = Translate.NameTextsItalian[1];
                TextsInGame[2].text = Translate.NameTextsItalian[2];
                TextsInGame[3].text = Translate.NameTextsItalian[3];
                TextsInGame[4].text = Translate.NameTextsItalian[4];
                TextsInGame[5].text = Translate.NameTextsItalian[5];
                TextsInGame[6].text = Translate.NameTextsItalian[6];
                TextsInGame[7].text = Translate.NameTextsItalian[7];
                TextsInGame[8].text = Translate.NameTextsItalian[8];
                TextsInGame[9].text = Translate.NameTextsItalian[9];
                TextsInGame[10].text = Translate.NameTextsItalian[10];
                TextsInGame[11].text = Translate.NameTextsItalian[11];
                TextsInGame[12].text = Translate.NameTextsItalian[14];
                TextsInGame[13].text = Translate.NameTextsItalian[12];
                TextsInGame[14].text = Translate.NameTextsItalian[11];
                TextsInGame[15].text = Translate.NameTextsItalian[14];
                TextsInGame[16].text = Translate.NameTextsItalian[13];
                TextsInGame[17].text = Translate.NameTextsItalian[11];
                TextsInGame[18].text = Translate.NameTextsItalian[14];
                TextsInGame[19].text = Translate.NameTextsItalian[16];
                TextsInGame[20].text = Translate.NameTextsItalian[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsItalian[18];
                TextsInGame[22].text = Translate.NameTextsItalian[27];
                TextsInGame[23].text = Translate.NameTextsItalian[28];
                TextsInGame[24].text = Translate.NameTextsItalian[29];
                TextsInGame[25].text = Translate.NameTextsItalian[30];
                TextsInGame[26].text = Translate.NameTextsItalian[31];
                TextsInGame[27].text = Translate.NameTextsItalian[32];
                TextsInGame[28].text = Translate.NameTextsItalian[33];
                TextsInGame[29].text = Translate.NameTextsItalian[34];
                TextsInGame[30].text = Translate.NameTextsItalian[35];
                TextsInGame[31].text = Translate.NameTextsItalian[36];
                TextsInGame[32].text = Translate.NameTextsItalian[37];
                TextsInGame[33].text = Translate.NameTextsItalian[38];
                TextsInGame[34].text = Translate.NameTextsItalian[39];
                TextsInGame[35].text = Translate.NameTextsItalian[39];
                TextsInGame[36].text = Translate.NameTextsItalian[40];
                TextsInGame[37].text = Translate.NameTextsItalian[49];
                TextsInGame[38].text = Translate.NameTextsItalian[50];
                TextsInGame[39].text = Translate.NameTextsItalian[51];
                TextsInGame[40].text = Translate.NameTextsItalian[52];
                TextsInGame[41].text = Translate.NameTextsItalian[53];
                TextsInGame[43].text = Translate.NameTextsItalian[55];
                TextsInGame[44].text = Translate.NameTextsItalian[56];
                TextsInGame[45].text = Translate.NameTextsItalian[57];
                TextsInGame[46].text = Translate.NameTextsItalian[58];
                TextsInGame[47].text = Translate.NameTextsItalian[59];
                TextsInGame[48].text = Translate.NameTextsItalian[60];
                TextsInGame[49].text = Translate.NameTextsItalian[61];
                TextsInGame[50].text = Translate.NameTextsItalian[62];
                TextsInGame[51].text = Translate.NameTextsItalian[63];
                TextsInGame[52].text = Translate.NameTextsItalian[63];
                TextsInGame[53].text = Translate.NameTextsItalian[63];

                TextsInGame[54].text = Translate.NameTextsItalian[35];
                TextsInGame[55].text = Translate.NameTextsItalian[72];
                TextsInGame[56].text = Translate.NameTextsItalian[69];
                TextsInGame[57].text = Translate.NameTextsItalian[93];
                TextsInGame[58].text = Translate.NameTextsItalian[67];
                TextsInGame[59].text = Translate.NameTextsItalian[71];
                TextsInGame[60].text = Translate.NameTextsItalian[72];
                TextsInGame[61].text = Translate.NameTextsItalian[69];
                TextsInGame[62].text = Translate.NameTextsItalian[68];
                TextsInGame[63].text = Translate.NameTextsItalian[74];
                TextsInGame[64].text = Translate.NameTextsItalian[75];
                TextsInGame[65].text = Translate.NameTextsItalian[76];
                TextsInGame[66].text = Translate.NameTextsItalian[77];
                TextsInGame[67].text = Translate.NameTextsItalian[78];
                TextsInGame[68].text = Translate.NameTextsItalian[79];
                TextsInGame[69].text = Translate.NameTextsItalian[79];
                TextsInGame[70].text = Translate.NameTextsItalian[80];
                TextsInGame[71].text = Translate.NameTextsItalian[70];
                TextsInGame[72].text = Translate.NameTextsItalian[97];
                TextsInGame[73].text = Translate.NameTextsItalian[91];
                TextsInGame[74].text = Translate.NameTextsItalian[90];
                TextsInGame[75].text = Translate.NameTextsItalian[99];
                TextsInGame[76].text = Translate.NameTextsItalian[100];
                break;
            case 8:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicNorwegian = new Dropdown.OptionData(Translate.NameTextsNorwegian[71]);
                Dropdown.OptionData optionDataBackgroundNorwegian = new Dropdown.OptionData(Translate.NameTextsNorwegian[72]);
                Dropdown.OptionData optionDataSkinAWPNorwegian = new Dropdown.OptionData(Translate.NameTextsNorwegian[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicNorwegian);
                dropdownlistWorkshop.options.Add(optionDataBackgroundNorwegian);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPNorwegian);
                TextsInGame[0].text = Translate.NameTextsNorwegian[0];
                TextsInGame[1].text = Translate.NameTextsNorwegian[1];
                TextsInGame[2].text = Translate.NameTextsNorwegian[2];
                TextsInGame[3].text = Translate.NameTextsNorwegian[3];
                TextsInGame[4].text = Translate.NameTextsNorwegian[4];
                TextsInGame[5].text = Translate.NameTextsNorwegian[5];
                TextsInGame[6].text = Translate.NameTextsNorwegian[6];
                TextsInGame[7].text = Translate.NameTextsNorwegian[7];
                TextsInGame[8].text = Translate.NameTextsNorwegian[8];
                TextsInGame[9].text = Translate.NameTextsNorwegian[9];
                TextsInGame[10].text = Translate.NameTextsNorwegian[10];
                TextsInGame[11].text = Translate.NameTextsNorwegian[11];
                TextsInGame[12].text = Translate.NameTextsNorwegian[14];
                TextsInGame[13].text = Translate.NameTextsNorwegian[12];
                TextsInGame[14].text = Translate.NameTextsNorwegian[11];
                TextsInGame[15].text = Translate.NameTextsNorwegian[14];
                TextsInGame[16].text = Translate.NameTextsNorwegian[13];
                TextsInGame[17].text = Translate.NameTextsNorwegian[11];
                TextsInGame[18].text = Translate.NameTextsNorwegian[14];
                TextsInGame[19].text = Translate.NameTextsNorwegian[16];
                TextsInGame[20].text = Translate.NameTextsNorwegian[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsNorwegian[18];
                TextsInGame[22].text = Translate.NameTextsNorwegian[27];
                TextsInGame[23].text = Translate.NameTextsNorwegian[28];
                TextsInGame[24].text = Translate.NameTextsNorwegian[29];
                TextsInGame[25].text = Translate.NameTextsNorwegian[30];
                TextsInGame[26].text = Translate.NameTextsNorwegian[31];
                TextsInGame[27].text = Translate.NameTextsNorwegian[32];
                TextsInGame[28].text = Translate.NameTextsNorwegian[33];
                TextsInGame[29].text = Translate.NameTextsNorwegian[34];
                TextsInGame[30].text = Translate.NameTextsNorwegian[35];
                TextsInGame[31].text = Translate.NameTextsNorwegian[36];
                TextsInGame[32].text = Translate.NameTextsNorwegian[37];
                TextsInGame[33].text = Translate.NameTextsNorwegian[38];
                TextsInGame[34].text = Translate.NameTextsNorwegian[39];
                TextsInGame[35].text = Translate.NameTextsNorwegian[39];
                TextsInGame[36].text = Translate.NameTextsNorwegian[40];
                TextsInGame[37].text = Translate.NameTextsNorwegian[49];
                TextsInGame[38].text = Translate.NameTextsNorwegian[50];
                TextsInGame[39].text = Translate.NameTextsNorwegian[51];
                TextsInGame[40].text = Translate.NameTextsNorwegian[52];
                TextsInGame[41].text = Translate.NameTextsNorwegian[53];
                TextsInGame[43].text = Translate.NameTextsNorwegian[55];
                TextsInGame[44].text = Translate.NameTextsNorwegian[56];
                TextsInGame[45].text = Translate.NameTextsNorwegian[57];
                TextsInGame[46].text = Translate.NameTextsNorwegian[58];
                TextsInGame[47].text = Translate.NameTextsNorwegian[59];
                TextsInGame[48].text = Translate.NameTextsNorwegian[60];
                TextsInGame[49].text = Translate.NameTextsNorwegian[61];
                TextsInGame[50].text = Translate.NameTextsNorwegian[62];
                TextsInGame[51].text = Translate.NameTextsNorwegian[63];
                TextsInGame[52].text = Translate.NameTextsNorwegian[63];
                TextsInGame[53].text = Translate.NameTextsNorwegian[63];

                TextsInGame[54].text = Translate.NameTextsNorwegian[35];
                TextsInGame[55].text = Translate.NameTextsNorwegian[72];
                TextsInGame[56].text = Translate.NameTextsNorwegian[69];
                TextsInGame[57].text = Translate.NameTextsNorwegian[93];
                TextsInGame[58].text = Translate.NameTextsNorwegian[67];
                TextsInGame[59].text = Translate.NameTextsNorwegian[71];
                TextsInGame[60].text = Translate.NameTextsNorwegian[72];
                TextsInGame[61].text = Translate.NameTextsNorwegian[69];
                TextsInGame[62].text = Translate.NameTextsNorwegian[68];
                TextsInGame[63].text = Translate.NameTextsNorwegian[74];
                TextsInGame[64].text = Translate.NameTextsNorwegian[75];
                TextsInGame[65].text = Translate.NameTextsNorwegian[76];
                TextsInGame[66].text = Translate.NameTextsNorwegian[77];
                TextsInGame[67].text = Translate.NameTextsNorwegian[78];
                TextsInGame[68].text = Translate.NameTextsNorwegian[79];
                TextsInGame[69].text = Translate.NameTextsNorwegian[79];
                TextsInGame[70].text = Translate.NameTextsNorwegian[80];
                TextsInGame[71].text = Translate.NameTextsNorwegian[70];
                TextsInGame[72].text = Translate.NameTextsNorwegian[97];
                TextsInGame[73].text = Translate.NameTextsNorwegian[91];
                TextsInGame[74].text = Translate.NameTextsNorwegian[90];
                TextsInGame[75].text = Translate.NameTextsNorwegian[99];
                TextsInGame[76].text = Translate.NameTextsNorwegian[100];
                break;
            case 9:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicPortuguese = new Dropdown.OptionData(Translate.NameTextsPortuguese[71]);
                Dropdown.OptionData optionDataBackgroundPortuguese = new Dropdown.OptionData(Translate.NameTextsPortuguese[72]);
                Dropdown.OptionData optionDataSkinAWPPortuguese = new Dropdown.OptionData(Translate.NameTextsPortuguese[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicPortuguese);
                dropdownlistWorkshop.options.Add(optionDataBackgroundPortuguese);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPPortuguese);
                TextsInGame[0].text = Translate.NameTextsPortuguese[0];
                TextsInGame[1].text = Translate.NameTextsPortuguese[1];
                TextsInGame[2].text = Translate.NameTextsPortuguese[2];
                TextsInGame[3].text = Translate.NameTextsPortuguese[3];
                TextsInGame[4].text = Translate.NameTextsPortuguese[4];
                TextsInGame[5].text = Translate.NameTextsPortuguese[5];
                TextsInGame[6].text = Translate.NameTextsPortuguese[6];
                TextsInGame[7].text = Translate.NameTextsPortuguese[7];
                TextsInGame[8].text = Translate.NameTextsPortuguese[8];
                TextsInGame[9].text = Translate.NameTextsPortuguese[9];
                TextsInGame[10].text = Translate.NameTextsPortuguese[10];
                TextsInGame[11].text = Translate.NameTextsPortuguese[11];
                TextsInGame[12].text = Translate.NameTextsPortuguese[14];
                TextsInGame[13].text = Translate.NameTextsPortuguese[12];
                TextsInGame[14].text = Translate.NameTextsPortuguese[11];
                TextsInGame[15].text = Translate.NameTextsPortuguese[14];
                TextsInGame[16].text = Translate.NameTextsPortuguese[13];
                TextsInGame[17].text = Translate.NameTextsPortuguese[11];
                TextsInGame[18].text = Translate.NameTextsPortuguese[14];
                TextsInGame[19].text = Translate.NameTextsPortuguese[16];
                TextsInGame[20].text = Translate.NameTextsPortuguese[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsPortuguese[18];
                TextsInGame[22].text = Translate.NameTextsPortuguese[27];
                TextsInGame[23].text = Translate.NameTextsPortuguese[28];
                TextsInGame[24].text = Translate.NameTextsPortuguese[29];
                TextsInGame[25].text = Translate.NameTextsPortuguese[30];
                TextsInGame[26].text = Translate.NameTextsPortuguese[31];
                TextsInGame[27].text = Translate.NameTextsPortuguese[32];
                TextsInGame[28].text = Translate.NameTextsPortuguese[33];
                TextsInGame[29].text = Translate.NameTextsPortuguese[34];
                TextsInGame[30].text = Translate.NameTextsPortuguese[35];
                TextsInGame[31].text = Translate.NameTextsPortuguese[36];
                TextsInGame[32].text = Translate.NameTextsPortuguese[37];
                TextsInGame[33].text = Translate.NameTextsPortuguese[38];
                TextsInGame[34].text = Translate.NameTextsPortuguese[39];
                TextsInGame[35].text = Translate.NameTextsPortuguese[39];
                TextsInGame[36].text = Translate.NameTextsPortuguese[40];
                TextsInGame[37].text = Translate.NameTextsPortuguese[49];
                TextsInGame[38].text = Translate.NameTextsPortuguese[50];
                TextsInGame[39].text = Translate.NameTextsPortuguese[51];
                TextsInGame[40].text = Translate.NameTextsPortuguese[52];
                TextsInGame[41].text = Translate.NameTextsPortuguese[53];
                TextsInGame[43].text = Translate.NameTextsPortuguese[55];
                TextsInGame[44].text = Translate.NameTextsPortuguese[56];
                TextsInGame[45].text = Translate.NameTextsPortuguese[57];
                TextsInGame[46].text = Translate.NameTextsPortuguese[58];
                TextsInGame[47].text = Translate.NameTextsPortuguese[59];
                TextsInGame[48].text = Translate.NameTextsPortuguese[60];
                TextsInGame[49].text = Translate.NameTextsPortuguese[61];
                TextsInGame[50].text = Translate.NameTextsPortuguese[62];
                TextsInGame[51].text = Translate.NameTextsPortuguese[63];
                TextsInGame[52].text = Translate.NameTextsPortuguese[63];
                TextsInGame[53].text = Translate.NameTextsPortuguese[63];

                TextsInGame[54].text = Translate.NameTextsPortuguese[35];
                TextsInGame[55].text = Translate.NameTextsPortuguese[72];
                TextsInGame[56].text = Translate.NameTextsPortuguese[69];
                TextsInGame[57].text = Translate.NameTextsPortuguese[93];
                TextsInGame[58].text = Translate.NameTextsPortuguese[67];
                TextsInGame[59].text = Translate.NameTextsPortuguese[71];
                TextsInGame[60].text = Translate.NameTextsPortuguese[72];
                TextsInGame[61].text = Translate.NameTextsPortuguese[69];
                TextsInGame[62].text = Translate.NameTextsPortuguese[68];
                TextsInGame[63].text = Translate.NameTextsPortuguese[74];
                TextsInGame[64].text = Translate.NameTextsPortuguese[75];
                TextsInGame[65].text = Translate.NameTextsPortuguese[76];
                TextsInGame[66].text = Translate.NameTextsPortuguese[77];
                TextsInGame[67].text = Translate.NameTextsPortuguese[78];
                TextsInGame[68].text = Translate.NameTextsPortuguese[79];
                TextsInGame[69].text = Translate.NameTextsPortuguese[79];
                TextsInGame[70].text = Translate.NameTextsPortuguese[80];
                TextsInGame[71].text = Translate.NameTextsPortuguese[70];
                TextsInGame[72].text = Translate.NameTextsPortuguese[97];
                TextsInGame[73].text = Translate.NameTextsPortuguese[91];
                TextsInGame[74].text = Translate.NameTextsPortuguese[90];
                TextsInGame[75].text = Translate.NameTextsPortuguese[99];
                TextsInGame[76].text = Translate.NameTextsPortuguese[100];
                break;
            case 10:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicRU = new Dropdown.OptionData(Translate.NameTextsRU[71]);
                Dropdown.OptionData optionDataBackgroundRU = new Dropdown.OptionData(Translate.NameTextsRU[72]);
                Dropdown.OptionData optionDataSkinAWPRU = new Dropdown.OptionData(Translate.NameTextsRU[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicRU);
                dropdownlistWorkshop.options.Add(optionDataBackgroundRU);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPRU);
                TextsInGame[0].text = Translate.NameTextsRU[0];
                TextsInGame[1].text = Translate.NameTextsRU[1];
                TextsInGame[2].text = Translate.NameTextsRU[2];
                TextsInGame[3].text = Translate.NameTextsRU[3];
                TextsInGame[4].text = Translate.NameTextsRU[4];
                TextsInGame[5].text = Translate.NameTextsRU[5];
                TextsInGame[6].text = Translate.NameTextsRU[6];
                TextsInGame[7].text = Translate.NameTextsRU[7];
                TextsInGame[8].text = Translate.NameTextsRU[8];
                TextsInGame[9].text = Translate.NameTextsRU[9];
                TextsInGame[10].text = Translate.NameTextsRU[10];
                TextsInGame[11].text = Translate.NameTextsRU[11];
                TextsInGame[12].text = Translate.NameTextsRU[14];
                TextsInGame[13].text = Translate.NameTextsRU[12];
                TextsInGame[14].text = Translate.NameTextsRU[11];
                TextsInGame[15].text = Translate.NameTextsRU[14];
                TextsInGame[16].text = Translate.NameTextsRU[13];
                TextsInGame[17].text = Translate.NameTextsRU[11];
                TextsInGame[18].text = Translate.NameTextsRU[14];
                TextsInGame[19].text = Translate.NameTextsRU[16];
                TextsInGame[20].text = Translate.NameTextsRU[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsRU[18];
                TextsInGame[22].text = Translate.NameTextsRU[27];
                TextsInGame[23].text = Translate.NameTextsRU[28];
                TextsInGame[24].text = Translate.NameTextsRU[29];
                TextsInGame[25].text = Translate.NameTextsRU[30];
                TextsInGame[26].text = Translate.NameTextsRU[31];
                TextsInGame[27].text = Translate.NameTextsRU[32];
                TextsInGame[28].text = Translate.NameTextsRU[33];
                TextsInGame[29].text = Translate.NameTextsRU[34];
                TextsInGame[30].text = Translate.NameTextsRU[35];
                TextsInGame[31].text = Translate.NameTextsRU[36];
                TextsInGame[32].text = Translate.NameTextsRU[37];
                TextsInGame[33].text = Translate.NameTextsRU[38];
                TextsInGame[34].text = Translate.NameTextsRU[39];
                TextsInGame[35].text = Translate.NameTextsRU[39];
                TextsInGame[36].text = Translate.NameTextsRU[40];
                TextsInGame[37].text = Translate.NameTextsRU[49];
                TextsInGame[38].text = Translate.NameTextsRU[50];
                TextsInGame[39].text = Translate.NameTextsRU[51];
                TextsInGame[40].text = Translate.NameTextsRU[52];
                TextsInGame[41].text = Translate.NameTextsRU[53];
                TextsInGame[43].text = Translate.NameTextsRU[55];
                TextsInGame[44].text = Translate.NameTextsRU[56];
                TextsInGame[45].text = Translate.NameTextsRU[57];
                TextsInGame[46].text = Translate.NameTextsRU[58];
                TextsInGame[47].text = Translate.NameTextsRU[59];
                TextsInGame[48].text = Translate.NameTextsRU[60];
                TextsInGame[49].text = Translate.NameTextsRU[61];
                TextsInGame[50].text = Translate.NameTextsRU[62];
                TextsInGame[51].text = Translate.NameTextsRU[63];
                TextsInGame[52].text = Translate.NameTextsRU[63];
                TextsInGame[53].text = Translate.NameTextsRU[63];

                TextsInGame[54].text = Translate.NameTextsRU[35];
                TextsInGame[55].text = Translate.NameTextsRU[72];
                TextsInGame[56].text = Translate.NameTextsRU[69];
                TextsInGame[57].text = Translate.NameTextsRU[93];
                TextsInGame[58].text = Translate.NameTextsRU[67];
                TextsInGame[59].text = Translate.NameTextsRU[71];
                TextsInGame[60].text = Translate.NameTextsRU[72];
                TextsInGame[61].text = Translate.NameTextsRU[69];
                TextsInGame[62].text = Translate.NameTextsRU[68];
                TextsInGame[63].text = Translate.NameTextsRU[74];
                TextsInGame[64].text = Translate.NameTextsRU[75];
                TextsInGame[65].text = Translate.NameTextsRU[76];
                TextsInGame[66].text = Translate.NameTextsRU[77];
                TextsInGame[67].text = Translate.NameTextsRU[78];
                TextsInGame[68].text = Translate.NameTextsRU[79];
                TextsInGame[69].text = Translate.NameTextsRU[79];
                TextsInGame[70].text = Translate.NameTextsRU[80];
                TextsInGame[71].text = Translate.NameTextsRU[70];
                TextsInGame[72].text = Translate.NameTextsRU[97];
                TextsInGame[73].text = Translate.NameTextsRU[91];
                TextsInGame[74].text = Translate.NameTextsRU[90];
                TextsInGame[75].text = Translate.NameTextsRU[99];
                TextsInGame[76].text = Translate.NameTextsRU[100];
                break;
            case 11:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicSpanishSpain = new Dropdown.OptionData(Translate.NameTextsSpanishSpain[71]);
                Dropdown.OptionData optionDataBackgroundSpanishSpain = new Dropdown.OptionData(Translate.NameTextsSpanishSpain[72]);
                Dropdown.OptionData optionDataSkinAWPSpanishSpain = new Dropdown.OptionData(Translate.NameTextsSpanishSpain[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicSpanishSpain);
                dropdownlistWorkshop.options.Add(optionDataBackgroundSpanishSpain);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPSpanishSpain);
                TextsInGame[0].text = Translate.NameTextsSpanishSpain[0];
                TextsInGame[1].text = Translate.NameTextsSpanishSpain[1];
                TextsInGame[2].text = Translate.NameTextsSpanishSpain[2];
                TextsInGame[3].text = Translate.NameTextsSpanishSpain[3];
                TextsInGame[4].text = Translate.NameTextsSpanishSpain[4];
                TextsInGame[5].text = Translate.NameTextsSpanishSpain[5];
                TextsInGame[6].text = Translate.NameTextsSpanishSpain[6];
                TextsInGame[7].text = Translate.NameTextsSpanishSpain[7];
                TextsInGame[8].text = Translate.NameTextsSpanishSpain[8];
                TextsInGame[9].text = Translate.NameTextsSpanishSpain[9];
                TextsInGame[10].text = Translate.NameTextsSpanishSpain[10];
                TextsInGame[11].text = Translate.NameTextsSpanishSpain[11];
                TextsInGame[12].text = Translate.NameTextsSpanishSpain[14];
                TextsInGame[13].text = Translate.NameTextsSpanishSpain[12];
                TextsInGame[14].text = Translate.NameTextsSpanishSpain[11];
                TextsInGame[15].text = Translate.NameTextsSpanishSpain[14];
                TextsInGame[16].text = Translate.NameTextsSpanishSpain[13];
                TextsInGame[17].text = Translate.NameTextsSpanishSpain[11];
                TextsInGame[18].text = Translate.NameTextsSpanishSpain[14];
                TextsInGame[19].text = Translate.NameTextsSpanishSpain[16];
                TextsInGame[20].text = Translate.NameTextsSpanishSpain[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsSpanishSpain[18];
                TextsInGame[22].text = Translate.NameTextsSpanishSpain[27];
                TextsInGame[23].text = Translate.NameTextsSpanishSpain[28];
                TextsInGame[24].text = Translate.NameTextsSpanishSpain[29];
                TextsInGame[25].text = Translate.NameTextsSpanishSpain[30];
                TextsInGame[26].text = Translate.NameTextsSpanishSpain[31];
                TextsInGame[27].text = Translate.NameTextsSpanishSpain[32];
                TextsInGame[28].text = Translate.NameTextsSpanishSpain[33];
                TextsInGame[29].text = Translate.NameTextsSpanishSpain[34];
                TextsInGame[30].text = Translate.NameTextsSpanishSpain[35];
                TextsInGame[31].text = Translate.NameTextsSpanishSpain[36];
                TextsInGame[32].text = Translate.NameTextsSpanishSpain[37];
                TextsInGame[33].text = Translate.NameTextsSpanishSpain[38];
                TextsInGame[34].text = Translate.NameTextsSpanishSpain[39];
                TextsInGame[35].text = Translate.NameTextsSpanishSpain[39];
                TextsInGame[36].text = Translate.NameTextsSpanishSpain[40];
                TextsInGame[37].text = Translate.NameTextsSpanishSpain[49];
                TextsInGame[38].text = Translate.NameTextsSpanishSpain[50];
                TextsInGame[39].text = Translate.NameTextsSpanishSpain[51];
                TextsInGame[40].text = Translate.NameTextsSpanishSpain[52];
                TextsInGame[41].text = Translate.NameTextsSpanishSpain[53];
                TextsInGame[43].text = Translate.NameTextsSpanishSpain[55];
                TextsInGame[44].text = Translate.NameTextsSpanishSpain[56];
                TextsInGame[45].text = Translate.NameTextsSpanishSpain[57];
                TextsInGame[46].text = Translate.NameTextsSpanishSpain[58];
                TextsInGame[47].text = Translate.NameTextsSpanishSpain[59];
                TextsInGame[48].text = Translate.NameTextsSpanishSpain[60];
                TextsInGame[49].text = Translate.NameTextsSpanishSpain[61];
                TextsInGame[50].text = Translate.NameTextsSpanishSpain[62];
                TextsInGame[51].text = Translate.NameTextsSpanishSpain[63];
                TextsInGame[52].text = Translate.NameTextsSpanishSpain[63];
                TextsInGame[53].text = Translate.NameTextsSpanishSpain[63];

                TextsInGame[54].text = Translate.NameTextsSpanishSpain[35];
                TextsInGame[55].text = Translate.NameTextsSpanishSpain[72];
                TextsInGame[56].text = Translate.NameTextsSpanishSpain[69];
                TextsInGame[57].text = Translate.NameTextsSpanishSpain[93];
                TextsInGame[58].text = Translate.NameTextsSpanishSpain[67];
                TextsInGame[59].text = Translate.NameTextsSpanishSpain[71];
                TextsInGame[60].text = Translate.NameTextsSpanishSpain[72];
                TextsInGame[61].text = Translate.NameTextsSpanishSpain[69];
                TextsInGame[62].text = Translate.NameTextsSpanishSpain[68];
                TextsInGame[63].text = Translate.NameTextsSpanishSpain[74];
                TextsInGame[64].text = Translate.NameTextsSpanishSpain[75];
                TextsInGame[65].text = Translate.NameTextsSpanishSpain[76];
                TextsInGame[66].text = Translate.NameTextsSpanishSpain[77];
                TextsInGame[67].text = Translate.NameTextsSpanishSpain[78];
                TextsInGame[68].text = Translate.NameTextsSpanishSpain[79];
                TextsInGame[69].text = Translate.NameTextsSpanishSpain[79];
                TextsInGame[70].text = Translate.NameTextsSpanishSpain[80];
                TextsInGame[71].text = Translate.NameTextsSpanishSpain[70];
                TextsInGame[72].text = Translate.NameTextsSpanishSpain[97];
                TextsInGame[73].text = Translate.NameTextsSpanishSpain[91];
                TextsInGame[74].text = Translate.NameTextsSpanishSpain[90];
                TextsInGame[75].text = Translate.NameTextsSpanishSpain[99];
                TextsInGame[76].text = Translate.NameTextsSpanishSpain[100];
                break;
            case 12:
                dropdownlistWorkshop.options.Clear();
                dropdownlistWorkshop.RefreshShownValue();
                Dropdown.OptionData optionDataMusicSwedish = new Dropdown.OptionData(Translate.NameTextsSwedish[71]);
                Dropdown.OptionData optionDataBackgroundSwedish = new Dropdown.OptionData(Translate.NameTextsSwedish[72]);
                Dropdown.OptionData optionDataSkinAWPSwedish = new Dropdown.OptionData(Translate.NameTextsSwedish[73]);
                dropdownlistWorkshop.options.Add(optionDataMusicSwedish);
                dropdownlistWorkshop.options.Add(optionDataBackgroundSwedish);
                dropdownlistWorkshop.options.Add(optionDataSkinAWPSwedish);
                TextsInGame[0].text = Translate.NameTextsSwedish[0];
                TextsInGame[1].text = Translate.NameTextsSwedish[1];
                TextsInGame[2].text = Translate.NameTextsSwedish[2];
                TextsInGame[3].text = Translate.NameTextsSwedish[3];
                TextsInGame[4].text = Translate.NameTextsSwedish[4];
                TextsInGame[5].text = Translate.NameTextsSwedish[5];
                TextsInGame[6].text = Translate.NameTextsSwedish[6];
                TextsInGame[7].text = Translate.NameTextsSwedish[7];
                TextsInGame[8].text = Translate.NameTextsSwedish[8];
                TextsInGame[9].text = Translate.NameTextsSwedish[9];
                TextsInGame[10].text = Translate.NameTextsSwedish[10];
                TextsInGame[11].text = Translate.NameTextsSwedish[11];
                TextsInGame[12].text = Translate.NameTextsSwedish[14];
                TextsInGame[13].text = Translate.NameTextsSwedish[12];
                TextsInGame[14].text = Translate.NameTextsSwedish[11];
                TextsInGame[15].text = Translate.NameTextsSwedish[14];
                TextsInGame[16].text = Translate.NameTextsSwedish[13];
                TextsInGame[17].text = Translate.NameTextsSwedish[11];
                TextsInGame[18].text = Translate.NameTextsSwedish[14];
                TextsInGame[19].text = Translate.NameTextsSwedish[16];
                TextsInGame[20].text = Translate.NameTextsSwedish[17];
                if (gameRules.currentGameModeSelected != GameMode.MatchMaking)
                    TextsInGame[21].text = Translate.NameTextsSwedish[18];
                TextsInGame[22].text = Translate.NameTextsSwedish[27];
                TextsInGame[23].text = Translate.NameTextsSwedish[28];
                TextsInGame[24].text = Translate.NameTextsSwedish[29];
                TextsInGame[25].text = Translate.NameTextsSwedish[30];
                TextsInGame[26].text = Translate.NameTextsSwedish[31];
                TextsInGame[27].text = Translate.NameTextsSwedish[32];
                TextsInGame[28].text = Translate.NameTextsSwedish[33];
                TextsInGame[29].text = Translate.NameTextsSwedish[34];
                TextsInGame[30].text = Translate.NameTextsSwedish[35];
                TextsInGame[31].text = Translate.NameTextsSwedish[36];
                TextsInGame[32].text = Translate.NameTextsSwedish[37];
                TextsInGame[33].text = Translate.NameTextsSwedish[38];
                TextsInGame[34].text = Translate.NameTextsSwedish[39];
                TextsInGame[35].text = Translate.NameTextsSwedish[39];
                TextsInGame[36].text = Translate.NameTextsSwedish[40];
                TextsInGame[37].text = Translate.NameTextsSwedish[49];
                TextsInGame[38].text = Translate.NameTextsSwedish[50];
                TextsInGame[39].text = Translate.NameTextsSwedish[51];
                TextsInGame[40].text = Translate.NameTextsSwedish[52];
                TextsInGame[41].text = Translate.NameTextsSwedish[53];
                TextsInGame[43].text = Translate.NameTextsSwedish[55];
                TextsInGame[44].text = Translate.NameTextsSwedish[56];
                TextsInGame[45].text = Translate.NameTextsSwedish[57];
                TextsInGame[46].text = Translate.NameTextsSwedish[58];
                TextsInGame[47].text = Translate.NameTextsSwedish[59];
                TextsInGame[48].text = Translate.NameTextsSwedish[60];
                TextsInGame[49].text = Translate.NameTextsSwedish[61];
                TextsInGame[50].text = Translate.NameTextsSwedish[62];
                TextsInGame[51].text = Translate.NameTextsSwedish[63];
                TextsInGame[52].text = Translate.NameTextsSwedish[63];
                TextsInGame[53].text = Translate.NameTextsSwedish[63];

                TextsInGame[54].text = Translate.NameTextsSwedish[35];
                TextsInGame[55].text = Translate.NameTextsSwedish[72];
                TextsInGame[56].text = Translate.NameTextsSwedish[69];
                TextsInGame[57].text = Translate.NameTextsSwedish[93];
                TextsInGame[58].text = Translate.NameTextsSwedish[67];
                TextsInGame[59].text = Translate.NameTextsSwedish[71];
                TextsInGame[60].text = Translate.NameTextsSwedish[72];
                TextsInGame[61].text = Translate.NameTextsSwedish[69];
                TextsInGame[62].text = Translate.NameTextsSwedish[68];
                TextsInGame[63].text = Translate.NameTextsSwedish[74];
                TextsInGame[64].text = Translate.NameTextsSwedish[75];
                TextsInGame[65].text = Translate.NameTextsSwedish[76];
                TextsInGame[66].text = Translate.NameTextsSwedish[77];
                TextsInGame[67].text = Translate.NameTextsSwedish[78];
                TextsInGame[68].text = Translate.NameTextsSwedish[79];
                TextsInGame[69].text = Translate.NameTextsSwedish[79];
                TextsInGame[70].text = Translate.NameTextsSwedish[80];
                TextsInGame[71].text = Translate.NameTextsSwedish[70];
                TextsInGame[72].text = Translate.NameTextsSwedish[97];
                TextsInGame[73].text = Translate.NameTextsSwedish[91];
                TextsInGame[74].text = Translate.NameTextsSwedish[90];
                TextsInGame[75].text = Translate.NameTextsSwedish[99];
                TextsInGame[76].text = Translate.NameTextsSwedish[100];
                break;
        }
    }
    private void ChangeBleedingUI()
    {
        int currentLvLBombSpawn = dataPlayer.LvLsBleeding[IDBombSpawnLvL];
        int currentLvLMagnitSpawn = dataPlayer.LvLsBleeding[IDMagnitSpawnLvL];
        int currentLvLMoneySpawn = dataPlayer.LvLsBleeding[IDMoneySpawnLvL];
        Bleedings[IDBombSpawnLvL].size = currentLvLBombSpawn / SizeScrollBar;
        Bleedings[IDMagnitSpawnLvL].size = currentLvLMagnitSpawn / SizeScrollBar;
        Bleedings[IDMoneySpawnLvL].size = currentLvLMoneySpawn / SizeScrollBar;
        LvLs[IDBombSpawnLvL].text = "LvL." + currentLvLBombSpawn;
        LvLs[IDMagnitSpawnLvL].text = "LvL." + currentLvLMagnitSpawn;
        LvLs[IDMoneySpawnLvL].text = "LvL." + currentLvLMoneySpawn;
        if (dataPlayer != null)
        {
            switch (dataPlayer.CurrentIDLanguage)
            {
                case 0:
                    currentMoneyPlayer.text = Translate.NameTextsChina[15] + dataPlayer.MoneyPlayer;
                    break;
                case 1:
                    currentMoneyPlayer.text = Translate.NameTextsDanish[15] + dataPlayer.MoneyPlayer;
                    break;
                case 2:
                    currentMoneyPlayer.text = Translate.NameTextsDutch[15] + dataPlayer.MoneyPlayer;
                    break;
                case 3:
                    currentMoneyPlayer.text = Translate.NameTextsEng[15] + dataPlayer.MoneyPlayer;
                    break;
                case 4:
                    currentMoneyPlayer.text = Translate.NameTextsFinnish[15] + dataPlayer.MoneyPlayer;
                    break;
                case 5:
                    currentMoneyPlayer.text = Translate.NameTextsFrench[15] + dataPlayer.MoneyPlayer;
                    break;
                case 6:
                    currentMoneyPlayer.text = Translate.NameTextsGerman[15] + dataPlayer.MoneyPlayer;
                    break;
                case 7:
                    currentMoneyPlayer.text = Translate.NameTextsItalian[15] + dataPlayer.MoneyPlayer;
                    break;
                case 8:
                    currentMoneyPlayer.text = Translate.NameTextsNorwegian[15] + dataPlayer.MoneyPlayer;
                    break;
                case 9:
                    currentMoneyPlayer.text = Translate.NameTextsPortuguese[15] + dataPlayer.MoneyPlayer;
                    break;
                case 10:
                    currentMoneyPlayer.text = Translate.NameTextsRU[15] + dataPlayer.MoneyPlayer;
                    break;
                case 11:
                    currentMoneyPlayer.text = Translate.NameTextsSpanishSpain[15] + dataPlayer.MoneyPlayer;
                    break;
                case 12:
                    currentMoneyPlayer.text = Translate.NameTextsSwedish[15] + dataPlayer.MoneyPlayer;
                    break;

            }

            BuyButtons[IDBulletsAWP].interactable = dataPlayer.MoneyPlayer >= BulletBuy;
            switch (GetLanguageID())
            {
                case 0:
                    TextsInGame[42].text = Translate.NameTextsChina[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 1:
                    TextsInGame[42].text = Translate.NameTextsDanish[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 2:
                    TextsInGame[42].text = Translate.NameTextsDutch[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 3:
                    TextsInGame[42].text = Translate.NameTextsEng[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 4:
                    TextsInGame[42].text = Translate.NameTextsFinnish[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 5:
                    TextsInGame[42].text = Translate.NameTextsFrench[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 6:
                    TextsInGame[42].text = Translate.NameTextsGerman[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 7:
                    TextsInGame[42].text = Translate.NameTextsItalian[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 8:
                    TextsInGame[42].text = Translate.NameTextsNorwegian[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 9:
                    TextsInGame[42].text = Translate.NameTextsPortuguese[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 10:
                    TextsInGame[42].text = Translate.NameTextsRU[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 11:
                    TextsInGame[42].text = Translate.NameTextsSpanishSpain[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
                case 12:
                    TextsInGame[42].text = Translate.NameTextsSwedish[54] + GetCurrentBulletsAWP() + " / " + GetMaxBulletsAWP();
                    break;
            }
            SetIsSposobnosty(GetMaxBulletsAWP() > 0 || GetCurrentBulletsAWP() > 0);
            switch (currentLvLBombSpawn)
            {
                case 1:
                    Buys[IDBombSpawnLvL].text = BombSpawnLvL2.ToString();
                    BuyButtons[IDBombSpawnLvL].interactable = dataPlayer.MoneyPlayer >= BombSpawnLvL2;
                    break;
                case 2:
                    Buys[IDBombSpawnLvL].text = BombSpawnLvL3.ToString();
                    BuyButtons[IDBombSpawnLvL].interactable = dataPlayer.MoneyPlayer >= BombSpawnLvL3;
                    break;
                case 3:
                    Buys[IDBombSpawnLvL].text = BombSpawnLvL4.ToString();
                    BuyButtons[IDBombSpawnLvL].interactable = dataPlayer.MoneyPlayer >= BombSpawnLvL4;
                    break;
                case 4:
                    Buys[IDBombSpawnLvL].text = BombSpawnLvL5.ToString();
                    BuyButtons[IDBombSpawnLvL].interactable = dataPlayer.MoneyPlayer >= BombSpawnLvL5;
                    break;
                case 5:
                    switch (GetLanguageID())
                    {
                        case 0:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsChina[47];
                            break;
                        case 1:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsDanish[47]; ;
                            break;
                        case 2:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsDutch[47]; ;
                            break;
                        case 3:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsEng[47]; ;
                            break;
                        case 4:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsFinnish[47]; ;
                            break;
                        case 5:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsFrench[47]; ;
                            break;
                        case 6:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsGerman[47]; ;
                            break;
                        case 7:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsItalian[47]; ;
                            break;
                        case 8:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsNorwegian[47];
                            break;
                        case 9:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsPortuguese[47];
                            break;
                        case 10:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsRU[47];
                            break;
                        case 11:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsSpanishSpain[47];
                            break;
                        case 12:
                            Buys[IDBombSpawnLvL].text = Translate.NameTextsSwedish[47];
                            break;

                    }
                    BuyButtons[IDBombSpawnLvL].interactable = false;
                    break;
            }
            switch (currentLvLMagnitSpawn)
            {
                case 1:
                    Buys[IDMagnitSpawnLvL].text = MagnitSpawnLvL2.ToString();
                    BuyButtons[IDMagnitSpawnLvL].interactable = dataPlayer.MoneyPlayer >= MagnitSpawnLvL2;
                    break;
                case 2:
                    Buys[IDMagnitSpawnLvL].text = MagnitSpawnLvL3.ToString();
                    BuyButtons[IDMagnitSpawnLvL].interactable = dataPlayer.MoneyPlayer >= MagnitSpawnLvL3;
                    break;
                case 3:
                    Buys[IDMagnitSpawnLvL].text = MagnitSpawnLvL4.ToString();
                    BuyButtons[IDMagnitSpawnLvL].interactable = dataPlayer.MoneyPlayer >= MagnitSpawnLvL4;
                    break;
                case 4:
                    Buys[IDMagnitSpawnLvL].text = MagnitSpawnLvL5.ToString();
                    BuyButtons[IDMagnitSpawnLvL].interactable = dataPlayer.MoneyPlayer >= MagnitSpawnLvL5;
                    break;
                case 5:
                    Buys[IDMagnitSpawnLvL].text = "Full Upgrade";
                    BuyButtons[IDMagnitSpawnLvL].interactable = false;
                    break;
            }
            switch (currentLvLMoneySpawn)
            {
                case 1:
                    Buys[IDMoneySpawnLvL].text = MoneySpawnLvL2.ToString();
                    BuyButtons[IDMoneySpawnLvL].interactable = dataPlayer.MoneyPlayer >= MoneySpawnLvL2;
                    break;
                case 2:
                    Buys[IDMoneySpawnLvL].text = MoneySpawnLvL3.ToString();
                    BuyButtons[IDMoneySpawnLvL].interactable = dataPlayer.MoneyPlayer >= MoneySpawnLvL3;
                    break;
                case 3:
                    Buys[IDMoneySpawnLvL].text = MoneySpawnLvL4.ToString();
                    BuyButtons[IDMoneySpawnLvL].interactable = dataPlayer.MoneyPlayer >= MoneySpawnLvL4;
                    break;
                case 4:
                    Buys[IDMoneySpawnLvL].text = MoneySpawnLvL5.ToString();
                    BuyButtons[IDMoneySpawnLvL].interactable = dataPlayer.MoneyPlayer >= MoneySpawnLvL5;
                    break;
                case 5:
                    Buys[IDMoneySpawnLvL].text = "Full Upgrade";
                    BuyButtons[IDMoneySpawnLvL].interactable = false;
                    break;
            }
            ChangeGameStatisticksSchanses(currentLvLBombSpawn, currentLvLMagnitSpawn, currentLvLMoneySpawn);
        }
    }

    private void ChangeGameStatisticksSchanses(int currentLvLBombSpawn, int currentLvLMagnitSpawn, int currentLvLMoneySpawn)
    {
        switch (currentLvLBombSpawn)
        {
            case 1:
                gameRules.changeSpawnCards[0].currentChange = 60;
                break;
            case 2:
                gameRules.changeSpawnCards[0].currentChange = SchansCharactheristikBombSpawnLvL2;
                break;
            case 3:
                gameRules.changeSpawnCards[0].currentChange = SchansCharactheristikBombSpawnLvL3;
                break;
            case 4:
                gameRules.changeSpawnCards[0].currentChange = SchansCharactheristikBombSpawnLvL4;
                break;
            case 5:
                gameRules.changeSpawnCards[0].currentChange = SchansCharactheristikBombSpawnLvL5;
                break;
        }
        switch (currentLvLMagnitSpawn)
        {
            case 1:
                gameRules.changeSpawnCards[2].currentChange = 1;
                break;
            case 2:
                gameRules.changeSpawnCards[2].currentChange = SchansCharactheristikMagnitSpawnLvL2;
                break;
            case 3:
                gameRules.changeSpawnCards[2].currentChange = SchansCharactheristikMagnitSpawnLvL3;
                break;
            case 4:
                gameRules.changeSpawnCards[2].currentChange = SchansCharactheristikMagnitSpawnLvL4;
                break;
            case 5:
                gameRules.changeSpawnCards[2].currentChange = SchansCharactheristikMagnitSpawnLvL5;
                break;
        }
        switch (currentLvLMoneySpawn)
        {
            case 1:
                gameRules.SetMinMoneyClickedNumber(1, 10);
                break;
            case 2:
                gameRules.SetMinMoneyClickedNumber(SchansCharactheristikMoneyMinLvL2, SchansCharactheristikMoneyMaxLvL2);
                break;
            case 3:
                gameRules.SetMinMoneyClickedNumber(SchansCharactheristikMoneyMinLvL3, SchansCharactheristikMoneyMaxLvL3);
                break;
            case 4:
                gameRules.SetMinMoneyClickedNumber(SchansCharactheristikMoneyMinLvL4, SchansCharactheristikMoneyMaxLvL4);
                break;
            case 5:
                gameRules.SetMinMoneyClickedNumber(SchansCharactheristikMoneyMinLvL5, SchansCharactheristikMoneyMaxLvL5);
                break;
        }
    }
    
    internal float GetThickness() { return dataPlayer.Thickness; }
    internal void SetThickness(float set) { dataPlayer.Thickness = set; }
    internal float GetDistance() { return dataPlayer.Distance; }
    internal void SetDistance(float set) { dataPlayer.Distance = set; }

    internal float GetOutLine() { return dataPlayer.OutLine; }
    internal void SetOutLine(float set) { dataPlayer.Distance = set; }
    internal float GetRedColor() { return dataPlayer.Red; }
    internal void SetRedColor(float set) { dataPlayer.Red = set; }
    internal float GetBlueColor() { return dataPlayer.Blue; }
    internal void SetBlueColor(float set) { dataPlayer.Blue = set; }
    internal float GetGreenColor() { return dataPlayer.Green; }
    internal void SetGreenColor(float set) { dataPlayer.Green = set; }
    
    internal bool GetTObrazz() { return dataPlayer.TObrazz; }
    internal void SetTObrazz(bool set) { dataPlayer.TObrazz = set; }

    internal bool GetIsSposobnosty() { return dataPlayer.isSposobnosty; }
    internal void SetIsSposobnosty(bool set) { dataPlayer.isSposobnosty = set;}

    internal float GetCurrentBulletsAWP() { return dataPlayer.CurrentAWPBullets; }
    internal void SetCurrentBulletsAWP(float set,char symbol) 
    {
        switch (symbol)
        {
            case '+':
                dataPlayer.CurrentAWPBullets += set;
                break;
            case '-':
                dataPlayer.CurrentAWPBullets -= set;
                break;
            case '=':
                dataPlayer.CurrentAWPBullets = set;
                break;
        }
    }

    internal bool GetBotData()
    {
        return dataPlayer.isBot;
    }
    internal void SetBotData(bool result)
    {
        dataPlayer.isBot = result;
    }

    internal float GetMaxBulletsAWP() { return dataPlayer.MaxAWPBullets; }
    internal void SetMaxBulletsAWP(float set,char symbol) 
    {
        switch (symbol)
        {
            case '+':
                dataPlayer.MaxAWPBullets += set;
                break;
            case '-':
                dataPlayer.MaxAWPBullets -= set;
                break;
            case '=':
                dataPlayer.MaxAWPBullets = set;
                break;
        }
    }

    internal void SetDaysCount(int set) { dataPlayer.DaysCount = set; }

    internal int GetDaysCount() { return dataPlayer.DaysCount; }

    internal void SetLastTimeGiftDateTime(string set) { dataPlayer.LastTimeGiftDateTime = set; }

    internal string GetLastTimeGiftDateTime() { return dataPlayer.LastTimeGiftDateTime; }


    void OnApplicationQuit()
    {
        AWPMat.mainTexture = DefaultTexture;
        SaveDataPlayer();
    }

    internal void SaveDataPlayer()
    {
        File.WriteAllText(pathToSave, JsonUtility.ToJson(dataPlayer));
        var Encode = File.ReadAllText(pathToSave, Encoding.Default).Replace("\n", " ");
        if(Encode != string.Empty)
        {
            var cypher = RsaEnc.Encrypt(Encode);
            UnityEngine.Debug.Log($"Cypher Text: \n {cypher} \n");
            File.WriteAllText(pathToSave, cypher, Encoding.Default);
        }

    }



   
}


[Serializable]
internal class PlayerInventory
{
    [SerializeField] internal List<Item> items;
}
[Serializable]
internal class Item
{
    [SerializeField] internal int ID;
    [SerializeField] internal string nameItem;
    [SerializeField] internal Sprite Image;
    [SerializeField] internal RuntimeAnimatorController runtimeAnimatorGIF;
}

[Serializable]
internal class SaveDataGame
{
    [SerializeField] internal string NickNamePlayer;
    [SerializeField] internal int IDPlayer,CurrentIDLanguage, RecordComboPlayer,DaysCount;
    [SerializeField] internal string NameAWPSkinCurrentSelected = "",LastTimeGiftDateTime;
    [SerializeField] internal bool isTraningMode = true, Ads = true, TObrazz = false;
    [SerializeField] internal float RecordScorePlayer, MoneyPlayer;
    [SerializeField] internal bool isBot = true;
    [SerializeField] internal float CurrentAWPBullets, MaxAWPBullets;
    [SerializeField] internal bool isSposobnosty;
    [SerializeField] internal float Thickness,Distance,OutLine,Red,Blue,Green;
    [SerializeField] internal float RecordTimerSurvive;
    [SerializeField] internal List<int> IDRewardsInInventory;
    [SerializeField] internal int currentIDSelectedBackground = -1;
    [SerializeField] internal string PathToBackgroundMod;
    [SerializeField] internal int[] LvLsBleeding;
}

[Serializable]
public class Profile : IComparable
{
    public int IDPlayer;
    public string NickNamePlayer;
    public float TimerSurvive,MoneyPlayer;

    public Profile(int IDPlayer,string NickNamePlayer,float TimerSurvive,float MoneyPlayer)
    {
        this.IDPlayer = IDPlayer;
        this.NickNamePlayer = NickNamePlayer;
        this.TimerSurvive = TimerSurvive;
        this.MoneyPlayer = MoneyPlayer;
    }

    public int CompareTo(object obj)
    {
        Profile profile = (Profile)obj;

        if (this.TimerSurvive < profile.TimerSurvive)
            return 1;
        else if (this.TimerSurvive > profile.TimerSurvive)
            return -1;
        return 0;
    }
}

