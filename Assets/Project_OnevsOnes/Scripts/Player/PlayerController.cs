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
    [SerializeField] public AudioClip jumpAudio;
    public AudioSource sprintAudioSource;
    public AudioSource jumpAudioSource;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private InputSystem_Actions _inputActions;
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        sprintAudioSource = GetComponent<AudioSource>();
        jumpAudioSource = GetComponent<AudioSource>();
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
        if (_isGrounded )
        {
            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, playerJumpForce);
        }

        if (jumpAudioSource != null && jumpAudio != null && _isGrounded)
        {
            jumpAudioSource.PlayOneShot(jumpAudio);
        }
    }

    void Sprint(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            playerMoveSpeed = Mathf.Min(playerMoveSpeed + sprintSpeed , maxSprintSpeed);
        }
        
        if (sprintAudioSource != null && sprintAudio != null && _isGrounded)
        {
            sprintAudioSource.PlayOneShot(sprintAudio);
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
