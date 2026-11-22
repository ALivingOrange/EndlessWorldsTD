
/***************************************************************
* file: Resource.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Defines a resource type for the game.
* Each resource contains a display name and icon
* that can be used by the inventory and UI.
*
****************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "NewResource",
    menuName = "Endless Worlds TD/Resource")]
public class Resource : ScriptableObject
{
    [SerializeField]
    private string displayName;

    [SerializeField]
    private Sprite icon;

    // function: getDisplayName
    // purpose: Returns the display name of the resource.
    public string getDisplayName()
    {
        return displayName;
    }

    // function: getIcon
    // purpose: Returns the resource's icon.
    public Sprite getIcon()
    {
        return icon;
    }
}
