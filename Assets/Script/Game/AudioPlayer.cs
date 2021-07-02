using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public int index;
    public AudioClip[] clips;
    private AudioSource source;
    public float vol;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = vol;
        source.clip = clips[index];
        source.Play();
    }

    private void LateUpdate()
    {
        if(source.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
