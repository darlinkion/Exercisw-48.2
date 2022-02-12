using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string AnimationTrigger = "Person";
    private bool _isOpened;

    public bool DoorIsOpen => _isOpened;

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
        _animator.SetTrigger(AnimationTrigger);
    }
}