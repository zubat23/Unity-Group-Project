using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    public float speed = 40f; // Movement speed
    public float mouseSensitivity = 2f; // Camera sensitivity
    public float jumpForce = 5f; // Jump force

    private Rigidbody rb; // Reference to Rigidbody component
    private Transform cameraTransform; //For 1st person camera

    public Animator PlayerAnimator; //All player Animations

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody
        cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z + .3f);
        cameraTransform.parent = transform; // Attach camera to player
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);

        //Animation
        if(moveX > 0 || moveZ > 0)
        {
            PlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }

        if (IsGrounded())
        {
            PlayerAnimator.SetBool("IsFalling", false);
        }
        else
        {
            PlayerAnimator.SetBool("IsFalling", true);
        }

            //Camera movement
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.left * mouseY);
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        
    }
}
