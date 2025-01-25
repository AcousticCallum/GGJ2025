using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public float speed;

    void Update()
    {
        rb.AddForce(Vector3.up * speed * Time.deltaTime, ForceMode.Force);
    }
}
