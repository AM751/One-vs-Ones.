using UnityEngine;

public class JumpBetweenTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _jumpBetweenTutorialCanvas;
    private bool _isjumpBetweenTutorialEnabled;

    private void Start()
    {
        _jumpBetweenTutorialCanvas.enabled = false;
    }

    private void Update()
    {
        if (_isjumpBetweenTutorialEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpOverTutorialDisable();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            JumpOverTutorialEnable();
        }

        else
        {
            JumpOverTutorialDisable();
        }

    }

    private void JumpOverTutorialEnable()
    {
        if (_jumpBetweenTutorialCanvas != null)
        {
            _jumpBetweenTutorialCanvas.enabled = true;
            _isjumpBetweenTutorialEnabled = true;
        }
    }

    private void JumpOverTutorialDisable()
    {
        if (_jumpBetweenTutorialCanvas != null)
        {
            _jumpBetweenTutorialCanvas.enabled = false;
            _isjumpBetweenTutorialEnabled = false;
        }
    }
}
