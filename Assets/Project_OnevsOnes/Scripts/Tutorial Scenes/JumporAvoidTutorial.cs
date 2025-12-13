using UnityEngine;

public class JumporAvoidTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _jumporAvoidTutorialCanvas;
    private bool _isjumporAvoidTutorialEnabled;

    private void Start()
    {
        _jumporAvoidTutorialCanvas.enabled = false;
    }

    private void Update()
    {
        if (_isjumporAvoidTutorialEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TheTutorialDisable();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            TheTutorialEnable();
        }

        else
        {
            TheTutorialDisable();
        }

    }

    private void TheTutorialEnable()
    {
        if (_jumporAvoidTutorialCanvas != null)
        {
            _jumporAvoidTutorialCanvas.enabled = true;
            _isjumporAvoidTutorialEnabled = true;
        }
    }

    private void TheTutorialDisable()
    {
        if (_jumporAvoidTutorialCanvas != null)
        {
            _jumporAvoidTutorialCanvas.enabled = false;
            _isjumporAvoidTutorialEnabled = false;
        }
    }

}
