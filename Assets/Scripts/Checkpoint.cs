using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Transform spawnTransform = transform.GetChild(0);
            Player.instance.SetCheckpoint(spawnTransform.position);
        }
    }
}
