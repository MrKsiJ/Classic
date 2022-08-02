using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameRules;
public class CardMechanic : MonoBehaviour {
    internal enum CardType
    {
        ScoreCard,
        BombCard,
        MoneyCard,
        MusicCard,
        MagnitCard,
        TimeCard,
        HealthCard,
        FreezingCard,
    }


    #region ConstCardRegion
    const float ScaleCard = 0.75f;
    const float MaxScaleCard = 1.1f;
    const string NameTextCountClicks = "TextCountClicks";
    const string NameTypeCard = "TypeCard";
    const string NameMusicEffect = "MusicEffect";
    const string NameMoneyEffect = "MoneyEffect";
    const string NameMusicCloudGame = "MusicCloudGame";
    const string NameProfile = "Profile";
    const string PathToExplosionEffect = "Prefabs/BoomEffect";
    const int minBombClicked = 1, maxBombClicked = 2;
    const float timerStopedCard = 1.05f;
    #endregion
    #region BaseComponents
    TextMesh CountClickedText;
    AudioSource music;
    // 0 - xScale 1 - yScale 2 - GravityScale
    float[] samples = new float[512];
    internal GameRules gameRules;
    internal PlayerProfille playerProfille;
    internal ScreenMechanic screenMechanic;
    internal bool isGravityStoped;
	Animator CardAnim;
    Color colorText, colorCard, colorTypeCard;
    int currentClicked,maxClicked,countMissClicks;
    float timerStopedCardDown = timerStopedCard;
    bool isMusicOff,russian;
    #endregion

    [Header("_Card Mechanic_")]
    [SerializeField] CardType typeCard;
    [SerializeField] GameObject CrashEffectCard;
    [SerializeField] AudioClip Clicked,MoreClicks,BeatifulClickeds,HealthClicks,TimerClicks, MusicRock;
    [SerializeField] internal Sprite[] IconsTypeCard;
    [SerializeField] internal List<CardMechanic> cards;
    [SerializeField] internal bool isReadyClicked,isMagnitCardNopClicked,isCardCrosshairAWP;


    internal void Start()
    {
        SearchBaseComponentsCard();
        StartWorkCard();
    }

    void StartWorkCard()
    {
        if (typeCard != CardType.BombCard)
            maxClicked = Random.Range(gameRules.GetMinClickedNumber(), gameRules.GetMaxClickedNumber());
        else if (typeCard != CardType.MoneyCard)
            maxClicked = Random.Range(minBombClicked, maxBombClicked);
        else
            maxClicked = Random.Range(gameRules.GetMinMoneyClickedNumber(), gameRules.GetMaxMoneyClickedNumber());
        CountClickedText.text = maxClicked.ToString();
        currentClicked = 0;
    }
    internal void SelectedTypeCard(CardType HashCode)
    {
        typeCard = HashCode;
    }
    void SearchBaseComponentsCard()
    {
        playerProfille = GameObject.Find(NameProfile).GetComponent<PlayerProfille>();
        CountClickedText = transform.Find(NameTextCountClicks).GetComponent<TextMesh>();
        timerStopedCardDown = timerStopedCard;
        isGravityStoped = true;
        colorText = CountClickedText.color;
        colorCard = GetComponent<SpriteRenderer>().color;
        colorTypeCard = transform.Find(NameTypeCard).GetComponent<SpriteRenderer>().color;
        music = GameObject.Find(NameMusicCloudGame).GetComponent<AudioSource>();
        CardAnim = GetComponent<Animator>();
    }

    void Update()
	{
		RefreshTextAndColorForCard ();
	}

	//Trigger
	internal void OnMouseDown()
    {
        try
        {
            if (gameRules != null)
            {
                if (!gameRules.GetIsPausedGame())
                {
                    if (typeCard != CardType.MusicCard)
                        gameRules.MethodComboConnectPlayer(false, false);
                }
            }
            if (!GetComponent<Animator>().enabled)
                GetComponent<Animator>().enabled = true;

            if (!gameRules.GetIsPausedGame())
            {
                if (typeCard != CardType.MusicCard)
                    AnimationEventCard("Clicked");
                MainEventCard();
            }
        }
        catch(System.Exception e)
        {

        }
           
    }

    internal void ScriptMoment()
    {
        if (typeCard != CardType.MusicCard)
            AnimationEventCard("Clicked");
        MainEventCard();
    }

