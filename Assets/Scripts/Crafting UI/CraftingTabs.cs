
/***************************************************************
* file: CraftingTabs.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/21/2026
*
* purpose: Controls crafting category visibility
* and highlights the currently selected tab.
*
****************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CraftingTabs : MonoBehaviour
{
    [SerializeField] private GameObject basesPanel;
    [SerializeField] private GameObject coresPanel;
    [SerializeField] private GameObject headsPanel;

    [SerializeField] private Button basesButton;
    [SerializeField] private Button coresButton;
    [SerializeField] private Button headsButton;

    [SerializeField] private Color selectedColor =
        new Color(0.35f, 0.60f, 0.85f);

    [SerializeField] private Color normalColor =
        new Color(0.55f, 0.65f, 0.75f);

    // function: Start
    // purpose: Selects the Bases category by default.
    private void Start()
    {
        showBases();
    }

    // function: showBases
    // purpose: Displays bases and highlights Bases.
    public void showBases()
    {
        setActiveCategory(true, false, false);
        updateButtonColors(basesButton);
    }

    // function: showCores
    // purpose: Displays cores and highlights Cores.
    public void showCores()
    {
        setActiveCategory(false, true, false);
        updateButtonColors(coresButton);
    }

    // function: showHeads
    // purpose: Displays heads and highlights Heads.
    public void showHeads()
    {
        setActiveCategory(false, false, true);
        updateButtonColors(headsButton);
    }

    // function: setActiveCategory
    // purpose: Shows the selected category panel.
    private void setActiveCategory(
        bool showBase,
        bool showCore,
        bool showHead)
    {
        if (basesPanel != null)
        {
            basesPanel.SetActive(showBase);
        }

        if (coresPanel != null)
        {
            coresPanel.SetActive(showCore);
        }

        if (headsPanel != null)
        {
            headsPanel.SetActive(showHead);
        }
    }

    // function: updateButtonColors
    // purpose: Highlights the selected tab and
    // resets the appearance of unselected tabs.
    private void updateButtonColors(Button selectedButton)
    {
        setButtonColor(basesButton,
            basesButton == selectedButton);

        setButtonColor(coresButton,
            coresButton == selectedButton);

        setButtonColor(headsButton,
            headsButton == selectedButton);
    }

    // function: setButtonColor
    // purpose: Updates the button's background color.
    private void setButtonColor(Button button, bool selected)
    {
        if (button == null)
        {
            return;
        }

        Color color = selected ? selectedColor : normalColor;

        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.selectedColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        button.colors = colors;

        if (button.targetGraphic != null)
        {
            button.targetGraphic.color = color;
        }
    }
}