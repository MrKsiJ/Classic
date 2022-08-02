using UnityEngine;
using UnityEngine.UI;
using static GameRules;
public class RunGameMode : MonoBehaviour
{
    [SerializeField] GameRules gameRules;
    [SerializeField] ResultGame resultGame;
    [SerializeField] GameObject FireTrigger;
    public void RestartGame()
    {
        if (gameRules.GetIsPausedGame())
        {
            gameRules.SetPauseGame(false);
            resultGame.SetIsStopedGame(false);
            gameRules.RestartMusicForRestartGame();
        }
       resultGame.SetIsStopedGame(false);
       if(gameRules.GetModeSelected() != null)
            gameRules.GetModeSelected().transform.Find("PauseMenu").GetComponent<Button>().interactable = true;
        if (!FireTrigger.activeSelf)
            FireTrigger.SetActive(true);
        gameRules.ResetRecordComboGame();
        gameRules.MethodComboConnectPlayer(true,false);
        gameRules.MethodIsGameChanged(true);
        gameRules.gameObject.SetActive(true);
        gameRules.MethodScorePlayer(0, true);
        gameRules.MethodMoneyPlayer(0, '0');
        gameRules.MethodRandomClicked(0);
        gameRules.MethodCounterMusicSurvial(true);
        if (gameRules.currentGameModeSelected == GameMode.Survival)
            gameRules.MethodHealthPlayer(0, 0);
        else if (gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking)
            gameRules.MethodTimerPlayer(0, 0);
        gameRules.SelectedMusicCurrentText();
    }
}
