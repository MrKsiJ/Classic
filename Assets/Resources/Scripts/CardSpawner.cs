using UnityEngine;
using static CardMechanic;
using static GameRules;
public class CardSpawner : MonoBehaviour {
    #region ConstCardSpawner
    const string PathToCard = "Prefabs/Card";
	const string CheckerPoints = "PointsSpawnCard";
    const int mininumChangeSpawn = 0, maxinumChangeSpawn = 100;
    #endregion
    #region BaseComponents
    float timerSpawnDown;

	GameObject points;
    GameRules gameRules;
    int[] changeSpawnCards;
	Transform[] pointsCardSpawn;
    #endregion
    [Header("__CARD SPAWNER_")]
    [Range(0, 10f)]
    public float timerSpawn;
    int ChangeSpawnCard = 0;
    void Start ()
    {
        timerSpawnDown = timerSpawn;
        gameRules = GetComponent<GameRules>();
        CheckerPointsSpawnCards();
    }

    private void CheckerPointsSpawnCards()
    {
        points = GameObject.Find(CheckerPoints);
        pointsCardSpawn = new Transform[points.transform.childCount];
        for (int i = 0; i < pointsCardSpawn.Length; i++)
            pointsCardSpawn[i] = points.transform.GetChild(i);
    }
    internal Transform[] GetPointsSpawnCards() { return pointsCardSpawn; }
    // Update is called once per frame
    void Update () 
    {
        if (gameRules.GetIsStartedGame() && !gameRules.GetIsPausedGame() && !gameRules.GameSchansUI.activeSelf)
        {
            if (timerSpawnDown > 0)
                timerSpawnDown -= 0.01f;
            else
                SpawnCard();
        }
    }

    private void SpawnCard()
    {
        CardMechanic card = Instantiate(Resources.Load<GameObject>(PathToCard), pointsCardSpawn[Random.Range(0, pointsCardSpawn.Length)].position, Quaternion.identity).GetComponent<CardMechanic>();
        card.gameRules = gameRules;
        CheckChangeAndSetupTypeCard(card);
        timerSpawnDown = timerSpawn;
    }

    private void CheckChangeAndSetupTypeCard(CardMechanic card)
    {
        int indexSpecifiedNumber = 0;
        ChangeSpawnCard = Random.Range(mininumChangeSpawn, maxinumChangeSpawn);
        //Заполняем массив рандомными числами.
        changeSpawnCards = new int [gameRules.changeSpawnCards.Length];
        for(int i = 0; i < changeSpawnCards.Length; i++)
            changeSpawnCards[i] = gameRules.changeSpawnCards[i].currentChange;

        for(int i = 0; i < changeSpawnCards.Length; i++)
            if (Mathf.Abs(changeSpawnCards[i] - ChangeSpawnCard) < Mathf.Abs(changeSpawnCards[indexSpecifiedNumber] - ChangeSpawnCard))
                indexSpecifiedNumber = i;
        if ((gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking) && gameRules.changeSpawnCards[indexSpecifiedNumber].typeCard == CardType.HealthCard)
            card.SelectedTypeCard(CardType.TimeCard);
        else if (gameRules.currentGameModeSelected == GameMode.Survival && gameRules.changeSpawnCards[indexSpecifiedNumber].typeCard == CardType.TimeCard && gameRules.GetHealthPlayer() < 3)
            card.SelectedTypeCard(CardType.HealthCard);
        else if (gameRules.currentGameModeSelected == GameMode.Survival && gameRules.changeSpawnCards[indexSpecifiedNumber].typeCard == CardType.MusicCard)
            card.SelectedTypeCard(CardType.ScoreCard);
        
        else if((gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking) && gameRules.GetTimerPlayer() < 10)
            card.SelectedTypeCard(CardType.TimeCard);
        else
            card.SelectedTypeCard(gameRules.changeSpawnCards[indexSpecifiedNumber].typeCard);
    }
}
