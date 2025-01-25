using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum MoveDirection { UP, DOWN, LEFT, RIGHT }
    public MoveDirection direction;
    public float openTime;
    public float openDistance;

    private WaitForEndOfFrame wait;

    public void Open()
    {
        StartCoroutine(OpenCoro());
    }

    IEnumerator OpenCoro()
    {
        Vector3 endPos;
        Vector3 startPos = endPos = transform.position;

        switch (direction)
        {
            case MoveDirection.UP:
                endPos += transform.up * openDistance;
                break;
            case MoveDirection.DOWN:
                endPos -= transform.up * openDistance;
                break;
            case MoveDirection.LEFT:
                endPos -= transform.right * openDistance;
                break;
            case MoveDirection.RIGHT:
                endPos += transform.right * openDistance;
                break;
        }

        float delta = 0;
        while (delta < openTime)
        {
            float t = delta / openTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);

            delta += Time.deltaTime;
            yield return wait;
        }
    }
}
