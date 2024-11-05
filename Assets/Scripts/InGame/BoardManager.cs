using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [Header("UI�\���p")]
    //[SerializeField] GameObject[,] GUIBoardValues;//UI��̃f�[�^
    [Header("��������UI")]
    [SerializeField] Text Vic;//��������UI
    [SerializeField] Material WinMaterial;//���ʂ��o�������Ɍ��点��p
    [SerializeField] Material AbsMaterial;//���ʂ��o�������Ɍ��点��
    [Header("�ڕW�l�ݒ��UI")]
    [SerializeField] Text Vv;//�ڕW�l��UI
    [SerializeField] Text[] RowCt;//�s�̌v�Z���ʕ\���pUI
    [SerializeField] Image[] RowCtImage;
    [SerializeField] Image[] ColCtImage;
    [SerializeField] Text[] ColCt;//��̌v�Z���ʕ\���pUI
    [SerializeField] Text VicTe1;//�ڕW�l�Ɠ����̏ꍇ
    [SerializeField] Text VicTe2;
    [SerializeField] Text ABS1;//��Βl�I�ɋ߂������̏ꍇ
    [SerializeField] Text ABS2;
    [SerializeField] Material playerColor_1;
    [SerializeField] Material playerColor_2;
    [SerializeField] GameObject VICTORY;

    [SerializeField] int[] ResultDataRow;//�s�̌v�Z���ʂ�����z��
    [SerializeField] int[] ResultDataCol;//��̌v�Z���ʂ�����z��

    [SerializeField] int[] MostNearAbsValue = new int[2];
    int[,] boardValues;//�{�[�h�̃}�X���̏��
    int[] boardValues_game;//�}���^
    int totalMoves;//�萔
    [SerializeField]int MostNearABS;
    [SerializeField]int[] AbsSumRow;
    [SerializeField] int[] AbsSumCol;//UI�ɓǂݍ��ޗp

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

    public static int Victory_Value;//�ڕW���l

    public void Start()
    {
        GameSet = false;
        boardValues_game = new int[16];
        Victory_Value = UnityEngine.Random.Range(10,35);
        Vv.text = Victory_Value.ToString();
        VICTORY.SetActive(false);
    }

    public int EmptyCheck()//�󔒂̃`�F�b�N
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

    public BoardManager(BoardManager board)//�w�肵���{�[�h���R�s�[����
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
        this.totalMoves++;//�萔�X�V
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
    //�v�Z���ʂ��o��
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
        {//�ڕW�l�̐���0�̏ꍇ
            return P1;
        }
        else if (P2AbsCount > P1AbsCount)
        {//�ڕW�l�̐���0�̏ꍇ
            return P2;
        }
        else if (P1Count == P2Count || P1AbsCount == P2AbsCount)
        {//�ڕW�l���͐�Βl�̐��������̏ꍇ
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
        {//�e�s�A�e��̌v�Z
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
        //�ȉ�UI�\���̂��߂̉�����

        for (int i = 0; i < size; i++)
        {
            ResultDataRow[i] = rowSUMd[i];
        }
        
        for (int i = 0; i < size; i++)
        {
            ResultDataCol[i] = colSUMd[i];
        }
        MostNearAbsValue[0] = ReturnAbsValue(ResultDataRow,0);//��Βl�v�Z
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
            if (Math.Abs(Victory_Value - ResultDataRow[i]) == MostNearAbsValue[0])//��Βl
            {
                RowCtImage[i].material = AbsMaterial;
                Debug.Log("Changed" + RowCtImage[i]);
            }           
            if (ResultDataRow[i] == Victory_Value)//�m��
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
    
    //���̃{�[�h�̋󂢂Ă���}�X���
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

    public void SetValue(int r,int c,int v,int bvg)//��A�s�A�l�A1�����p
    {
        boardValues[r,c] = v;//���l�}��
        //Debug.Log($"{r} : {c} : {boardValues[r,c]}");
        boardValues_game[bvg] = v;//1�����p�Ɋi�[
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

    public int GetboardV(int i)//�S�̂̃{�[�h�̔z��̒l��Ԃ�
    {
        return boardValues_game[i];
    }

    public int GetVictoryValue()
    {
        return Victory_Value;
    }

}
