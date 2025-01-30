using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{
    // Singleton
    public static CameraTarget instance;

    public float lerp;
    public float mouseLerp;
    public float sensitivity;
    public float mouseInactivityPause;

    public CinemachineVirtualCamera virtualCamera;

    private Vector3 followVelocity;

    private Vector2 mouseDelta;
    private float currentDeltaX;
    private float mouseDeltaTimer = 100.0f;

    void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position = Player.instance.transform.position;

        currentDeltaX = Mathf.Lerp(currentDeltaX, mouseDelta.x * sensitivity, mouseLerp * Time.deltaTime);

        mouseDeltaTimer += Time.deltaTime;

        if(mouseDelta.x != 0.0f)
        {
            mouseDeltaTimer = 0.0f;
        }

        if(mouseDeltaTimer > mouseInactivityPause)
        {
            mouseDeltaTimer = mouseInactivityPause;

            Vector3 playerVelocity = Player.instance.rb.velocity;
            playerVelocity.y = 0.0f;

            if (playerVelocity.sqrMagnitude != 0.0f)
            {
                followVelocity = playerVelocity;
            }

            CinemachineComposer composerA = virtualCamera.GetCinemachineComponent<CinemachineComposer>();

            composerA.m_HorizontalDamping = 0.5f;
            composerA.m_VerticalDamping = 0.5f;

            transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, followVelocity.normalized, lerp * Time.deltaTime), Vector3.up);

            return;
        }

        CinemachineComposer composerB = virtualCamera.GetCinemachineComponent<CinemachineComposer>();

        composerB.m_HorizontalDamping = 0.0f;
        composerB.m_VerticalDamping = 0.0f;

        transform.Rotate(currentDeltaX * Vector3.up);

        //transform.RotateAroundLocal(Vector3.right, mouseDelta.y);
    }

    public void SetMouseDelta(InputAction.CallbackContext ctx)
    {
        mouseDelta = ctx.ReadValue<Vector2>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + followVelocity.normalized);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
