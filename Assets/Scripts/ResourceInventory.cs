
/***************************************************************
* file: ResourceInventory.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/7/2026
*
* purpose: Manages the player's resource inventory,
* stores resources and allows them to be added,
* checked, and spent.
*
****************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    // Stores each resource and its quantity.
    private Dictionary<string, int> resources;

    // function: Awake
    // purpose: Initializes the resource inventory when
    // the GameObject is loaded.
    private void Awake()
    {
        resources = new Dictionary<string, int>();
    }

    // function: addResource
    // purpose: Adds a specified amount of a resource
    // to the player's inventory.
    public void addResource(string resourceName, int amount)
    {
        if (string.IsNullOrWhiteSpace(resourceName) || amount <= 0)
        {
            return;
        }

        if (!resources.ContainsKey(resourceName))
        {
            resources[resourceName] = 0;
        }

        resources[resourceName] += amount;
    }

    // function: getResource
    // purpose: Returns the quantity of a resource
    // currently stored in the inventory.
    public int getResource(string resourceName)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
        {
            return 0;
        }

        if (resources.ContainsKey(resourceName))
        {
            return resources[resourceName];
        }

        return 0;
    }

    // function: spendResource
    // purpose: Removes a specified amount of a resource
    // if the player has enough to spend.
    // Returns true if successful, otherwise false.
    public bool spendResource(string resourceName, int amount)
    {
        if (string.IsNullOrWhiteSpace(resourceName) || amount <= 0)
        {
            return false;
        }

        if (getResource(resourceName) < amount)
        {
            return false;
        }

        resources[resourceName] -= amount;

        return true;
    }
}
