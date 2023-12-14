using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource[] audioSources; // Массив источников звука
    public AudioClip[] audioClips; // Массив звуков для воспроизведения

    private int currentIndex = 0;

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
        currentIndex = (currentIndex + 1) % audioClips.Length; // Переход к следующему звуку
        return clipToPlay;
    }
}

