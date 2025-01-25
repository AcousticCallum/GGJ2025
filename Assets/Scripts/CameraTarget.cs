using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Singleton
    public static CameraTarget instance;

    public float lerp;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.position = Player.instance.transform.position;
        
        Vector3 playerVelocity = Player.instance.rb.velocity;
        playerVelocity.y = 0.0f;

        if(playerVelocity.sqrMagnitude == 0.0f)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, playerVelocity.normalized, lerp * Time.deltaTime), Vector3.up);
    }
}
