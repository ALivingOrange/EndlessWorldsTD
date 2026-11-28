
/***************************************************************
* file: TowerPartEntryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/21/2026
*
* purpose: Displays tower part information and
* opens the appropriate details panel based
* on the selected tower part category.
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

    [SerializeField]
    private TextMeshProUGUI partName;

    [SerializeField]
    private TextMeshProUGUI partCost;

    [SerializeField]
    private Button craftButton;

    [SerializeField]
    private Button detailsButton;

    private PartDetailsUI partDetailsUI;
    private string currentPartName;
    private PartType currentPartType;

    // function: Awake
    // purpose: Connects the card button to its click handler.
    private void Awake()
    {
        if (detailsButton != null)
        {
            detailsButton.onClick.AddListener(openDetails);
        }
    }

    // function: setBase
    // purpose: Displays information about a tower base.
    public void setBase(TowerBaseData baseData)
    {
        if (baseData == null)
        {
            return;
        }

        currentPartType = PartType.Base;
        setPartInformation(baseData.name);
    }

    // function: setCore
    // purpose: Displays information about a tower core.
    public void setCore(TowerCoreData coreData)
    {
        if (coreData == null)
        {
            return;
        }

        currentPartType = PartType.Core;
        setPartInformation(coreData.name);
    }

    // function: setHead
    // purpose: Displays information about a tower head.
    public void setHead(TowerHeadData headData)
    {
        if (headData == null)
        {
            return;
        }

        currentPartType = PartType.Head;
        setPartInformation(headData.name);
    }

    // function: setPartInformation
    // purpose: Updates the displayed name and cost placeholder.
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
    }

    // function: setDetailsUI
    // purpose: Connects this card to the details panel.
    public void setDetailsUI(PartDetailsUI detailsUI)
    {
        partDetailsUI = detailsUI;
    }

    // function: openDetails
    // purpose: Opens the details panel for the selected category.
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