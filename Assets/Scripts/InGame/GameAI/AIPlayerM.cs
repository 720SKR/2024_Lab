using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayerM : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] Player player1;
    [SerializeField] Image[] SettingPanel;
    [SerializeField] Text[] SelectMass_Text;
    [SerializeField] Image[] SelectMass_Image;
    Material playerMaterial;
    int Level;
    int row;
    int col;
    int value;
    int playerNo;
    MonteCarloTreeSearch M_AIplayer;
    BoardManager board;
    State state;
    DemoPlayer p1;
    DemoPlayer p2;

    // Start is called before the first frame update
    void Start()
    {
        //Level UIÇ©ÇÁéÊìæÇ∑ÇÈ
        playerMaterial = UIManager.SetM_P2();
        Level = UIManager.GetLevel();
        M_AIplayer.SetLevel(Level);
        playerNo = manager.GetPlayerNo();
        M_AIplayer = new MonteCarloTreeSearch(Level);
        switch (playerNo)
        {
            case 1:
                p1 = new DemoPlayer(playerNo);
                p2 = new DemoPlayer(3 - playerNo);
                break;
            case 2:
                p1 = new DemoPlayer(3 - playerNo);
                p2 = new DemoPlayer(playerNo);
                break;
        }
        state = new State(p1, p2);
    }

    public void Turn()
    {
        SettingPanel[0].material = playerMaterial;
        SettingPanel[1].material = playerMaterial;
        board = M_AIplayer.findNextMove(board,playerNo,p1,p2);
        //row = M_AIplayer.GetBestMove();
        //UIÇÃèàóù
        Debug.Log(board);

    }



    
}
