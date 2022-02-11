using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isOpened;
    private const string _animationTrigger = "Person";

    private void Awake()
    {
        _isOpened = false;
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
