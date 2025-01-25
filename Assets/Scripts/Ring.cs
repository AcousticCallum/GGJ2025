using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public Player.State state;
    public bool timed;
    public float timerTime;

    static Coroutine currentTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.ChangeState(state);

            if(currentTimer != null)
            {
                StopCoroutine(currentTimer);

                currentTimer = null;
            }

            if (timed)
            {
                currentTimer = StartCoroutine(Timer());
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timerTime);

        if (Player.instance.ReadState() == state)
        {
            Player.instance.ChangeState(state == Player.State.MARBLE ? Player.State.BUBBLE : Player.State.MARBLE);

            Debug.Log("Changed state");
        }

        currentTimer = null;
    }
}
