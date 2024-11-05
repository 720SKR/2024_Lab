using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //List<int> availableNumPlayer1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    //List<int> availableNumPlayer2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [SerializeField] bool Game;
    [SerializeField] bool isPlayer2;//コンピュータか否か
    [SerializeField] bool Player1Turn;
    [SerializeField] bool Player2Turn;
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject AIPlayer;
    [SerializeField] GameObject Cards1;
    [SerializeField] GameObject Cards2;
    [SerializeField] GameObject[] PlayerList;//2人のPlayer格納するための配列

    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject ExitButton;
    //[SerializeField] BGMManager bgm;

    [SerializeField] Image PlayerIcon1;
    [SerializeField] Image PlayerIcon2;
    [SerializeField] Material Icon01;
    [SerializeField] Material Icon02;
    [SerializeField] Text TurnText;//今誰のターン？
    [SerializeField] int Randv;
    [Header("PlayerNo.")]
    [SerializeField] int playerNum;//プレイヤーの番号
    [Header("Scripts")]
    [SerializeField] BoardManager board;
    [SerializeField] DoToUIMove GameUIM;
    [SerializeField] GameObject GameSetPanel;
    [SerializeField] RandomValueUIMove RandomUI;
    [SerializeField] CardsAnimation CardsAnimation;
    int RandVsendUI;//randomizerUIに送る値
    //プレイヤー変数をセッターゲッターに。マウスターゲットで参照できるようにする。
    // Start is called before the first frame update
    void Start()
    {
        GameInit();
    }

    void Update()
    {
        
        if (Game)//ゲームが終了してません
        {
            return;
        }
        WhoTurn();
        if (board.SetGame())//マスが全部埋まったら計算
        {
            Debug.Log("GameSet");
            //bgm.GameSetBGM_AllStop();
            GameObject.Find("AudioManager").GetComponent<BGMManager>().GameSetBGM_AllStop();
            Player1.GetComponent<Player>().NotTurn();
            if (!isPlayer2)
            {
                AIPlayer.GetComponent<EasyAIPlayer>().enabled = false;
            }
            else
            {
                Player2.GetComponent<Player_1>().NotTurn();
            }
            
            Cards1.SetActive(false);
            Cards2.SetActive(false);
            GameSetPanel.SetActive(true);
            GameUIM.GameUIMove();
            
            board.PrintCalculation();
            //board.printStatus();
            Game = true;
        }

    }

    public void GameInit()//Unitask入れまっせ
    {
        RetryButton.SetActive(false);
        ExitButton.SetActive(false);
        GameSetPanel.SetActive(false);
        isPlayer2 = UIManager.GetisPlayer();
        if (!isPlayer2)
        {
            //コンピュータ用のPlayerスクリプトを起動
            Player2.GetComponent<Player_1>().enabled = false;
            AIPlayer.GetComponent<EasyAIPlayer>().enabled = true;
        }
        if (isPlayer2)
        {
            //Player用のスクリプトを起動する
            AIPlayer.GetComponent<EasyAIPlayer>().enabled = false;
            Player2.GetComponent<Player_1>().enabled = true;
        }
        Icon01 = UIManager.SetM_P1();
        Icon02 = UIManager.SetM_P2();
        PlayerIcon1.material = Icon01;//パネル色設定
        PlayerIcon2.material = Icon02;
        
        //先攻後攻決め
        Randv = Random.Range(0, 2);
        //Game = false;
        RandVsendUI = board.GetVictoryValue();
        RandomUI.RandomAnimetionS(RandVsendUI);//ランダム数値決定UIを動かす
        
        if (Randv == 0)
        {
            playerNum = 0;//Player1が先行
            if (isPlayer2)
            {
                Player2.GetComponent<Player_1>().NotTurn();
            }
            
            Player1.GetComponent<Player>().Turn();

            TurnText.text = "Player 1 Turn";
        }
        if (Randv == 1)
        {
            playerNum = 1;//Player2が先行

            Player1.GetComponent<Player>().NotTurn();
            if (isPlayer2)
            {
                Player2.GetComponent<Player_1>().Turn();
            }
            else
            {
                AIPlayer.GetComponent<EasyAIPlayer>().Turn();
            }
            TurnText.text = "Player 2 Turn";
        }

    }

    public void WhoTurn()//誰のターン？
    {
        if (Player1.GetComponent<Player>().GetPlayerTurn())
        {
            //Debug.Log("Player1");
            TurnText.text = "Player 1 Turn";
        }
        if (Player2.GetComponent<Player_1>().GetPlayerTurn())
        {
            //Debug.Log("Player2");
            TurnText.text = "Player 2 Turn";
        }
    }

    public int GetPlayerNo()
    {
        return playerNum;
    }

    public int GetRandV()
    {
        return Randv;
    }
}
