using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    State state;
    Node parent;//e
    List<Node> childArray;

    public Node()
    {
        this.state = new State();
        childArray = new List<Node>();
    }

    public Node(State state)
    {
        this.state = state;
        childArray = new List<Node>();
    }

    public Node(State state,Node parent,List<Node> childArray)
    {
        this.state = state;
        this.parent = parent;
        this.childArray = childArray;
    }


    public Node(Node node)
    {
        this.childArray = new List<Node>();
        this.state = new State(node.getState());
        if(node.getParent() != null)
        {
            this.parent = node.getParent();
        }
        List<Node> childArray = node.getChildArray();
        foreach(Node child in childArray)
        {
            this.childArray.Add(new Node(child));
        }

    }

    public State getState()
    {
        return state;
    }

    public void setState(State state)
    {
        this.state = state;
    }

    public Node getParent()
    {
        return parent;
    }

    public void setParent(Node parent)
    {
        this.parent = parent;
    }

    public List<Node> getChildArray()
    {
        return childArray;
    }

    public void setChildArray(List<Node> childArray)
    {
        this.childArray = childArray;
    }
    /*
    public Node getRandomChildNode()
    {
        int noOfPossibleMoves = this.childArray.size();
        int selectRandom = (int)(Random. * noOfPossibleMoves);

        return this.childArray.get(selectRandom);
    }
    */
    /*
    public Node getChildWithMaxScore()
    {
        return  childArray.max(this.childArray, Comparator.comparing(c-> {
            return c.getState().getVisitCount();//Å‘½–K–â”BÅ‚•½‹Ï•ñV‚ğg‚¤
        }));
    }
    */
}
