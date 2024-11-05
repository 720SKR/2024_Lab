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
    [SerializeField] bool isPlayer2;//�R���s���[�^���ۂ�
    [SerializeField] bool Player1Turn;
    [SerializeField] bool Player2Turn;
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject AIPlayer;
    [SerializeField] GameObject Cards1;
    [SerializeField] GameObject Cards2;
    [SerializeField] GameObject[] PlayerList;//2�l��Player�i�[���邽�߂̔z��

    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject ExitButton;
    //[SerializeField] BGMManager bgm;

    [SerializeField] Image PlayerIcon1;
    [SerializeField] Image PlayerIcon2;
    [SerializeField] Material Icon01;
    [SerializeField] Material Icon02;
    [SerializeField] Text TurnText;//���N�̃^�[���H
    [SerializeField] int Randv;
    [Header("PlayerNo.")]
    [SerializeField] int playerNum;//�v���C���[�̔ԍ�
    [Header("Scripts")]
    [SerializeField] BoardManager board;
    [SerializeField] DoToUIMove GameUIM;
    [SerializeField] GameObject GameSetPanel;
    [SerializeField] RandomValueUIMove RandomUI;
    [SerializeField] CardsAnimation CardsAnimation;
    int RandVsendUI;//randomizerUI�ɑ���l
    //�v���C���[�ϐ����Z�b�^�[�Q�b�^�[�ɁB�}�E�X�^�[�Q�b�g�ŎQ�Ƃł���悤�ɂ���B
    // Start is called before the first frame update
    void Start()
    {
        GameInit();
    }

    void Update()
    {
        
        if (Game)//�Q�[�����I�����Ă܂���
        {
            return;
        }
        WhoTurn();
        if (board.SetGame())//�}�X���S�����܂�����v�Z
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

    public void GameInit()//Unitask����܂���
    {
        RetryButton.SetActive(false);
        ExitButton.SetActive(false);
        GameSetPanel.SetActive(false);
        isPlayer2 = UIManager.GetisPlayer();
        if (!isPlayer2)
        {
            //�R���s���[�^�p��Player�X�N���v�g���N��
            Player2.GetComponent<Player_1>().enabled = false;
            AIPlayer.GetComponent<EasyAIPlayer>().enabled = true;
        }
        if (isPlayer2)
        {
            //Player�p�̃X�N���v�g���N������
            AIPlayer.GetComponent<EasyAIPlayer>().enabled = false;
            Player2.GetComponent<Player_1>().enabled = true;
        }
        Icon01 = UIManager.SetM_P1();
        Icon02 = UIManager.SetM_P2();
        PlayerIcon1.material = Icon01;//�p�l���F�ݒ�
        PlayerIcon2.material = Icon02;
        
        //��U��U����
        Randv = Random.Range(0, 2);
        //Game = false;
        RandVsendUI = board.GetVictoryValue();
        RandomUI.RandomAnimetionS(RandVsendUI);//�����_�����l����UI�𓮂���
        
        if (Randv == 0)
        {
            playerNum = 0;//Player1����s
            if (isPlayer2)
            {
                Player2.GetComponent<Player_1>().NotTurn();
            }
            
            Player1.GetComponent<Player>().Turn();

            TurnText.text = "Player 1 Turn";
        }
        if (Randv == 1)
        {
            playerNum = 1;//Player2����s

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

    public void WhoTurn()//�N�̃^�[���H
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
