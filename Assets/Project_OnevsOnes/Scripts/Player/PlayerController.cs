using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerMoveSpeed;
    [SerializeField] public float playerJumpForce;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float maxSprintSpeed;
    //[SerializeField] public float sprintDeceleration;
    [SerializeField] public AudioClip sprintAudio;
    public AudioSource audioSource;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private InputSystem_Actions _inputActions;
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Jump.performed += Jump;
        _inputActions.Player.Sprint.performed += Sprint;
        _inputActions.Enable();

    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump;
        _inputActions.Player.Sprint.performed -= Sprint;
        _inputActions.Disable();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, playerJumpForce);
        }
    }

    void Sprint(InputAction.CallbackContext context)
    {
        playerMoveSpeed = Mathf.Min(playerMoveSpeed + sprintSpeed , maxSprintSpeed);
        if (audioSource != null && sprintAudio != null)
        {
            audioSource.PlayOneShot(sprintAudio);
        }
    }
    void Start()
    {
        //playerMoveSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.linearVelocity = new Vector2(playerMoveSpeed, _rigidbody2D.linearVelocity.y);
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
}
