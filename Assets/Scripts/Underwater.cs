using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
    [SerializeField] Color colour;

    [SerializeField] FogMode fogMode;
    [SerializeField] float fogDensity;
    [SerializeField] float fogEndDistance;

    private void Start()
    {
        RenderSettings.fogColor = colour;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogEndDistance = fogEndDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            RenderSettings.fog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            RenderSettings.fog = false;
        }
    }
}
