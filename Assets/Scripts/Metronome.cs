using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] metronomeBeats;

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
}
