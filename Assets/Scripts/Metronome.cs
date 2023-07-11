using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] metronomeBeats;

    private int currentBeatIndex = 0;

    private void Start()
    {
        PlayNextBeat();
    }

    private void PlayNextBeat()
    {
        audioSource.clip = metronomeBeats[currentBeatIndex];
        audioSource.Play();

        currentBeatIndex = (currentBeatIndex + 1) % metronomeBeats.Length;

        Invoke(nameof(PlayNextBeat), audioSource.clip.length);
    }

    public void PlayBeat(int beatIndex)
    {
        if (beatIndex >= 0 && beatIndex < metronomeBeats.Length)
        {
            audioSource.Stop();
            audioSource.clip = metronomeBeats[beatIndex];
            audioSource.Play();
        }
    }
}
