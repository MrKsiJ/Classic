using UnityEngine;

public class WorkshopPreviewAWP : MonoBehaviour
{
    [SerializeField] private float SpeedRotMax = 5f;
    [SerializeField] private float SpeedRot = 0.0f;
    [SerializeField] private float SpeedRotF = 0.0f;
    void FixedUpdate()
    {
        Quaternion rotationY = Quaternion.AngleAxis(SpeedRot, transform.up);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (SpeedRot < -SpeedRotMax)
                SpeedRot = -SpeedRotMax;
            if (SpeedRot > SpeedRotMax)
                SpeedRot = SpeedRotMax;
            SpeedRotF = 1f;
            if (Input.GetAxis("Mouse X") > 0)
                SpeedRot += Time.deltaTime * 2f;
            if (Input.GetAxis("Mouse X") < 0)
                SpeedRot -= Time.deltaTime * 2f;
            if (Input.GetAxis("Mouse X") == 0)
                RotationNull();

        }
        else
            RotationNull();

        if (SpeedRotF > 0)
            transform.rotation *= rotationY;
    }

    private void RotationNull()
    {
        SpeedRot = Mathf.Lerp(SpeedRot, 0, Time.deltaTime * 2f);
        if (SpeedRotF > 0)
            SpeedRotF -= Time.deltaTime / 2f;
    }
}
