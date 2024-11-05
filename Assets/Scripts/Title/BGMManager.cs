using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    float BGMVolume;
    public static BGMManager Instance;
    [SerializeField] int BGMSelectNo = 1;

    [SerializeField] Image[] TestPlayIcon;
    [SerializeField] AudioClip AudioClips;
    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("BGM List")]
    [SerializeField] AudioSource Title;
    [SerializeField] AudioSource GameBGM_1;
    [SerializeField] AudioSource GameBGM_2;
    [SerializeField] AudioSource GameBGM_3;
    [SerializeField] AudioSource GameBGM_4;
    [SerializeField] AudioSource Result;

    private string beforeScene;//前のシーンの名前記録

    void Start()
    {
        beforeScene = "Title";
        Title.Play();

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioSource GetAudioSource() => Title;

    void OnActiveSceneChanged(Scene prevScene,Scene nextScene)
    {
        //ゲームに行く時
        if(beforeScene == "GameSetting" && nextScene.name == "InGame"){
            Title.Stop();
            if(BGMSelectNo == 1)
            {
                GameBGM_2.Stop();
                GameBGM_3.Stop();
                GameBGM_4.Stop();


                GameBGM_1.Play();
            }
            if(BGMSelectNo == 2)
            {
                GameBGM_1.Stop();
                GameBGM_3.Stop();
                GameBGM_4.Stop();


                GameBGM_2.Play();
            }
            if(BGMSelectNo == 3)
            {
                GameBGM_2.Stop();
                GameBGM_1.Stop();
                GameBGM_4.Stop();


                GameBGM_3.Play();
            }
            if(BGMSelectNo == 4)
            {
                GameBGM_2.Stop();
                GameBGM_3.Stop();
                GameBGM_1.Stop();


                GameBGM_4.Play();
            }
        }
        //Titleに戻る時
        if (beforeScene == "InGame" && (nextScene.name == "Title" || nextScene.name == "GameSetting"))
        {

            Result.Stop();
            Title.Play();
        }
        beforeScene = nextScene.name;
    }

    public void GameSetBGM_AllStop()
    {
        //ゲームセット時曲を止めマス。
        GameBGM_1.Stop();
        GameBGM_2.Stop();
        GameBGM_3.Stop();
        GameBGM_4.Stop();
    }

    public void GameSetBGM_Start()
    {
        //音量を設定し直す
        Result.Play();
    }

    public void OpenUserSetting()
    {
        SetTestPlayIcon(BGMSelectNo);
        Title.Stop();
    }

    public void TestPlayBGM(int No)
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(AudioClips);
        switch (No)
        {
            case 1:
                Title.Stop();
                SetTestPlayIcon(No);
                GameBGM_2.Stop();
                GameBGM_3.Stop();
                GameBGM_4.Stop();
                BGMSelectNo = 1;
                GameBGM_1.Play();
                break;
            case 2: 
                Title.Stop();
                SetTestPlayIcon(No);
                GameBGM_1.Stop();
                GameBGM_3.Stop();
                GameBGM_4.Stop();
                BGMSelectNo = 2;
                GameBGM_2.Play();
                break;
            case 3:
                Title.Stop();
                SetTestPlayIcon(No);
                GameBGM_2.Stop();
                GameBGM_1.Stop();
                GameBGM_4.Stop();
                BGMSelectNo = 3;
                GameBGM_3.Play();
                break;
            case 4:
                Title.Stop();
                SetTestPlayIcon(No);
                GameBGM_2.Stop();
                GameBGM_3.Stop();
                GameBGM_1.Stop();
                BGMSelectNo = 4;
                GameBGM_4.Play();
                break;
        }
    }

    public void SetBGMVolume()
    {
        BGMVolume = BGMVolumeChanger.GetSliderVolume();
        Title.volume = BGMVolume;
        GameBGM_1.volume = BGMVolume;
        GameBGM_2.volume = BGMVolume;
        GameBGM_3.volume = BGMVolume;
        GameBGM_4.volume = BGMVolume;
        Result.volume = BGMVolume;
    }

    public void SetTestPlayIcon(int i)
    {
        for(int j = 1; j < 5; j++)
        {
            if (j == i)
            {
                TestPlayIcon[j-1].enabled = true;
            }
            else
            {
                TestPlayIcon[j-1].enabled = false;
            }
        }
    }

    public void ClosedTest()
    {
        GameBGM_1.Stop();
        GameBGM_2.Stop();
        GameBGM_3.Stop();
        GameBGM_4.Stop();
        Title.Play();
    }
}
