using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject OptionPanel;

    [SerializeField] AudioClip AudioClips;
    [SerializeField] float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (OptionPanel == null) return;
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoStart()//ÉQÅ[ÉÄâÊñ Ç…à⁄çs
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene("InGame", fadeSpeed);
    }

    public void GoGameSetting()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene("GameSetting", fadeSpeed);
    }

    public void ExitTitle()//TitleÇ…ñﬂÇÈ
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene("Title", fadeSpeed);
    }

    public void GoHowToPlay()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene("HowToPlay", fadeSpeed);
    }

    public void GoCredit()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene("Credit", fadeSpeed);
    }

    public void GoScene(string sceneName)
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        FadeManager.Instance.LoadScene(sceneName, fadeSpeed);
    }

    public void GoOption()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        OptionPanel.SetActive(true);
    }

    public void OptionBack()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        OptionPanel.SetActive(false);
    }
}
