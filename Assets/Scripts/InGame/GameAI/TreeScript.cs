public class TreeScript
{
    Node root;
    public TreeScript(DemoPlayer p1, DemoPlayer p2)
    {
        root = new Node(p1,p2);
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
        parent.getChildArray().Add(child);
    }
}
