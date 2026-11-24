/***************************************************************
*file: MinHeap.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/16/2026
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
            if (items[i].cost >= items[parent].cost)
                break;
            Swap(i, parent);
            i = parent;
        }
    }
    
    private void SiftDown(int i)
    {
        int Count = items.Count;
        while (true)
        {
            int left = 2 * i + 1;
            int right = left + 1;
            int smallest = i;

            if (left < Count && items[left].cost < items[smallest].cost)
                smallest = left;
            if (right < Count && items[right].cost < items[smallest].cost)
                smallest = right;
            if (smallest == i)
                break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        (int cell, float cost) temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }
}
