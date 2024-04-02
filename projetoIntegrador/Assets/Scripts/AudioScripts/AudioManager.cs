using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> musicList;
    //public static float volume;
    //public float volume2;
    //public  Slider musicVolume;
    int indexmusic;

    private void Update()
    {
        //volume = musicVolume.value;   
        //musicList[indexmusic].volume = volume;
        musicList[indexmusic].volume = VolumeControl.volume;

    }
    private void Start()
    {
        //musicVolume.value = volume;
        StartCoroutine(PlayMusic(musicList));
    }
    public IEnumerator PlayMusic(List <AudioSource> music)
    {
        int index = 0;  
      
        while(true)
        {
            music[index].Play();
            yield return new WaitForSeconds(music[index].clip.length);
         
            index = Random.Range(0, music.Count);
            indexmusic = index;
        }
    }
}
