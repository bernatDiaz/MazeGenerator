using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVelocity : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;

    private AudioSource source;

    private const float MAX_VELOCITY = 15f;
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    private const float MIN_PITCH = 0.1f;
    private const float MAX_PITCH = 1f;

    private float volume = MIN_VOLUME;
    private float pitch = MIN_PITCH;
    private float targetVolume;
    private float targetPitch;

    private const float MAX_CHANGE_VOLUME = 2.5f;
    private const float MAX_CHANGE_PITCH = 2.5f;
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        //Direct();
        MaxChanges(Time.deltaTime);
    }
    private void Direct()
    {
        float velocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        TransformLog(velocity, out volume, out pitch);
        Set();
    }
    private void MaxChanges(float deltaTime)
    {
        UpdateTarget();
        MoveToTarget(deltaTime);
        Set();
    }
    private void Set()
    {
        source.volume = Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
        source.pitch = Mathf.Clamp(volume, MIN_PITCH, MAX_PITCH);
    }
    private void MoveToTarget(float deltaTime)
    {
        float diferenceVolume = targetVolume - volume;
        float maxChangeVolume = MAX_CHANGE_VOLUME * deltaTime;
        float volumeChange = Mathf.Clamp(diferenceVolume, -maxChangeVolume, maxChangeVolume);
        volume += volumeChange;

        float diferencePitch = targetPitch - pitch;
        float maxChangePitch = MAX_CHANGE_PITCH * deltaTime;
        float pitchChange = Mathf.Clamp(diferencePitch, -maxChangePitch, maxChangePitch);
        pitch += pitchChange;
    }
    private void UpdateTarget()
    {
        float velocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        TransformLog(velocity, out targetVolume, out targetPitch);
    }
    private void TransformLineal(float velocity, out float volume, out float pitch)
    {
        float clampVelocity = Mathf.Clamp(velocity, 0f, MAX_VELOCITY);

        float relativeVelocity = clampVelocity / MAX_VELOCITY;

        volume = relativeVelocity * (MAX_VOLUME - MIN_VOLUME);
        pitch = relativeVelocity * (MAX_PITCH - MIN_PITCH);
    }
    private void TransformLog(float velocity, out float volume, out float pitch)
    {
        float clampVelocity = Mathf.Clamp(velocity, 0f, MAX_VELOCITY);
        float log = Mathf.Log(clampVelocity + 1f, MAX_VELOCITY + 1f);

        volume = log * (MAX_VOLUME - MIN_VOLUME);
        pitch = log * (MAX_PITCH - MIN_PITCH);
    }
}
