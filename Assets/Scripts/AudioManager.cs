using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //Handles SFX - sfx called through LevelManager
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip craneLetGoSfx;
    [SerializeField] AudioClip crashSfx;
    [SerializeField] AudioClip goodLandingSfx;
    [SerializeField] AudioClip splashSfx;

    //Crane sfx come from always looping crane sounds that are turned on/off by changing volume
    [SerializeField] AudioSource craneMoveSource;
    [SerializeField] AudioSource craneLowerSource;

    public void ToggleCraneMoveSound(bool on)
    {
        if (on) craneMoveSource.volume = 1;
        else craneMoveSource.volume = 0;
    }

    public void ToggleCraneLowerSound(bool on)
    {
        if (on) craneLowerSource.volume = 1;
        else craneLowerSource.volume = 0;
    }

    public void PlayCraneLetGo()
    {
        sfxSource.PlayOneShot(craneLetGoSfx);
    }

    public void PlayCrash()
    {
        sfxSource.PlayOneShot(crashSfx);
    }

    public void PlayGoodLanding()
    {
        sfxSource.PlayOneShot(goodLandingSfx);
    }

    public void PlaySplash()
    {
        sfxSource.PlayOneShot(splashSfx);
    }
}
