using UnityEngine;
using System.Collections.Generic;
using System;

public enum CLIPTYPE
{
    MUSIC, WIN, LOSE, HUMAN_PLAY, AI_PLAY, CLICK, FAIL_CLICK
}

[Serializable]
public struct AudioEntry {

    public string name;
    public AudioClip clip;
    public CLIPTYPE genre;
}

public class SoundManager : Singleton<SoundManager> {

    #region Variables
    public List<AudioSource> audioSources = new List<AudioSource>();
    public List<AudioEntry> audioClips = new List<AudioEntry>();
    public AudioSource musicPlayer;
    Dictionary<CLIPTYPE, List<AudioEntry>> genreList = new Dictionary<CLIPTYPE, List<AudioEntry>>();
    int maxSources = 15;
    bool isMusicMuted = false;
    bool isSoundMuted = false;
    #endregion

    #region Start
    private void Start()
    {
        string[] temp = System.Enum.GetNames(typeof(CLIPTYPE));
        for (int i = 0; i < temp.Length; i++)
        {
            genreList.Add((CLIPTYPE)System.Enum.Parse(typeof(CLIPTYPE), temp[i]), new List<AudioEntry>());
        }
        for (int i = 0; i < audioClips.Count; i++)
        {
            genreList[audioClips[i].genre].Add(audioClips[i]);
        }
    }
    #endregion

    #region Set Sound On or Off
    public void SetSound(bool sound)
    {
        isSoundMuted = sound;
        foreach (AudioSource source in audioSources)
        {
            source.mute = isSoundMuted;
        }
    }
    #endregion

    #region Set Music On or Off
    public void SetMusic(bool music)
    {
        isMusicMuted = music;
        if (musicPlayer != null)
        {
            musicPlayer.mute = isMusicMuted;
        }
    }
    #endregion

    #region Play Event From Source
    public void PlayEventFromSource(AudioSource source, CLIPTYPE genre)
    {
        source.clip = genreList[genre][UnityEngine.Random.Range(0, genreList[genre].Count)].clip;
        source.Play();
    }
    #endregion

    #region Play Clip From Source
    public void PlayClipFromSource(AudioSource source, string clip)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (clip == audioClips[i].name)
            {
                source.clip = audioClips[i].clip;
                source.Play();
                return;
            }
        }
    }
    #endregion

    #region Play Music
    public void PlayMusic(string musicClip)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (musicClip == audioClips[i].name)
            {
                musicPlayer.clip = audioClips[i].clip;
            }
        }
        musicPlayer.Play();
    }
    #endregion

    #region Play Event
    public void PlayEvent(CLIPTYPE genre)
    {
        AudioSource source = GetFreeSource();
        source.clip = genreList[genre][UnityEngine.Random.Range(0, genreList[genre].Count)].clip;
        source.Play();
    }
    #endregion

    #region Play Clip
    public void PlayClip(string clip)
    {
        AudioSource source = GetFreeSource();
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (clip == audioClips[i].name)
            {
                source.clip = audioClips[i].clip;
                source.Play();
                return;
            }
        }
    }
    #endregion

    #region Force Play
    public void ForcePlay()
    {

    }
    #endregion

    #region Get Free Source
    public AudioSource GetFreeSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying){
                audioSources[i].mute = isSoundMuted;
                return audioSources[i];
            }
        }
        if (audioSources.Count < maxSources)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.mute = isSoundMuted;
            audioSources.Add(newSource);
            return newSource;
        }
        else
        {
            return null;
        }
    }
    #endregion
}