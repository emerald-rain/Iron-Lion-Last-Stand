using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public float volume = 1.0f;
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;

    void Start() {
        foreach (AudioSource source in audioSources)
        {
            source.volume = volume;
        }
    }

    public void PlayRandom()
    {
        AudioClip clipToPlay = GetNextClip();

        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clipToPlay;
                source.Play();
                break;
            }
        }
    }

    private AudioClip GetNextClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }
}

