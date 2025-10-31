using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject inactiveTarget;
    public float speed = 5;

    private bool aggressive = false;
    private bool atStartPoint = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(rb.transform.position, player.transform.position);
        updateAggression(distance);

        if (player != null && aggressive)
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        else if (!atStartPoint)
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, inactiveTarget.transform.position, speed * Time.deltaTime);
            transform.LookAt(inactiveTarget.transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        atStartPoint = true;
    }

    private void OnTriggerExit(Collider other)
    {
        atStartPoint=false;
    }
    void updateAggression(float distance)
    {
        if (aggressive)
        {
            if (distance > 30.0f)
            {
                aggressive = false;
            }
        }
        else
        {
            if (distance < 20.0f)
            {
                aggressive= true;
            }
        }
    }
}
