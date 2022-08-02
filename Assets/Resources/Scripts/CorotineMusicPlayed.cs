using UnityEngine;
public class CorotineMusicPlayed : MonoBehaviour
{
    internal MenuGameController gameController;
    internal string pathToFile;

    void Start()
    {
        StartCoroutine(gameController.LoadMusic(pathToFile));
    }

}
