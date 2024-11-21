using System;

public class UCT
{
    public static double uctValue(int totalVisit,double nodeWinScore,int nodeVisit)
    {
        if(nodeVisit == 0)
        {
            return int.MaxValue;
        }
        return (nodeWinScore / nodeVisit) + Math.Sqrt(2) * Math.Sqrt(Math.Log(totalVisit) / nodeVisit);
    }

    
    public static Node findBestNodeWithUCT(Node node)
    {
        int parentVisit = node.getState().getVisitCount();
        var ChildArray =  node.getChildArray();

        Node MaxNode = ChildArray[0];
        double maxUCT = uctValue(parentVisit,MaxNode.getState().getWinScore(),MaxNode.getState().getVisitCount());

        foreach( var child in node.getChildArray() )
        {
            double UCT = uctValue(parentVisit, child.getState().getWinScore(), child.getState().getVisitCount());
            if(maxUCT < UCT)
            {
                MaxNode = child;
                maxUCT = UCT;
            }
        }
        return MaxNode;
    }
}
