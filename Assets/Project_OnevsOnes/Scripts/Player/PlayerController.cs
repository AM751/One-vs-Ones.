using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Stamina))] 
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private float _sprintSpeed; 
    [SerializeField] private float _sprintAccelaration;
    [SerializeField] private float _sprintDeceleration;
    [SerializeField] private float _maxSprintSpeed;

    [Header("System Connections")]
    
    private float _currentMoveSpeed;
    private Stamina _staminaSystem;
    private PlayerHealth _playerHealth;

    [Header("Player Components")]
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultSpriteColor;
    private Light2D _defaultLight;
    
    [Header("Visual Effects")]
    [SerializeField] private Color _sprintColor;
    [SerializeField] private Light2D _sprintEffectLight;
    [SerializeField] private Color _sprintLightColor;
    [SerializeField] private float _lightFadeDuration;
    
    private Coroutine _lightFadeCoroutine;
    
    [Header ("Ground Check Zone")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    private InputSystem_Actions _inputActions;
    private bool _isSprintingInput;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem _playerJumpEffect;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSpriteColor = _spriteRenderer.color;

        
        _staminaSystem = GetComponent<Stamina>();
        _playerHealth = GetComponent<PlayerHealth>();

        _currentMoveSpeed = _playerMoveSpeed;
        
        if (_sprintEffectLight != null)
        {
            _defaultLight = _sprintEffectLight.GetComponent<Light2D>();
        }
    }

    void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Jump.performed += Jump;
        
    
        _inputActions.Player.Sprint.performed += ctx => _isSprintingInput = true;
        _inputActions.Player.Sprint.canceled += StopSprint;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump;
       
        _inputActions.Player.Sprint.canceled -= StopSprint;
        _inputActions.Disable();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
           
            if (_staminaSystem.isExhausted) return;

            
            _staminaSystem.InstantDrain(10f);

            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, _playerJumpForce);
           
             PlayerAudio.Instance.playSoundOnJump(); 
        }

        if (_playerJumpEffect != null)
        {
            _playerJumpEffect.Play();
        }
    }
    
    void StopSprint(InputAction.CallbackContext context)
    {
        _isSprintingInput = false;
        
        
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultSpriteColor;
        }
        
    }

    
    private void UpdateLightColor(Color color)
    {
        if (_sprintEffectLight == null) return;
        if (_lightFadeCoroutine != null) StopCoroutine(_lightFadeCoroutine);
        _lightFadeCoroutine = StartCoroutine(FadeLightRoutine (color, _lightFadeDuration));
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
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);

        
        float globalMultiplier = 1f;
        if (GameManager.Instance != null) globalMultiplier = GameManager.Instance.globalSpeedMultiplier;

        
        float healthMultiplier = 1f;
       
        if (_playerHealth != null && _playerHealth._currentHealth < 30) 
        {
            healthMultiplier = 0.6f; 
        }

        
        bool isSprinting = _isSprintingInput && !_staminaSystem.isExhausted;

        float targetSpeed = _playerMoveSpeed;

        if (isSprinting)
        {
            targetSpeed = _maxSprintSpeed; 
            
            
            _staminaSystem.DrainStamina(Time.fixedDeltaTime);

           
            if (_spriteRenderer != null) _spriteRenderer.color = _sprintColor;
            UpdateLightColor(_sprintLightColor);
        }
        else
        {
            
            if (_spriteRenderer != null) _spriteRenderer.color = _defaultSpriteColor;
        }

        
        targetSpeed = targetSpeed * globalMultiplier * healthMultiplier;

    
        float currentRate = isSprinting ? _sprintAccelaration : _sprintDeceleration;
        _currentMoveSpeed = Mathf.MoveTowards (_currentMoveSpeed, targetSpeed, currentRate * Time.fixedDeltaTime );
        
        // Move
        _rigidbody2D.linearVelocity = new Vector2(_currentMoveSpeed, _rigidbody2D.linearVelocity.y);
    }
}