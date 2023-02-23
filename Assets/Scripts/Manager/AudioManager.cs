using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{

    public AudioSource bgmPlayer;
    public AudioSource soundPlayer;

    public AudioClip defaultBGM;// 因为是AA 是异步加载 所以设置默认BGM

    public AudioClip pobi;
    public AudioClip kill;
    public AudioClip zhanling;
    public AudioClip killerend;

    public AudioClip win;
    public AudioClip laugh;

    public void Awake()
    {
        bgmPlayer.Play();
    }

    private void Start()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.clip = defaultBGM;
        bgmPlayer.loop = true;
        bgmPlayer.playOnAwake = false;
    }

    public void PlayOneShout(AudioClip clip, float volumeScale = 1)
    {
        soundPlayer.PlayOneShot(clip, volumeScale);
    }

}
