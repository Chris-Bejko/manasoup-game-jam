using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Manasoup.UI
{
    public class UIManager : MonoBehaviour
    {
        public List<UIScreenBase> uiScreens;

        public static event Action<UIScreenID> OnUIPanelChanged;

        private UIScreenID currentPanel;

        private UIScreenID previousPanel;

        public void Init()
        {
            uiScreens = FindObjectsOfType<UIScreenBase>().ToList();
            ChangeScreen(UIScreenID.MainMenu);
        }
        public void ChangeScreen(UIScreenID panel)
        {
            previousPanel = currentPanel;
            currentPanel = panel;

            foreach (var e in uiScreens)
            {
                e.gameObject.SetActive(e.screenID == panel);
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
        InGameUI,
        MainMenu,
        Settings,
        LostMenu,

        //Keep it going here

    }

    public class UIScreenBase : MonoBehaviour
    {
        public UIScreenID screenID;

    }
}