using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement.")]
    [SerializeField] public float playerMoveSpeed;
    [SerializeField] public float playerJumpForce;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float maxSprintSpeed;
    
    // [Header("Audio Zone.")]
    // [SerializeField] public AudioClip sprintAudio;
    // [SerializeField] public AudioClip jumpAudio;
    // public AudioSource sprintAudioSource;
    // public AudioSource jumpAudioSource;
    
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
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
            PlayerAudio.Instance.playSoundOnJump();
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
            PlayerAudio.Instance.playSoundOnSprint();
        }
        
    }

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
