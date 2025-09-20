using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource _bgmAudioSource;
    private AudioSource[] _audioSources = new AudioSource[32];

    [SerializeField]
    public SoundAsset SoundAsset;

    public AudioMixerGroup AudioMixerGroup;

    public void Start()
    {
        GameObject bgmchild = new GameObject("BGMAudioSource");
        bgmchild.transform.SetParent(transform);
        _bgmAudioSource = bgmchild.AddComponent<AudioSource>();

        for (int i = 0; i < _audioSources.Length; ++i)
        {
            GameObject child = new GameObject("AudioSource_" + i);
            child.transform.SetParent(transform);
            _audioSources[i] = child.AddComponent<AudioSource>();
        }
    }

    public void PlaySFX(string name, int index = -1)
    {
        if (index >= 0)
        {
            var source = _audioSources[index];
            source.Stop();

            var data = SoundAsset.Sounds.Find(p => p.Name == name);
            source.clip = data.Clip;
            source.volume = data.Volume + UnityEngine.Random.Range(-data.RandomVolume, data.RandomVolume);
            source.pitch = data.Pitch + UnityEngine.Random.Range(-data.RandomPitch, data.RandomPitch);
            source.outputAudioMixerGroup = AudioMixerGroup;
            source.Play();
        }

        foreach (var source_ in _audioSources)
        {
            if (source_.isPlaying) continue;

            var data_ = SoundAsset.Sounds.Find(p => p.Name == name);
            source_.clip = data_.Clip;
            source_.volume = data_.Volume + UnityEngine.Random.Range(-data_.RandomVolume, data_.RandomVolume);
            source_.pitch = data_.Pitch + UnityEngine.Random.Range(-data_.RandomPitch, data_.RandomPitch);
            source_.outputAudioMixerGroup = AudioMixerGroup;
            source_.Play();

            return;
        }

        var source__ = _audioSources[0];
        source__.Stop();

        var data__ = SoundAsset.Sounds.Find(p => p.Name == name);
        source__.clip = data__.Clip;
        source__.volume = data__.Volume + UnityEngine.Random.Range(-data__.RandomVolume, data__.RandomVolume);
        source__.pitch = data__.Pitch + UnityEngine.Random.Range(-data__.RandomPitch, data__.RandomPitch);
        source__.outputAudioMixerGroup = AudioMixerGroup;
        source__.Play();
    }

    public void PlayBGM(string name, bool overlap = false)
    {
        if (!overlap && _bgmAudioSource.isPlaying) return;

        _bgmAudioSource.Stop();

        var data = SoundAsset.Sounds.Find(p => p.Name == name);
        _bgmAudioSource.clip = data.Clip;
        _bgmAudioSource.volume = data.Volume + UnityEngine.Random.Range(-data.RandomVolume, data.RandomVolume);
        _bgmAudioSource.pitch = data.Pitch + UnityEngine.Random.Range(-data.RandomPitch, data.RandomPitch);
        _bgmAudioSource.outputAudioMixerGroup = AudioMixerGroup;
        _bgmAudioSource.loop = true;
        _bgmAudioSource.Play();
    }

    public void PauseBGM()
    {
        _bgmAudioSource.Pause();
    }
    public void StopBGM()
    {
        _bgmAudioSource.Stop();
    }
}

[Serializable]
public class SoundData
{
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(-3f, 3f)]
    public float Pitch = 1f;

    [Range(0f, .5f)]
    public float RandomVolume = 0f;
    [Range(0f, 1f)]
    public float RandomPitch = 0f;
}