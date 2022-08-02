using UnityEngine;
using static GameRules;
public class ResultGameReturn : MonoBehaviour
{
    [SerializeField] GameRules gameRules;
    [SerializeField] GameObject Mode;

    public void ResultGameReturnMethod()
    {
        gameRules.GetResultGameUI().SetActive(true);
        if (gameRules.currentGameModeSelected == GameMode.MatchMaking)
            gameRules.GetResultGameUI().GetComponent<Animator>().Play("AnimationResult", 0, 0);
        else if(gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.Survival)
            gameRules.GetResultGameUI().GetComponent<Animator>().Play("AnimationResult2", 0, 0);
        Mode.SetActive(false);
    }
}
