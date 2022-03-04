using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler self;

    public AudioSource MusicSource;
    public AudioSource EffectSource;
    public AudioSource MenuSource;
    public AudioSource WalkSource;

    [Range(0, 1)]
    public float volume = 1;

    [Serializable]
    public struct NamedClip
    {
        public string name;
        public AudioClip[] clip;
    }

    public NamedClip[] clipArray;

    public static Dictionary<string, AudioClip[]> clips = new Dictionary<string, AudioClip[]>();

    // Start is called before the first frame update
    void Awake()
    {
        if (!self) self = this;

        // fill clip dict
        foreach (NamedClip c in clipArray)
        {
            if (clips.ContainsKey(c.name))
                throw new ArgumentException("Two same Audio Clip Names");
            else 
                clips.Add(c.name, c.clip);
        }
    }

    public static void PlayClip(AudioClip c)
    {
        self.EffectSource.PlayOneShot(c);
    }

    public static void PlayClip(AudioClip[] c)
    {
        //play random clip from list
        PlayClip(c[UnityEngine.Random.Range(0, c.Length)]);
    }

    public static void SetVolume(float volMusic, float volEffects)
    {
        // self.MusicSource.volume = volMusic;
        // self.EffectSource.volume = volEffects;
        // self.MenuSource.volume = volEffects;
        // self.WalkSource.volume = volEffects;
    }

    public static void PlayClip(string s) => PlayClip(clips[s]);

    public static void PlayClick()
    {
        // self.MenuSource.Play();
    }

    public static void StartWalk()
    {
        // if (!self.WalkSource.isPlaying)
        //     self.WalkSource.Play();
    }

    public static void StopWalk()
    {
        // self.WalkSource.Pause();
    }
}
