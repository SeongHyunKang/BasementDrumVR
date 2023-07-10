using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    public string targetTag;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(targetTag))
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
