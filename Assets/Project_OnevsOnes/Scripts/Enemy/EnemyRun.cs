using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyJumpForce;
    [SerializeField] private float _speedIncreaseRate;
    [SerializeField] private float _maxSpeed;
    //[SerializeField] public GameObject obstacle;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    public LayerMask enemyJumpLayers;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyJumpLayers) != 0)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _enemyJumpForce);
        }
    }

    // void EnemyJump()
    // {
    //     if (_isGrounded)
    //     {
    //         _rigidbody2D.linearVelocity = new Vector2 (_rigidbody2D.linearVelocity.x, _enemyJumpForce);
    //     }    
    // }
    
    // Update is called once per frame
    void Update()
    {
        _enemyMoveSpeed = Mathf.MoveTowards(_enemyMoveSpeed, _maxSpeed, _speedIncreaseRate * Time.deltaTime);
        
        _rigidbody2D.linearVelocity = new Vector2(_enemyMoveSpeed, _rigidbody2D.linearVelocity.y);
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle (_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
}
