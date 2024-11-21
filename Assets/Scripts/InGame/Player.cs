using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool MyTurn;
    [SerializeField] bool isSelect;
    [SerializeField] GameObject[] SendPlayer;
    [SerializeField] int Num;
    [Header("UI")]
    [SerializeField] GameObject[] SetNumButton;//選択ボタン
    [SerializeField] Text[] SelectMass_t;//選択したテキスト
    [SerializeField] Text[] JustMass_t;//確定したテキスト
    [SerializeField] Image[] JustMass_i;//確定したマス
    [SerializeField] int[] JustMass_v;//確定したマスに格納した値
    [SerializeField] Text SelectCurrentNum;//現在選択中の数字
    [SerializeField] Image Panel1;
    [SerializeField] Image Panel2;
    [SerializeField] GameManager GM;
    [SerializeField] Material playerColor_1;
    List<int> availableNumPlayer = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [Header("Debug")]
    [SerializeField] int count;
    [SerializeField] bool isReload;
    bool isInit;
    [SerializeField]bool isAI;
    [SerializeField] bool isSet;
    public bool[] NumCards;
    int sr, sc, sv;
    int sv_in;//1次元配列に格納用値
    [Header("ゲーム運営に必要なオブジェクト")]
    [SerializeField] int tg_v;
    [SerializeField] int pretg_v;
    [SerializeField] int R,C,V,BVG;
    [SerializeField] BoardManager board;
    [SerializeField] BoardUIManager boardUI;
    [SerializeField] CardsAnimation cardAnimation;
    [SerializeField] AudioClip[] SetSE;
    [Header("Player番号確認")]
    [SerializeField] int GamePlayerNo; //インスペクター上で固定するNo
    void Start()
    {
        isAI = UIManager.GetisPlayer();
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
                if(!cardAnimation.TurnStart_Animation_P1)
                {
                    cardAnimation.CardsAnim_S();
                    await UniTask.WaitUntil(() => cardAnimation.TurnStart_Animation_P1 == true);
                }
            }
            if (isSelect)//数値を押した処理
            {
                //Debug.Log("Call"+ GamePlayerNo);
                isSelect = false;
                if (!cardAnimation.TurnEnd_Animation2_P1)//アニメーションまだ再生してない場合
                {
                    cardAnimation.BoolChangeTurnCP1();//カード出しアニメーションのBool値の変更
                    cardAnimation.CardsAnim_E();
                    await UniTask.WaitUntil(() => cardAnimation.TurnEnd_Animation2_P1 == true);//Animation...
                }
                board.EmptyCheck();
                isReload = false;
                V = 0;
                cardAnimation.BoolChangeTurnECP1();//カード仕舞いアニメーションのBool値の変更
                NotTurn();
                if (!isAI)
                {
                    SendPlayer[0].GetComponent<Player_1>().Turn();
                }
                else
                {
                    SendPlayer[1].GetComponent<EasyAIPlayer>().Turn();
                }
            }
        }
    }

    public void init()
    {
        playerColor_1 = UIManager.SetM_P1();
        Panel1.material = playerColor_1;
        Panel2.material = playerColor_1;
        for(int i = 0; i < JustMass_t.Length; i++)//初期化
        {
            SelectMass_t[i].text = string.Empty;
            JustMass_t[i].text = string.Empty;
        }
        tg_v = 0;
        V = 0; R = -1; C = -1;
        SelectMass_t[tg_v].text = V.ToString();
        SelectCurrentNum.text = V.ToString();
        isInit = true;
    }

    public void Turn()
    {
        Panel1.material = playerColor_1;
        Panel2.material = playerColor_1;
        R = -1;C = -1;
        MyTurn = true;
    }

    public void NotTurn()
    {
        MyTurn = false;
    }


    public void ReloadNum()//リスト更新ボタンの表示非表示
    {
        int n = 1;
        R = 0; C = 0; V = 0;
        for (int i = 0; i < 10; i++)
        {
            if (availableNumPlayer.Contains(n))//その数字が存在しているか判定
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
                break;//最低値を初期選択にする。
            }
        }
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

    public void SetTarget(int Num)//設定した位置にターゲットを移動
    {//自分のターンの時使えるように変更
        if (MyTurn)
        {
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE[1]);
            JustMass_v[tg_v] = board.GetboardV(Num);
            if (JustMass_v[tg_v] != 0)//既に設置している場合はreturn
            {
                isSet = false;
                //Debug.Log("Return");
                return;
            }
            pretg_v = tg_v;
            //Debug.Log(pretg_v + ":前の位置");
            R = Num % 4;//行
            C = Num / 4;//列
            tg_v = Num;
            BVG = Num;
            //Debug.Log(tg_v + "現在位置");

            if (pretg_v != tg_v)
            {
                //Debug.Log("削除");
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
            if (R == -1 || C== -1) return;//値が入ってない or 場所を指定してない
            if (JustMass_t[tg_v].text == (V != 0).ToString()) return;//既に確定マスに０以外入っている。
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE[0]);
            SelectMass_t[tg_v].text = "";
            //JustMass_i[tg_v].material = playerColor_1;
            //JustMass_t[tg_v].text = V.ToString();
            JustMass_v[tg_v] = V;
            boardUI.SetMassBoard(V,tg_v,0);
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

    public bool[] GetNumCards()
    {
        return NumCards;
    }

}
