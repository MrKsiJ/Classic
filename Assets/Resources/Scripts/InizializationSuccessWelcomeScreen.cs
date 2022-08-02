using UnityEngine;

public class InizializationSuccessWelcomeScreen : MonoBehaviour
{
    [SerializeField] internal GameObject MainMenu,EnteringNickName,DallyUI;
    [SerializeField] PlayerProfille playerProfille;
    public void OpenMainMenu()
    {
        if (!playerProfille.GetTraningMode())
        {
            MainMenu.SetActive(true);
            DallyUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            EnteringNickName.SetActive(true);
            gameObject.SetActive(false);
        }
            
    }
}
