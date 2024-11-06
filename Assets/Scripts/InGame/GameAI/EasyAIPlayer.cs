using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class EasyAIPlayer : MonoBehaviour
{
    List<int> availableNumPlayer = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [SerializeField] GameObject[] NumButton;
    [SerializeField] float waitTime;
    [Header("UI")]
    [SerializeField] Text[] JustMass_Text;
    [SerializeField] Image[] JustMass_Image;
    [SerializeField] int[] JustMass_Value;
    [SerializeField] Text CurrentText;
    [SerializeField] Image Panel1;
    [SerializeField] Image Panel2;
    [SerializeField] Material playerColor;
    [Header("Manager")]
    [SerializeField] GameManager GM;
    [SerializeField] BoardManager board;
    [SerializeField] CardsAnimation cardsAnimation;
    [SerializeField] AudioClip SetSE;
    [SerializeField] GameObject SendPlayer;

    int SelectedRow,SelectedColumn;//選択された行/列
    int SelectedNUM;//選択されたナンバー
    int SelectedNUMBoard;
    int[,] CurrentBoard;//現在の盤面導入
    int[] SelectedBoard;//一次元用に入れる配列
    // Start is called before the first frame update
    void Start()
    {
        CurrentBoard = new int[4, 4];
        SelectedBoard = new int[16];
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        playerColor = UIManager.SetM_P2();
        Panel1.material = playerColor;
        Panel2.material = playerColor;

    }

    public async void Turn()//試行しているという時間を演出するためUnitask使用。
    {
        Debug.Log("MyTurn");
        // どのように手を打つかを書く。今回は順々に空いているマスを探して適当に数字を置く。
        AvailableNumSearch();
        Debug.Log("Searched");
        if (!cardsAnimation.TurnStart_Animation_P2)
        {
            cardsAnimation.CardsAnim_S2();
            await UniTask.WaitUntil(() => cardsAnimation.TurnStart_Animation_P2 == true);
        }
        SearchEmpty();
        Debug.Log("Searched2");
        await UniTask.WaitForSeconds(waitTime);
        SelectNumRowCol();
        if (!cardsAnimation.TurnEnd_Animation2_P2)//アニメーションまだ再生してない場合
        {
            cardsAnimation.BoolChangeTurnCP2();//カード出しアニメーションのBool値の変更
            cardsAnimation.CardsAnim_E2();
            await UniTask.WaitUntil(() => cardsAnimation.TurnEnd_Animation2_P2 == true);//Animation...
        }
        board.EmptyCheck();
        cardsAnimation.BoolChangeTurnECP2();//カード仕舞いアニメーションのBool値の変更
        SendPlayer.GetComponent<Player>().Turn();
    }

    void SearchEmpty()
    {
        Debug.Log("Search");
        SelectedNUMBoard = 0;
        CurrentBoard = board.GetBoardValues();
        Debug.Log("Length:"+CurrentBoard.Length);
        for(int i = 0; i < SelectedBoard.Length; i++)
        {
            SelectedBoard[i] = board.GetboardV(i);
            SelectedNUMBoard = i;
            if (SelectedBoard[i] == 0) break;
        }
        SelectedRow = SelectedNUMBoard % 4;
        SelectedColumn = SelectedNUMBoard % 4;
    }

    void AvailableNumSearch()
    {
        int n = 1;
        for(int i = 0; i<10; i++)
        {
            if (availableNumPlayer.Contains(n))
            {
                Debug.Log("あるで" + NumButton[i]);
                NumButton[i].SetActive(true);
            }
            else
            {
                Debug.Log("ないで" + NumButton[i]);
                NumButton[i].SetActive(false);
            }
            n++;
        }
        for (int i = 0; i < 10; i++)
        {
            if (NumButton[i].activeSelf == true)
            {
                SelectedNUM = i + 1;
                Debug.Log("Set"+SelectedNUM + 1);
                CurrentText.text = (SelectedNUM + 1).ToString();
                break;//最低値を初期選択にする。
            }
        }
    }

    void SelectNumRowCol()
    {
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(SetSE);
        JustMass_Value[SelectedNUMBoard] = SelectedNUM;
        JustMass_Text[SelectedNUMBoard].text = JustMass_Value[SelectedNUMBoard].ToString();
        JustMass_Image[SelectedNUMBoard].material = playerColor;
        Debug.Log("Value:"+JustMass_Value[SelectedNUMBoard]);
    //    Debug.Log(SelectedNUM + "1 :" + (SelectedNUM+1));
        NumButton[SelectedNUM].SetActive(false);
        board.SetValue(SelectedRow, SelectedColumn,SelectedNUM,SelectedNUMBoard);
        availableNumPlayer.Remove(SelectedNUM);
        Debug.Log("Remove:" + SelectedNUM);
    }
}
