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
    [SerializeField] Material[] SetMaterial;//�}�e���A���ꗗ
    [SerializeField] static Material SendMaterial_p1;//�Q�[�����M�p�}�e���A��
    [SerializeField] static Material SendMaterial_p2;//player2��
    public static bool isPlayer; //Player���ǂ���

    [SerializeField] Toggle[] PorC;//Player��COM��
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

    public void OnValueChangedP1(int value)//�I�������l����}�e���A���𑗐M_p1
    {
        SendMaterial_p1 = SetMaterial[value];
        Player1.material = SendMaterial_p1;
        Debug.Log("Material " + value + " select.");
    }
    public void OnValueChangedP2(int value)//�I�������l����}�e���A���𑗐M_p2
    {
        SendMaterial_p2 = SetMaterial[value];
        Player2.material = SendMaterial_p2;
        Debug.Log("Material " + value + " select.");
    }

    public void SetPlayer()//���S���[�J���ΐ�
    {
        isPlayer = true;
    }

    public void SetCom()//�l�ΐ�
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
        {//�G���[�p�|�b�v�A�b�v
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(ErrorSE);
            ErrorText.SetActive(true);
            await UniTask.WaitForSeconds(3);
            Debug.LogWarning("�����F�̓_����");
            ErrorText.SetActive(false);
            return;//�F�������ꍇ
        }
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(ButtonSE);
        FadeManager.Instance.LoadScene("InGame", 0.5f);
    }
}
