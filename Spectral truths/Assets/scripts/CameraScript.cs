using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float MouseSensitivity = 200f;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Lantern;
    private float smoothTime = 0.2f;
    private float lanternDelay = 12f;

    private float rotationX = 0f;
    private float currentMouseX;
    private float currentMouseY;
    private float mouseXVelocity = 0f;
    private float mouseYVelocity = 0f;
    private Quaternion targetLanternRotation;

    void Update()
    {
        float targetMouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float targetMouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        currentMouseX = Mathf.SmoothDamp(currentMouseX, targetMouseX, ref mouseXVelocity, smoothTime);
        currentMouseY = Mathf.SmoothDamp(currentMouseY, targetMouseY, ref mouseYVelocity, smoothTime);

        rotationX -= currentMouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Player.Rotate(Vector3.up * currentMouseX);

        // Actualiza la rotación de la linterna con un retraso
        targetLanternRotation = Quaternion.Lerp(targetLanternRotation, transform.rotation, lanternDelay * Time.deltaTime);
        Lantern.rotation = targetLanternRotation;
    }
       
}
