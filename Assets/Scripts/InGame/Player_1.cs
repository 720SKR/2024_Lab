using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool MyTurn;
    [SerializeField] bool isSelect;
    [SerializeField] GameObject SendPlayer;
    [SerializeField] int Num;
    [Header("UI")]
    [SerializeField] GameObject[] SetNumButton;//�I���{�^��
    [SerializeField] Text[] SelectMass_t;//�I�������e�L�X�g
    [SerializeField] Text[] JustMass_t;//�m�肵���e�L�X�g
    [SerializeField] Image[] JustMass_i;//�m�肵���}�X
    [SerializeField] int[] JustMass_v;//�m�肵���}�X�Ɋi�[�����l
    [SerializeField] Text SelectCurrentNum;//���ݑI�𒆂̐���
    [SerializeField] Image Panel1;
    [SerializeField] Image Panel2;
    [SerializeField] GameManager GM;
    [SerializeField] Material playerColor_1;
    List<int> availableNumPlayer = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [Header("Debug")]
    [SerializeField] int count;
    [SerializeField] bool isReload;
    bool isInit;
    [SerializeField] bool isSet;
    int sr, sc, sv;
    int sv_in;//1�����z��Ɋi�[�p�l
    [Header("�Q�[���^�c�ɕK�v�ȃI�u�W�F�N�g")]
    [SerializeField] int tg_v;
    [SerializeField] int pretg_v;
    [SerializeField] int R,C,V,BVG;
    [SerializeField] AudioClip[] SetSE;
    [SerializeField] BoardManager board;
    [SerializeField] CardsAnimation cardAnimation;
    [Header("Player�ԍ��m�F")]
    [SerializeField] int GamePlayerNo; //�C���X�y�N�^�[��ŌŒ肷��No
    void Start()
    {
        isInit = false;
        init();
    }

    // Update is called once per frame
    async void Update()
    {
        if (MyTurn)
        {
            if(!isReload || !isInit)
            {
                ReloadNum();
                isReload = true;
                //Debug.Log("Reload"+GamePlayerNo);
                if(!cardAnimation.TurnStart_Animation_P2)
                {
                    cardAnimation.CardsAnim_S2();
                    await UniTask.WaitUntil(() => cardAnimation.TurnStart_Animation_P2 == true);
                }
                
            }
            if (isSelect)//���l������������
            {
                //Debug.Log("Call"+ GamePlayerNo);
                isSelect = false;
                if(!cardAnimation.TurnEnd_Animation2_P2)
                {
                    cardAnimation.BoolChangeTurnCP2();//�J�[�h�o���A�j���[�V������Bool�l�̕ύX
                    cardAnimation.CardsAnim_E2();
                    await UniTask.WaitUntil(() => cardAnimation.TurnEnd_Animation2_P2 == true);//Animation...

                }
                board.EmptyCheck();
                isReload = false;
                V = 0;
                cardAnimation.BoolChangeTurnECP2();//�J�[�h�d�����A�j���[�V������Bool�l�̕ύX
                NotTurn();
                SendPlayer.GetComponent<Player>().Turn();
            }
        }
    }

    public void init()
    {
        playerColor_1 = UIManager.SetM_P2();
        Panel1.material = playerColor_1;
        Panel2.material = playerColor_1;
        for(int i = 0; i < JustMass_t.Length; i++)//������
        {
            SelectMass_t[i].text = string.Empty;
            JustMass_t[i].text = string.Empty;
        }
        tg_v = 0;
        V = 1; R = 0; C = 0;
        SelectMass_t[tg_v].text = V.ToString();
        SelectCurrentNum.text = V.ToString();
        isInit = true;
        ///sammary///
        ///Player�I����ʂŕύX�����J���[���擾�APlayer�J���[������ɁB
        ///sammary///
    }

    public void Turn()
    {
        //Debug.Log("Turn next");
        Panel1.material = playerColor_1;
        Panel2.material = playerColor_1;
        MyTurn = true;
    }

    public void NotTurn()
    {
        MyTurn = false;
    }


    public void ReloadNum()//���X�g�X�V�{�^���̕\����\��
    {
        int n = 1;
        R = 0; C = 0; V = 0;
        for (int i = 0; i < 10; i++)
        {
            if (availableNumPlayer.Contains(n))//���̐��������݂��Ă��邩����
            {
                SetNumButton[i].SetActive(true);
            }
            else
            {
                SetNumButton[i].SetActive(false);
            }
            n++;
        }
        for (int i = 0; i < 10; i++)
        {
            if (SetNumButton[i].activeSelf == true)
            {
                V = i + 1;
                SelectCurrentNum.text = V.ToString();
                break;//�Œ�l�������I���ɂ���B
            }
        }/*
        for(int i= 0;i<16;i++)//�󔒂̃}�X������
        {
            if (JustMass_t[i].text == "")//�ŏ��ɋ󔒂�����΂����ɒl��}�� 
            {
                SelectMass_t[i].text = V.ToString();
                break;
            }
        }*/
    }

    public void ValueSetteing(int Num)
    {
        if (MyTurn)
        {
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE[2]);
            V = Num;
            SelectMass_t[tg_v].text = V.ToString();
            SelectCurrentNum.text = V.ToString();
        }
    }

    public void SetTarget(int Num)//�ݒ肵���ʒu�Ƀ^�[�Q�b�g���ړ�
    {//�����̃^�[���̎��g����悤�ɕύX
        if (MyTurn)
        {
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE[1]);
            JustMass_v[tg_v] = board.GetboardV(Num);
            if (JustMass_v[tg_v] != 0)//���ɐݒu���Ă���ꍇ��return
            {
                isSet = false;
                //Debug.Log("Return");
                return;
            }
            pretg_v = tg_v;
            //Debug.Log(pretg_v + ":�O�̈ʒu");
            R = Num % 4;//�s
            C = Num / 4;//��
            tg_v = Num;
            BVG = Num;
            //Debug.Log(tg_v + "���݈ʒu");

            if (pretg_v != tg_v)
            {
                //Debug.Log("�폜");
                SelectMass_t[pretg_v].text = "";
            }
            isSet = true;
            SelectMass_t[tg_v].text = V.ToString();
        }
    }
    public void SelectValue()
    {
        if (MyTurn)
        {
            if (V == 0) return;//�l�������ĂȂ� or �ꏊ���w�肵�ĂȂ�
            if (JustMass_t[tg_v].text == (V != 0).ToString()) return;//���Ɋm��}�X�ɂO�ȊO�����Ă���B
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE[0]);
            SelectMass_t[tg_v].text = "";
            JustMass_i[tg_v].material = playerColor_1;
            JustMass_t[tg_v].text = V.ToString();
            JustMass_v[tg_v] = V;
            SetNumButton[V - 1].SetActive(false);
            //Debug.Log("Set");
            sr = R;
            sc = C;
            sv = V;
            sv_in = BVG;
            board.SetValue(sr, sc, sv, sv_in);
            availableNumPlayer.Remove(V);
            isSelect = true;
        }
    }

    public bool GetPlayerTurn()
    {
        return MyTurn;
    }

    public bool GetisSelect()
    {
        return isSelect;
    }
}