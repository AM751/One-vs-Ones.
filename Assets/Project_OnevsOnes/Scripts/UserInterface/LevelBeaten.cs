using Unity.VisualScripting;
using UnityEngine;

public class LevelBeaten : MonoBehaviour
{
    [SerializeField] private Canvas _levelBeatenCanvas;
    [SerializeField] private GameObject _player;

    void Start()
    {
        _levelBeatenCanvas.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _levelBeatenCanvas.enabled = true;
            Time.timeScale = 0;
            
            PlayerController controller = _player.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.enabled = false;
            }
        }
    }
}
