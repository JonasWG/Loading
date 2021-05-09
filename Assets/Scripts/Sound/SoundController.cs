using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    
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
        
        if (Instance != null)
            GameObject.Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayMusic("Game1");
    }

    public void PlayMusic(string sceneName)
    {
        StopAll();
        
        Regex regex = new Regex("Game\\d");

        if (regex.IsMatch(sceneName))
            sceneName = "Game";

        Debug.Log(sceneName);
        
        Sound s = Array.Find(sounds, sound => sound.name == sceneName);
        s.pitch = 1;
        
        if (!s.source.isPlaying)
            s.source.Play();
        else if (s.source.isPlaying) //stop and start again if sound is already playing
        { s.source.Stop(); s.source.Play(); }
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

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.source.Stop();
        }
    }
    
}
