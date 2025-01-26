using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [Space]

    [SerializeField] float force;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] Vector2 heightRange;
    [SerializeField] float lerp;

    [Space]

    [SerializeField] float recalculateTime;

    Vector3 nextPosition;
    Vector3 direction;

    private void Start()
    {
        StartCoroutine(Recalculate());
    }

    void FixedUpdate()
    {
        direction = nextPosition - transform.position;

        rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(direction), lerp);

        rb.AddForce(force * Time.deltaTime * transform.forward, ForceMode.Impulse);
    }

    IEnumerator Recalculate()
    {
        nextPosition.x = Random.Range(distanceRange.x, distanceRange.y);
        nextPosition.y = Random.Range(heightRange.x, heightRange.y);
        nextPosition.z = Random.Range(distanceRange.x, distanceRange.y);

        yield return new WaitForSeconds(recalculateTime);

        StartCoroutine(Recalculate());
    }
}
