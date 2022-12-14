using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Manasoup.UI
{
    public class UIManager : MonoBehaviour
    {
        public List<UIScreenBase> uiScreens;

        public static event Action<UIScreenID> OnUIPanelChanged;

        public GameObject fadePanel;

        private UIScreenID currentPanel;

        private UIScreenID previousPanel;

        [SerializeField]
        AudioSource UISource;
        
        public void Init()
        {
            InitButtonUISFX();
            uiScreens = FindObjectsOfType<UIScreenBase>().ToList();
            ChangeScreen(UIScreenID.MainMenu);
        }

        private void InitButtonUISFX()
        {
            List<Button> allUIButtons = new List<Button>();
            foreach (var e in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
            {
                allUIButtons.Add(e);
            }
            foreach (var e in allUIButtons)
            {
                e.onClick.AddListener(PlayButtonAudio);
            }
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

        public void FadePanel()
        {
            StartCoroutine(FadeToBlack());
        }

        private IEnumerator FadeToBlack()
        {
            float fadeSpeed = 1f;
            Color objectColor = fadePanel.GetComponent<Image>().color;
            float fadeAmount;
            while (fadePanel.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadePanel.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }

        public void PlayButtonAudio()
        {
            UISource.Play();
        }

        public void SetUIVolume(float volume)
        {
            UISource.volume = volume;
        }
    }

    public enum UIScreenID
    {
        None,
        InGameUI,
        MainMenu,
        Settings,
        LostMenu,
        WinMenu
        //Keep it going here

    }

    public class UIScreenBase : MonoBehaviour
    {
        public UIScreenID screenID;

    }
}