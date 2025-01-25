using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public Player.State state;
    public bool timed;
    public float timerTime;

    private WaitForSeconds wait;

    private void Start()
    {
        wait = new WaitForSeconds(timerTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.ChangeState(state);

            if (timed) StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        Player.State otherState = Player.State.BUBBLE;
        if (state == otherState)
            otherState = Player.State.MARBLE;

        Player.instance.ChangeState(state);

        yield return wait;

        Player.instance.ChangeState(otherState);
        yield return null;
    }
}
