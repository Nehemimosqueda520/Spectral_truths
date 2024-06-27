using UnityEngine;

public class PlayerController : MonoBehaviour
{  
    private CharacterController chController;
    private Vector3 velocity;
    private float gravity = -49.05f;
    private float speed = 35f;
    private float groundDistance = 0.4f;
    private bool isGrounded;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        applyGravity();
        checkGrounded();
        movePlayer();
    }

    void movePlayer()
    {
        // Capturar el input del jugador
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Mover el jugador
        Vector3 move = transform.right * x + transform.forward * z;
        chController.Move(move * speed * Time.deltaTime);
    }

    void checkGrounded()    
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void applyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        chController.Move(velocity * Time.deltaTime);
    }
}
