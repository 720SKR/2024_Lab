using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class State
{
    
    private BoardManager board;//この盤面時のボード
    private int playerNo;//この盤面で次に手を打ったplayerの番号
    private int visitCount;//今までの訪問数
    private double winScore;//報酬合計

    public State()
    {
        board = new BoardManager();
    }

    public State(State state)
    {
        this.board = new BoardManager(state.getBoard());
        this.playerNo = state.getPlayerNo();
        this.visitCount = state.getVisitCount();
        this.winScore = state.getWinScore();
    }

    public State(BoardManager board)
    {
        this.board = new BoardManager(board);
    }

    public BoardManager getBoard()
    {
        return board;
    }

    public void setBoard(BoardManager board)
    {
        this.board = board;
    }

    public int getPlayerNo()
    {
        return playerNo;
    }

    public void setPlayerNo(int playerNo)
    {
        this.playerNo = playerNo;
    }

    public int getOpponent()
    {
        return 3 - playerNo;
    }

    public int getVisitCount()
    {
        return visitCount;
    }

    public void setVisitCount(int visitCount)
    {
        this.visitCount = visitCount;
    }

    public double getWinScore()
    {
        return winScore;
    }

    public void setWinScore(double winScore)
    {
        this.winScore = winScore;
    }

    //盤面の1手後になりうる盤面を全て列挙して返す
    public List<State> getAllPossibleStates()
    {
        List<State> possibleStates = new List<State>();
        List<Position> availablePositions = this.board.getEmptyPositions();
        availablePositions.ForEach(p => {
            State newState = new State(this.board);
            newState.setPlayerNo(3 - this.playerNo);
            newState.getBoard().performMove(newState.getPlayerNo(), p);
            possibleStates.Add(newState);
        });
        return possibleStates;
    }

    public void incrementVisit()
    {
        this.visitCount++;
    }

    public void addScore(double score)
    {
        if (this.winScore != int.MinValue)
            this.winScore += score;
    }

    public void randomPlay()
    {
        List<Position> availablePositions = this.board.getEmptyPositions();
        int totalPossibilities = availablePositions.Count;
        int selectRandom = (int)(Random.Range(0,totalPossibilities));
        this.board.performMove(this.playerNo, availablePositions[selectRandom]);
    }

    public void togglePlayer()
    {
        this.playerNo = 3 - this.playerNo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
