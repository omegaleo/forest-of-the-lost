using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    [Header("SFX")]
    [SerializeField] private List<AudioClip> sfxSounds;

    public enum Source { MUSIC, SFX }

    public static AudioController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public float GetVolume(Source source)
    {
        if(source == Source.MUSIC)
        {
            return music.volume;
        }
        else
        {
            return sfx.volume;
        }
    }

    public void SetVolume(Source source, float volume)
    {
        if (source == Source.MUSIC)
        {
            music.volume = volume;
        }
        else
        {
            sfx.volume = volume;
        }
    }

    public void PlayClip(Source source, AudioClip clip)
    {
        if (source == Source.MUSIC)
        {
            music.clip = clip;
            music.Play();
        }
        else
        {
            sfx.clip = clip;
            sfx.Play();
        }
    }

    public void Stop(Source source)
    {
        if (source == Source.MUSIC)
        {
            music.Stop();
        }
        else
        {
            sfx.Stop();
        }
    }

    public void PlaySFX(int id)
    {
        if(sfxSounds.Count > id)
        {
            sfx.clip = sfxSounds[id];
            sfx.Play();
        }
    }
}
