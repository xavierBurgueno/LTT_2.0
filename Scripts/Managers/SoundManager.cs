using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager instance = null;
    [SerializeField] Settings soundData;


    private void Awake()
    {
        //Set Master Volume when script is called;
        AudioListener.volume = soundData.masterVolume;

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        foreach(Sound s in sounds)
        {
            s. source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            //Using the settings data to set volume on sounds;
            if (s.isMusic)
                s.source.volume = soundData.musicVolume;
            else if (s.isSoundEffect)
                s.source.volume = soundData.soundFxVolume;
        }
    }

   public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Lambda expression
        if (s == null)
            return;
        if(!s.source.isPlaying)
            s.source.Play();
    }

    public void PlayRaw(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Lambda expression
        if (s == null)
            return;

            s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); 
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); 
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Pause();
    }
}
