using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement.")]
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _sprintAccelaration;
    [SerializeField] private float _sprintDeceleration;
    [SerializeField] private float _maxSprintSpeed;
    [SerializeField] private float _sprintSeconds;
    [SerializeField] private GameObject _obstacle;
    private bool _isSprinting;
    
    [Header("Player's Speed Check.")]
    [SerializeField] private float _currentMoveSpeed;
    
    [Header("Player Components.")]
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultSpriteColor;
    private Light2D _defaultLight;
    
    [Header("System Feedback Visuals and Light Effects.")]
    [SerializeField] private Color _sprintColor;
    [SerializeField] private Light2D _sprintEffectLight;
    [SerializeField] private Color _sprintLightColor;
    [SerializeField] private float _lightFadeDuration;
    
    private Color _defaultLightColor;
    private Coroutine _lightFadeCoroutine;
    
    [Header ("Ground Check Zone.")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Player InputSystem Action Zone.")]
    private InputSystem_Actions _inputActions;
    
    [Header("Effects.")]
    [SerializeField] private ParticleSystem _playerJumpEffect;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSpriteColor = _spriteRenderer.color;

        //Actual Speed:
        _currentMoveSpeed = _playerMoveSpeed;
        
        //Light Effect:
        if (_sprintEffectLight != null)
        {
            _defaultLight = _sprintEffectLight.GetComponent<Light2D>();
        }
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
            _isSprinting = true;
           // _playerMoveSpeed = Mathf.Min(_playerMoveSpeed + _sprintSpeed , _maxSprintSpeed);
            PlayerAudio.Instance.playSoundOnSprint();

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _sprintColor;
            }

            UpdateLightColor(_sprintLightColor);
        }
    }

    private void UpdateLightColor(Color color)
    {
        if (_sprintEffectLight == null) return;

        if (_lightFadeCoroutine != null)
        {
            StopCoroutine(_lightFadeCoroutine);
        }
        
        _lightFadeCoroutine = StartCoroutine(FadeLightRoutine (color, _lightFadeDuration));
    }
    
    void StopSprint(InputAction.CallbackContext context)
    {
        _isSprinting = false;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultSpriteColor;
        }

    }

    private IEnumerator FadeLightRoutine(Color color, float duration)
    {
        Color startColor = _sprintEffectLight.color;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            _sprintEffectLight.color = Color.Lerp(startColor, color, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _sprintEffectLight.color = color;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            _currentMoveSpeed = -5;
        }
    }
    
    void FixedUpdate()
    {
        //For the Player to check the ground platform:
        //if (_groundCheck != null)
        //{
            _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
       // }
        
        
        float targetSpeed = _isSprinting ? _maxSprintSpeed : _playerMoveSpeed;
        
        //bool isSpeedingUp = _currentMoveSpeed < targetSpeed;

        float currentRate = _isSprinting ? _sprintAccelaration : _sprintDeceleration;

        // if (isSpeedingUp)
        // {
        //     currentRate = _sprintAccelaration;
        // }
        //
        // else
        // {
        //     currentRate = _sprintDeceleration;
        // }
        
        //Accelaration Part:
        _currentMoveSpeed = Mathf.MoveTowards (_currentMoveSpeed, targetSpeed,currentRate * Time.fixedDeltaTime );
        
        //For the Player to continue the Auto-Run throughout the game:
        _rigidbody2D.linearVelocity = new Vector2(_currentMoveSpeed, _rigidbody2D.linearVelocity.y);
    }
}
