
public class DemoPlayer
{
    int PlayerNo;
    public bool[] NumCards = new bool[10];//

    public DemoPlayer(int playerNo)
    {
        PlayerNo = playerNo;
        for(int i= 0; i < NumCards.Length; i++)
        {
            NumCards[i] = true;
        }
    }

    public DemoPlayer(int playerNo, bool[] numCards)
    {
        PlayerNo=playerNo;
        NumCards = new bool[NumCards.Length];
        for(int i = 0; i < NumCards.Length; i++)
        {
            NumCards[i] = numCards[i];
        }
    }
    //CopyConstructor
    public DemoPlayer(DemoPlayer player)
    {
        PlayerNo = player.PlayerNo;
        NumCards = new bool[player.NumCards.Length];
        for (int i = 0; i < NumCards.Length; i++)
        {
            NumCards[i] = player.NumCards[i];
        }
    }
}
