
/***************************************************************
* file: TowerCraftingUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Generates tower crafting cards and
* connects them to the shared parts inventory
* and tower part details panel.
*
****************************************************************/

using UnityEngine;

public class TowerCraftingUI : MonoBehaviour
{
    [SerializeField] private TowerPartEntryUI towerPartPrefab;

    [SerializeField] private Transform basesContent;
    [SerializeField] private Transform coresContent;
    [SerializeField] private Transform headsContent;

    [SerializeField] private TowerBaseData[] availableBases;
    [SerializeField] private TowerCoreData[] availableCores;
    [SerializeField] private TowerHeadData[] availableHeads;

    [SerializeField] private PartDetailsUI partDetailsUI;
    [SerializeField] private TowerPartInventory towerPartInventory;

    // function: Start
    // purpose: Generates crafting cards when the game starts.
    private void Start()
    {
        createTowerPartCards();
    }

    // function: createTowerPartCards
    // purpose: Creates cards for all available tower parts.
    private void createTowerPartCards()
    {
        if (towerPartPrefab == null)
        {
            Debug.LogWarning("Tower part prefab is missing.");
            return;
        }

        if (towerPartInventory == null)
        {
            Debug.LogWarning("Tower Part Inventory is not assigned.");
        }

        // Generate base cards.
        if (basesContent != null && availableBases != null)
        {
            foreach (TowerBaseData data in availableBases)
            {
                if (data == null)
                {
                    continue;
                }

                TowerPartEntryUI entry =
                    Instantiate(towerPartPrefab, basesContent);

                entry.setBase(data);
                entry.setDetailsUI(partDetailsUI);
                entry.setTowerPartInventory(towerPartInventory);
            }
        }

        // Generate core cards.
        if (coresContent != null && availableCores != null)
        {
            foreach (TowerCoreData data in availableCores)
            {
                if (data == null)
                {
                    continue;
                }

                TowerPartEntryUI entry =
                    Instantiate(towerPartPrefab, coresContent);

                entry.setCore(data);
                entry.setDetailsUI(partDetailsUI);
                entry.setTowerPartInventory(towerPartInventory);
            }
        }

        // Generate head cards.
        if (headsContent != null && availableHeads != null)
        {
            foreach (TowerHeadData data in availableHeads)
            {
                if (data == null)
                {
                    continue;
                }

                TowerPartEntryUI entry =
                    Instantiate(towerPartPrefab, headsContent);

                entry.setHead(data);
                entry.setDetailsUI(partDetailsUI);
                entry.setTowerPartInventory(towerPartInventory);
            }
        }
    }
}