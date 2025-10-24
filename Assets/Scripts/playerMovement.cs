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


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Controller = GetComponent<CharacterController>(); // Initialize Rigidbody
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
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
