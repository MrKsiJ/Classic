using UnityEngine;
using static CardMechanic;
public class ScreenMechanic : MonoBehaviour
{
    [SerializeField] GameRules gameRules;

    void OnTriggerEnter2D(Collider2D card)
    {
        if (card.gameObject.tag == "Card")
        {
            if (card.GetComponent<CardMechanic>().GetTypeCard() != CardType.MagnitCard && card.GetComponent<CardMechanic>().GetTypeCard() != CardType.BombCard && card.GetComponent<CardMechanic>().GetTypeCard() != CardType.FreezingCard)
                gameRules.MagnitCardEvent += card.GetComponent<CardMechanic>().OnMouseDown;
        }
    }

    void OnTriggerExit2D(Collider2D card)
    {
        if (card.gameObject.tag == "Card")
            gameRules.MagnitCardEvent -= card.GetComponent<CardMechanic>().OnMouseDown;
    }
}
