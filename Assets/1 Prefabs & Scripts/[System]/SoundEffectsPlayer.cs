using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] grassSounds;

    void PlayRandomGrassSound() {
        // Вибираємо випадковий звук з масиву
        int randomIndex = Random.Range(0, grassSounds.Length);

        // Перевірка, чи є AudioSource та звук для відтворення
        if (src != null && grassSounds[randomIndex] != null)
        {
            // Встановлюємо вибраний звук та відтворюємо його
            src.clip = grassSounds[randomIndex];
            src.Play();
            Debug.Log("Sound played");
        }
    }
}
