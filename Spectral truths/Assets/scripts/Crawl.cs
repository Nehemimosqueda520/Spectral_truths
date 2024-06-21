using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawl : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerCamera;
    public float crouchHeight = 0.8f; // Altura de la cápsula cuando está agachado
    public float normalHeight = 2.0f; // Altura normal de la cápsula
    public float crouchSpeed = 2.0f; // Velocidad de movimiento al agacharse
    public float cameraCrouchOffset = 0.5f; // Desplazamiento de la cámara cuando está agachado

    private float originalCameraHeight;
    private bool isCrouching = false;

    void Start()
    {
        originalCameraHeight = playerCamera.localPosition.y;
        characterController.height = normalHeight;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                ToggleCrouch(true);
            }
        }
        else
        {
            if (isCrouching)
            {
                ToggleCrouch(false);
            }
        }
    }

    void ToggleCrouch(bool crouch)
    {
        isCrouching = crouch;

        if (isCrouching)
        {
            characterController.height = crouchHeight;
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, originalCameraHeight - cameraCrouchOffset, playerCamera.localPosition.z);
        }
        else
        {
            characterController.height = normalHeight;
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, originalCameraHeight, playerCamera.localPosition.z);
        }
    }
}
