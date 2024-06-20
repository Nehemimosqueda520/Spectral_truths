using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DInteraction : MonoBehaviour
{
    private float rotationSpeed = 350f; 
    private float zoomSpeed = 200f; 
    private float minZoom = 300f; 
    private float maxZoom = 450f; 
    private Camera itemCamera;

    void Update()
    {
       if (Input.GetMouseButton(1))
       {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.unscaledDeltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.unscaledDeltaTime;

            gameObject.transform.Rotate(Vector3.up, -rotX, Space.World);
            gameObject.transform.Rotate(Vector3.right, rotY, Space.World);
       }

       float scroll = Input.GetAxis("Mouse ScrollWheel");
       if (scroll != 0.0f)
       {
            Vector3 direction = itemCamera.transform.forward * scroll * zoomSpeed;
            Vector3 newPosition = itemCamera.transform.position + direction;

            float distance = Vector3.Distance(newPosition, transform.position);
            if (distance >= minZoom && distance <= maxZoom)
            {
                itemCamera.transform.position = newPosition;
            }
        }
    }

    public void SetCamera(Camera cam)
    {
        itemCamera = cam;
    }
}
