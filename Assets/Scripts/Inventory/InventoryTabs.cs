
/***************************************************************
* file: InventoryTabs.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Switches between the Resources and
* Tower Parts inventory tabs and highlights
* the currently selected tab.
*
****************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class InventoryTabs : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceScrollView;

    [SerializeField]
    private GameObject towerPartsPanel;

    [SerializeField]
    private Button resourcesTabButton;

    [SerializeField]
    private Button towerPartsTabButton;

    [SerializeField]
    private Color selectedColor = new Color(0.35f, 0.60f, 0.85f);

    [SerializeField]
    private Color normalColor = new Color(0.55f, 0.65f, 0.75f);

    // function: Awake
    // purpose: Selects the Resources tab by default.
    private void Awake()
    {
        showResources();
    }

    // function: showResources
    // purpose: Displays resources and highlights the Resources tab.
    public void showResources()
    {
        if (resourceScrollView != null)
        {
            resourceScrollView.SetActive(true);
        }

        if (towerPartsPanel != null)
        {
            towerPartsPanel.SetActive(false);
        }

        updateButtonColors(resourcesTabButton);
    }

    // function: showTowerParts
    // purpose: Displays tower parts and highlights their tab.
    public void showTowerParts()
    {
        if (resourceScrollView != null)
        {
            resourceScrollView.SetActive(false);
        }

        if (towerPartsPanel != null)
        {
            towerPartsPanel.SetActive(true);
        }

        updateButtonColors(towerPartsTabButton);
    }

    // function: updateButtonColors
    // purpose: Updates the colors of both inventory tabs.
    private void updateButtonColors(Button selectedButton)
    {
        setButtonColor(
            resourcesTabButton,
            resourcesTabButton == selectedButton
        );

        setButtonColor(
            towerPartsTabButton,
            towerPartsTabButton == selectedButton
        );
    }

    // function: setButtonColor
    // purpose: Highlights the selected button and resets the other.
    private void setButtonColor(Button button, bool selected)
    {
        if (button == null)
        {
            return;
        }

        Color color = selected ? selectedColor : normalColor;

        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        colors.pressedColor = color;

        button.colors = colors;

        if (button.targetGraphic != null)
        {
            button.targetGraphic.color = color;
        }
    }
}
