using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Workshop : MonoBehaviour
{
    const string PathToMusicsUploadsFiles = "ftp://files.000webhost.com/Workshop/Music/";
    const string PathToBackgroundUploadsFiles = "ftp://files.000webhost.com/Workshop/Backgrounds/";
    const string PathToSkinsAWPUploadsFiles = "ftp://files.000webhost.com/Workshop/SkinsAWP/";

    [SerializeField] private PlayerProfille profille;
    [SerializeField] private int CurrentSelectionWorkshopFilter = 0;
    [SerializeField] private GameObject workShopItem,PreviewModUI;
    [SerializeField] private Transform parentworkShopsItems;
    [SerializeField] internal List<string> NamesWorks = new List<string>();
    [SerializeField] private List<string> NamesWorksImgs = new List<string>();
    [SerializeField] private List<string> NamesWorksIcons = new List<string>();
    [SerializeField] internal List<string> DataWorks = new List<string>();

    [SerializeField] private Text[] ButtonsFitlerClicks;

    public void LoadDataWorkshop()
    {
        switch (CurrentSelectionWorkshopFilter)
        {
            case 0:
                NamesWorks = GetFileNamesListFTP(PathToMusicsUploadsFiles);
                NamesWorksImgs = GetFileNamesImamgesListFTP(PathToMusicsUploadsFiles, false);
                NamesWorksIcons = new List<string>();
                DataWorks = new List<string>();
                for (int i = 0; i < NamesWorks.Count; i++)
                    DataWorks.Add(GetLoadDataInFileFTP(PathToMusicsUploadsFiles, NamesWorks[i]));
                break;
            case 1:
                NamesWorks = GetFileNamesListFTP(PathToBackgroundUploadsFiles);
                NamesWorksImgs = GetFileNamesImamgesListFTP(PathToBackgroundUploadsFiles, false);
                NamesWorksIcons = new List<string>();
                DataWorks = new List<string>();
                for (int i = 0; i < NamesWorks.Count; i++)
                    DataWorks.Add(GetLoadDataInFileFTP(PathToBackgroundUploadsFiles, NamesWorks[i]));
                break;
            case 2:
                NamesWorks = GetFileNamesListFTP(PathToSkinsAWPUploadsFiles);
                NamesWorksIcons = GetFileNamesImamgesListFTP(PathToSkinsAWPUploadsFiles, true);
                NamesWorksImgs = GetFileNamesImamgesListFTP(PathToSkinsAWPUploadsFiles, false);
                DataWorks = new List<string>();
                for (int i = 0; i < NamesWorks.Count; i++)
                    DataWorks.Add(GetLoadDataInFileFTP(PathToSkinsAWPUploadsFiles, NamesWorks[i]));
                break;
        }
        SpawnWorkshopitems();
        LoadSS();
    }
    public void ClearListWorkshopContent()
    {
        for (int i = 0; i < parentworkShopsItems.childCount; i++)
            Destroy(parentworkShopsItems.transform.GetChild(i).gameObject);
    }
    private void SpawnWorkshopitems()
    {
        for (int i = 0; i < NamesWorks.Count; i++)
        {
            string[] buffernames = DataWorks[i].Split(' ');
            GameObject workshopitem = Instantiate(workShopItem);
            workshopitem.transform.SetParent(parentworkShopsItems);
            workshopitem.transform.localScale = new Vector3(1, 1, 1);
            WorkShopItem item = workshopitem.GetComponent<WorkShopItem>();
            item.NameItem = NamesWorks[i].Substring(0, NamesWorks[i].Length - 4);
            item.WorkShopUI = gameObject;
            item.PreviewModUI = PreviewModUI;
            item.NameAuthor = buffernames[0];
            item.CountLikes = int.Parse(buffernames[1]);
            switch (CurrentSelectionWorkshopFilter)
            {
                case 0:
                    item.typeItem = WorkShopItem.TypeItem.Music;
                    break;
                case 1:
                    item.typeItem = WorkShopItem.TypeItem.Background;
                    item.IconItem = GetLoadImgInWorkshopItem(PathToBackgroundUploadsFiles, NamesWorksImgs[i]);
                    break;
                case 2:
                    item.typeItem = WorkShopItem.TypeItem.Icon;
                    item.IconItem = GetLoadImgInWorkshopItem(PathToSkinsAWPUploadsFiles, NamesWorksIcons[i]);
                    break;
            }
            item.PreviewLoadItem();

        }
    }

    internal void LoadSS()
    {
        switch (profille.GetLanguageID())
        {
            case 0:
                SSTools.ShowMessage(Translate.NameTextsChina[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 1:
                SSTools.ShowMessage(Translate.NameTextsDanish[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 2:
                SSTools.ShowMessage(Translate.NameTextsDutch[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 3:
                SSTools.ShowMessage(Translate.NameTextsEng[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 4:
                SSTools.ShowMessage(Translate.NameTextsFinnish[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 5:
                SSTools.ShowMessage(Translate.NameTextsFrench[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 6:
                SSTools.ShowMessage(Translate.NameTextsGerman[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 7:
                SSTools.ShowMessage(Translate.NameTextsItalian[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 8:
                SSTools.ShowMessage(Translate.NameTextsNorwegian[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 9:
                SSTools.ShowMessage(Translate.NameTextsPortuguese[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 10:
                SSTools.ShowMessage(Translate.NameTextsRU[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 11:
                SSTools.ShowMessage(Translate.NameTextsSpanishSpain[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
            case 12:
                SSTools.ShowMessage(Translate.NameTextsSwedish[81], SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
        }
    }

    private List<string> GetFileNamesListFTP(string ftpurl)
    {
        //result data from file
        List<string> file_list = new List<string>();
        //db ftpbrequit
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl);
        request.Method = WebRequestMethods.Ftp.ListDirectory;
        //set up credentials
        request.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        //initialize ftp resopnse
        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //open renders
        using (Stream responseStream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(".txt"))
                        file_list.Add(line);
                }
            };
        };
        return file_list;
    }

    private List<string> GetFileNamesImamgesListFTP(string ftpurl,bool isIcon)
    {
        //result data from file
        List<string> file_list = new List<string>();
        //db ftpbrequit
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl);
        request.Method = WebRequestMethods.Ftp.ListDirectory;
        //set up credentials
        request.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        //initialize ftp resopnse
        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //open renders
        using (Stream responseStream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (((line.Contains(".png") || line.Contains(".jpg")) && !isIcon) || (isIcon && line.Contains("Icon")))
                        file_list.Add(line);
                }
            };
        };
        return file_list;
    }

    private string GetLoadDataInFileFTP(string ftpurl,string filename)
    {
        string data = "";
        //db ftpbrequit
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl+filename);
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        //set up credentials
        request.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        //initialize ftp resopnse
        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //open renders
        using (Stream responseStream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(responseStream))
            {
                data = reader.ReadToEnd();
            };
        };
        return data;
    }

    private Sprite GetLoadImgInWorkshopItem(string ftpurl,string filename)
    {
        Sprite img;
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + filename);
        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = true;
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.Credentials = new NetworkCredential("classic-workshop", "123456zimaqwerty");
        //initialize ftp resopnse
        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        byte[] downloadAsbyteArray = new byte[1];
        Stream input = response.GetResponseStream();
        byte[] buffer = new byte[16 * 1024];
        MemoryStream ms = new MemoryStream();
        int read;
        while (input.CanRead && (read = input.Read(buffer, 0, buffer.Length)) > 0)
            ms.Write(buffer, 0, read);
        downloadAsbyteArray = ms.ToArray();

        Texture2D tex = new Texture2D(640, 1136);
        tex.LoadImage(downloadAsbyteArray);
        img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return img;
    }



    public void ResetFilter()
    {
        CurrentSelectionWorkshopFilter = 0;
        UIChangedFilter();
    }

    public void FilterChanged(int filterID)
    {
        CurrentSelectionWorkshopFilter = filterID;
        UIChangedFilter();
        ClearListWorkshopContent();
        LoadDataWorkshop();
    }

    private void UIChangedFilter()
    {
        foreach (Text B in ButtonsFitlerClicks)
        {
            if (B.name == ButtonsFitlerClicks[CurrentSelectionWorkshopFilter].name)
                B.color = new Color(0.895999f, 0, 1, 1);
            else
                B.color = new Color(0.895999f, 0, 1, 0.5019608f);
        }
    }
}
