
/***************************************************************
* file: ResourceInventory.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Manages the player's resource inventory.
* Stores resource types and their quantities,
* allowing resources to be added, checked, and spent.
*
****************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    private Dictionary<Resource, int> resources;

    // Notifies other scripts when inventory quantities change.
    public event Action onInventoryChanged;

    // function: Awake
    // purpose: Initializes the resource inventory.
    private void Awake()
    {
        resources = new Dictionary<Resource, int>();
    }

    // function: addResource
    // purpose: Adds a specified amount of a resource
    // to the player's inventory.
    public void addResource(Resource resource, int amount)
    {
        if (resource == null || amount <= 0)
        {
            return;
        }

        if (!resources.ContainsKey(resource))
        {
            resources[resource] = 0;
        }

        resources[resource] += amount;

        onInventoryChanged?.Invoke();
    }

    // function: getResource
    // purpose: Returns how much of a resource the player owns.
    public int getResource(Resource resource)
    {
        if (resource == null)
        {
            return 0;
        }

        if (resources.ContainsKey(resource))
        {
            return resources[resource];
        }

        return 0;
    }

    // function: spendResource
    // purpose: Spends a specified amount of a resource
    // if enough is available.
    public bool spendResource(Resource resource, int amount)
    {
        if (resource == null || amount <= 0)
        {
            return false;
        }

        if (getResource(resource) < amount)
        {
            return false;
        }

        resources[resource] -= amount;

        onInventoryChanged?.Invoke();

        return true;
    }

    // function: getAllResources
    // purpose: Returns a read-only view of all resources
    // currently stored in the inventory.
    public IReadOnlyDictionary<Resource, int> getAllResources()
    {
        return resources;
    }
}
