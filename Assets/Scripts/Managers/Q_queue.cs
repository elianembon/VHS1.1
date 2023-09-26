using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_queue<T> : IQueue<T>
{
    private Queue<T> queue = new Queue<T>();

    public void Enqueue(T item)
    {
        queue.Enqueue(item);
    }

    public T Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new System.InvalidOperationException("La cola est� vac�a");
        }

        return queue.Dequeue();
    }

    public T Peek()
    {
        if (queue.Count == 0)
        {
            throw new System.InvalidOperationException("La cola est� vac�a");
        }

        return queue.Peek();
    }
    public int Counter()
    {
        return queue.Count;
    }

    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    public void Clear()
    {
        queue.Clear();
    }
}
