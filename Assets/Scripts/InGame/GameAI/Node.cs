using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    State state;
    Node parent;//�e
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
    //�q�m�[�h���烉���_����1�l�I������
    public Node getRandomChildNode()
    {
        int noOfPossibleMoves = childArray.Count;
        int selectRandom = Random.Range(0, noOfPossibleMoves);

        return childArray[selectRandom];
    }
    
    //�ő��K�␔�̎q�m�[�h�����߂�
    public Node getChildWithMaxScore()
    {
        Node Max = childArray[0];
        for(int i = 0; i<childArray.Count; i++)
        {
            if (childArray[i].getState().getVisitCount() > Max.getState().getVisitCount())
            {
                Max = childArray[i];
            }
        }
        return Max;
        /*
        return  childArray.Max(this.childArray, Comparer.(c-> {
            return c.getState().getVisitCount();//�ő��K�␔�B�ō����ϕ�V���g��
        }));
        */
    }
    
}
