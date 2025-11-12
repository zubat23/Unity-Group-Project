using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    private float MouseX = 0.0f;
    private float MouseY = 0.0f;

    public float distance = 10.0f;   
    public float sensitivity = 200f;

    public Transform lookAt;
    public Transform Player;

    // Update is called once per frame
    void LateUpdate()
    {
        MouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        MouseY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, 10.0f, 80.0f);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(MouseY, MouseX, 0);
        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);
    }
}
