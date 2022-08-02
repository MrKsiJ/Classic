using System;
using System.Net.NetworkInformation;
using System.Collections;
using GracesGames.SimpleFileBrowser.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;

public class AddWorkShopItem : MonoBehaviour
{

    const string PathToMusicsUploadsFiles = "ftp://files.000webhost.com/Workshop/Music/";
    const string PathToBackgroundUploadsFiles = "ftp://files.000webhost.com/Workshop/Backgrounds/";
    const string PathToSkinsAWPUploadsFiles = "ftp://files.000webhost.com/Workshop/SkinsAWP/";


    const string HeaderSelectFile = "HeaderSelectFileText";
    const string HeaderSelectFileIcon = "HeaderSelectFileIconText";
    const string HeaderLoadTextOtInDo = "HeaderLoadTextOtInDo";
    const string HeaderSpeedInternetText = "HeaderSpeedInternetText";

    [SerializeField] private Dropdown dropdownList;
    [SerializeField] private GameObject[] HeadersFileLoadInWorkshopAndIcon;
    [SerializeField] private GameObject HeaderNameWork,DownloadButton,BacktoButton;
    [SerializeField] private GameObject ProgressBar;
    [SerializeField] private Button FileButton, IconButton;
    [SerializeField] private DemoCaller BrowseFileManager;
    [SerializeField] private PlayerProfille profille;
    [SerializeField] internal Text FileNameText, IconNameText;

    private NetworkInterface[] nicArr;

    [SerializeField] internal string pathToFile;
    [SerializeField] internal string pathToIcon;

    [SerializeField] internal bool isSearchFileInAddWorkshopItem;
    [SerializeField] internal bool isSelectedFileWorkshop, isSelectedIconWorkshop;
    [SerializeField] internal bool isAWPSkin;

    void FixedUpdate()
    {
        HeaderNameWorkInputField();
        DownloadButton.SetActive(HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text.Length >= 3);
    }




   
    private void HeaderNameWorkInputField()
    {
        if ((dropdownList.value == 0 || dropdownList.value == 1) && isSelectedFileWorkshop || (dropdownList.value == 2 && isSelectedFileWorkshop && isSelectedIconWorkshop))
            HeaderNameWork.SetActive(true);
        else
            HeaderNameWork.SetActive(false);
    }

    public void DownloadOnServer()
    {
        string ftpfullpath = "";
        string typeFile = "";
        ProgressBar.SetActive(true);
        dropdownList.interactable = false;
        typeFile = pathToFile.Substring(pathToFile.Length - 4);
        DownloadButton.GetComponent<Button>().interactable = false;
        BacktoButton.GetComponent<Button>().interactable = false;
        FileButton.interactable = false;
        IconButton.interactable = false;

        switch (dropdownList.value)
        {
            case 0:
                ftpfullpath = PathToMusicsUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typeFile;
                break;
            case 1:
                ftpfullpath = PathToBackgroundUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typeFile;
                break;
            case 2:
                ftpfullpath = PathToSkinsAWPUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typeFile;
                break;
        }

        dropdownList.interactable = true;
        FileButton.interactable = true;
        IconButton.interactable = true;
        DownloadButton.GetComponent<Button>().interactable = true;
        DownloadButton.SetActive(false);
        BacktoButton.GetComponent<Button>().interactable = true;
        switch (profille.GetLanguageID())
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
        LoadFileInWorkshop(ftpfullpath,pathToFile,false, typeFile);
        if (dropdownList.value == 2)
            LoadFileInWorkshop(ftpfullpath,pathToIcon,true,typeFile);
        CreateFile();
    }

