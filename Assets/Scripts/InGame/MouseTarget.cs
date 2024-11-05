using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MouseTarget : MonoBehaviour
{
    List<int> SetPlayerNum;//GameManager���̃��X�g�ǂݍ���

    [SerializeField] int row, col, value;
    int sr, sc, sv;//�m��l
    int P;
    [SerializeField] int tg_v;

    [SerializeField] bool PlayerT;
    //bool TurnEnd;
    [SerializeField] Material playerColor_1;
    [SerializeField] Material playerColor_2;
    [SerializeField] Image Panel_1;
    [SerializeField] Image Panel_2;
    [Header("�{�^���I�u�W�F�N�g")]
    [SerializeField] GameObject[] NumButtons;
    [Header("���l��u���e�L�X�g")]
    [SerializeField] Text[] GUIMass;
    [Header("�m�肵�����̐��l�����e�L�X�g")]
    [SerializeField] Text[] JustMass;//�m�莞�����}�X
    [SerializeField] Image[] JustMass_UI;
    //[SerializeField] GameObject target;//�}�E�X�ɒǏ]
    [SerializeField] Text SelectCurrentNum;//���ݑI�𒆂̐���
    [SerializeField] GameManager gm;

    struct RCV
    {
       public int R;//�s
       public int C;//��
       public int V;//�l
       public RCV(int r,int c, int v)
        {
            R = r;
            C = c;
            V = v;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //gm = new GameManager();
        //P = GameObject.Find("GameSystem").GetComponent<GameManager>().GetP();//�v���C���[�����擾
        //PlayerT = GameObject.Find("GameSystem").GetComponent<GameManager>().GetboolPT();
        Debug.Log(P);
        //�J���[�ݒ�
        if (P == 1)
        {
            Panel_1.material = playerColor_1;
            Panel_2.material = playerColor_1;

        }
        if (P == 2)
        {
            Panel_1.material = playerColor_2;
            Panel_2.material = playerColor_2;
        }

        for (int i = 0; i < GUIMass.Length; i++)
        {
            GUIMass[i].text = string.Empty;//������
            JustMass[i].text = string.Empty;
        }
        tg_v = 0;
        value = 1; row = 0; col = 0;
        GUIMass[0].text = value.ToString();
        SelectCurrentNum.text = value.ToString();
    }

    public void ReloadNum()//���X�g�X�V�{�^���̕\����\��
    {
        int n = 1;
        //if (PlayerT) SetPlayerNum = gm.GetListP1();//List�ǂݍ��� bool�l�œǂݎ�郊�X�g��ύX
        //else SetPlayerNum = gm.GetListP2();
        for (int i = 0; i < 10; i++)
        {
            if (SetPlayerNum.Contains(n))//���̐��������݂��Ă��邩����
            {
                NumButtons[i].SetActive(true);
            }
            else
            {
                NumButtons[i].SetActive(false);
            }
            n++;
        }
        for (int i = 0; i < 10; i++)
        {
            if (NumButtons[i].activeSelf == true)
            {
                value = i + 1;
                SelectCurrentNum.text = value.ToString();
                break;//�Œ�l�������I���ɂ���B
            }
        }
    }

    public void SetTarget(int Num)//�ݒ肵���ʒu�Ƀ^�[�Q�b�g���ړ�
    {
        int pretg_v;
        pretg_v = tg_v;
        Debug.Log(pretg_v + ":�O�̈ʒu");
        switch (Num)
        {
            case 0:
                row = 0; col = 0;
                tg_v = 0;
                break;
            case 1:
                row = 0; col = 1;
                tg_v = 1;
                break;
            case 2:
                row = 0; col = 2;
                tg_v = 2;
                break;
            case 3:
                row = 0; col = 3;
                tg_v = 3;
                break;
            case 4:
                row = 1; col = 0;
                tg_v = 4;
                break;
            case 5:
                row = 1; col = 1;
                tg_v = 5;
                break;
            case 6:
                row = 1; col = 2;
                tg_v = 6;
                break;
            case 7:
                row = 1; col = 3;
                tg_v = 7;
                break;
            case 8:
                row = 2; col = 0;
                tg_v = 8;
                break;
            case 9:
                row = 2; col = 1;
                tg_v = 9;
                break;
            case 10:
                row = 2; col = 2;
                tg_v = 10;
                break;
            case 11:
                row = 2; col = 3;
                tg_v = 11;
                break;
            case 12:
                row = 3; col = 0;
                tg_v = 12;
                break;
            case 13:
                row = 3; col = 1;
                tg_v = 13;
                break;
            case 14:
                row = 3; col = 2;
                tg_v = 14;
                break;
            case 15:
                row = 3; col = 3;
                tg_v = 15;
                break;
            default:
                break;
        }
        Debug.Log(tg_v + "���݈ʒu");
        if (JustMass[tg_v].text.ToString() == value.ToString())//���ɐݒu���Ă���ꍇ��return
        {
            Debug.Log("Return");
            return;
        }
        if (pretg_v != tg_v)
        {
            Debug.Log("�폜");
            GUIMass[pretg_v].text = "";
        }
        GUIMass[tg_v].text = value.ToString();
    }

    public void ValueSetteing(int Num)
    {
        switch (Num)
        {
            case 1:
                value = 1; break;
            case 2:
                value = 2; break;
            case 3:
                value = 3; break;
            case 4:
                value = 4; break;
            case 5:
                value = 5; break;
            case 6:
                value = 6; break;
            case 7:
                value = 7; break;
            case 8:
                value = 8; break;
            case 9:
                value = 9; break;
            case 10:
                value = 10; break;
            default:
                break;
        }
        GUIMass[tg_v].text = value.ToString();
        SelectCurrentNum.text = value.ToString();
    }

    public void SelectV()
    {
        //P = gm.GetP();//�v���C���[�����擾
        Debug.Log(P);
        if (P == 1)
        {
            GUIMass[tg_v].text = "";
            JustMass_UI[tg_v].material = playerColor_1;
            NumButtons[tg_v].GetComponent<Button>().enabled = false;
        }
        else
        {
            GUIMass[tg_v].text = "";
            JustMass_UI[tg_v].material = playerColor_2;
            NumButtons[tg_v].GetComponent<Button>().enabled = false;
        }
        JustMass[tg_v].text = value.ToString();
        NumButtons[value - 1].SetActive(false);
        setNum(row, col, value);
        if (PlayerT)
        {
            PlayerT = false;
        }
        else
        {
            PlayerT = true;
        }
        return;
    }

    public void setNum(int r, int c, int v)
    {

        RCV rcv = new RCV(r,c,v);
    }

    public (int, int, int) GetNum()
    {
        return (sr, sc, sv);
    }

    public bool GetTurn()
    {
        return PlayerT;
    }

}
