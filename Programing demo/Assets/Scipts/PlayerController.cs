using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private CharacterController controller;

    private Vector2 moveInput = Vector2.zero;
    private bool jumpPressed = false;

    private Vector3 velocity;
    private bool isGrounded;

    private Scene activeScene;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start ()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        controller.Move(move * moveSpeed * Time.deltaTime);


        transform.Rotate(Vector3.up * moveInput.x * turnSpeed * Time.deltaTime);

        if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;
            
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
       
    }

    public void OnJump()
    {
        jumpPressed = true;
        Debug.Log("test");
    }

    public void OnDeath()
    {
        if ( )
        {
            SceneManager.LoadScene(activeScene)
        }
    }
}
