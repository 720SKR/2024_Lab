using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class COMPlayer : MonoBehaviour
{
    private static int WIN_SCORE = 10;//”ƒ‚Á‚½Û‚Ì•ñV
    private int level;//1è‚ğŒvZ‚·‚é‚½‚ß‚Ì§ŒÀŠÔ
    private int opponent;//AI‚Ì“G‚ğ‹L‰¯

    public COMPlayer()
    {
        this.level = 3;//’l‚Í“K‹X•ÏX‚·‚é
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private int GetMillisForCurrentLevel()//w’è‚µ‚½ƒŒƒxƒ‹‚©‚ç1è‚ÌŒvZŠÔ‚ÌãŒÀ‚ğ’è‚ß‚é
    {
        return 2 * (this.level - 1) + 1;
    }

    /*
    public BoardManager findNextMove(BoardManager board,int playerNo)
    {
        long start = DateTime.Now.Ticks;
        long end = start + 60 *GetMillisForCurrentLevel();

        opponent = 3 - playerNo;

        Tree tree = new Tree();//‹ó‚Ì’Tõ–Ø‚ğì‚é
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
