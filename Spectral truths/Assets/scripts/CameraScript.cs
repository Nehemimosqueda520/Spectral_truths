using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float MouseSensitivity = 80f;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Lantern;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        rotationX -= MouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += MouseX;


        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);

        // Lantern.localRotation = Quaternion.Euler(0f, 0f, MouseX);
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Lantern.rotation = targetRotation;
        //Lantern.Rotate(Vector3.left * MouseY)
    }
       
}
