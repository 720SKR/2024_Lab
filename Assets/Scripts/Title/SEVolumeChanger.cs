using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEVolumeChanger : MonoBehaviour
{
    public static bool ChangedVolume;
    [SerializeField] Slider SEVolumeSlider;
    public static float volume;
    [SerializeField] AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        //audioS = SEManager.instance.();
        ChangedVolume = GetBool();
        if (ChangedVolume)
        {
            SEVolumeSlider.value = GetSliderVolume();
        }
        audioS.volume = SEVolumeSlider.value;
        volume = audioS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SESliderOnValueChange(float newSliderValue)
    {
        ChangedVolume = true;
        audioS.volume = newSliderValue;
        volume = audioS.volume;
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
