using UnityEngine;

public class CrashEffectCard : MonoBehaviour
{
    const string NameBulletObjects = "Bullet";
    const string NameTextCountClicks = "TextCountClicks";
    const string NameTypeCard = "TypeCard";
    [SerializeField] private Transform CardMain;
    void Start()
    {
        BulletSpritesCheck();
        CardMain.GetComponent<SpriteRenderer>().enabled = false;
        CardMain.transform.Find(NameTextCountClicks).gameObject.SetActive(false);
        CardMain.transform.Find(NameTypeCard).gameObject.SetActive(false);
    }

    private void BulletSpritesCheck()
    {
        SpriteRenderer[] Bullets = CardMain.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < Bullets.Length; i++)
        {
            if (Bullets[i].name == NameBulletObjects)
                Destroy(Bullets[i]);
        }
    }
}
