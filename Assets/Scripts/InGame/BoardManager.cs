using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [Header("UI表示用")]
    //[SerializeField] GameObject[,] GUIBoardValues;//UI上のデータ
    [Header("勝利時のUI")]
    [SerializeField] Text Vic;//勝利時のUI
    [SerializeField] Material WinMaterial;//結果を出した時に光らせる用
    [SerializeField] Material AbsMaterial;//結果を出した時に光らせる
    [Header("目標値設定のUI")]
    [SerializeField] Text Vv;//目標値のUI
    [SerializeField] Text[] RowCt;//行の計算結果表示用UI
    [SerializeField] Image[] RowCtImage;
    [SerializeField] Image[] ColCtImage;
    [SerializeField] Text[] ColCt;//列の計算結果表示用UI
    [SerializeField] Text VicTe1;//目標値と同じの場合
    [SerializeField] Text VicTe2;
    [SerializeField] Text ABS1;//絶対値的に近い数字の場合
    [SerializeField] Text ABS2;
    [SerializeField] Material playerColor_1;
    [SerializeField] Material playerColor_2;
    [SerializeField] GameObject VICTORY;

    [SerializeField] int[] ResultDataRow;//行の計算結果を入れる配列
    [SerializeField] int[] ResultDataCol;//列の計算結果を入れる配列

    [SerializeField] int[] MostNearAbsValue = new int[2];
    int[,] boardValues;//ボードのマス内の情報
    int[] boardValues_game;//挿入型
    int totalMoves;//手数
    [SerializeField]int MostNearABS;
    [SerializeField]int[] AbsSumRow;
    [SerializeField] int[] AbsSumCol;//UIに読み込む用

    [SerializeField]int count;

    [SerializeField]int Interval;

    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject ExitButton;
    bool GameSet;

    int P1Count, P2Count, P1AbsCount, P2AbsCount;

    Material winner;

    public const int DEFAULT_BOARD_SIZE = 4;

    public const int IN_PROGRESS = -1;
    public const int DRAW = 0;
    public const int P1 = 1;
    public const int P2 = 2;

    public static int Victory_Value;//目標数値

    public void Start()
    {
        GameSet = false;
        boardValues_game = new int[16];
        Victory_Value = UnityEngine.Random.Range(10,35);
        Vv.text = Victory_Value.ToString();
        VICTORY.SetActive(false);
    }

    public int EmptyCheck()//空白のチェック
    {
        count = 0;
        int size = boardValues_game.Length;
        for (int i = 0; i < size; i++)
        {

            if (boardValues_game[i] == 0)
            {
                count++;
            }
        }
        if (count == 0)
        {
            GameSet = true;
        }
        return count;
    }

    public BoardManager()
    {
        boardValues = new int[DEFAULT_BOARD_SIZE, DEFAULT_BOARD_SIZE];
    }

    public BoardManager(int boardsize)
    {
        boardValues = new int[boardsize, boardsize];
    }

    public BoardManager(int[,] boardValues)
    {
        this.boardValues = boardValues;
    }

    public BoardManager(BoardManager board)//指定したボードをコピーする
    {
        int boardLength = board.GetBoardValues().Length;
        this.boardValues =new int[boardLength, boardLength];
        int[,] boardValues = board.GetBoardValues();
        int n = boardValues.Length;
        for(int i = 0; i < n; i++)
        {
            int m = boardValues.GetLength(i);
            for(int j = 0; j<m; j++)
            {
                this.boardValues[i,j]= boardValues[i,j];
            }
        }
    }

    
    public void performMove(int player, Position p)
    {
        this.totalMoves++;//手数更新
        boardValues[p.getX(),p.getY()]= player;
    }
    

    public int[,] GetBoardValues()
    {
        return boardValues;
    }

    public void SetBoardValues(int[,] boardValues)
    {
        this.boardValues = boardValues;
    }
    
    public int CheckStatus()
    {
        int Judge;
/*        if (getEmptyPositions().Count>0)
        {
            Debug.Log("Game Status:" + getEmptyPositions().Count);
            return IN_PROGRESS;
        }
        else
        {
            Judge = CalculationVic();
            return Judge;
        }*/
        Judge = CalculationVic();
        Debug.Log("Judge:" + Judge);
        return Judge;
    }
    //計算結果を出す
    private int CalculationVic()
    {

        P1Count = CountVic(ResultDataRow);
        P2Count = CountVic(ResultDataCol);
        P1AbsCount = AbsCount(0);
        P2AbsCount = AbsCount(1);

        if (P1Count > P2Count)
        {
            return P1;
        }
        else if (P2Count > P1Count)
        {
            return P2;
        }
        else if (P1AbsCount > P2AbsCount)
        {//目標値の数が0の場合
            return P1;
        }
        else if (P2AbsCount > P1AbsCount)
        {//目標値の数が0の場合
            return P2;
        }
        else if (P1Count == P2Count || P1AbsCount == P2AbsCount)
        {//目標値又は絶対値の数が同数の場合
            return DRAW;
        }
        else
        {
            return 120;
        }

    }
    public int CountVic(int[] SumData)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (SumData[i] == Victory_Value)
            {
                count++;
            }
        }
        return count;
    }

    public int AbsCount(int No)
    {
        int AbsCount = 0;
        for (int i = 0; i < 4; i++)
        {
            switch (No)
            {
                case 0:
                    if (AbsSumRow[i] == MostNearAbsValue[0])
                    {
                        AbsCount++;
                    }
                    break;
                case 1:
                    if (AbsSumCol[i] == MostNearAbsValue[0])
                    {
                        AbsCount++;
                    }
                    break;
            }
        }
        return AbsCount;
    }

    public int ReturnAbsValue(int[] SumData,int No)
    {
        int MostNearAbs;
        int[] ABSV = new int[4];
        for (int i = 0; i < 4; i++)
        {
            ABSV[i] = Math.Abs(Victory_Value - SumData[i]);
        }
        switch (No)
        {
            case 0:
                AbsSumRow = new int[4];
                for(int j=0;j < 4; j++)
                {
                    AbsSumRow[j] = ABSV[j];
                    Debug.Log(j+ " " + AbsSumRow[j]);
                }
                break;
            case 1:
                AbsSumCol = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    AbsSumCol[j] = ABSV[j];
                    Debug.Log(j + " " + AbsSumCol[j]);
                }
                break;
        }
        Array.Sort(ABSV);
        MostNearAbs = ABSV[0];
        return MostNearAbs;
    }


    public void PrintCalculation()
    {
        int size = boardValues.GetLength(0);
        int rowSUM, colSUM;
        int[] rowSUMd = new int[size];
        int[] colSUMd = new int[size];
        ResultDataRow = new int[size];
        ResultDataCol = new int[size];


        for (int i = 0; i < size; i++)
        {//各行、各列の計算
            rowSUM = 0;
            colSUM = 0;
            for (int j = 0; j < size; j++)
            {
                rowSUM += boardValues[j, i];
                colSUM += boardValues[i, j];
            }
            rowSUMd[i] = rowSUM;
            colSUMd[i] = colSUM;
        }
        //以下UI表示のための下準備

        for (int i = 0; i < size; i++)
        {
            ResultDataRow[i] = rowSUMd[i];
        }
        
        for (int i = 0; i < size; i++)
        {
            ResultDataCol[i] = colSUMd[i];
        }
        MostNearAbsValue[0] = ReturnAbsValue(ResultDataRow,0);//絶対値計算
        MostNearAbsValue[1] = ReturnAbsValue(ResultDataCol,1);
        Array.Sort(MostNearAbsValue);

        ResultAnimation();
    }

    public async void ResultAnimation()
    {
        int j = 0;
        float Speed = 0.5f;
        for (int i=0; i < Interval; i++)
        {
            if (j > 3) j = 0;
            RowCt[j].text = UnityEngine.Random.Range(10,35).ToString();
            ColCt[j].text = UnityEngine.Random.Range(10, 35).ToString();
            j++;
            await UniTask.WaitForSeconds(0.1f);
        }
        for(int i =0; i < 4; i++)
        {
            if (Math.Abs(Victory_Value - ResultDataRow[i]) == MostNearAbsValue[0])//絶対値
            {
                RowCtImage[i].material = AbsMaterial;
                Debug.Log("Changed" + RowCtImage[i]);
            }           
            if (ResultDataRow[i] == Victory_Value)//確定
            {
                RowCtImage[i].material = WinMaterial;
                Debug.Log("WinCount"+ RowCtImage[i]);
            } 
            RowCt[i].text = ResultDataRow[i].ToString();
            await UniTask.WaitForSeconds(Speed + 0.1f) ;
        }
        for (int i = 0; i < 4; i++)
        {
            if (Math.Abs(Victory_Value - ResultDataCol[i]) == MostNearAbsValue[0])
            {
                ColCtImage[i].material = AbsMaterial;
                Debug.Log("Changed" + ColCtImage[i]);
            }
            if (ResultDataCol[i] == Victory_Value)
            {
                ColCtImage[i].material = WinMaterial;
                Debug.Log("WinCount" + ColCtImage[i]);
            }
            ColCt[i].text = ResultDataCol[i].ToString();
            await UniTask.WaitForSeconds(Speed + 0.1f);
        }
        printStatus();
    }
    
    //このボードの空いているマスを列挙
    public List<Position> getEmptyPositions()
    {
        int size = this.boardValues.GetLength(0);
        List<Position> emptyPositions = new List<Position>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (boardValues[i,j] == 0)
                    emptyPositions.Add(new Position(i, j));
            }
        }
        return emptyPositions;
    }


    //Getter Setter
    public int GetValue(int r,int c)
    {
        return boardValues[r,c];
    }

    public void SetValue(int r,int c,int v,int bvg)//列、行、値、1次元用
    {
        boardValues[r,c] = v;//数値挿入
        //Debug.Log($"{r} : {c} : {boardValues[r,c]}");
        boardValues_game[bvg] = v;//1次元用に格納
    }

    public int GetNumberOfRows()
    {
        return boardValues.Length;
    }

    public void printStatus()
    {
        GameObject.Find("AudioManager").GetComponent<BGMManager>().GameSetBGM_Start();
        RetryButton.SetActive(true);
        ExitButton.SetActive(true);
        VICTORY.SetActive(true);
        switch (this.CheckStatus())
        {
            case P1:
                winner = UIManager.SetM_P1();
                Vic.material = winner;
                Vic.text = "Player" + P1 + "WIN!";
                break;
            case P2:
                winner = UIManager.SetM_P2();
                Vic.material = winner;
                Vic.text = "Player" + P2 + "WIN!";
                break;
            case DRAW:
                Vic.text = "Draw!";
                break;
            case IN_PROGRESS:
                Vic.text = "Game In Progress!";
                break;
        }
    }

    public bool SetGame()
    {
        return GameSet;
    }

    public int GetboardV(int i)//全体のボードの配列の値を返す
    {
        return boardValues_game[i];
    }

    public int GetVictoryValue()
    {
        return Victory_Value;
    }

}
