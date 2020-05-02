using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager audioManagerInstance;
    AudioSource AudioSource;

    private void Start()
    {
        //set the audiomanager to this
        audioManagerInstance = this;
        AudioSource = this.gameObject.GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip clip)
    {
        //set the clip and play it
        AudioSource.clip = clip;
        AudioSource.Play();
    }
    public void StopMusic()
    {
        //stops music
        AudioSource.Stop();
    }
}
