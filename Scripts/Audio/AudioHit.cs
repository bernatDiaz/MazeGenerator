using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHit : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;

    private AudioSource source;

    private const float MAX_IMPULSE = 25f;
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 0.5f;
    private const float MIN_PITCH = 0.1f;
    private const float MAX_PITCH = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    void Init()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        float impulse = collision.impulse.magnitude;
        float volume, pitch;
        TransformLineal(impulse, out volume, out pitch);
        AudioPlayer.CreateNew(gameObject, source, clip, volume, pitch);    
    }
    private void TransformLineal(float impulse, out float volume, out float pitch)
    {
        float clampImpulse = Mathf.Clamp(impulse, 0f, MAX_IMPULSE);

        float relativeImpulse = clampImpulse / MAX_IMPULSE;

        volume = relativeImpulse * (MAX_VOLUME - MIN_VOLUME);
        pitch = relativeImpulse * (MAX_PITCH - MIN_PITCH);
    }
}
