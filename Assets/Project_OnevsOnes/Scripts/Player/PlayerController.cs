using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerMoveSpeed;
    [SerializeField] public float playerJumpForce;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //playerMoveSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.linearVelocity = new Vector2(playerMoveSpeed * Time.deltaTime, _rigidbody2D.linearVelocity.y);
        
       //Jump Input:
       if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && _isGrounded)
       {
           _rigidbody2D.linearVelocity = Vector2.up * playerJumpForce * Time.deltaTime;
       }
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
}
