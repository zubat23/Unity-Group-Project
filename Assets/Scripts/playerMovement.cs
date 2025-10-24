using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class playerMovement : MonoBehaviour
{

    public float speed = 100f; // Movement speed

    public float jumpForce = 5f; // Jump force

    private float Yrotation = 0;
    private Rigidbody rb; // Reference to Rigidbody component


    public Animator PlayerAnimator; //All player Animations

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody
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
