using UnityEngine;
using UnityEngine.UI;

public class HitnsPreStartGame : MonoBehaviour
{
    [SerializeField] Text PreStartUI;
    [SerializeField] PlayerProfille playerProfille;
    [SerializeField] GameRules typeGameMode;

    int[] arrayBlueShift = {86,87,88 };
    int[] arraySurvival = {85,87,89 };
    int hitIndexID;
    // Start is called before the first frame update
    public void StartHit()
    {
        if(typeGameMode.currentGameModeSelected == GameRules.GameMode.BlueShift || typeGameMode.currentGameModeSelected == GameRules.GameMode.MatchMaking)
        {
            hitIndexID = Random.Range(0, arrayBlueShift.Length);
            hitIndexID = arrayBlueShift[hitIndexID];
        }
        else if(typeGameMode.currentGameModeSelected == GameRules.GameMode.Survival)
        {
            hitIndexID = Random.Range(0, arraySurvival.Length);
            hitIndexID = arraySurvival[hitIndexID];
        }
            
        
        LanguageHit(hitIndexID);
    }

    private void LanguageHit(int hitIndexID)
    {
        switch (playerProfille.GetLanguageID())
        {
            case 0:
                PreStartUI.text = Translate.NameTextsChina[hitIndexID];
                break;
            case 1:
                PreStartUI.text = Translate.NameTextsDanish[hitIndexID];
                break;
            case 2:
                PreStartUI.text = Translate.NameTextsDutch[hitIndexID];
                break;
            case 3:
                PreStartUI.text = Translate.NameTextsEng[hitIndexID];
                break;
            case 4:
                PreStartUI.text = Translate.NameTextsFinnish[hitIndexID];
                break;
            case 5:
                PreStartUI.text = Translate.NameTextsFrench[hitIndexID];
                break;
            case 6:
                PreStartUI.text = Translate.NameTextsGerman[hitIndexID];
                break;
            case 7:
                PreStartUI.text = Translate.NameTextsItalian[hitIndexID];
                break;
            case 8:
                PreStartUI.text = Translate.NameTextsNorwegian[hitIndexID];
                break;
            case 9:
                PreStartUI.text = Translate.NameTextsPortuguese[hitIndexID];
                break;
            case 10:
                PreStartUI.text = Translate.NameTextsRU[hitIndexID];
                break;
            case 11:
                PreStartUI.text = Translate.NameTextsSpanishSpain[hitIndexID];
                break;
            case 12:
                PreStartUI.text = Translate.NameTextsSwedish[hitIndexID];
                break;

        }
    }
}
