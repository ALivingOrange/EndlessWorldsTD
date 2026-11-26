
/***************************************************************
* file: GameMenuController.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Opens and closes the inventory and
* crafting menus using keyboard input or buttons.
*
****************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject resourceText;

    [SerializeField] private InputAction toggleInventoryAction;
    [SerializeField] private InputAction toggleCraftingAction;

    // function: Awake
    // purpose: Starts the game with both menus closed.
    private void Awake()
    {
        closeAllMenus();
    }

    // function: OnEnable
    // purpose: Enables the keyboard input actions.
    private void OnEnable()
    {
        toggleInventoryAction.Enable();
        toggleCraftingAction.Enable();
    }

    // function: OnDisable
    // purpose: Disables the keyboard input actions.
    private void OnDisable()
    {
        toggleInventoryAction.Disable();
        toggleCraftingAction.Disable();
    }

    // function: Update
    // purpose: Detects keyboard input for both menus.
    private void Update()
    {
        if (toggleInventoryAction.WasPressedThisFrame())
        {
            toggleInventory();
        }

        if (toggleCraftingAction.WasPressedThisFrame())
        {
            toggleCrafting();
        }
    }

    // function: toggleInventory
    // purpose: Opens or closes the inventory menu.
    public void toggleInventory()
    {
        bool shouldOpen = !inventoryPanel.activeSelf;

        closeAllMenus();

        if (shouldOpen)
        {
            inventoryPanel.SetActive(true);
            updateResourceHUD();
        }
    }

    // function: toggleCrafting
    // purpose: Opens or closes the crafting menu.
    public void toggleCrafting()
    {
        bool shouldOpen = !craftingPanel.activeSelf;

        closeAllMenus();

        if (shouldOpen)
        {
            craftingPanel.SetActive(true);
            updateResourceHUD();
        }
    }

    // function: closeAllMenus
    // purpose: Closes both menus and restores the HUD.
    public void closeAllMenus()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
        }

        updateResourceHUD();
    }

    // function: updateResourceHUD
    // purpose: Shows the resource HUD during gameplay.
    private void updateResourceHUD()
    {
        if (resourceText != null)
        {
            bool menuOpen =
                (inventoryPanel != null && inventoryPanel.activeSelf) ||
                (craftingPanel != null && craftingPanel.activeSelf);

            resourceText.SetActive(!menuOpen);
        }
    }
}