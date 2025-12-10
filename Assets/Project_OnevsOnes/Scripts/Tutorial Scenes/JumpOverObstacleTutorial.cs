using UnityEngine;

public class JumpOverObstacleTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _jumpTutorialCanvas;
    private bool _isjumpTutorialEnabled;

    private void Start()
    {
        _jumpTutorialCanvas.enabled = false;
    }

    private void Update()
    {
        if (_isjumpTutorialEnabled)
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
        if (_jumpTutorialCanvas != null)
        {
            _jumpTutorialCanvas.enabled = true;
            _isjumpTutorialEnabled = true;
        }
    }

    private void JumpOverTutorialDisable()
    {
        if (_jumpTutorialCanvas != null)
        {
            _jumpTutorialCanvas.enabled = false;
            _isjumpTutorialEnabled = false;
        }
    }
}
