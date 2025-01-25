using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float followDistance;

    void Update()
    {
        Vector3 position = Player.instance.transform.position - (Vector3.forward * followDistance);
        position.x = 0f;
        transform.position = position;
    }
}
