using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name, float _pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.pitch = _pitch;

        if (!s.source.isPlaying)
            s.source.Play();
        else if (s.source.isPlaying) //stop and start again if sound is already playing
        { s.source.Stop(); s.source.Play(); }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
