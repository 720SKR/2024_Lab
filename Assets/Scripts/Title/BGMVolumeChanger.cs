using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeChanger : MonoBehaviour
{
    public static bool ChangedVolume;
    [SerializeField] Slider BGMVolumeSlider;
    [SerializeField] BGMManager bgm;
    public static float volume;
    [SerializeField] AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        audioS = BGMManager.Instance.GetAudioSource();
        ChangedVolume = GetBool();
        if( ChangedVolume)
        {
            BGMVolumeSlider.value = GetSliderVolume();
        }
        audioS.volume = BGMVolumeSlider.value;
        volume = audioS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BGMSliderOnValueChange(float newSliderValue)
    {
        ChangedVolume = true;
        audioS.volume = newSliderValue;
        volume = audioS.volume;
        bgm.SetBGMVolume();
    }

    public static float GetSliderVolume()
    {
        return volume;
    }

    public static bool GetBool()
    {
        return ChangedVolume;
    }
}
