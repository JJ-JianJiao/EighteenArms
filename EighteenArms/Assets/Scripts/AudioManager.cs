using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    [Header("Environment Sounds")]
    public AudioClip ambientClip;
    public AudioClip musicClip;

    [Header("Player sounds")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;

    public AudioClip jumpVoiceClip;


    private void Awake()
    {
        current = this;
    }


}
