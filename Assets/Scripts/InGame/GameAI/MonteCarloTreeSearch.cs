using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonteCarloTreeSearch
{
    private static int WIN_SCORE = 10;//買った際の報酬
    private int level;//1手を計算するための制限時間
    private int opponent;//AIの敵を記憶

    public MonteCarloTreeSearch(int Level)
    {
        this.level = Level;//値は適宜変更する（レベル設定するため）
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private int GetMillisForCurrentLevel()//指定したレベルから1手の計算時間の上限を定める
    {
        return 2 * (level - 1) + 1;
    }


    public BoardManager findNextMove(BoardManager board,int playerNo)
    {
        long start = DateTime.Now.Ticks;
        long end = start + 60 *GetMillisForCurrentLevel();

        opponent = 3 - playerNo;

        TreeScript tree = new TreeScript();//空の探索木を作る
        Node rootNode = tree.getRoot();
        rootNode.getState().setBoard(board);

        rootNode.getState().setPlayerNo(opponent);


        while(DateTime.Now.Ticks < end)
        {
            //Phase1 - Selection
            Node promisingNode = selectPromisingNode(rootNode);

            //Phase2 - Expansion
            if (promisingNode.getState().getBoard().CheckStatus() == BoardManager.IN_PROGRESS)
                expandNode(promisingNode);

            //Phase3 - Simulation
            Node nodeToExplore = promisingNode;
            if (promisingNode.getChildArray().Count > 0)
            {
                nodeToExplore = promisingNode.getRandomChildNode();
            }
            int playoutResult = simulateRandomPlayout(nodeToExplore);

            //Phase4 - Update
            backPropogation(nodeToExplore, playoutResult);
        }

        Node WinnerNode = rootNode.getChildWithMaxScore();
        tree.setRoot(WinnerNode);
        return WinnerNode.getState().getBoard();

    }

    
    private Node selectPromisingNode(Node rootNode)
    {
        Node node = rootNode;
        while (node.getChildArray().Count != 0)
        {
            node = UCT.findBestNodeWithUCT(node);
        }

        return node;
    }

    //指定したNodeを展開する
    private void expandNode(Node node)
    {
        List<State> possibleStates = node.getState().getAllPossibleStates();
        possibleStates.ForEach(State =>
        {
            Node newNode = new Node(State);
            newNode.setParent(node);
            newNode.getState().setPlayerNo(node.getState().getOpponent());
            node.getChildArray().Add(newNode);
        });
    }

    private void backPropogation(Node nodeToEcplore,int playerNo)
    {
        Node tempNode = nodeToEcplore;
        while(tempNode != null)
        {
            tempNode.getState().incrementVisit();
            if (tempNode.getState().getPlayerNo() == playerNo)
                tempNode.getState().addScore(WIN_SCORE);
            tempNode = tempNode.getParent();
        }
    }

    //指定したNodeから勝負が決まるまでランダムにPlay
    private int simulateRandomPlayout(Node node)
    {
        Node tempNode = new Node(node);
        State tempState = tempNode.getState();
        int boardStatus = tempState.getBoard().CheckStatus();

        if(boardStatus == opponent)//終局かつAIが勝った場合
        {//親ノードに対応する盤面の報酬を無理やり-∞にセットする。
            tempNode.getParent().getState().setWinScore(int.MinValue);

            return boardStatus;
        }
        while(boardStatus == BoardManager.IN_PROGRESS)
        {
            tempState.togglePlayer();//Player交代
            tempState.randomPlay();//Randomに打つ
            boardStatus = tempState.getBoard().CheckStatus();//結果を見る
        }

        return boardStatus;
    }
}
