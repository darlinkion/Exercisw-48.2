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

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _isOpened = false;
        _deltaVolume = 0.01f;
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
        }

        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        int iterations = 100;

        if (_isOpened)
        {
            for (int i = 0; i < iterations; i++)
            {
                _volume = Mathf.MoveTowards(_audioSource.volume, _audioSource.maxDistance, _deltaVolume);
                _audioSource.volume = _volume;

                yield return waitForFixedUpdate;
            }
        }
        else
        {
            for (int i = 0; i < iterations; i++)
            {
                _volume = Mathf.MoveTowards(_audioSource.volume, _audioSource.minDistance, -_deltaVolume);
                _audioSource.volume = _volume;

                yield return waitForFixedUpdate;
            }
        }
    }
}
