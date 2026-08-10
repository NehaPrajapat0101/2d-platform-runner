
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public SoundNames name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    public bool inLoop;

    [HideInInspector]
    public AudioSource source;

}
