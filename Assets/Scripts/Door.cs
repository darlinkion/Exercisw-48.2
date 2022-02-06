using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isOpened;
    private string _animationTrigger;

    private void Awake()
    {
        _isOpened = false;
        _animationTrigger = "Person";
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isOpened = !_isOpened;
            Open();
        }
    }

    private void Open()
    {
        _animator.SetTrigger(_animationTrigger);
    }
}
