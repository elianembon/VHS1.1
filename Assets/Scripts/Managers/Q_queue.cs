using System;
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
            throw new System.InvalidOperationException("La cola está vacía");
        }

        return queue.Dequeue();
    }

    public T GetElement(int index)
    {
        if (index < 0 || index >= queue.Count)
        {
            throw new IndexOutOfRangeException("Índice fuera de rango en la cola.");
        }

        List<T> list = new List<T>(queue);
        return list[index];
    }

    public T Peek()
    {
        if (queue.Count == 0)
        {
            throw new System.InvalidOperationException("La cola está vacía");
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
