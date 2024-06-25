using UnityEngine;
public class FlashlightController : MonoBehaviour
{
    // Referencia al componente de luz
    [SerializeField] private Light flashlight;
    [SerializeField] private GameManager gameManager;
    

    void Start()
    {
        // Asegúrate de que la luz esté apagada al inicio del juego
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
    }

    void Update()
    {
        if (!GameManager.isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {

                ChangeLight();
            }
        }
       

    }

    void ChangeLight () {
        if (flashlight != null)
            {
                 // Cambia el estado de la luz
                flashlight.enabled = !flashlight.enabled;
            }
    }

}