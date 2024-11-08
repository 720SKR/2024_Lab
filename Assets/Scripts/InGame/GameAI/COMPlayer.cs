using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class COMPlayer : MonoBehaviour
{
    private static int WIN_SCORE = 10;//買った際の報酬
    private int level;//1手を計算するための制限時間
    private int opponent;//AIの敵を記憶

    public COMPlayer()
    {
        this.level = 3;//値は適宜変更する
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
        return 2 * (this.level - 1) + 1;
    }

    /*
    public BoardManager findNextMove(BoardManager board,int playerNo)
    {
        long start = DateTime.Now.Ticks;
        long end = start + 60 *GetMillisForCurrentLevel();

        opponent = 3 - playerNo;

        Tree tree = new Tree();//空の探索木を作る
        Node rootNode = tree.getRoot();
        rootNode.getState().setBoard(board);



        rootNode.getState().setPlayerNo(opponent);


        while(DateTime.Now.Ticks < end)
        {
            //Phase1 - Selection
            Node promisingNode = selectPromisingNode(rootNode);

            if (promisingNode.getState().getBoard().CheckStatus() == BoardManager.IN_PROGRESS)
                expandNode(promisingNode);
        }


    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
