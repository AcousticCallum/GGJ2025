using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] fishes;

    [SerializeField] int fishCount;

    [SerializeField] Vector3 offset;

    void Start()
    {
        for (int i = 0; i < fishCount; i++)
        {
            Vector3 newOffset;
            newOffset.x = Random.Range(-offset.x, offset.x);
            newOffset.y = Random.Range(-offset.y, offset.y);
            newOffset.z = Random.Range(-offset.z, offset.z);

            Instantiate(fishes[Random.Range(0, fishes.Length)], transform.position + newOffset, Quaternion.identity);
        }
    }
}
