
/***************************************************************
* file: ResourceUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Displays resources in the HUD and generates
* inventory cards dynamically whenever resource
* quantities change.
*
****************************************************************/

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private ResourceInventory inventory;

    [SerializeField]
    private TextMeshProUGUI resourceText;

    [SerializeField]
    private ResourceEntryUI resourceEntryPrefab;

    [SerializeField]
    private Transform resourceContent;

    // function: OnEnable
    // purpose: Subscribes to inventory changes.
    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChanged += updateResourceDisplay;
        }
    }

    // function: Start
    // purpose: Displays the inventory when the scene starts.
    private void Start()
    {
        updateResourceDisplay();
    }

    // function: OnDisable
    // purpose: Unsubscribes from inventory changes.
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChanged -= updateResourceDisplay;
        }
    }

    // function: updateResourceDisplay
    // purpose: Refreshes the HUD text and inventory cards.
    private void updateResourceDisplay()
    {
        if (inventory == null)
        {
            return;
        }

        IReadOnlyDictionary<Resource, int> resources =
            inventory.getAllResources();

        // Update the resource counter.
        if (resourceText != null)
        {
            StringBuilder display = new StringBuilder();

            foreach (KeyValuePair<Resource, int> entry in resources)
            {
                if (entry.Key != null)
                {
                    display.AppendLine(
                        entry.Key.getDisplayName() + ": " + entry.Value);
                }
            }

            resourceText.text = display.Length > 0
                ? display.ToString()
                : "No resources";
        }

        // Check the inventory grid references.
        if (resourceEntryPrefab == null || resourceContent == null)
        {
            return;
        }

        // Remove old resource cards.
        for (int i = resourceContent.childCount - 1; i >= 0; i--)
        {
            Destroy(resourceContent.GetChild(i).gameObject);
        }

        // Create a card for each resource.
        foreach (KeyValuePair<Resource, int> entry in resources)
        {
            if (entry.Key == null)
            {
                continue;
            }

            ResourceEntryUI newEntry =
                Instantiate(resourceEntryPrefab, resourceContent);

            newEntry.setResource(entry.Key, entry.Value);
        }
    }
}
