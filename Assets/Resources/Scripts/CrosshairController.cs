using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CrosshairController : MonoBehaviour, IDragHandler, IPointerUpHandler,IPointerDownHandler
{
    [SerializeField] GameRules gameRules;

    private Image joystickBG;
    private Image joystick;
    private Vector2 inputVector;

    public GameObject crosshairs;
    const float speed = 0.05f;
    private bool isDrag;
    
    private void Start()
    {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(gameRules.GetIsStartedGame() && !gameRules.GetIsPausedGame())
        {
            Vector2 pos;
            isDrag = true;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
                pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.x);

                inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
            }
        }

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        isDrag = false;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return 0;
    }

    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return 0;
    }
    void Update()
    {
        crosshairs.SetActive((gameRules.GetFire() && gameRules.GetAWPOn()) || crosshairs.GetComponent<CrosshairMechanic>().isFire);
    }
    void FixedUpdate()
    {
        if (isDrag && gameRules.GetFire())
        {
            if (Vertical() > 0 && crosshairs.transform.position.y < GameCamera.Size.y - crosshairs.transform.localScale.y / 2)
                crosshairs.transform.Translate(0, speed, 0);
            else if (Vertical() < 0 && crosshairs.transform.position.y > -GameCamera.Size.y + crosshairs.transform.localScale.y / 2)
                crosshairs.transform.Translate(0, -speed, 0);

            if (Horizontal() > 0 && crosshairs.transform.position.x < GameCamera.Size.x - crosshairs.transform.localScale.x / 2)
                crosshairs.transform.Translate(speed, 0, 0);
            else if(Horizontal() < 0 && crosshairs.transform.position.x > -GameCamera.Size.x + crosshairs.transform.localScale.x / 2)
                crosshairs.transform.Translate(-speed, 0, 0);
        }    
    }


}
