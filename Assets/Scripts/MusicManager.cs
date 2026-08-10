using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField, Range(0f, 1f)] float volume = 0.5f;
    [SerializeField] bool loop = true;
    [SerializeField] bool playOnAwake = true;

    AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.clip = musicClip;
        src.volume = volume;
        src.loop = loop;
        src.playOnAwake = playOnAwake;

        if (playOnAwake && musicClip != null)
            src.Play();
    }

    public void SetVolume(float v)
    {
        volume = Mathf.Clamp01(v);
        src.volume = volume;
    }

    public void Play() => src.Play();
    public void Stop() => src.Stop();
}
