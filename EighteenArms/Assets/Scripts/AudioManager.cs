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

    [Header("FX sounds")]
    public AudioClip deathFXClip;
    public AudioClip orbFXClip;
    public AudioClip doorOpenFXClip;
    public AudioClip startLevelClip;
    public AudioClip winClip;



    [Header("Player sounds")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip getHurtClip;
    public AudioClip slideJumpClip;


    public AudioClip jumpVoiceClip;
    public AudioClip deathVoiceClip;
    public AudioClip orbVoiceClip;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    [Header("Audio Group")]
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup FXGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup voiceGroup;
    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();

        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        fxSource.outputAudioMixerGroup = FXGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;


        StartLevelAudio();
    }

    public static void PlayFootstepAudio() {
        int index = Random.Range(0, current.walkStepClips.Length);
        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }


    public static void PlayCrouchAudio()
    {
        int index = Random.Range(0, current.crouchStepClips.Length);
        current.playerSource.clip = current.crouchStepClips[index];
        current.playerSource.Play();
    }
    void StartLevelAudio() {
        current.ambientSource.clip = current.ambientClip;
        current.musicSource.clip = current.musicClip;
        current.ambientSource.loop = true;
        current.musicSource.loop = true;

        current.ambientSource.Play();
        current.musicSource.Play();

        current.fxSource.clip = current.startLevelClip;
        current.fxSource.Play();
    }

    public static void PlayerWinAudio() {
        current.fxSource.clip = current.winClip;
        current.fxSource.Play();
        current.playerSource.Stop();
    }

    public static void PlayJumpAudio() {
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }

    public static void PlaySlideJumpAudio() {
        current.playerSource.clip = current.slideJumpClip;
        current.playerSource.Play();
    }

    public static void PlayGetHurtAudio()
    {
        current.playerSource.clip = current.getHurtClip;
        current.playerSource.Play();

    }

    public static void PlayDeathAudio() {
        //current.playerSource.clip = current.deathClip;
        //current.playerSource.Play();

        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();

        //current.fxSource.clip = current.deathFXClip;
        //current.fxSource.Play();
    }

    public static void PlayOrbAudio() {
        current.fxSource.clip = current.orbFXClip;
        current.fxSource.Play();

        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();
    }

    public static void PlayDoorOpenAudio() {
        current.fxSource.clip = current.doorOpenFXClip;
        current.fxSource.PlayDelayed(1f);
    }

}
