
/***************************************************************
* file: TowerPartEntryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Displays tower part information, owned
* quantities, and opens the part details panel.
*
****************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPartEntryUI : MonoBehaviour
{
    private enum PartType
    {
        Base,
        Core,
        Head
    }

    [SerializeField] private TextMeshProUGUI partName;
    [SerializeField] private TextMeshProUGUI partCost;
    [SerializeField] private TextMeshProUGUI partOwned;
    [SerializeField] private Button craftButton;
    [SerializeField] private Button detailsButton;

    private PartDetailsUI partDetailsUI;
    private TowerPartInventory towerPartInventory;

    private TowerBaseData baseData;
    private TowerCoreData coreData;
    private TowerHeadData headData;

    private string currentPartName;
    private PartType currentPartType;

    // function: Awake
    // purpose: Connects the card's details button.
    private void Awake()
    {
        if (detailsButton != null)
        {
            detailsButton.onClick.AddListener(openDetails);
        }
    }

    // function: OnDestroy
    // purpose: Removes inventory event subscriptions.
    private void OnDestroy()
    {
        if (towerPartInventory != null)
        {
            towerPartInventory.onInventoryChanged -= updateOwnedCount;
        }
    }

    // function: setBase
    // purpose: Assigns base data to this card.
    public void setBase(TowerBaseData data)
    {
        baseData = data;
        currentPartType = PartType.Base;
        setPartInformation(data != null ? data.name : "");
    }

    // function: setCore
    // purpose: Assigns core data to this card.
    public void setCore(TowerCoreData data)
    {
        coreData = data;
        currentPartType = PartType.Core;
        setPartInformation(data != null ? data.name : "");
    }

    // function: setHead
    // purpose: Assigns head data to this card.
    public void setHead(TowerHeadData data)
    {
        headData = data;
        currentPartType = PartType.Head;
        setPartInformation(data != null ? data.name : "");
    }

    // function: setPartInformation
    // purpose: Updates the part name and cost.
    private void setPartInformation(string name)
    {
        currentPartName = name;

        if (partName != null)
        {
            partName.text = name;
        }

        if (partCost != null)
        {
            partCost.text = "Cost: TBD";
        }

        if (craftButton != null)
        {
            craftButton.interactable = false;
        }

        updateOwnedCount();
    }

    // function: setDetailsUI
    // purpose: Connects the card to the details panel.
    public void setDetailsUI(PartDetailsUI detailsUI)
    {
        partDetailsUI = detailsUI;
    }

    // function: setTowerPartInventory
    // purpose: Connects the card to the shared part inventory.
    public void setTowerPartInventory(TowerPartInventory inventory)
    {
        if (towerPartInventory != null)
        {
            towerPartInventory.onInventoryChanged -= updateOwnedCount;
        }

        towerPartInventory = inventory;

        if (towerPartInventory != null)
        {
            towerPartInventory.onInventoryChanged += updateOwnedCount;
        }

        updateOwnedCount();
    }

    // function: updateOwnedCount
    // purpose: Refreshes how many of this part are owned.
    private void updateOwnedCount()
    {
        if (partOwned == null)
        {
            return;
        }

        int quantity = 0;

        if (towerPartInventory != null)
        {
            switch (currentPartType)
            {
                case PartType.Base:
                    quantity = towerPartInventory.getBaseCount(baseData);
                    break;

                case PartType.Core:
                    quantity = towerPartInventory.getCoreCount(coreData);
                    break;

                case PartType.Head:
                    quantity = towerPartInventory.getHeadCount(headData);
                    break;
            }
        }

        partOwned.text = "Owned: " + quantity;
    }

    // function: openDetails
    // purpose: Opens the matching tower part details.
    private void openDetails()
    {
        if (partDetailsUI == null)
        {
            return;
        }

        switch (currentPartType)
        {
            case PartType.Base:
                partDetailsUI.showBaseDetails(currentPartName);
                break;

            case PartType.Core:
                partDetailsUI.showCoreDetails(currentPartName);
                break;

            case PartType.Head:
                partDetailsUI.showHeadDetails(currentPartName);
                break;
        }
    }
}