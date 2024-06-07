using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float velocity = 300f;
    
    void Start()
    {

    }

    void FixedUpdate()
    {
        // Capturar el input del jugador
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Mover el jugador
        transform.Translate(x * velocity * Time.deltaTime, 0, z * velocity * Time.deltaTime);
    

        
    
    }
}
