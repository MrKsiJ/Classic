using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private static Vector2 size;
    public static Vector2 Size
    {
        get { return size; }
    }

    private void Start()
    {
        var cam = Camera.main;
        size = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
    }
}
