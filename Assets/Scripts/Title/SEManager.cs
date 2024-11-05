using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager instance;

    float SEVolume;
    private AudioSource SEAudio;

    // Start is called before the first frame update
    void Start()
    { 
        SEAudio = GetComponent<AudioSource>();
        SEVolume = SEVolumeChanger.GetSliderVolume();
        SEAudio.volume = SEVolume;
    }

    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SEManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(AudioClip SE)
    {
        SEAudio.PlayOneShot(SE);
    }
    public static void Play(AudioClip SE)
    {
        GetInstance().PlayClip(SE);
    }


}
