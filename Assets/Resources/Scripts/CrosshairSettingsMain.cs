using UnityEngine;
using UnityEngine.UI;
public class CrosshairSettingsMain : MonoBehaviour
{
    [SerializeField] private Scrollbar Thickness, Distance, OutLine, Red, Blue, Green;
    [SerializeField] private Text ThicknessValue, DistanceValue, OutLineValue, RedValue, BlueValue, GreenValue;
    [SerializeField] private Toggle TObrazz;
    [SerializeField] private GameObject[] Crossings;
    [SerializeField] private GameObject[] CrossingsT;
    [SerializeField] private GameObject[] ControlPoint;
    [SerializeField] private PlayerProfille playerProfille;

    float oldThickness, oldDistance, oldOutLine, oldRed, oldBlue, oldGreen;
    bool oldisOn;

    internal void LoadSettings(float ThicknessValue,float DistanceValue,float OutlineValue,float RedValue,float BlueValue,float GreenValue,bool isOn)
    {
        Thickness.value = ThicknessValue;
        Distance.value = DistanceValue;
        OutLine.value = OutlineValue;
        Red.value = RedValue;
        Blue.value = BlueValue;
        Green.value = GreenValue;
        TObrazz.isOn = isOn;
        ChangedColor();
        ChangedThickes();
        ChangedDistance();
        ChangedOutLine();
    }
    public void OldSetup()
    {
        oldThickness = Thickness.value;
        oldDistance = Distance.value;
        oldOutLine = OutLine.value;
        oldRed = Red.value;
        oldBlue = Blue.value;
        oldGreen = Green.value;
        oldisOn = TObrazz.isOn;
        ChangedColor();
        ChangedThickes();
        ChangedDistance();
        ChangedOutLine();
    }
    public void DeSave()
    {
        Thickness.value = oldThickness;
        Distance.value = oldDistance;
        OutLine.value = oldOutLine;
        Red.value = oldRed;
        Blue.value = oldBlue;
        Green.value = oldGreen;
        TObrazz.isOn = oldisOn;
    }

    public void SaveSettings()
    {
        playerProfille.SetThickness(Thickness.value);
        playerProfille.SetDistance(Distance.value);
        playerProfille.SetOutLine(OutLine.value);
        playerProfille.SetRedColor(Red.value);
        playerProfille.SetBlueColor(Blue.value);
        playerProfille.SetGreenColor(Green.value);
        playerProfille.SetTObrazz(TObrazz.isOn);
    }

    public void ChangedThickes()
    {
        for(int i = 0; i < Crossings.Length; i++)
            Crossings[i].transform.localScale = new Vector3(Thickness.value * 2f, Thickness.value * 2f, Thickness.value * 2f);
        ThicknessValue.text = Thickness.value.ToString();
    }
    public void ChangedDistance()
    {
        Crossings[0].transform.localPosition = new Vector3(0, (Distance.value * 305.4185f), 0);
        Crossings[1].transform.localPosition = new Vector3(0, -(Distance.value * 305.4185f), 0);
        Crossings[2].transform.localPosition = new Vector3(-(Distance.value * 305.4185f), 0, 0);
        Crossings[3].transform.localPosition = new Vector3((Distance.value * 305.4185f), 0, 0);
        DistanceValue.text = Distance.value.ToString();
    }
    public void ChangedOutLine()
    {
        for (int i = 0; i < Crossings.Length; i++)
            Crossings[i].GetComponent<Outline>().effectDistance = new Vector2(OutLine.value * 2f, -(OutLine.value * 2f));
        OutLineValue.text = OutLine.value.ToString();
    }
    public void ChangedColor()
    {
        for (int i = 0; i < Crossings.Length; i++)
            Crossings[i].GetComponent<Image>().color = new Color(Red.value, Green.value, Blue.value);
        for(int i = 0; i < ControlPoint.Length; i++)
            ControlPoint[i].GetComponent<Image>().color = new Color(Red.value, Green.value, Blue.value);
        RedValue.text = Red.value.ToString();
        BlueValue.text = Blue.value.ToString();
        GreenValue.text = Green.value.ToString();
    }
    public void ChangedT()
    {
        for (int i = 0; i < CrossingsT.Length; i++)
            CrossingsT[i].gameObject.SetActive(!TObrazz.isOn);
    }
    
}
