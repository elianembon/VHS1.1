using System;
using System.Collections.Generic;

// Clase nodos para el árbol
public class TreeNode<T>
{
    public Func<T, bool> Condition { get; set; }
    public Action<T> Action { get; set; }
    public TreeNode<T> TrueNode { get; set; }
    public TreeNode<T> FalseNode { get; set; }

    public TreeNode(Func<T, bool> condition, Action<T> action, TreeNode<T> trueNode = null, TreeNode<T> falseNode = null)
    {
        Condition = condition;
        Action = action;
        TrueNode = trueNode;
        FalseNode = falseNode;
    }

    public void Execute(T context)
    {
        if (Condition(context))
        {
            Console.WriteLine("Condition is true");
            Action(context);
            TrueNode?.Execute(context);
        }
        else
        {
            Console.WriteLine("Condition is true");
            FalseNode?.Execute(context);
        }
    }
}
