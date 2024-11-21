using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class State
{
    
    private BoardManager board;//���̔Ֆʎ��̃{�[�h
    private int playerNo;//���̔ՖʂŎ��Ɏ��ł���player�̔ԍ�
    private int visitCount;//���܂ł̖K�␔
    private double winScore;//��V���v

    public State(DemoPlayer p1,DemoPlayer p2)
    {
        board = new BoardManager(p1,p2);
    }

    public State(State state)
    {
        board = new BoardManager(state.getBoard());
        playerNo = state.getPlayerNo();
        visitCount = state.getVisitCount();
        winScore = state.getWinScore();
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
    //�������ύX���Ȃ��Ƃ����Ȃ������B
    //�Ֆʂ�1���ɂȂ肤��Ֆʂ�S�ė񋓂��ĕԂ�
    public List<State> getAllPossibleStates()
    {
        List<State> possibleStates = new List<State>();
        List<Position> availablePositions = board.getEmptyPositions();
        int nm;
        bool[] NumCards = board.GetDemoPlayer(3 - playerNo).NumCards;
        foreach (Position position in availablePositions)
        {
            for(int i = 1; i <= 10; i++)
            {
                State newState = new State(board);
                newState.setPlayerNo(3 - playerNo);
                nm = i;
                if (!NumCards[i-1])
                {
                    continue;
                }
                newState.getBoard().performMove(newState.getPlayerNo(), position,nm);
                possibleStates.Add(newState);
            }

        }
        return possibleStates;
    }

    public void incrementVisit()
    {
        visitCount++;
    }

    public void addScore(double score)
    {
        if (winScore != int.MinValue)
            winScore += score;
    }
    //NearOrJust�p�ɂ���
    public void randomPlay()
    {
        List<Position> availablePositions = board.getEmptyPositions();
        

        bool[] NumCards = board.GetDemoPlayer(playerNo).NumCards;
        
        List<int> SelectNumIndex = new();
        for(int i =0; i< NumCards.Length; i++)
        {
            if (NumCards[i]) SelectNumIndex.Add(i + 1);
        }
        int SelectNumRandom = Random.Range(0, SelectNumIndex.Count);

        int totalPossibilities = availablePositions.Count;
        int selectRandom = Random.Range(0,totalPossibilities);

        board.performMove(playerNo, availablePositions[selectRandom],SelectNumRandom);
    }

    public void togglePlayer()
    {
        playerNo = 3 - playerNo;
    }
}
