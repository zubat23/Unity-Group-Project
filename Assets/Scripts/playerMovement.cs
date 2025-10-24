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
        PlayerAnimator = GetComponent<Animator>();//Animator
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

        //Player Animation

        if (Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.SetBool("IsRunFoward", true);
            Debug.Log("y");
        }
        else
        {
            PlayerAnimator.SetBool("IsRunFoward", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.SetBool("IsRunBack", true);
            Debug.Log("y");
        }
        else
        {
            PlayerAnimator.SetBool("IsRunBack", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAnimator.SetBool("IsRunLeft", true);
            Debug.Log("y");
        }
        else
        {
            PlayerAnimator.SetBool("IsRunLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.SetBool("IsRunRight", true);
            Debug.Log("y");
        }
        else
        {
            PlayerAnimator.SetBool("IsRunRight", false);
        }

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
            PlayerAnimator.SetBool("IsFalling", false);
        }
        else 
        {

            PlayerAnimator.SetBool("IsFalling", true);
        }

        
    }
}
