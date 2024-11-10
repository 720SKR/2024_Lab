using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonteCarloTreeSearch
{
    private static int WIN_SCORE = 10;//�������ۂ̕�V
    private int level;//1����v�Z���邽�߂̐�������
    private int opponent;//AI�̓G���L��

    public MonteCarloTreeSearch(int Level)
    {
        this.level = Level;//�l�͓K�X�ύX����i���x���ݒ肷�邽�߁j
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private int GetMillisForCurrentLevel()//�w�肵�����x������1��̌v�Z���Ԃ̏�����߂�
    {
        return 2 * (level - 1) + 1;
    }


    public BoardManager findNextMove(BoardManager board,int playerNo)
    {
        long start = DateTime.Now.Ticks;
        long end = start + 60 *GetMillisForCurrentLevel();

        opponent = 3 - playerNo;

        TreeScript tree = new TreeScript();//��̒T���؂����
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

    //�w�肵��Node��W�J����
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

    //�w�肵��Node���珟�������܂�܂Ń����_����Play
    private int simulateRandomPlayout(Node node)
    {
        Node tempNode = new Node(node);
        State tempState = tempNode.getState();
        int boardStatus = tempState.getBoard().CheckStatus();

        if(boardStatus == opponent)//�I�ǂ���AI���������ꍇ
        {//�e�m�[�h�ɑΉ�����Ֆʂ̕�V�𖳗����-���ɃZ�b�g����B
            tempNode.getParent().getState().setWinScore(int.MinValue);

            return boardStatus;
        }
        while(boardStatus == BoardManager.IN_PROGRESS)
        {
            tempState.togglePlayer();//Player���
            tempState.randomPlay();//Random�ɑł�
            boardStatus = tempState.getBoard().CheckStatus();//���ʂ�����
        }

        return boardStatus;
    }
}
