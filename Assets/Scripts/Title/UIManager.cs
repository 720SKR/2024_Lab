using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int i,j;
    [SerializeField] AudioClip ButtonSE;
    [SerializeField] AudioClip ErrorSE;
    [SerializeField] Dropdown playercolor1;
    [SerializeField] Dropdown playercolor2;
    [SerializeField] GameObject ErrorText;
    [SerializeField] Image Player1;
    [SerializeField] Image Player2;
    [SerializeField] Material[] SetMaterial;//マテリアル一覧
    [SerializeField] static Material SendMaterial_p1;//ゲーム送信用マテリアル
    [SerializeField] static Material SendMaterial_p2;//player2版
    public static bool isPlayer; //Playerかどうか

    [SerializeField] Toggle[] PorC;//PlayerかCOMか
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void init()
    {
        isPlayer = true;
        i = playercolor1.value;
        playercolor2.value = i+1;
        Player1.material = SetMaterial[i];
        SendMaterial_p1 = SetMaterial[i];
        Player2.material = SetMaterial[i+1];
        SendMaterial_p2 = SetMaterial[i+1];
    }

    public void OnValueChangedP1(int value)//選択した値からマテリアルを送信_p1
    {
        SendMaterial_p1 = SetMaterial[value];
        Player1.material = SendMaterial_p1;
        Debug.Log("Material " + value + " select.");
    }
    public void OnValueChangedP2(int value)//選択した値からマテリアルを送信_p2
    {
        SendMaterial_p2 = SetMaterial[value];
        Player2.material = SendMaterial_p2;
        Debug.Log("Material " + value + " select.");
    }

    public void SetPlayer()//完全ローカル対戦
    {
        isPlayer = true;
    }

    public void SetCom()//個人対戦
    {
        isPlayer = false;
    }

    public static Material SetM_P1()
    {
        return SendMaterial_p1;
    }

    public static Material SetM_P2()
    {
        return SendMaterial_p2;
    }

    public static bool GetisPlayer()
    {
        return isPlayer;
    }

    public async void GoStartGame()
    {
        
        if (SendMaterial_p1 == SendMaterial_p2)
        {//エラー用ポップアップ
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(ErrorSE);
            ErrorText.SetActive(true);
            await UniTask.WaitForSeconds(3);
            Debug.LogWarning("同じ色はダメよ");
            ErrorText.SetActive(false);
            return;//色が同じ場合
        }
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(ButtonSE);
        FadeManager.Instance.LoadScene("InGame", 0.5f);
    }
}