    private void MainEventCard()
    {
        currentClicked++;
        if (typeCard == CardType.MagnitCard)
        {
            if (gameRules != null)
                gameRules.MethodCardEvent();
        }
        if(currentClicked > maxClicked)
        {
            countMissClicks = currentClicked - maxClicked;
            if (gameRules.currentGameModeSelected == GameMode.Survival)
            {
                if (typeCard == CardType.BombCard || typeCard == CardType.ScoreCard || typeCard == CardType.MagnitCard)
                {
                    gameRules.MethodHealthPlayer(1,-1);
                    gameRules.MethodComboConnectPlayer(true,true);
                    gameRules.GetComboCounterText().transform.GetChild(0).GetComponent<Scrollbar>().size = 0;
                    TriggerDestroyter.SpawnSound(MoreClicks);
                    CameraShake.Shake(0.5f, 0.5f);
                }
            }
            else if(gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking)
            {
                if (typeCard == CardType.BombCard || typeCard == CardType.ScoreCard || typeCard == CardType.MagnitCard)
                {
                    gameRules.MethodTimerPlayer(countMissClicks, -1);
                    gameRules.MethodComboConnectPlayer(true, true);
                    gameRules.GetComboCounterText().transform.GetChild(0).GetComponent<Scrollbar>().size = 0;
                    CameraShake.Shake(0.5f, 0.5f);
                    TriggerDestroyter.SpawnSound(MoreClicks);
                }
            }
        }
        else
        {
            if (!GetComponent<AudioSource>().isPlaying && typeCard != CardType.MusicCard && typeCard != CardType.BombCard)
                TriggerDestroyter.SpawnSound(Clicked);
            if(maxClicked - currentClicked == 0)
            switch (typeCard)
            {
                case CardType.BombCard:
                    if (gameRules.currentGameModeSelected == GameMode.Survival)
                        {
                            gameRules.MethodHealthPlayer(1, -1);
                            gameRules.MethodComboConnectPlayer(true, true);
                            gameRules.GetComboCounterText().transform.GetChild(0).GetComponent<Scrollbar>().size = 0;
                        }

                    else if (gameRules.currentGameModeSelected == GameMode.BlueShift || gameRules.currentGameModeSelected == GameMode.MatchMaking)
                        {
                            gameRules.MethodTimerPlayer(countMissClicks, -1);
                            gameRules.MethodComboConnectPlayer(true, true);
                            gameRules.GetComboCounterText().transform.GetChild(0).GetComponent<Scrollbar>().size = 0;
                        }
                        Instantiate(Resources.Load<GameObject>(PathToExplosionEffect), transform.position, Quaternion.identity);
                        CameraShake.Shake(1, 1);
                     gameRules.MagnitCardEvent -= gameObject.GetComponent<CardMechanic>().OnMouseDown;
                     Destroy(gameObject);
                    break;
                case CardType.ScoreCard:
                    gameRules.MethodScorePlayer(currentClicked, false);
                    TriggerDestroyter.SpawnSound(BeatifulClickeds);
                    break;
                case CardType.MusicCard:
                    gameRules.MethodScorePlayer(currentClicked, false);
                    break;
                case CardType.HealthCard:
                    gameRules.MethodHealthPlayer(1, 1);
                    TriggerDestroyter.SpawnSound(HealthClicks);

                    break;
                case CardType.MagnitCard:
                    gameRules.MethodScorePlayer(currentClicked, false);
                    TriggerDestroyter.SpawnSound(BeatifulClickeds);
                    break;
                case CardType.MoneyCard:
                    gameRules.MethodMoneyPlayer(currentClicked, '+');
                    TriggerDestroyter.SpawnSound(BeatifulClickeds);
                    break;
                case CardType.TimeCard:
                    gameRules.MethodTimerPlayer(currentClicked, 1);
                    TriggerDestroyter.SpawnSound(TimerClicks);
                    break;
                    case CardType.FreezingCard:
                        if (!gameRules.GetGameFrezzed())
                        {
                            gameRules.SetGameFrezzed(true);
                            gameRules.UpdateTimerFreezed();
                        }
                        else
                        {
                            gameRules.UpdateTimerFreezed();
                            gameRules.MethodScorePlayer(currentClicked, false);
                            TriggerDestroyter.SpawnSound(BeatifulClickeds);
                        }
                        break;
            }
        }

        if(currentClicked <= maxClicked && typeCard == CardType.MusicCard)
        {
            isMusicOff = false;
            if (!transform.Find(NameMusicEffect).gameObject.activeSelf)
                transform.Find(NameMusicEffect).gameObject.SetActive(!isMusicOff);
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().pitch = 1f;
                GetComponent<AudioSource>().volume = 0.5f;
                GetComponent<AudioSource>().loop = !isMusicOff;
                GetComponent<AudioSource>().clip = MusicRock;
                GetComponent<AudioSource>().Play();
            }
            if(GetComponent<AudioSource>().volume < 1f)
                GetComponent<AudioSource>().volume += Time.deltaTime;
        }
        if(currentClicked <= maxClicked && typeCard == CardType.MoneyCard)
        {
            if (!transform.Find(NameMoneyEffect).gameObject.activeSelf)
                transform.Find(NameMoneyEffect).gameObject.SetActive(true);
        }
    }

    private int SoundGameKerenPlay()
    {
        int rnd = Random.Range(0, 2);
        return rnd;
    }

    internal void CrashEffect()
    {
        CrashEffectCard.SetActive(true);
    }

    private void AnimationEventCard(string NameAnimationSelected)
    {
          CardAnim.SetBool(NameAnimationSelected, true);
    }

    void OnMouseDrag()
    {
        if (!GetComponent<Animator>().enabled)
            GetComponent<Animator>().enabled = true;
        
            if (!gameRules.GetIsPausedGame())
            {
                if (typeCard == CardType.MusicCard)
                {
                    AnimationEventCard("Datche");
                    MainEventCard();
                }
            }
        
    }
	void OnMouseUp()
	{
        
            if (GetComponent<Animator>().enabled)
                GetComponent<Animator>().enabled = false;
        

        if (CardAnim.GetBool("Clicked"))
            CardAnim.SetBool("Clicked", false);
        if (CardAnim.GetBool("Datche"))
            CardAnim.SetBool("Datche", false);
        if (typeCard == CardType.MusicCard)
        {
            isMusicOff = true;
            ParticleSystem ps = transform.Find(NameMusicEffect).GetComponent<ParticleSystem>();
            var main = ps.main;
            main.loop = !isMusicOff;
            GetComponent<AudioSource>().pitch = 1f;
            GetComponent<AudioSource>().loop = !isMusicOff;
        }
	}

	void RefreshTextAndColorForCard()
    {
        if (currentClicked <= maxClicked)
            CountClickedText.text = (maxClicked - currentClicked).ToString();
        if (currentClicked == maxClicked || (currentClicked >= maxClicked && typeCard == CardType.MusicCard))
            colorText = colorCard = colorTypeCard = Color.green;
        else if (currentClicked > maxClicked && typeCard != CardType.MusicCard && typeCard != CardType.HealthCard && typeCard != CardType.TimeCard && typeCard != CardType.MoneyCard && typeCard != CardType.FreezingCard)
            colorText = colorCard = colorTypeCard = Color.red;
        else if(currentClicked < maxClicked)
            colorText = colorCard = colorTypeCard = Color.white;
        EnteringDataForCard();

    }

    void EnteringDataForCard()
    {
        GetComponent<SpriteRenderer>().color = colorCard;
        russian = playerProfille.GetLanguageID() == 10;
        transform.Find(NameTypeCard).GetComponent<SpriteRenderer>().sprite = IconsTypeCard[typeCard.GetHashCode()];
        transform.Find(NameTypeCard).GetComponent<SpriteRenderer>().color = colorTypeCard;
        
            if (music != null)
                music.GetSpectrumData(samples, 0, FFTWindow.Blackman);
            transform.localScale = new Vector3((samples[0] * MaxScaleCard) + ScaleCard, (samples[1] * MaxScaleCard) + ScaleCard, 1);
            if (gameRules.GetIsPausedGame() || gameRules.GameSchansUI.activeSelf)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().gravityScale = samples[2] + 0.01f * gameRules.GetDifficulyIndex();
            }

            if (gameRules.GetGameFrezzed())
            {
                if (GetComponent<Rigidbody2D>().gravityScale > 0)
                    GetComponent<Rigidbody2D>().gravityScale -= Time.deltaTime;
            }
       
        if (typeCard == CardType.MoneyCard && transform.Find(NameMoneyEffect).gameObject.activeSelf)
            if (!transform.Find(NameMoneyEffect).GetComponent<ParticleSystem>().isPlaying)
                transform.Find(NameMoneyEffect).gameObject.SetActive(false);
        CountClickedText.color = colorText;
        if (typeCard == CardType.MusicCard && isMusicOff)
            if (GetComponent<AudioSource>().volume > 0)
                GetComponent<AudioSource>().volume -= Time.deltaTime;
        if (gameRules != null)
        {
            if (!gameRules.GetIsStartedGame())
            {
                gameRules.MagnitCardEvent -= gameObject.GetComponent<CardMechanic>().OnMouseDown;
                Destroy(gameObject);
            }
        }

    }

    internal int GetCurrentOST() { return currentClicked; }

    internal void SetCurrentOST(int currentClicked) { this.currentClicked = currentClicked; }

    internal int GetMaxClicked() { return maxClicked; }

    internal void SetMaxClicked(int countMax)
    {
        maxClicked = countMax;
    }

    internal CardType GetTypeCard() { return typeCard; }

    internal AudioClip GetMoreClicks() { return MoreClicks; }
}
