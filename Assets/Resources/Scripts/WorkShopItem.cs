using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class WorkShopItem : MonoBehaviour
{

    internal enum TypeItem
    {
        Music,
        Background,
        Icon,
    }
    const string PathToMusicsUploadsFiles = "ftp://files.000webhost.com/Workshop/Music/";
    const string PathToBackgroundUploadsFiles = "ftp://files.000webhost.com/Workshop/Backgrounds/";
    const string PathToSkinsAWPUploadsFiles = "ftp://files.000webhost.com/Workshop/SkinsAWP/";
    const string NameAWP = "AWP";
    const string PathToBackgroundInDesktop = "Mods/Backgrounds";
    const string PahToMusicInDesktop = "Mods/Music";
    const string PathToAWPSkinsInDesktop = "Mods/AWPSkins";
    const string NameProfile = "Profile";
    const string NameBackgroundMusic = "BackgroundMusic";
    const string NameNameModText = "NameModText";
    const string PathToMusicSprite = "Sprites/MusicFile";
    const string NameAuthorText = "AuthorText";
    const string NameBackToWorkshopButton = "BackToWorkshopButton";
    const string NameMusicCloundGame = "MusicCloudGame";

    [SerializeField] internal string NameAuthor, NameItem;
    [SerializeField] internal Sprite IconItem;
    [SerializeField] internal int CountLikes;

    [SerializeField] internal GameObject WorkShopUI, PreviewModUI;
    [SerializeField] internal GameObject BackgroundMusic;
    [SerializeField] internal Text NameItemTextPreview, NameAuthorTextPreview;
    [SerializeField] internal Button ListerButtonBack;
    [SerializeField] internal TypeItem typeItem;
    [SerializeField] private bool isLike,isDownloaded;
    [SerializeField] private Text ItemNameText;
    [SerializeField] private Text AuthorNameText;
    [SerializeField] private PlayerProfille Language;
    [SerializeField] private AudioSource MusicZone;
    [SerializeField] private Image IconItemImg,IconRatingImg;
    [SerializeField] private Sprite[] RatingsImgs;
    [SerializeField] private Sprite DownloadIcon, DeleteModIcon;
    [SerializeField] private Button PreviewItem, DownloadItem,LikeItem;
    [SerializeField] private Sprite[] LikeStatus;
    [SerializeField] private Text CountLikesText;
    [SerializeField] private AudioClip ClickButton;
    [SerializeField] private Material AWPMain;

    Sprite tempSpriteImgBackground;
    AudioClip tempAudioMusicFile;
    Texture2D AWPPreSkin;


    internal void PreviewLoadItem()
    {
        Language = GameObject.Find(NameProfile).GetComponent<PlayerProfille>();
        BackgroundMusic = GameObject.Find(NameBackgroundMusic);
        NameItemTextPreview = PreviewModUI.transform.Find(NameNameModText).GetComponent<Text>();
        NameAuthorTextPreview = PreviewModUI.transform.Find(NameAuthorText).GetComponent<Text>();
        ListerButtonBack = PreviewModUI.transform.Find(NameBackToWorkshopButton).GetComponent<Button>();
        MusicZone = GameObject.Find(NameMusicCloundGame).GetComponent<AudioSource>();
        PreviewItem.onClick.AddListener(SoundClick);
        PreviewItem.onClick.AddListener(Preview);
        LikeItem.onClick.AddListener(Like);
        isLike = isCheckerLikePre();
        isDownloaded = isCheckerDownloadMod();
        ChangeLanguage();
    }

    void FixedUpdate()
    {
        CountLikesText.text = CountLikes.ToString();

        if (CountLikes >= 10000)
            IconRatingImg.sprite = RatingsImgs[11];
        else if(CountLikes >= 9091)
            IconRatingImg.sprite = RatingsImgs[10];
        else if (CountLikes >= 8182)
            IconRatingImg.sprite = RatingsImgs[9];
        else if (CountLikes >= 7273)
            IconRatingImg.sprite = RatingsImgs[8];
        else if (CountLikes >= 6364)
            IconRatingImg.sprite = RatingsImgs[7];
        else if (CountLikes >= 5455)
            IconRatingImg.sprite = RatingsImgs[6];
        else if (CountLikes >= 4546)
            IconRatingImg.sprite = RatingsImgs[5];
        else if (CountLikes >= 3637)
            IconRatingImg.sprite = RatingsImgs[4];
        else if (CountLikes >= 2728)
            IconRatingImg.sprite = RatingsImgs[3];
        else if (CountLikes >= 1819)
            IconRatingImg.sprite = RatingsImgs[2];
        else if (CountLikes >= 910)
            IconRatingImg.sprite = RatingsImgs[1];
        else if (CountLikes >= 1)
            IconRatingImg.sprite = RatingsImgs[0];

        if (typeItem == TypeItem.Music)
            IconItemImg.sprite = Resources.Load<Sprite>(PathToMusicSprite);
        else
            IconItemImg.sprite = IconItem;
    }

    private void ChangeLanguage()
    {
        switch (Language.GetLanguageID())
        {
            case 0:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 1:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 2:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 3:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 4:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 5:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 6:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 7:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 8:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 9:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 10:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 11:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
            case 12:
                NameItemTextPreview.text = ItemNameText.text = Translate.NameTextsRU[65] + NameItem;
                NameAuthorTextPreview.text = AuthorNameText.text = Translate.NameTextsRU[66] + NameAuthor;
                break;
        }
    }

    void Download()
    {
        if (!isDownloaded)
        {
            switch (typeItem)
            {
                case TypeItem.Music:
#if UNITY_ANDROID && !UNITY_EDITOR
                    WebClient ftp = new WebClient();
                    ftp.BaseAddress = "ftp://files.000webhost.com";
                    ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uri = new Uri("/Workshop/Music/" + NameItem + ".mp3", UriKind.Relative);
                    ftp.DownloadFile(uri, Application.persistentDataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
                    Loaded();
                    isDownloaded = isCheckerDownloadMod();
                    break;
#else
                    WebClient ftp = new WebClient();
                    ftp.BaseAddress = "ftp://files.000webhost.com";
                    ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uri = new Uri("/Workshop/Music/" + NameItem + ".mp3", UriKind.Relative);
                    ftp.DownloadFile(uri, Application.dataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
                    Loaded();
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif

                case TypeItem.Background:
#if UNITY_ANDROID && !UNITY_EDITOR
                string path = Application.persistentDataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png";
                File.WriteAllBytes(path, IconItem.texture.EncodeToPNG());
                Loaded();
                isDownloaded = isCheckerDownloadMod();
                break;
#else
                    string path = Application.dataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png";
                    File.WriteAllBytes(path, IconItem.texture.EncodeToPNG());
                    Loaded();
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif
                case TypeItem.Icon:
#if UNITY_ANDROID && !UNITY_EDITOR
                   WebClient ftpAWP = new WebClient();
                    ftpAWP.BaseAddress = "ftp://files.000webhost.com";
                    ftpAWP.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uriAWP = new Uri("/Workshop/SkinsAWP/" + NameItem + ".png", UriKind.Relative);
                    ftpAWP.DownloadFileAsync(uriAWP, Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                    WebClient ftpAWPIcon = new WebClient();
                    ftpAWPIcon.BaseAddress = "ftp://files.000webhost.com";
                    ftpAWPIcon.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uriAWPIcon = new Uri("/Workshop/SkinsAWP/" + NameItem + "Icon" + ".png", UriKind.Relative);
                    ftpAWPIcon.DownloadFileAsync(uriAWPIcon, Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + "Icon" + ".png");
                    Loaded();
                    isDownloaded = isCheckerDownloadMod();
                    break;
#else
                    WebClient ftpAWP = new WebClient();
                    ftpAWP.BaseAddress = "ftp://files.000webhost.com";
                    ftpAWP.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uriAWP = new Uri("/Workshop/SkinsAWP/" + NameItem + ".png", UriKind.Relative);
                    ftpAWP.DownloadFileAsync(uriAWP, Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                    WebClient ftpAWPIcon = new WebClient();
                    ftpAWPIcon.BaseAddress = "ftp://files.000webhost.com";
                    ftpAWPIcon.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                    Uri uriAWPIcon = new Uri("/Workshop/SkinsAWP/" + NameItem + "Icon" + ".png", UriKind.Relative);
                    ftpAWPIcon.DownloadFileAsync(uriAWPIcon, Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + "Icon" + ".png");
                    Loaded();
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif
            }
        }
    }


    private void Loaded()
    {
        switch (Language.GetLanguageID())
        {
            case 0:
                SSTools.ShowMessage(Translate.NameTextsChina[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 1:
                SSTools.ShowMessage(Translate.NameTextsDanish[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 2:
                SSTools.ShowMessage(Translate.NameTextsDutch[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 3:
                SSTools.ShowMessage(Translate.NameTextsEng[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 4:
                SSTools.ShowMessage(Translate.NameTextsFinnish[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 5:
                SSTools.ShowMessage(Translate.NameTextsFrench[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 6:
                SSTools.ShowMessage(Translate.NameTextsGerman[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 7:
                SSTools.ShowMessage(Translate.NameTextsItalian[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 8:
                SSTools.ShowMessage(Translate.NameTextsNorwegian[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 9:
                SSTools.ShowMessage(Translate.NameTextsPortuguese[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 10:
                SSTools.ShowMessage(Translate.NameTextsRU[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 11:
                SSTools.ShowMessage(Translate.NameTextsSpanishSpain[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 12:
                SSTools.ShowMessage(Translate.NameTextsSwedish[84], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
        }
    }

    void SoundClick()
    {
        TriggerDestroyter.SpawnSound(ClickButton);
    }
    void Preview()
    {
        ListerButtonBack.onClick.AddListener(BackToMainScreenBackgroundMusic);
        WorkShopUI.SetActive(false);
        PreviewModUI.SetActive(true);
        PreviewModUI.transform.Find(NameAWP).gameObject.SetActive(typeItem == TypeItem.Icon);
        
        switch (typeItem)
        {
            case TypeItem.Music:
#if UNITY_ANDROID && !UNITY_EDITOR
                string path = Application.persistentDataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3";
                WebClient ftp = new WebClient();
                ftp.BaseAddress = "ftp://files.000webhost.com";
                ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                Uri uri = new Uri("/Workshop/Music/" + NameItem + ".mp3", UriKind.Relative);
                ftp.DownloadFile(uri, path);
                tempAudioMusicFile = MusicZone.clip;
                GameObject Add = new GameObject();
                Add.name = "WorkshopMusic";
                Add.AddComponent<CorotineMusicPlayed>();
                Add.GetComponent<CorotineMusicPlayed>().gameController = Language.menugameController;
                Add.GetComponent<CorotineMusicPlayed>().pathToFile = path;
                break;
#else
                string path = Application.dataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3";
                WebClient ftp = new WebClient();
                ftp.BaseAddress = "ftp://files.000webhost.com";
                ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                Uri uri = new Uri("/Workshop/Music/" + NameItem + ".mp3", UriKind.Relative);
                ftp.DownloadFile(uri, path);
                tempAudioMusicFile = MusicZone.clip;
                GameObject Add = new GameObject();
                Add.name = "WorkshopMusic";
                Add.AddComponent<CorotineMusicPlayed>();
                Add.GetComponent<CorotineMusicPlayed>().gameController = Language.menugameController;
                Add.GetComponent<CorotineMusicPlayed>().pathToFile = path;
                break;
#endif

            case TypeItem.Background:
                    tempSpriteImgBackground = BackgroundMusic.GetComponent<Image>().sprite;
                    BackgroundMusic.GetComponent<Image>().sprite = IconItem;
                break;
            case TypeItem.Icon:
#if UNITY_ANDROID && !UNITY_EDITOR
                WebClient ftpAWP = new WebClient();
                ftpAWP.BaseAddress = "ftp://files.000webhost.com";
                ftpAWP.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                Uri uriMainTextureAWP = new Uri("/Workshop/SkinsAWP/" + NameItem + ".png", UriKind.Relative);
                ftpAWP.DownloadFile(uriMainTextureAWP, Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                byte[] data = File.ReadAllBytes(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                AWPPreSkin = new Texture2D(1024, 1024);
                AWPPreSkin.LoadImage(data);
                AWPMain.mainTexture = AWPPreSkin;
                break;
#else
                WebClient ftpAWP = new WebClient();
                ftpAWP.BaseAddress = "ftp://files.000webhost.com";
                ftpAWP.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
                Uri uriMainTextureAWP = new Uri("/Workshop/SkinsAWP/" + NameItem + ".png", UriKind.Relative);
                ftpAWP.DownloadFile(uriMainTextureAWP, Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                byte[] data = File.ReadAllBytes(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                AWPPreSkin = new Texture2D(1024, 1024);
                AWPPreSkin.LoadImage(data);
                AWPMain.mainTexture = AWPPreSkin;
                break;
#endif
        }
    }



    public void BackToMainScreenBackgroundMusic()
    {
        if (tempSpriteImgBackground)
            BackgroundMusic.GetComponent<Image>().sprite = tempSpriteImgBackground;
        if (tempAudioMusicFile)
        {
            MusicZone.clip = tempAudioMusicFile;
            MusicZone.Play();
#if UNITY_ANDROID && !UNITY_EDITOR
            File.Delete(Application.persistentDataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
#else
            File.Delete(Application.dataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
#endif
        }
        if (AWPPreSkin)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            File.Delete(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
#else
            File.Delete(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
#endif
        }
        PreviewModUI.transform.Find(NameAWP).gameObject.SetActive(true);
        ListerButtonBack.onClick.RemoveListener(BackToMainScreenBackgroundMusic);
    }

    void Like()
    {
        isLike = !isLike;
        Debug.Log(isLike);
        switch (isLike)
        {
            case true:
                CountLikes++;
                LikeItem.GetComponent<Image>().sprite = LikeStatus[1];
                UpdateLikesInServer();
                break;
            case false:
                CountLikes--;
                LikeItem.GetComponent<Image>().sprite = LikeStatus[0];
                UpdateLikesInServer();
                break;
        }
    }

    void DeleteMod()
    {
        if (isDownloaded)
        {
            switch (typeItem)
            {
                case TypeItem.Music:
#if UNITY_ANDROID && !UNITY_EDITOR
                    File.Delete(Application.persistentDataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#else
                    File.Delete(Application.dataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif

                case TypeItem.Background:
#if UNITY_ANDROID && !UNITY_EDITOR
                    File.Delete(Application.persistentDataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#else
                    File.Delete(Application.dataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif

                case TypeItem.Icon:
#if UNITY_ANDROID && !UNITY_EDITOR
                    File.Delete(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                    File.Delete(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + "Icon" + ".png");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#else
                    File.Delete(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png");
                    File.Delete(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + "Icon" + ".png");
                    isDownloaded = isCheckerDownloadMod();
                    break;
#endif

            }
        }
    }

    private void UpdateLikesInServer()
    {
        int IDWork = SearchIDWork();

        string datawork = WorkShopUI.GetComponent<Workshop>().DataWorks[IDWork];
        string[] buffernames = datawork.Split(' ');
        int startindexCountLikes = 0;
        int endindexCountLikes = 0;
        int count = 0;
        buffernames[1] = CountLikes.ToString();
        if (isLike)
        {
            foreach(char c in datawork)
            {
                if (c == ' ')
                {
                    startindexCountLikes++;
                    break;
                }
                else
                    startindexCountLikes++;
            }
            for(int i = startindexCountLikes; i < datawork.Length; i++)
                if(!int.TryParse(datawork[i].ToString(), out count))
                {
                    endindexCountLikes = i;
                    break;
                }
            if (endindexCountLikes == 0)
                endindexCountLikes = startindexCountLikes + 1;
            datawork = datawork.Remove(startindexCountLikes, endindexCountLikes - startindexCountLikes);
            datawork = datawork.Insert(startindexCountLikes, CountLikes.ToString());
            datawork += " " + Language.GetIDPlayer();
        }
        else
        {
            datawork = "";
            int IDRemovePlayerNickNameInLikeList = 0;
            if (buffernames.Length >= 2)
            {
                for (int i = 2; i <= buffernames.Length; i++)
                    if (buffernames[i] == Language.GetIDPlayer().ToString())
                    {
                        IDRemovePlayerNickNameInLikeList = i;
                        Debug.Log(IDRemovePlayerNickNameInLikeList);
                        break;
                    }
                for (int i = 0; i < buffernames.Length; i++)
                {
                    if (i != IDRemovePlayerNickNameInLikeList)
                        datawork += buffernames[i] + ' ';
                }
            }  
        }
        WorkShopUI.GetComponent<Workshop>().DataWorks[IDWork] = datawork;
        CreateFile(datawork);
    }

    private void CreateFile(string datawork)
    {
        string ftpurl = "";
        string typefile = ".txt";
        switch (typeItem)
        {
            case TypeItem.Music:
                ftpurl = PathToMusicsUploadsFiles + NameItem + typefile;
                break;
            case TypeItem.Background:
                ftpurl = PathToBackgroundUploadsFiles + NameItem + typefile;
                break;
            case TypeItem.Icon:
                ftpurl = PathToSkinsAWPUploadsFiles + NameItem + typefile;
                break;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        string temppath = Application.persistentDataPath + '/' + NameItem + typefile;
        File.AppendAllText(temppath, datawork);
        LoadFileInWorkshop(ftpurl, temppath, false, typefile);
        File.Delete(temppath);
#else
        string temppath = Application.dataPath + '/' + NameItem + typefile;
        File.AppendAllText(temppath, datawork);
        LoadFileInWorkshop(ftpurl, temppath, false, typefile);
        File.Delete(temppath);
#endif
    }

    private void LoadFileInWorkshop(string ftpfullpath, string pathToFile, bool isIcon, string typeFile)
    {
        string ftpurl = "";
        if (!isIcon)
            ftpurl = ftpfullpath;
        else
            ftpurl = PathToSkinsAWPUploadsFiles + NameItem + "Icon" + typeFile;

        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpurl);
        ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        ftp.KeepAlive = true;
        ftp.UseBinary = true;
        ftp.Proxy = null;
        ftp.Method = WebRequestMethods.Ftp.UploadFile;
        FileStream fs = File.OpenRead(pathToFile);
        byte[] buffer = new byte[fs.Length];
        fs.Read(buffer, 0, buffer.Length);
        fs.Close();
        Stream ftpstream = ftp.GetRequestStream();
        ftpstream.Write(buffer, 0, buffer.Length);
        ftpstream.Close();
    }


    private int SearchIDWork()
    {
        int IDWork = 0;

        for (int i = 0; i < WorkShopUI.GetComponent<Workshop>().NamesWorks.Count; i++)
        {
            if (WorkShopUI.GetComponent<Workshop>().NamesWorks[i].Contains(NameItem))
            {
                IDWork = i;
                break;
            }
        }

        return IDWork;
    }

    bool isCheckerLikePre()
    {
        bool isLike = false;
        int IDWork = SearchIDWork();
        string datawork = WorkShopUI.GetComponent<Workshop>().DataWorks[IDWork];
        string[] bufferwork = datawork.Split(' ');
        if(bufferwork.Length >=3)
            for(int i = 2; i < bufferwork.Length; i++)
            {
                if(bufferwork[i].Contains(Language.GetNickNamePlayer()))
                {
                    isLike = true;
                    break;
                }
            }
        else
            isLike = false;
        switch (isLike)
        {
            case true:
                LikeItem.GetComponent<Image>().sprite = LikeStatus[1];
                break;
            case false:
                LikeItem.GetComponent<Image>().sprite = LikeStatus[0];
                break;
        }
        return isLike;
    }

    bool isCheckerDownloadMod()
    {
        bool isDownloaded = false;
        switch (typeItem)
        {
            case TypeItem.Music:
#if UNITY_ANDROID && !UNITY_EDITOR
                if (File.Exists(Application.persistentDataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#else
                if (File.Exists(Application.dataPath + '/' + PahToMusicInDesktop + '/' + NameItem + ".mp3"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#endif

            case TypeItem.Background:
#if UNITY_ANDROID && !UNITY_EDITOR
                if (File.Exists(Application.persistentDataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#else
                if (File.Exists(Application.dataPath + '/' + PathToBackgroundInDesktop + '/' + NameItem + ".png"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#endif

            case TypeItem.Icon:
#if UNITY_ANDROID && !UNITY_EDITOR
                  if (File.Exists(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#else
                if (File.Exists(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + NameItem + ".png"))
                {
                    DownloadItem.GetComponent<Image>().sprite = DeleteModIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(DeleteMod);
                    isDownloaded = true;
                }
                else
                {
                    DownloadItem.GetComponent<Image>().sprite = DownloadIcon;
                    DownloadItem.onClick.RemoveAllListeners();
                    DownloadItem.onClick.AddListener(Download);
                    isDownloaded = false;
                }
                break;
#endif

        }
        return isDownloaded;
    }

}
