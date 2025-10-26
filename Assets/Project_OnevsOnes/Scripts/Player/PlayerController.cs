using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement.")]
    [SerializeField] public float playerMoveSpeed;
    [SerializeField] public float playerJumpForce;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float maxSprintSpeed;
    //[SerializeField] public float sprintDeceleration;
    
    [Header("Audio Zone.")]
    [SerializeField] public AudioClip sprintAudio;
    [SerializeField] public AudioClip jumpAudio;
    public AudioSource sprintAudioSource;
    public AudioSource jumpAudioSource;
    
    [Header("Player Components.")]
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    [Header ("Ground Check Zone.")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Player InputSystem Action Zone.")]
    private InputSystem_Actions _inputActions;
    
    [Header("Effects.")]
    [SerializeField] private ParticleSystem _playerJumpEffect;
    // [SerializeField] private Transform _playerJumpVisual;
    // [SerializeField] private float _playerJumpEffectDuration;
    // [SerializeField] private Vector2 _squashScale = new Vector2(1.2f, 0.8f);
    // [SerializeField] private Vector2 _stretchScale = new Vector2(0.8f, 1.2f);
    //
    // private Vector2 _originalVisualScale;
    // private Coroutine _squashStretchCoroutine;
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
        if (_isGrounded)
        {
            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, playerJumpForce);
            
        }

        if (jumpAudioSource != null && jumpAudio != null && _isGrounded)
        {
            jumpAudioSource.PlayOneShot(jumpAudio);
        }

        if (_playerJumpEffect != null)
        {
            _playerJumpEffect.Play();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            playerMoveSpeed = -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.linearVelocity = new Vector2(playerMoveSpeed, _rigidbody2D.linearVelocity.y);
    }

    void FixedUpdate()
    {
        // bool playerGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
        // if (playerGrounded && !_isGrounded)
        // {
        //     if (_playerJumpEffect != null)
        //     {
        //         _playerJumpEffect.enabled = true;
        //     }
        //     else if (!playerGrounded && _isGrounded)
        //     {
        //         if (_playerJumpEffect != null)
        //         {
        //             _playerJumpEffect.enabled = false;
        //         }
        //     }
        // }
    }
}
