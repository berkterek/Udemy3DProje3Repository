using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class SoundController : MonoBehaviour
    {
        AudioSource _audioSource;

        public bool IsPlaying => _audioSource.isPlaying;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetClip(AudioClip clip)
        {
            if (clip == _audioSource.clip) return;
            
            _audioSource.clip = clip;
        }

        public void PlaySound(Vector3 position)
        {
            if (_audioSource.isPlaying) return;
            
            transform.position = position;
            _audioSource.Play();
        }
    }    
}

