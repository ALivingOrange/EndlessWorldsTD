
/***************************************************************
* file: ResourceCost.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Stores a resource type and the amount
* required to craft a tower component.
*
****************************************************************/

using System;
using UnityEngine;

[Serializable]
public class ResourceCost
{
    [SerializeField]
    private Resource resource;

    [SerializeField]
    private int amount;

    // function: getResource
    // purpose: Returns the required resource type.
    public Resource getResource()
    {
        return resource;
    }

    // function: getAmount
    // purpose: Returns the required quantity.
    public int getAmount()
    {
        return amount;
    }
}