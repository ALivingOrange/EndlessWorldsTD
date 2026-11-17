
/***************************************************************
* file: InventoryToggle.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Opens and closes the resource inventory
* using Unity's Input System. Hides the resource
* counter while the inventory is open.
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

    [SerializeField]
    private InputAction toggleInventoryAction;

    // function: OnEnable
    // purpose: Enables the inventory input action.
    private void OnEnable()
    {
        toggleInventoryAction.Enable();
    }

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
    // purpose: Checks whether the inventory input
    // action was pressed.
    private void Update()
    {
        if (toggleInventoryAction.WasPressedThisFrame())
        {
            toggleInventory();
        }
    }

    // function: OnDisable
    // purpose: Disables the inventory input action.
    private void OnDisable()
    {
        toggleInventoryAction.Disable();
    }

    // function: toggleInventory
    // purpose: Toggles inventory visibility and
    // adjusts the resource counter.
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
