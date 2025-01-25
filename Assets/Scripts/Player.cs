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
    
    [SerializeField] private float torque;

    [SerializeField] private float angularDrag, stoppingAngularDrag;

    [SerializeField] private GameObject bubbleArt;

    // Private
    private Vector2 moveInput;
    private Vector3 respawnPoint;

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
        if (moveInput.sqrMagnitude > 0.0f)
        {
            // Convert input direction to world space relative to camera and flatten to Y plane
            Vector3 targetDirection = Camera.main.transform.TransformDirection(moveInput);
            targetDirection.y = 0.0f;
            targetDirection.Normalize();

            // Rotate around axis that moves in target direction
            rb.AddTorque(torque * moveInput.magnitude * Vector3.Cross(Vector3.up, targetDirection));
        }

        if (state == State.BUBBLE)
        {
            rb.AddForce(Vector3.Scale(bubbleDrag, -rb.velocity));
        }
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

                state = State.BUBBLE;
                break;

            case State.MARBLE:
                rb.excludeLayers = 0;
                rb.useGravity = true;

                bubbleArt.SetActive(false);

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

    public enum State
    {
        MARBLE,
        BUBBLE
    }
}
