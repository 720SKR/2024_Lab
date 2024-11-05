using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MouseTarget : MonoBehaviour
{
    List<int> SetPlayerNum;//GameManager側のリスト読み込み

    [SerializeField] int row, col, value;
    int sr, sc, sv;//確定値
    int P;
    [SerializeField] int tg_v;

    [SerializeField] bool PlayerT;
    //bool TurnEnd;
    [SerializeField] Material playerColor_1;
    [SerializeField] Material playerColor_2;
    [SerializeField] Image Panel_1;
    [SerializeField] Image Panel_2;
    [Header("ボタンオブジェクト")]
    [SerializeField] GameObject[] NumButtons;
    [Header("数値を置くテキスト")]
    [SerializeField] Text[] GUIMass;
    [Header("確定した時の数値いれるテキスト")]
    [SerializeField] Text[] JustMass;//確定時入れるマス
    [SerializeField] Image[] JustMass_UI;
    //[SerializeField] GameObject target;//マウスに追従
    [SerializeField] Text SelectCurrentNum;//現在選択中の数字
    [SerializeField] GameManager gm;

    struct RCV
    {
       public int R;//行
       public int C;//列
       public int V;//値
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
        //P = GameObject.Find("GameSystem").GetComponent<GameManager>().GetP();//プレイヤー○を取得
        //PlayerT = GameObject.Find("GameSystem").GetComponent<GameManager>().GetboolPT();
        Debug.Log(P);
        //カラー設定
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
            GUIMass[i].text = string.Empty;//初期化
            JustMass[i].text = string.Empty;
        }
        tg_v = 0;
        value = 1; row = 0; col = 0;
        GUIMass[0].text = value.ToString();
        SelectCurrentNum.text = value.ToString();
    }

    public void ReloadNum()//リスト更新ボタンの表示非表示
    {
        int n = 1;
        //if (PlayerT) SetPlayerNum = gm.GetListP1();//List読み込み bool値で読み取るリストを変更
        //else SetPlayerNum = gm.GetListP2();
        for (int i = 0; i < 10; i++)
        {
            if (SetPlayerNum.Contains(n))//その数字が存在しているか判定
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
                break;//最低値を初期選択にする。
            }
        }
    }

    public void SetTarget(int Num)//設定した位置にターゲットを移動
    {
        int pretg_v;
        pretg_v = tg_v;
        Debug.Log(pretg_v + ":前の位置");
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
        Debug.Log(tg_v + "現在位置");
        if (JustMass[tg_v].text.ToString() == value.ToString())//既に設置している場合はreturn
        {
            Debug.Log("Return");
            return;
        }
        if (pretg_v != tg_v)
        {
            Debug.Log("削除");
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
        //P = gm.GetP();//プレイヤー○を取得
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
