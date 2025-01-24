using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform followTarget;

    public float maxSqrVelocity;
    public float forcePerFrame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.sqrMagnitude <= maxSqrVelocity)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rb.AddForce(transform.forward * horizontal);
            rb.AddForce(transform.right * vertical);
        }
    }
}
