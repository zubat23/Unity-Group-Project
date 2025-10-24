using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class playerMovement : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    public float jumpForce = 10f; // Jump force
    public Transform cam;

    private float gravity = -20.0f;
    Rigidbody rb;
    CharacterController Controller; // Reference to Rigidbody component

    public Animator PlayerAnimator; //All player Animations

    void Start()
    {
        Controller = GetComponent<CharacterController>(); // Initialize Rigidbody
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

        if (move.magnitude != 0)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * cam.GetComponent<CameraController>().sensitivity * Time.deltaTime);

            Quaternion CamRotation = cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);


        }


        // Calculate movement direction
        move.y += gravity * Time.deltaTime;
        Controller.Move(move * speed * Time.deltaTime);
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
