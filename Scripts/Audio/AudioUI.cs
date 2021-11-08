using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickClip;
    [SerializeField]
    private AudioClip backClip;
    [SerializeField]
    private AudioClip okClip;
    [SerializeField]
    private AudioClip maximizeClip;
    [SerializeField]
    private AudioClip minimizeClip;

    private const float MAX_MIN_VOLUME = 0.5f;
    private const float MAX_MIN_PITCH = 0.5f;

    private AudioSource clickSource;
    private AudioSource backSource;
    private AudioSource okSource;
    private AudioSource maximizeSource;
    private AudioSource minimizeSource;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        clickSource = gameObject.AddComponent<AudioSource>();
        clickSource.clip = clickClip;

        backSource = gameObject.AddComponent<AudioSource>();
        backSource.clip = backClip;

        okSource = gameObject.AddComponent<AudioSource>();
        okSource.clip = okClip;

        maximizeSource = gameObject.AddComponent<AudioSource>();
        maximizeSource.clip = maximizeClip;
        maximizeSource.volume = MAX_MIN_VOLUME;
        maximizeSource.pitch = MAX_MIN_PITCH;

        minimizeSource = gameObject.AddComponent<AudioSource>();
        minimizeSource.clip = minimizeClip;
        minimizeSource.volume = MAX_MIN_VOLUME;
        minimizeSource.pitch = MAX_MIN_PITCH;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        AudioPlayer.CreateNew(gameObject, clickSource, clickClip);
    }

    public void OnBack()
    {
        AudioPlayer.CreateNew(gameObject, backSource, backClip);
    }

    public void OnOk()
    {
        AudioPlayer.CreateNew(gameObject, okSource, okClip);
    }

    public void OnMaximize()
    {
        AudioPlayer.Stop(maximizeSource);
    }

    public void OnMinimize()
    {
        AudioPlayer.Stop(minimizeSource);
    }
}
