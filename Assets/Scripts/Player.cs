using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player instance;

    // Public
    public State state;

    // Hidden Public
    [HideInInspector] public Rigidbody rb;

    // Serialized Private
    [SerializeField] private float torque;

    [SerializeField] private float angularDrag, stoppingAngularDrag;

    // Private
    private Vector2 moveInput;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(moveInput.sqrMagnitude > 0.0f)
        {
            // Convert input direction to world space relative to camera and flatten to Y plane
            Vector3 targetDirection = Camera.main.transform.TransformDirection(moveInput);
            targetDirection.y = 0.0f;
            targetDirection.Normalize();

            // Rotate around axis that moves in target direction
            rb.AddTorque(torque * moveInput.magnitude * Vector3.Cross(Vector3.up, targetDirection));

            // Moving in target direction
            if (Vector3.Dot(rb.velocity.normalized, targetDirection.normalized) >= 0.0f)
            {
                // Default drag
                rb.angularDrag = angularDrag;

                return;
            }
        }

        // Brake with high drag
        rb.angularDrag = stoppingAngularDrag;
    }

    public void SetMoveInput(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        if(moveInput.sqrMagnitude > 1.0f)
        {
            moveInput.Normalize();
        }
    }

    public enum State
    {
        MARBLE,
        BUBBLE
    }
}
