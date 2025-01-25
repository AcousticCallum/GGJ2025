using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollowTarget : MonoBehaviour
{
    Transform sphere;

    public float mouseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sphere.position;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        print(mouseY);
        print(mouseX);

        Vector3 rotateVector = new(-mouseY * mouseSpeed, mouseX * mouseSpeed);
        transform.Rotate(rotateVector);
        transform.Rotate(new Vector3(0, rotateVector.y));
    }
}
