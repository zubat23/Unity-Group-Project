using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class playerMovement : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    public float jumpForce = 80f; // Jump force
    public float health = 3;
    public Transform cam;

    private bool knockback = false;
    private bool iFrames = false;
    private float MouseX;
    private float gravity = -80.0f;
    CharacterController Controller; // Reference to Character Controller component

    public Animator PlayerAnimator; //All player Animations

    void Start()
    {
        Controller = GetComponent<CharacterController>(); // Initialize Controller
        PlayerAnimator = GetComponent<Animator>();//Animator
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        MouseX = Input.GetAxis("Mouse X");

        if (Mathf.Abs(MouseX) > 0.01f)
        {
            transform.Rotate(Vector3.up * MouseX * cam.GetComponent<CameraController>().sensitivity * Time.deltaTime);
        }


        // Calculate movement direction
        if (knockback)
        {
            move.x = -2.5f;
        }
        move.y += gravity * Time.deltaTime;
        Controller.Move(move * speed * Time.deltaTime);

        //Player Animation

        if (Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.SetBool("IsRunFoward", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsRunFoward", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.SetBool("IsRunBack", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsRunBack", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAnimator.SetBool("IsRunLeft", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsRunLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.SetBool("IsRunRight", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsRunRight", false);
        }
        if (IsGrounded())
        {
            PlayerAnimator.SetBool("IsFalling", false);
        }
        else
        {
            PlayerAnimator.SetBool("IsFalling", true);
        }
        Debug.Log(health);
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    IEnumerator jumping()
    {
        gravity = 20.0f;
        yield return new WaitForSeconds(1.0f);
        gravity = -80.0f;
    }

    IEnumerator Knockback()
    {
        knockback = true;
        iFrames = true;
        yield return new WaitForSeconds(0.5f);
        knockback = false;
        iFrames = false;
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            StartCoroutine(jumping());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BearAttack") && !iFrames)
        {
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(Knockback());
        }
    }
}
