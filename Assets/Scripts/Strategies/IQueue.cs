using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueue<T> 
{
    void Enqueue(T item);  // Agrega un elemento al final de la cola.
    T Dequeue();           // Elimina y devuelve el elemento al principio de la cola.
    T Peek();              // Devuelve el elemento al principio de la cola sin eliminarlo.
    int Counter();          // Devuelve la cantidad de elementos en la cola.
    bool IsEmpty();        // Verifica si la cola está vacía.
    void Clear();          // Elimina todos los elementos de la cola.
}
