using System;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _runAccomplishedCanvas;

    private void Start()
    {
        _runAccomplishedCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _runAccomplishedCanvas.enabled = true;
        }
    }
}
