using UnityEngine;
using System.Collections;

public class GunSway : MonoBehaviour
{
    //Original position
    Vector3 oPos;
    float amount = 0.075f;
    float posSpeed = 5.0f;
    float rotSpeed = 5.0f;

    float mH;
    float mV;

    float mAmt = 10.0f;
    float mSpeed = 10.0f;

    void Start ()
    {
        oPos = transform.localPosition;
	}

    public void Sway (Vector3 movement, Vector2 mouse)
    {
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movement = -movement;
        Vector3 finalMovement = oPos + (movement * amount);

        mouse.x *= 2.0f;
        mouse.y = Mathf.Clamp(mouse.y, -5.0f, 5.0f);
        Quaternion finalRotation = Quaternion.Euler(mouse.y, mouse.x, 0.0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalMovement, Time.deltaTime * posSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation, Time.deltaTime * rotSpeed);
    }

    public void Push(Vector3 dir, float force)
    {
        Vector3 nPos = transform.localPosition;
        nPos += dir * force;

        transform.localPosition = nPos;
    }
}
