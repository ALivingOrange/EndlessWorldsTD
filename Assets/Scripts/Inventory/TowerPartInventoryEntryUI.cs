
/***************************************************************
* file: TowerPartInventoryEntryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Displays the name and quantity of a
* crafted tower part inside the inventory menu.
*
****************************************************************/

using TMPro;
using UnityEngine;

public class TowerPartInventoryEntryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI partNameText;

    [SerializeField]
    private TextMeshProUGUI partQuantityText;

    // function: setPart
    // purpose: Updates the displayed tower part
    // name and owned quantity.
    public void setPart(string partName, int quantity)
    {
        if (partNameText != null)
        {
            partNameText.text = partName;
        }

        if (partQuantityText != null)
        {
            partQuantityText.text = "×" + quantity;
        }
    }
}