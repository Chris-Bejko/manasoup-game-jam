using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<UIScreenBase> uiScreens;

    public static event Action<UIScreenID> OnUIPanelChanged;

    private UIScreenID currentPanel;

    private UIScreenID previousPanel;

    public void ChangeScreen(UIScreenID panel)
    {
        previousPanel = currentPanel;
        currentPanel = panel;

        foreach (var e in uiScreens)
        {
            if (e.screenID == panel)
            {
                e.gameObject.SetActive(e.screenID == panel);
            }
        }

        OnUIPanelChanged?.Invoke(currentPanel);
    }

    ///for back button
    public void LoadLastPanel()
    {
        if (previousPanel != UIScreenID.None)
            ChangeScreen(previousPanel);
        else
            Debug.LogError("There is no screen previously loaded");
    }
}

public enum UIScreenID
{
    None,
    MainMenu,
    Settings,

}

public class UIScreenBase : MonoBehaviour
{
    public UIScreenID screenID;

}