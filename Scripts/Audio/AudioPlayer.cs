using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPlayer 
{ 
    public static void CreateNew(GameObject gameObject, AudioSource source, AudioClip clip)
    {
        if (source.isPlaying)
        {
            AudioSource source2 = gameObject.AddComponent<AudioSource>();
            source2.clip = clip;
            source2.Play();
        }
        else
        {
            source.Play();
        }
    }
    public static void CreateNew(GameObject gameObject, AudioSource source, AudioClip clip, float volume, float pitch)
    {
        if (source.isPlaying)
        {
            AudioSource source2 = gameObject.AddComponent<AudioSource>();
            source2.clip = clip;
            source2.volume = volume;
            source2.pitch = pitch;
            source2.Play();
        }
        else
        {
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }
    public static void NoCreate(AudioSource source)
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
    public static void Stop(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
            source.Play();
        }
        else
        {
            source.Play();
        }
    }
}
