using UnityEngine;
using static GameRules;
using static CardMechanic;
public class TriggerDestroyter : MonoBehaviour {

    [SerializeField] GameRules gameRules;
    [SerializeField] CardSpawner cardSpawner;
    [SerializeField] PlayerProfille playerProfille;
    Transform[] pointsSpawnCards;
    void OnTriggerEnter2D(Collider2D card)
	{
		if (card.gameObject.tag == "Card")
            CardChecker(card);
        if (card.GetComponent<CardDestoyterMain>())
            Destroy(card.GetComponent<CardDestoyterMain>().CardMain);
    }

    private void CardChecker(Collider2D card)
    {
        CardMechanic cardM = card.GetComponent<CardMechanic>();
        if (cardM.GetCurrentOST() < cardM.GetMaxClicked() && cardM.GetTypeCard() != CardType.BombCard && cardM.GetTypeCard() != CardType.MoneyCard && cardM.GetTypeCard() != CardType.HealthCard && cardM.GetTypeCard() != CardType.TimeCard && cardM.GetTypeCard() != CardType.MagnitCard && cardM.GetTypeCard() != CardType.FreezingCard)
        {
            if (cardM.gameRules.currentGameModeSelected == GameMode.Survival)
                cardM.gameRules.MethodHealthPlayer(1, -1);
            else if (cardM.gameRules.currentGameModeSelected == GameMode.BlueShift || cardM.gameRules.currentGameModeSelected == GameMode.MatchMaking)
                cardM.gameRules.MethodTimerPlayer(cardM.GetMaxClicked() - cardM.GetCurrentOST(), -1);
            SpawnSound(cardM.GetMoreClicks());
            bool russian = playerProfille.GetLanguageID() == 10;
            CameraShake.Shake(0.5f, 0.5f);
        }
        gameRules.MagnitCardEvent -= card.GetComponent<CardMechanic>().OnMouseDown;
        if (pointsSpawnCards == null)
            pointsSpawnCards = cardSpawner.GetPointsSpawnCards();
        card.GetComponent<CardMechanic>().gameRules.MagnitCardEvent -= card.GetComponent<CardMechanic>().OnMouseDown;
        Destroy(card.gameObject);
    }

    void Update()
    {
        if (!gameRules.GetIsStartedGame())
            gameObject.SetActive(false);
    }
    private int SoundGameKerenPlay()
    {
        int rnd = Random.Range(0, 2);
        return rnd;
    }

    internal static void SpawnSound(AudioClip clip)
    {
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = clip;
        Sound.GetComponent<AudioSource>().volume = 0.5f;
        Sound.GetComponent<AudioSource>().Play();
        Sound.AddComponent<DestoryGameObject>();
    }
    internal static void SpawnSound(AudioClip clip,float volume)
    {
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = clip;
        Sound.GetComponent<AudioSource>().volume = volume;
        Sound.GetComponent<AudioSource>().Play();
        Sound.AddComponent<DestoryGameObject>();
    }
}
