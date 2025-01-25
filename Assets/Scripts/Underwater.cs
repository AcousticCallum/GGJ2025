using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
    [SerializeField] Color colour;
    [SerializeField] float fogEndDistance;

    private void Start()
    {
        RenderSettings.fogColor = colour;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogEndDistance = fogEndDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.fog = true;
    }

    private void OnTriggerExit(Collider other)
    {
        RenderSettings.fog = false;
    }
}
