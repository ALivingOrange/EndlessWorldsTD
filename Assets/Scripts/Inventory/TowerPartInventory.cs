
/***************************************************************
* file: TowerPartInventory.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Tracks crafted tower bases, cores, and
* heads owned by the player. Allows other systems
* to add, retrieve, and spend tower parts.
*
****************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerPartInventory : MonoBehaviour
{
    private Dictionary<TowerBaseData, int> bases;
    private Dictionary<TowerCoreData, int> cores;
    private Dictionary<TowerHeadData, int> heads;

    public event Action onInventoryChanged;

    // function: Awake
    // purpose: Initializes the tower part inventories.
    private void Awake()
    {
        bases = new Dictionary<TowerBaseData, int>();
        cores = new Dictionary<TowerCoreData, int>();
        heads = new Dictionary<TowerHeadData, int>();
    }

    // function: addBase
    // purpose: Adds crafted tower bases to inventory.
    public void addBase(TowerBaseData baseData, int amount)
    {
        if (baseData == null || amount <= 0)
        {
            return;
        }

        bases[baseData] = getBaseCount(baseData) + amount;
        onInventoryChanged?.Invoke();
    }

    // function: addCore
    // purpose: Adds crafted tower cores to inventory.
    public void addCore(TowerCoreData coreData, int amount)
    {
        if (coreData == null || amount <= 0)
        {
            return;
        }

        cores[coreData] = getCoreCount(coreData) + amount;
        onInventoryChanged?.Invoke();
    }

    // function: addHead
    // purpose: Adds crafted tower heads to inventory.
    public void addHead(TowerHeadData headData, int amount)
    {
        if (headData == null || amount <= 0)
        {
            return;
        }

        heads[headData] = getHeadCount(headData) + amount;
        onInventoryChanged?.Invoke();
    }

    // function: getBaseCount
    // purpose: Returns the quantity of a specific base.
    public int getBaseCount(TowerBaseData baseData)
    {
        if (baseData == null)
        {
            return 0;
        }

        return bases.TryGetValue(baseData, out int amount)
            ? amount : 0;
    }

    // function: getCoreCount
    // purpose: Returns the quantity of a specific core.
    public int getCoreCount(TowerCoreData coreData)
    {
        if (coreData == null)
        {
            return 0;
        }

        return cores.TryGetValue(coreData, out int amount)
            ? amount : 0;
    }

    // function: getHeadCount
    // purpose: Returns the quantity of a specific head.
    public int getHeadCount(TowerHeadData headData)
    {
        if (headData == null)
        {
            return 0;
        }

        return heads.TryGetValue(headData, out int amount)
            ? amount : 0;
    }

    // function: spendBase
    // purpose: Removes bases if enough are available.
    public bool spendBase(TowerBaseData baseData, int amount)
    {
        if (amount <= 0 || getBaseCount(baseData) < amount)
        {
            return false;
        }

        bases[baseData] -= amount;
        onInventoryChanged?.Invoke();
        return true;
    }

    // function: spendCore
    // purpose: Removes cores if enough are available.
    public bool spendCore(TowerCoreData coreData, int amount)
    {
        if (amount <= 0 || getCoreCount(coreData) < amount)
        {
            return false;
        }

        cores[coreData] -= amount;
        onInventoryChanged?.Invoke();
        return true;
    }

    // function: spendHead
    // purpose: Removes heads if enough are available.
    public bool spendHead(TowerHeadData headData, int amount)
    {
        if (amount <= 0 || getHeadCount(headData) < amount)
        {
            return false;
        }

        heads[headData] -= amount;
        onInventoryChanged?.Invoke();
        return true;
    }

    // function: getAllBases
    // purpose: Returns the stored base quantities.
    public IReadOnlyDictionary<TowerBaseData, int> getAllBases()
    {
        return bases;
    }

    // function: getAllCores
    // purpose: Returns the stored core quantities.
    public IReadOnlyDictionary<TowerCoreData, int> getAllCores()
    {
        return cores;
    }

    // function: getAllHeads
    // purpose: Returns the stored head quantities.
    public IReadOnlyDictionary<TowerHeadData, int> getAllHeads()
    {
        return heads;
    }
}
