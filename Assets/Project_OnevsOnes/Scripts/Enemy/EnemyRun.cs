using UnityEngine;

public class EnemyRun : MonoBehaviour
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
        Time.timeScale = 1;
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
