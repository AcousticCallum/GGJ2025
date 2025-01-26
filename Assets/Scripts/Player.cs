using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player instance;

    // Public
    public Vector3 bubbleDrag;

    // Hidden Public
    [HideInInspector] public Rigidbody rb;

    // Serialized Private
    [SerializeField] private State state;

    [SerializeField] private LayerMask groundMask;
    
    [SerializeField] private float torque;
    [SerializeField] private float marbleAirForce;
    [SerializeField] private float force;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float angularDrag, stoppingAngularDrag;

    [SerializeField] private GameObject bubbleArt, marbleArt;

    // Private
    private Vector2 moveInput;
    private Vector3 respawnPoint;

    private bool jump;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        bool grounded = false;

        if (Physics.CheckSphere(transform.position, 0.55f, groundMask))
        {
            grounded = true;

            if (jump)
            {
                Vector3 v = rb.velocity;
                v.y = Mathf.Sqrt(2.0f * -Physics.gravity.y * jumpHeight);
                rb.velocity = v;

                jump = false;
            }
        }

        if (moveInput.sqrMagnitude > 0.0f)
        {
            // Convert input direction to world space relative to camera and flatten to Y plane
            Vector3 targetDirection = Camera.main.transform.TransformDirection(moveInput);
            targetDirection.y = 0.0f;
            targetDirection.Normalize();

            if (state == State.MARBLE)
            {
                if (grounded)
                {
                    // Rotate around axis that moves in target direction
                    rb.AddTorque(torque * moveInput.magnitude * Vector3.Cross(Vector3.up, targetDirection));
                }
                else
                {
                    // Move in target direction
                    rb.AddForce(marbleAirForce * moveInput.magnitude * targetDirection);
                }
            }
            else
            {
                // Move in target direction
                rb.AddForce(force * moveInput.magnitude * targetDirection);
            }
        }

        if (state == State.BUBBLE)
        {
            rb.AddForce(Vector3.Scale(bubbleDrag, -rb.velocity));
        }

        jump = false;
    }

    private void LateUpdate()
    {
        bubbleArt.transform.rotation = Quaternion.identity;
    }

    public void ChangeState(State newState)
    {
        switch (newState)
        {
            case State.BUBBLE:
                rb.excludeLayers = 1 << LayerMask.NameToLayer("Mesh");
                rb.useGravity = false;

                bubbleArt.SetActive(true);
                marbleArt.SetActive(false);

                state = State.BUBBLE;
                break;

            case State.MARBLE:
                rb.excludeLayers = 0;
                rb.useGravity = true;

                bubbleArt.SetActive(false);
                marbleArt.SetActive(true);

                state = State.MARBLE;
                break;
        }
    }

    public State ReadState()
    {
        return state;
    }

    public void SetMoveInput(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        if(moveInput.sqrMagnitude > 1.0f)
        {
            moveInput.Normalize();
        }
    }

    public void SetJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            jump = true;
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPoint = position;
    }

    public void Die()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.75f);
    }

    public enum State
    {
        MARBLE,
        BUBBLE
    }
}
