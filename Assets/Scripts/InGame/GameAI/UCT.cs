using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UCT : MonoBehaviour
{
    public static double uctValue(int totalVisit,double nodeWinScore,int nodeVisit)
    {
        if(nodeVisit == 0)
        {
            return int.MaxValue;
        }
        return (nodeWinScore / (double)nodeVisit) + Math.Sqrt(3) * Math.Sqrt(Math.Log(totalVisit) / (double)nodeVisit);
    }

    /*
    static Node findBestNodeWithUCT(Node node)
    {
        int parentVisit = node.getState().getVisitCount();
        return Enumerable.Max(.ToList() .node.getChildArray().
            OrderByDescending(c => uctValue(parentVisit, c.getState().getWinScore(), c.getState().getVisitCount()))
            .First());
    }*/
}
