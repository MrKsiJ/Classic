using UnityEngine;

public class CrosshairMechanic : MonoBehaviour
{
    internal bool isFire;
    [SerializeField] GameRules gameRules;
    [SerializeField] Sprite[] BulletsShoots;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Card")
        {
            if(isFire)
            {

                if(other.GetComponent<SpriteRenderer>().color != Color.green)
                {
                    int Premax = other.GetComponent<CardMechanic>().GetMaxClicked();
                    other.GetComponent<CardMechanic>().isCardCrosshairAWP = true;
                    Prostrel(other);
                    for (int i = 0; i < Premax; i++)
                        other.GetComponent<CardMechanic>().OnMouseDown();
                }
                else
                    other.GetComponent<CardMechanic>().CrashEffect();
                
                isFire = false;
            }
        }
    }

    private void Prostrel(Collider2D other)
    {
        GameObject Shoot = new GameObject();
        Shoot.name = "Bullet";
        Shoot.transform.position = transform.position;
        Shoot.AddComponent<SpriteRenderer>();
        Shoot.GetComponent<SpriteRenderer>().sprite = BulletsShoots[Random.Range(0, BulletsShoots.Length)];
        Shoot.transform.SetParent(other.transform);
    }

    void Update()
    {
        if (!gameRules.GetIsStartedGame() || !gameRules.GetAWPOn())
            gameObject.SetActive(false);
    }

    private int SoundPlay()
    {
        return Random.Range(-5, 5);
    }
    
}
