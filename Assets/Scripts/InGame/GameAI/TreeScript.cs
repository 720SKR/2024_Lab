using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    Node root;
    public TreeScript()
    {
        root = new Node();
    }

    public TreeScript(Node root)
    {
        this.root = root;
    }

    public Node getRoot()
    {
        return root;
    }

    public void setRoot(Node root)
    {
        this.root = root;
    }

    public void addChild(Node parent,Node child)
    {
//        parent.getChildArray().add(child);
    }
}
