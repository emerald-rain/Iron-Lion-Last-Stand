using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public float volume = 1.0f;
    public int maxAudioSources = 1;
    private List<AudioSource> audioSourcesList = new List<AudioSource>();
    public AudioClip[] audioClips;

    void Start()
    {
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = volume;
            audioSourcesList.Add(newSource);
        }
    }

    public void PlayRandom()
    {
        AudioClip clipToPlay = GetNextClip();

        AudioSource availableSource = GetAvailableAudioSource();
        if (availableSource != null)
        {
            availableSource.clip = clipToPlay;
            availableSource.Play();
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSourcesList)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        // Если все источники заняты, создаем новый
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.volume = volume;
        audioSourcesList.Add(newSource);
        return newSource;
    }

    private AudioClip GetNextClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }
}
