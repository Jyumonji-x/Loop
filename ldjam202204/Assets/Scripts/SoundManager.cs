using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip beat;
    private void Awake()
    {
        instance = this;
    }

   public void BeatAudio()
    {
        audioSource.clip = beat;
        audioSource.Play();
    }
}
