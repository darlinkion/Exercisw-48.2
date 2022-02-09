using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AlarmManagement : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    private bool _isOpened;
    private float _deltaVolume;
    private float _volume;
    private float _maxVolume;
    private float _minVolume;
    private WaitForFixedUpdate _waitForFixedUpdate;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _isOpened = false;
        _deltaVolume = 0.01f;
        _maxVolume = 1f;
        _minVolume = 0f;
        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        _audioSource.Play();
        _audioSource.volume = 0f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isOpened = !_isOpened;

            if (_isOpened)
            {
                StopCoroutine(DecreaseVolume());
                StartCoroutine(RaiseVolume());
            }
            else
            {
                StopCoroutine(RaiseVolume());
                StartCoroutine(DecreaseVolume());
            }
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_audioSource.volume != _minVolume)
        {
            _volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _deltaVolume);
            _audioSource.volume = _volume;

            yield return _waitForFixedUpdate;
        }
    }

    private IEnumerator RaiseVolume()
    {
        while (_audioSource.volume != _maxVolume)
        {
            _volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _deltaVolume);
            _audioSource.volume = _volume;

            yield return _waitForFixedUpdate;
        }
    }
}
