using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
[SerializeField] private float MouseSensitivity = 80f;
[SerializeField] private Transform Player;
[SerializeField] private Transform Lantern;
float RotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        RotationX -= MouseY;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);
       Lantern.Rotate(Vector3.left * MouseY);

    }
       
}
