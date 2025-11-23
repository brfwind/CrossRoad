using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Mixer")]
    public AudioMixer mixer;

    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip jumpClip;
    public AudioClip longJumpClip;
    public AudioClip deadClip;

    [Header("Audio Source")]
    public AudioSource bgmMusic;
    public AudioSource fx;

    #region 单例模式、播放主bgm
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        //指定主bgm，以及播放主bgm
        bgmMusic.clip = bgmClip;
        PlayMusic();
    }
    #endregion

    #region 事件系统
    private void OnEnable()
    {
        EventHandler.GameOverEvent += OnGameOverEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }

    //游戏结束时，播放死亡音效
    private void OnGameOverEvent()
    {
        fx.clip = deadClip;
        fx.Play();
    }
    #endregion

    #region 根据大小跳，切换跳跃音效
    /// <summary>
    /// 设置跳跃的音效片段
    /// </summary>
    /// <param name="type">0小跳，1大跳</param>
    public void SetJumpClip(int type)
    {
        switch (type)
        {
            case 0:
                fx.clip = jumpClip;
                break;
            case 1:
                fx.clip = longJumpClip;
                break;
        }
    }
    #endregion

    #region 播放主bgm、音效
    public void PlayJumpFx()
    {
        fx.Play();
    }

    public void PlayMusic()
    {
        if (!bgmMusic.isPlaying)
        {
            bgmMusic.Play();
        }
    }
    #endregion

    #region 音乐开关按键
    public void ToggleAudio(bool show)
    {
        if (show)
        {
            mixer.SetFloat("masterVolume", 0);
        }
        else
        {
            mixer.SetFloat("masterVolume", -80);
        }
    }
    #endregion
}