    private void CreateFile()
    {
        string ftpurl = "";
        string typefile = ".txt";
        switch (dropdownList.value)
        {
            case 0:
                ftpurl = PathToMusicsUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typefile;
                break;
            case 1:
                ftpurl = PathToBackgroundUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typefile;
                break;
            case 2:
                ftpurl = PathToSkinsAWPUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typefile;
                break;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        string temppath = Application.persistentDataPath + '/' + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typefile;
        File.AppendAllText(temppath,profille.GetNickNamePlayer() + "\n" + 0);
        LoadFileInWorkshop(ftpurl, temppath, false, typefile);
        File.Delete(temppath);
#else
        string temppath = Application.dataPath + '/' + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + typefile;
        File.AppendAllText(temppath, profille.GetNickNamePlayer() + ' ' + 0);
        LoadFileInWorkshop(ftpurl, temppath, false, typefile);
        File.Delete(temppath);
#endif
    }

    private void LoadFileInWorkshop(string ftpfullpath,string pathToFile,bool isIcon,string typeFile)
    {
        string ftpurl = "";
        if (!isIcon)
            ftpurl = ftpfullpath;
        else
            ftpurl = PathToSkinsAWPUploadsFiles + HeaderNameWork.transform.GetChild(0).GetComponent<InputField>().text + "Icon" + typeFile;
        
        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpurl);
        ftp.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        ftp.KeepAlive = true;
        ftp.UseBinary = true;
        ftp.Proxy = null;
        ftp.Method = WebRequestMethods.Ftp.UploadFile;
        using (FileStream fs = File.OpenRead(pathToFile))
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            ProgressBar.GetComponent<Slider>().value = ProgressBar.GetComponent<Slider>().maxValue = buffer.Length;
            nicArr = NetworkInterface.GetAllNetworkInterfaces();
            switch (profille.GetLanguageID())
            {
                case 0:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsChina[81] + buffer.Length + Translate.NameTextsChina[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsChina[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 1:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsDanish[81] + buffer.Length + Translate.NameTextsDanish[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsDanish[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 2:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsDutch[81] + buffer.Length + Translate.NameTextsDutch[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsDutch[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 3:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsEng[81] + buffer.Length + Translate.NameTextsEng[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsEng[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 4:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsFinnish[81] + buffer.Length + Translate.NameTextsFinnish[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsFinnish[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 5:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsFrench[81] + buffer.Length + Translate.NameTextsFrench[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsFrench[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 6:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsGerman[81] + buffer.Length + Translate.NameTextsGerman[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsGerman[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 7:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsItalian[81] + buffer.Length + Translate.NameTextsItalian[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsItalian[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 8:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsNorwegian[81] + buffer.Length + Translate.NameTextsNorwegian[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsNorwegian[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 9:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsPortuguese[81] + buffer.Length + Translate.NameTextsPortuguese[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsPortuguese[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 10:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsRU[81] + buffer.Length + Translate.NameTextsRU[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsRU[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 11:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsSpanishSpain[81] + buffer.Length + Translate.NameTextsSpanishSpain[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsSpanishSpain[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
                case 12:
                    ProgressBar.transform.Find(HeaderLoadTextOtInDo).GetComponent<Text>().text = Translate.NameTextsSwedish[81] + buffer.Length + Translate.NameTextsSwedish[82] + buffer.Length;
                    ProgressBar.transform.Find(HeaderSpeedInternetText).GetComponent<Text>().text = Translate.NameTextsSwedish[83] + nicArr[nicArr.Length - 1].Speed.ToString();
                    break;
            }
            using(Stream ftpstream = ftp.GetRequestStream())
            {
                ftpstream.Write(buffer, 0, buffer.Length);
            };
        };
    }


    private void NullPathFileAndIconBackToLanguageP()
    {
        switch (profille.GetLanguageID())
        {
            case 0:
                FileNameText.text = Translate.NameTextsChina[79];
                break;
            case 1:
                FileNameText.text = Translate.NameTextsDanish[79];
                break;
            case 2:
                FileNameText.text = Translate.NameTextsDutch[79];
                break;
            case 3:
                FileNameText.text = Translate.NameTextsEng[79];
                break;
            case 4:
                FileNameText.text = Translate.NameTextsFinnish[79];
                break;
            case 5:
                FileNameText.text = Translate.NameTextsFrench[79];
                break;
            case 6:
                FileNameText.text = Translate.NameTextsGerman[79];
                break;
            case 7:
                FileNameText.text = Translate.NameTextsItalian[79];
                break;
            case 8:
                FileNameText.text = Translate.NameTextsNorwegian[79];
                break;
            case 9:
                FileNameText.text = Translate.NameTextsPortuguese[79];
                break;
            case 10:
                FileNameText.text = Translate.NameTextsRU[79];
                break;
            case 11:
                FileNameText.text = Translate.NameTextsSpanishSpain[79];
                break;
            case 12:
                FileNameText.text = Translate.NameTextsSwedish[79];
                break;
        }
        switch (profille.GetLanguageID())
        {
            case 0:
                IconNameText.text = Translate.NameTextsChina[79];
                break;
            case 1:
                IconNameText.text = Translate.NameTextsDanish[79];
                break;
            case 2:
                IconNameText.text = Translate.NameTextsDutch[79];
                break;
            case 3:
                IconNameText.text = Translate.NameTextsEng[79];
                break;
            case 4:
                IconNameText.text = Translate.NameTextsFinnish[79];
                break;
            case 5:
                IconNameText.text = Translate.NameTextsFrench[79];
                break;
            case 6:
                IconNameText.text = Translate.NameTextsGerman[79];
                break;
            case 7:
                IconNameText.text = Translate.NameTextsItalian[79];
                break;
            case 8:
                IconNameText.text = Translate.NameTextsNorwegian[79];
                break;
            case 9:
                IconNameText.text = Translate.NameTextsPortuguese[79];
                break;
            case 10:
                IconNameText.text = Translate.NameTextsRU[79];
                break;
            case 11:
                IconNameText.text = Translate.NameTextsSpanishSpain[79];
                break;
            case 12:
                IconNameText.text = Translate.NameTextsSwedish[79];
                break;
        }
    }

    public void OnActionEventDropListChanged()
    {
        switch (dropdownList.value)
        {
            case 0:
                ActivHeadrFileLoadWorkshopText();
                HeaderSelectIconFileText(false);
                BrowseFileHeader(0);
                NullPathFileAndIconBackToLanguageP();
                ChangedButtonsAndTexts(false);
                break;
            case 1:
                ActivHeadrFileLoadWorkshopText();
                HeaderSelectIconFileText(false);
                BrowseFileHeader(1);
                NullPathFileAndIconBackToLanguageP();
                ChangedButtonsAndTexts(false);
                break;
            case 2:
                ActivHeadrFileLoadWorkshopText();
                BrowseFileHeader(1);
                NullPathFileAndIconBackToLanguageP();
                ChangedButtonsAndTexts(false);
                HeaderSelectIconFileText(true);
                break;
        }
    }

    private void BrowseFileHeader(int header)
    {
        switch (header)
        {
            case 0:
                BrowseFileManager.FileExtensions = new string[3];
                BrowseFileManager.FileExtensions[0] = "mp3";
                BrowseFileManager.FileExtensions[1] = "wav";
                BrowseFileManager.FileExtensions[2] = "ogg";
                break;
            case 1:
                BrowseFileManager.FileExtensions = new string[2];
                BrowseFileManager.FileExtensions[0] = "jpg";
                BrowseFileManager.FileExtensions[1] = "png";
                break;
        }
    }

    public void OnActionButtonBrowser(bool isAWPSkin)
    {
        BrowseFileManager.OpenFileBrowser();
        BrowseFileManager.FileBrowserPrefab.GetComponent<FileBrowser>().addWorkItem = this;
        isSearchFileInAddWorkshopItem = true;
        this.isAWPSkin = isAWPSkin;
    }



    private void ActivHeadrFileLoadWorkshopText()
    {
        foreach (GameObject T in HeadersFileLoadInWorkshopAndIcon)
            if (T.name == HeaderSelectFile && !T.activeSelf)
                T.SetActive(true);
    }

    private void HeaderSelectIconFileText(bool isAcitve)
    {
        foreach (GameObject T in HeadersFileLoadInWorkshopAndIcon)
            if (T.name == HeaderSelectFileIcon)
                T.SetActive(isAcitve);
    }

    public void ChangedButtonsAndTexts(bool isActive)
    {
        if (!isActive)
        {
            ProgressBar.SetActive(false);
            NullPathFileAndIconBackToLanguageP();
            pathToIcon = "";
            pathToFile = "";
            isSelectedIconWorkshop = false;
            isSelectedFileWorkshop = false;
        }
        foreach (GameObject T in HeadersFileLoadInWorkshopAndIcon)
            T.SetActive(isActive);
    }
    public void DropDownListReturn()
    {
        dropdownList.value = 0;
    }
}
