using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChangedBackgroundGame : MonoBehaviour
{
    const string NameProfile = "Profile";
    const string NameOKIconItem = "OKIconItem";
    const string PathToAWPSkinsInDesktop = "Mods/AWPSkins";

    [SerializeField] internal PlayerProfille Profile;
    [SerializeField] internal Transform ParentWorkshopUncheker;
    [SerializeField] internal string nameItem,PathToModBackground;
    [SerializeField] protected int IDBackground;
    [SerializeField] protected bool isModSchablon,isAWPSkin;
    [SerializeField] internal Material AWPMat;

    [SerializeField] Texture2D SkinAWP;
    void Start()
    {
        Profile = GameObject.Find(NameProfile).GetComponent<PlayerProfille>(); 
    }
   
    internal void SetIDBackground(int id)
    {
        IDBackground = id;
    }
    public void SelectBackgroundButton()
    {
        if(Profile != null)
        {
            if (!isModSchablon)
            {
                SearchCurrentIDBackgroundSelected();
                Profile.SetCurrentIDBackground(IDBackground);
            }
            else
            {
                if(!isAWPSkin)
                    Profile.SetCurrentIDBackground(PathToModBackground);
                else
                {
                    if (ParentWorkshopUncheker)
                        ParentWorkshopUncheker.GetComponent<WorkshopSelectedItemsUnCheker>().UnCheckOK(this.transform);
                    transform.Find(NameOKIconItem).GetComponent<Image>().enabled = true;
                    Profile.SetCurrentAWPSkinSelected(nameItem);
                    AWPMat.mainTexture = SkinAWP;
                }
            }
               

            if(!isAWPSkin)
                Profile.GetBackgroundMusicObject().GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        }
    }

    internal void ModSelection(Texture2D tex,string nameItemMod,string PathToModBackground,bool isAWPSkin)
    {
        Profile = GameObject.Find(NameProfile).GetComponent<PlayerProfille>();
        Sprite Icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        GetComponent<Animator>().enabled = false;
        GetComponent<Image>().sprite = Icon;
        nameItem = nameItemMod;
        this.PathToModBackground = PathToModBackground;
        this.isAWPSkin = isAWPSkin;
        isModSchablon = true;
        if (isAWPSkin)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            SkinAWP = new Texture2D(1024, 1024);
            SkinAWP.LoadImage(File.ReadAllBytes(Application.persistentDataPath + '/' + PathToAWPSkinsInDesktop + '/' + nameItem + ".png"));
            if(string.IsNullOrEmpty(Profile.GetCurrentAWPSkinSelected()))
            {
                if(Profile.GetCurrentAWPSkinSelected() == nameItem)
                {
                    transform.Find(NameOKIconItem).GetComponent<Image>().enabled = true;
                    AWPMat.mainTexture = SkinAWP;
                }
            }
#else
            SkinAWP = new Texture2D(1024, 1024);
            SkinAWP.LoadImage(File.ReadAllBytes(Application.dataPath + '/' + PathToAWPSkinsInDesktop + '/' + nameItem + ".png"));
            if(!string.IsNullOrEmpty(Profile.GetCurrentAWPSkinSelected()))
            {
                Debug.Log(nameItem);
                if(Profile.GetCurrentAWPSkinSelected() == nameItem)
                {
                    transform.Find(NameOKIconItem).GetComponent<Image>().enabled = true;
                    AWPMat.mainTexture = SkinAWP;
                }
            }
#endif
        }    
    }

    private void SearchCurrentIDBackgroundSelected()
    {
        for (int i = 0; i < Translate.NameRewardsRU.Length; i++)
        {
            if (Translate.NameRewardsRU[i] == nameItem)
            {
                IDBackground = i;
                break;
            }
        }
    }
}
