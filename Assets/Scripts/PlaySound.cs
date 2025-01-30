using UnityEngine;
using System.Collections.Generic;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioToPlay;
    [Range(0,1)] [SerializeField] private float _volume;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlaySounds()
    {
        _audioSource.volume = _volume;

        if (_audioToPlay.Count > 1)
        {
            int audioIndx = Random.Range(0,_audioToPlay.Count);

            _audioSource.clip = _audioToPlay[audioIndx];
        }
        else
            _audioSource.clip = _audioToPlay[0];
        
        if(!_audioSource.isPlaying)
            _audioSource.Play();

        Debug.Log($"{gameObject.name} is Playing Sounds");
    } 
}
