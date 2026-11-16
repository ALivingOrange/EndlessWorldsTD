/***************************************************************
*file: MinHeap.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: Used for enemy AI pathfinding calculation, see FlowField.cs
* Unity 6 doesn't have a built in min heap so I added a basic one. 
*
****************************************************************/
using System.Collections.Generic;

public class MinHeap
{
    private readonly List<(int cell, float cost)> items;

    public MinHeap(int capacity = 0)
    {
        items = new List<(int, float)>(capacity);
    }

    public int Count => items.Count;

    public void Clear() => items.Clear();

    public void Push(int cell, float cost)
    {
        items.Add((cell, cost));
        SiftUp(items.Count - 1);
    }

    public (int cell, float cost) Pop()
    {
        (int cell, float cost) top = items[0];
        int last = items.Count - 1;
        items[0] = items[last];
        items.RemoveAt(last);
        if (items.Count > 0) SiftDown(0);
        return top;
    }

    private void SiftUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (_items[i].Cost >= _items[parent].Cost)
                break;
            Swap(i, parent);
            i = parent;
        }
    }
    
    private void SiftDown(int i)
    {
        int count = _items.Count;
        while (true)
        {
            int left = 2 * i + 1;
            int right = left + 1;
            int smallest = i;

            if (left < count && _items[left].Cost < _items[smallest].Cost)
                smallest = left;
            if (right < count && _items[right].Cost < _items[smallest].Cost)
                smallest = right;
            if (smallest == i)
                break;

            Swap(i, smallest);
            i = smallest;
        }
    }
}