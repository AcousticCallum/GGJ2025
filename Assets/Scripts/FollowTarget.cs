using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private static FollowTarget instance;
    public float followDistance;

    private void Awake()
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
        Vector3 position = Player.instance.transform.position - (Vector3.forward * followDistance);
        position.x = 0f;
        transform.position = position;
    }
}
