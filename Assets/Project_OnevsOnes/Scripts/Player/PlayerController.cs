using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerMoveSpeed;
    private Rigidbody2D _rigidbody2D;
    
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
    }
}
