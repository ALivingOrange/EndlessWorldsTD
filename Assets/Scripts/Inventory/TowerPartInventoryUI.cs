
/***************************************************************
* file: TowerPartInventoryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Dynamically displays crafted tower
* parts in the inventory menu and updates
* when inventory quantities change.
*
****************************************************************/

using UnityEngine;

public class TowerPartInventoryUI : MonoBehaviour
{
    [SerializeField]
    private TowerPartInventory towerPartInventory;

    [SerializeField]
    private TowerPartInventoryEntryUI entryPrefab;

    [SerializeField]
    private Transform content;

    // function: Start
    // purpose: Subscribes to inventory changes
    // and displays the initial inventory.
    private void Start()
    {
        if (towerPartInventory == null)
        {
            Debug.LogWarning("Tower Part Inventory is not assigned.");
            return;
        }

        towerPartInventory.onInventoryChanged += refreshInventory;
        refreshInventory();
    }

    // function: OnDestroy
    // purpose: Unsubscribes from inventory changes.
    private void OnDestroy()
    {
        if (towerPartInventory != null)
        {
            towerPartInventory.onInventoryChanged -= refreshInventory;
        }
    }

    // function: refreshInventory
    // purpose: Rebuilds the inventory entries to
    // reflect currently owned tower parts.
    private void refreshInventory()
    {
        if (content == null || entryPrefab == null)
        {
            return;
        }

        // Remove old inventory entries.
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Display crafted bases.
        foreach (var entry in towerPartInventory.getAllBases())
        {
            if (entry.Value > 0)
            {
                createEntry(entry.Key.getDisplayName(), entry.Value);
            }
        }

        // Display crafted cores.
        foreach (var entry in towerPartInventory.getAllCores())
        {
            if (entry.Value > 0)
            {
                createEntry(entry.Key.getDisplayName(), entry.Value);
            }
        }

        // Display crafted heads.
        foreach (var entry in towerPartInventory.getAllHeads())
        {
            if (entry.Value > 0)
            {
                createEntry(entry.Key.getDisplayName(), entry.Value);
            }
        }
    }

    // function: createEntry
    // purpose: Creates one tower part inventory entry.
    private void createEntry(string partName, int quantity)
    {
        TowerPartInventoryEntryUI newEntry =
            Instantiate(entryPrefab, content);

        newEntry.setPart(partName, quantity);
    }
}