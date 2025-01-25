using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public enum AffectedState {NONE ,MARBLE, BUBBLE, ANY}

    public AffectedState affectedState;
    public float force;
    public float speed;
    public float spinSpeed;

    [Space]

    [SerializeField] private Transform fanModel;

    private void OnTriggerStay(Collider other)
    {
        switch (affectedState)
        {
            case AffectedState.NONE:
                return;

            case AffectedState.MARBLE:

                if(Player.instance.ReadState() == Player.State.MARBLE)
                    break;

                return;

            case AffectedState.BUBBLE:

                if (Player.instance.ReadState() == Player.State.BUBBLE)
                    break;

                return;

            case AffectedState.ANY:
                break;
        }

        if(other.gameObject == Player.instance.gameObject)
        {
            if(Vector3.Dot(Player.instance.rb.velocity, transform.forward) < speed)
            {
                Player.instance.rb.AddForce(force * transform.forward);
            }
        }
    }

    private void Update()
    {
        fanModel.localRotation *= Quaternion.AngleAxis(spinSpeed * Time.deltaTime * 360.0f, Vector3.forward);
    }
}
