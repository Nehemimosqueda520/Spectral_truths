using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAppearence : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTexture;
    private bool isPaused;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        if (!GameManager.isGamePaused)
        {
            float xMin = (Screen.width / 2) - (crosshairTexture.width / 2);
            float yMin = (Screen.height / 2) - (crosshairTexture.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairTexture.width, crosshairTexture.height), crosshairTexture);
        }
    }
}
