
/***************************************************************
* file: TowerCraftingUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/21/2026
*
* purpose: Generates crafting menu cards using
* existing tower part ScriptableObjects and
* connects them to the details panel.
*
****************************************************************/

using UnityEngine;

public class TowerCraftingUI : MonoBehaviour
{
    [SerializeField]
    private TowerPartEntryUI towerPartPrefab;

    [SerializeField]
    private Transform basesContent;

    [SerializeField]
    private Transform coresContent;

    [SerializeField]
    private Transform headsContent;

    [SerializeField]
    private TowerBaseData[] availableBases;

    [SerializeField]
    private TowerCoreData[] availableCores;

    [SerializeField]
    private TowerHeadData[] availableHeads;

    [SerializeField]
    private PartDetailsUI partDetailsUI;

    // function: Start
    // purpose: Creates the tower part cards when
    // the crafting menu first loads.
    private void Start()
    {
        createTowerPartCards();
    }

    // function: createTowerPartCards
    // purpose: Generates cards for available bases,
    // cores, and heads.
    private void createTowerPartCards()
    {
        if (towerPartPrefab == null)
        {
            Debug.LogWarning("Tower part prefab is missing.");
            return;
        }

        // Create base cards.
        if (basesContent != null && availableBases != null)
        {
            foreach (TowerBaseData baseData in availableBases)
            {
                if (baseData == null)
                {
                    continue;
                }

                TowerPartEntryUI newEntry =
                    Instantiate(towerPartPrefab, basesContent);

                newEntry.setBase(baseData);
                newEntry.setDetailsUI(partDetailsUI);
            }
        }

        // Create core cards.
        if (coresContent != null && availableCores != null)
        {
            foreach (TowerCoreData coreData in availableCores)
            {
                if (coreData == null)
                {
                    continue;
                }

                TowerPartEntryUI newEntry =
                    Instantiate(towerPartPrefab, coresContent);

                newEntry.setCore(coreData);
                newEntry.setDetailsUI(partDetailsUI);
            }
        }

        // Create head cards.
        if (headsContent != null && availableHeads != null)
        {
            foreach (TowerHeadData headData in availableHeads)
            {
                if (headData == null)
                {
                    continue;
                }

                TowerPartEntryUI newEntry =
                    Instantiate(towerPartPrefab, headsContent);

                newEntry.setHead(headData);
                newEntry.setDetailsUI(partDetailsUI);
            }
        }
    }
}