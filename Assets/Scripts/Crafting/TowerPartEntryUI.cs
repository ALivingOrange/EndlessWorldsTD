
/***************************************************************
* file: TowerPartEntryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Displays tower part information and
* handles crafting, resource costs, owned quantities,
* progress bars, and part details.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    private const float CRAFT_TIME = 5f;

    [SerializeField] private TextMeshProUGUI partName;
    [SerializeField] private TextMeshProUGUI partCost;
    [SerializeField] private TextMeshProUGUI partOwned;
    [SerializeField] private Button craftButton;
    [SerializeField] private Button detailsButton;

    [SerializeField] private GameObject craftProgressBar;
    [SerializeField] private Image progressFill;

    private PartDetailsUI partDetailsUI;
    private TowerPartInventory towerPartInventory;
    private ResourceInventory resourceInventory;
    private MonoBehaviour craftingRunner;

    private TowerPartData currentPartData;
    private TowerBaseData baseData;
    private TowerCoreData coreData;
    private TowerHeadData headData;

    private string currentPartName;
    private PartType currentPartType;
    private bool isCrafting;

    // function: Awake
    // purpose: Initializes buttons and progress bar.
    private void Awake()
    {
        isCrafting = false;

        if (detailsButton != null)
        {
            detailsButton.onClick.AddListener(openDetails);
        }

        if (craftButton != null)
        {
            craftButton.onClick.AddListener(startCrafting);
        }

        if (craftProgressBar != null)
        {
            craftProgressBar.SetActive(false);
        }
    }

    // function: OnDestroy
    // purpose: Removes event subscriptions.
    private void OnDestroy()
    {
        if (detailsButton != null)
        {
            detailsButton.onClick.RemoveListener(openDetails);
        }

        if (craftButton != null)
        {
            craftButton.onClick.RemoveListener(startCrafting);
        }

        if (towerPartInventory != null)
        {
            towerPartInventory.onInventoryChanged -= updateOwnedCount;
        }

        if (resourceInventory != null)
        {
            resourceInventory.onInventoryChanged -= updateCraftButton;
        }
    }

    // function: setBase
    // purpose: Assigns tower base data.
    public void setBase(TowerBaseData data)
    {
        baseData = data;
        currentPartType = PartType.Base;
        setPartInformation(data);
    }

    // function: setCore
    // purpose: Assigns tower core data.
    public void setCore(TowerCoreData data)
    {
        coreData = data;
        currentPartType = PartType.Core;
        setPartInformation(data);
    }

    // function: setHead
    // purpose: Assigns tower head data.
    public void setHead(TowerHeadData data)
    {
        headData = data;
        currentPartType = PartType.Head;
        setPartInformation(data);
    }

    // function: setPartInformation
    // purpose: Displays part name and crafting costs.
    private void setPartInformation(TowerPartData data)
    {
        currentPartData = data;

        if (data == null)
        {
            return;
        }

        currentPartName = data.getDisplayName();

        if (partName != null)
        {
            partName.text = currentPartName;
        }

        if (partCost != null)
        {
            partCost.text = getCostText(data);
        }

        updateOwnedCount();
        updateCraftButton();
    }

    // function: getCostText
    // purpose: Formats crafting requirements.
    private string getCostText(TowerPartData data)
    {
        ResourceCost[] costs = data.getCraftingCosts();

        if (costs == null || costs.Length == 0)
        {
            return "Cost: TBD";
        }

        StringBuilder result = new StringBuilder();

        foreach (ResourceCost cost in costs)
        {
            if (cost == null || cost.getResource() == null)
            {
                continue;
            }

            if (result.Length > 0)
            {
                result.Append(" | ");
            }

            result.Append(cost.getResource().getDisplayName());
            result.Append(": ");
            result.Append(cost.getAmount());
        }

        return result.Length > 0
            ? result.ToString()
            : "Cost: TBD";
    }

    // function: setDetailsUI
    // purpose: Connects the details panel.
    public void setDetailsUI(PartDetailsUI detailsUI)
    {
        partDetailsUI = detailsUI;
    }

    // function: setTowerPartInventory
    // purpose: Connects the crafted parts inventory.
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
        updateCraftButton();
    }

    // function: setResourceInventory
    // purpose: Connects the material inventory.
    public void setResourceInventory(ResourceInventory inventory)
    {
        if (resourceInventory != null)
        {
            resourceInventory.onInventoryChanged -= updateCraftButton;
        }

        resourceInventory = inventory;

        if (resourceInventory != null)
        {
            resourceInventory.onInventoryChanged += updateCraftButton;
        }

        updateCraftButton();
    }

    // function: setCraftingRunner
    // purpose: Runs crafting timers on an active object.
    public void setCraftingRunner(MonoBehaviour runner)
    {
        craftingRunner = runner;
        updateCraftButton();
    }

    // function: updateOwnedCount
    // purpose: Displays the current owned quantity.
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

    // function: getRequiredResources
    // purpose: Validates and combines resource requirements.
    private bool getRequiredResources(
        out Dictionary<Resource, int> requirements)
    {
        requirements = new Dictionary<Resource, int>();

        if (currentPartData == null)
        {
            return false;
        }

        ResourceCost[] costs = currentPartData.getCraftingCosts();

        if (costs == null || costs.Length == 0)
        {
            return false;
        }

        foreach (ResourceCost cost in costs)
        {
            if (cost == null ||
                cost.getResource() == null ||
                cost.getAmount() <= 0)
            {
                return false;
            }

            Resource resource = cost.getResource();
            int amount = cost.getAmount();

            requirements.TryGetValue(resource, out int existing);

            if (existing > int.MaxValue - amount)
            {
                return false;
            }

            requirements[resource] = existing + amount;
        }

        return true;
    }

    // function: hasEnoughResources
    // purpose: Checks all resource requirements.
    private bool hasEnoughResources(
        Dictionary<Resource, int> requirements)
    {
        foreach (var requirement in requirements)
        {
            if (resourceInventory.getResource(requirement.Key)
                < requirement.Value)
            {
                return false;
            }
        }

        return true;
    }

    // function: updateCraftButton
    // purpose: Enables crafting only when requirements are met.
    private void updateCraftButton()
    {
        if (craftButton == null)
        {
            return;
        }

        craftButton.interactable = false;

        if (isCrafting ||
            resourceInventory == null ||
            towerPartInventory == null ||
            craftingRunner == null ||
            !craftingRunner.isActiveAndEnabled)
        {
            return;
        }

        if (!getRequiredResources(out Dictionary<Resource, int> costs))
        {
            return;
        }

        craftButton.interactable = hasEnoughResources(costs);
    }

    // function: startCrafting
    // purpose: Deducts resources and starts crafting.
    private void startCrafting()
    {
        if (isCrafting ||
            resourceInventory == null ||
            towerPartInventory == null ||
            craftingRunner == null ||
            !craftingRunner.isActiveAndEnabled)
        {
            return;
        }

        if (!getRequiredResources(out Dictionary<Resource, int> costs))
        {
            return;
        }

        if (!hasEnoughResources(costs))
        {
            updateCraftButton();
            return;
        }

        // Prevent another craft while resources are deducted.
        isCrafting = true;
        updateCraftButton();

        foreach (var requirement in costs)
        {
            resourceInventory.spendResource(
                requirement.Key,
                requirement.Value
            );
        }

        craftingRunner.StartCoroutine(craftRoutine());
    }

    // function: craftRoutine
    // purpose: Tracks five-second progress and awards the part.
    private IEnumerator craftRoutine()
    {
        float elapsed = 0f;

        if (craftProgressBar != null)
        {
            craftProgressBar.SetActive(true);
        }

        if (progressFill != null)
        {
            progressFill.fillAmount = 0f;
        }

        while (elapsed < CRAFT_TIME)
        {
            elapsed += Time.deltaTime;

            if (progressFill != null)
            {
                progressFill.fillAmount =
                    Mathf.Clamp01(elapsed / CRAFT_TIME);
            }

            yield return null;
        }

        if (progressFill != null)
        {
            progressFill.fillAmount = 1f;
        }

        switch (currentPartType)
        {
            case PartType.Base:
                towerPartInventory.addBase(baseData, 1);
                break;

            case PartType.Core:
                towerPartInventory.addCore(coreData, 1);
                break;

            case PartType.Head:
                towerPartInventory.addHead(headData, 1);
                break;
        }

        if (craftProgressBar != null)
        {
            craftProgressBar.SetActive(false);
        }

        isCrafting = false;
        updateCraftButton();
    }


    // function: openDetails
    // purpose: Displays the actual stats of the selected tower part.
    private void openDetails()
    {
        if (partDetailsUI == null)
        {
            return;
        }

        switch (currentPartType)
        {
            case PartType.Base:
                partDetailsUI.showBaseDetails(baseData);
                break;

            case PartType.Core:
                partDetailsUI.showCoreDetails(coreData);
                break;

            case PartType.Head:
                partDetailsUI.showHeadDetails(headData);
                break;
        }
    }

}