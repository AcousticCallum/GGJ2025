using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] GameObject glass;
    [SerializeField] GameObject glassFractured;

    [Space]

    [SerializeField] float fractureThreshold;

    [SerializeField] float timeUntillDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.instance.gameObject)
        {
            return;
        }

        if (Player.instance.rb.velocity.magnitude < fractureThreshold)
        {
            return;
        }

        if (glass.activeSelf)
        {
            glass.SetActive(false);
        }

        if (!glass.activeSelf)
        {
            glassFractured.SetActive(true);

            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(timeUntillDestroy);

        Destroy(gameObject);
    }
}
