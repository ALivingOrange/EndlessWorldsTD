
/***************************************************************
* file: InventoryToggle.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Allows the player to open and close the
* resource inventory using the I key. Hides the
* resource counter while the inventory is open.
*
****************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject resourceText;

    // function: Start
    // purpose: Hides the inventory panel and displays
    // the resource counter when gameplay begins.
    private void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (resourceText != null)
        {
            resourceText.SetActive(true);
        }
    }

    // function: Update
    // purpose: Checks if the player presses I to
    // toggle the inventory menu.
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.iKey.wasPressedThisFrame)
        {
            toggleInventory();
        }
    }

    // function: toggleInventory
    // purpose: Opens or closes the inventory panel
    // and adjusts the resource counter visibility.
    public void toggleInventory()
    {
        if (inventoryPanel == null)
        {
            return;
        }

        bool isOpen = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isOpen);

        if (resourceText != null)
        {
            resourceText.SetActive(!isOpen);
        }
    }
}
