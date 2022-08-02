using UnityEngine;

public class DestoryGameObject : MonoBehaviour
{
    [SerializeField] bool isActive;
    [SerializeField] float lengthclip;
    void Start()
    {
        if (GetComponent<AudioSource>().isPlaying)
            lengthclip = GetComponent<AudioSource>().clip.length;
        else
            lengthclip = 0.0f;
        if(!isActive)
            Destroy(gameObject, lengthclip);
    }

    void Update()
    {
        if (isActive)
        {
            if (lengthclip > 0)
                lengthclip -= Time.deltaTime;
            else
            {
                lengthclip = GetComponent<AudioSource>().clip.length;
                gameObject.SetActive(false);
            }
        }
    }
}
