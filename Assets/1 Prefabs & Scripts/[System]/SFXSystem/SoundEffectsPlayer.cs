using System.Collections;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    // Adjustable parameters
    public float volume = 1.0f;
    public int maxAudioSources = 1;
    public AudioClip[] audioClips;

    // Internal variables
    private AudioSource[] audioSources;

    void Start()
    {
        // Initialize audio sources on startup
        InitializeAudioSources();
    }

    // Initialize audio sources and set their volumes
    private void InitializeAudioSources()
    {
        audioSources = new AudioSource[maxAudioSources];

        for (int i = 0; i < maxAudioSources; i++)
        {
            // Add AudioSource component to the game object
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            // Set volume for each AudioSource
            audioSources[i].volume = volume;
        }
    }

    // Play a random audio clip immediately
    public void PlayRandom()
    {
        // Get the next random audio clip
        AudioClip clipToPlay = GetNextClip();
        
        // Get an available AudioSource
        AudioSource availableSource = GetAvailableAudioSource();

        // Play the audio clip on the available AudioSource
        if (availableSource != null)
        {
            availableSource.clip = clipToPlay;
            availableSource.Play();
        }
    }

    // Begin playing background tracks
    public void PlayInRow()
    {
        // Start a coroutine for continuous background track playback
        StartCoroutine(PlayInRowCoroutine());
    }

    // Coroutine for playing tracks in a continuous loop
    private IEnumerator PlayInRowCoroutine()
    {
        while (true)
        {
            // Get the next random audio clip
            AudioClip nextClip = GetNextClip();
            // Get an available AudioSource
            AudioSource availableSource = GetAvailableAudioSource();

            // Play the audio clip on the available AudioSource
            if (availableSource != null)
            {
                availableSource.clip = nextClip;
                availableSource.Play();
            }

            // Wait for the duration of the audio clip
            yield return new WaitForSeconds(nextClip.length);
        }
    }

    // Get an available AudioSource from the array or create a new one
    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            // Check if the AudioSource is not currently playing
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // Create a new AudioSource if all are occupied
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.volume = volume;
        return newSource;
    }

    // Get a random audio clip from the array
    private AudioClip GetNextClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }
}
