using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    SoundClip[] audioClips;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }

    public void PlayOneShot(string name)
    {
        foreach(var song in audioClips)
        {
            if (song.name == name)
            {
                source.PlayOneShot(song.clip);
            }
        }
    }

    public void PlayBGM(string name)
    {
        foreach (var song in audioClips)
        {
            if (song.name == name)
            {
                source.clip = song.clip;
                source.Play();
            }
        }
    }
}
