using UnityEngine;
public class FlashlightController : MonoBehaviour
{
    // Referencia al componente de luz
    public Light flashlight;
    

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
        // Verifica si el jugador hizo clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(1))
        {

            ChangeLight();
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