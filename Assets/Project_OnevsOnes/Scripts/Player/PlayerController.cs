using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement.")]
    [SerializeField] public float playerMoveSpeed;
    [SerializeField] public float playerJumpForce;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float maxSprintSpeed;
    
    
    [Header("Player Components.")]
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
   // private Color _defaultColor;
    
    [Header("System Feedback Visuals.")]
   // [SerializeField] private Color _sprintColor;
    
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
        
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        // if (_spriteRenderer == null)
        // {
        //    // _defaultColor = _spriteRenderer.color;
        // }
    }

    void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Jump.performed += Jump;
        _inputActions.Player.Sprint.performed += Sprinting;
        //_inputActions.Player.Sprint.performed += NotSprinting;
        _inputActions.Enable();

    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump;
        _inputActions.Player.Sprint.performed -= Sprinting;
        //_inputActions.Player.Sprint.performed -= NotSprinting;
        _inputActions.Disable();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, playerJumpForce);
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
            playerMoveSpeed = Mathf.Min(playerMoveSpeed + sprintSpeed , maxSprintSpeed);
            PlayerAudio.Instance.playSoundOnSprint();

            // if (_spriteRenderer != null)
            // {
            //     _spriteRenderer.color = _sprintColor;
            // }
        }
        
    }

    // void NotSprinting(InputAction.CallbackContext context)
    // {
    //     if (_spriteRenderer != null)
    //     {
    //         _spriteRenderer.color = _defaultColor;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            playerMoveSpeed = -5;
        }
    }
    void FixedUpdate()
    {
        //For the Player to check the ground platform:
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
        
        //For the Player to continue the Auto-Run throughout the game:
        _rigidbody2D.linearVelocity = new Vector2(playerMoveSpeed, _rigidbody2D.linearVelocity.y);
    }
}
