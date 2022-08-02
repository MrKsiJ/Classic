using UnityEngine;
using UnityEngine.UI;

public class WorkshopMusicPlayButton : MonoBehaviour
{
    const string NameMusicCloudGame = "MusicCloudGame";

     AudioSource MusicCloudGame;
    [SerializeField] internal AudioClip clip;
    void Start()
    {
        MusicCloudGame = GameObject.Find(NameMusicCloudGame).GetComponent<AudioSource>();
        gameObject.GetComponent<Button>().onClick.AddListener(PlayMusic);
    }

    void PlayMusic()
    {
        MusicCloudGame.clip = clip;
        MusicCloudGame.Play();
    }
}
