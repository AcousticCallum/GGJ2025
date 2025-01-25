using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float explosionForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player.instance.gameObject)
        {
            Player.instance.rb.AddExplosionForce(explosionForce, transform.position, 1);

            Destroy(gameObject);
        }
    }
}
