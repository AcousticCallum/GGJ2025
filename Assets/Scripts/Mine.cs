using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float explosionForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.rb.AddExplosionForce(explosionForce, transform.position, 1);

            Destroy(gameObject);
        }
    }
}
