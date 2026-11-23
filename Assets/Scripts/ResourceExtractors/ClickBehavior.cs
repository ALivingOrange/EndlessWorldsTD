/***************************************************************
*file: ClickBehavior.cs
*author: J. Johnson
*class: CS 4700 – Game Development
*assignment: Endless Worlds TD
*date last modified: 11/21/2026
*
*purpose: Script that activates a function upon clicking a game object.
*
****************************************************************/
using UnityEngine;

public class ClickBehavior : MonoBehaviour
{
    public ExtractorPopupManager popupManager;

    private void OnMouseDown()
    {
        // Makes the popup appear on click
        popupManager.Appear(this.gameObject);
    }
}
