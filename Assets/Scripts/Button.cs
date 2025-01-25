using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent pressEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == Player.instance.transform)
        {
            if (Player.instance.state == Player.State.MARBLE)
            {
                pressEvent.Invoke();
            }
        }
    }

    public void ButtonTest()
    {
        print(name + " was pressed!");
    }
}
