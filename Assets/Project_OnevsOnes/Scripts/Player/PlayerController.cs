using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement.")]
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _maxSprintSpeed;
    [SerializeField] private float _sprintSeconds;
    
    
    [Header("Player Components.")]
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    
    [Header("System Feedback Visuals.")]
    [SerializeField] private Color _sprintColor;
    
    [Header ("Ground Check Zone.")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Player InputSystem Action Zone.")]
    private InputSystem_Actions _inputActions;
    
    [Header("Effects.")]
    [SerializeField] private ParticleSystem _playerJumpEffect;

    //private Coroutine _sprintCoroutine;
    private float _currentMoveSpeed;
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;

        //Actual Speed:
        _currentMoveSpeed = _playerMoveSpeed;
    }

    void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Jump.performed += Jump;
        _inputActions.Player.Sprint.performed += Sprinting;
        _inputActions.Player.Sprint.canceled += StopSprint;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump;
        _inputActions.Player.Sprint.performed -= Sprinting;
        _inputActions.Player.Sprint.canceled -= StopSprint;
        _inputActions.Disable();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, _playerJumpForce);
            PlayerAudio.Instance.playSoundOnJump();
        }

        if (_playerJumpEffect != null)
        {
            _playerJumpEffect.Play();
        }
    }
    
    void Sprinting(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _playerMoveSpeed = Mathf.Min(_playerMoveSpeed + _sprintSpeed , _maxSprintSpeed);
            PlayerAudio.Instance.playSoundOnSprint();

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _sprintColor;
            }

        }
    }

    
    void StopSprint(InputAction.CallbackContext context)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultColor;
        }

    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            _playerMoveSpeed = -5;
        }
    }
    
    void FixedUpdate()
    {
        //For the Player to check the ground platform:
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
        
        //For the Player to continue the Auto-Run throughout the game:
        _rigidbody2D.linearVelocity = new Vector2(_playerMoveSpeed, _rigidbody2D.linearVelocity.y);
    }
}
