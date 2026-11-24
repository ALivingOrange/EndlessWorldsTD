
/***************************************************************
* file: ResourceEntryUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Controls an individual resource entry in the
* inventory UI by displaying the resource's icon,
* name, and quantity.
*
****************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceEntryUI : MonoBehaviour
{
    [SerializeField]
    private Image resourceIcon;

    [SerializeField]
    private TextMeshProUGUI resourceName;

    [SerializeField]
    private TextMeshProUGUI resourceQuantity;

    // function: setResource
    // purpose: Updates the resource card's icon, display
    // name, and quantity using a Resource ScriptableObject.
    public void setResource(Resource resource, int amount)
    {
        if (resource == null)
        {
            return;
        }

        if (resourceName != null)
        {
            resourceName.text = resource.getDisplayName();
        }

        if (resourceQuantity != null)
        {
            resourceQuantity.text = "x" + amount;
        }

        if (resourceIcon != null)
        {
            Sprite icon = resource.getIcon();

            resourceIcon.sprite = icon;
            resourceIcon.enabled = icon != null;
        }
    }
}
