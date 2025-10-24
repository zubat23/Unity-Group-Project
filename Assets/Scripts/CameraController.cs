using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    private float MouseX;
    private float MouseY;
    private float angle;
    
    public float sensitivity = 100f;
    public Transform body;
    public Transform head;

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X")* sensitivity * Time.deltaTime;
        body.Rotate(Vector3.up, MouseX);

        MouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        angle -= MouseY;
        angle = Mathf.Clamp(angle, -90.0f, 90.0f);
        head.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}
