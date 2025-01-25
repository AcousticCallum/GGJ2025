using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Singleton
    public static CameraTarget instance;

    public float lerp;
    public float cameraHeight;

    private Vector3 followVelocity;

    void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transform.position = Player.instance.transform.position + (Vector3.up * cameraHeight);
        
        Vector3 playerVelocity = Player.instance.rb.velocity;
        playerVelocity.y = 0.0f;

        if(playerVelocity.sqrMagnitude != 0.0f)
        {
            followVelocity = playerVelocity;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, followVelocity.normalized, lerp * Time.deltaTime), Vector3.up);
    }
}
