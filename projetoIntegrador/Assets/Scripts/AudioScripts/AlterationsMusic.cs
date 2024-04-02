using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterationsMusic : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudio;
    public static AlterationsMusic Instance { get; set; }
    public IEnumerator PlayMusic(AudioClip music)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.clip = music;
        yield return new WaitForSeconds(audio.clip.length);
        audio.Play();
    }
}
