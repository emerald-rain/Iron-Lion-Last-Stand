using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public float volume = 1.0f;
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;

    private int currentIndex = 0;

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
        AudioClip clipToPlay = audioClips[currentIndex];
        currentIndex = (currentIndex + 1) % audioClips.Length;
        return clipToPlay;
    }
}

