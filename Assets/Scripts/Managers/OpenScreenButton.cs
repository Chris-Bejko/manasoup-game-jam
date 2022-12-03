using Manasoup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manasoup.UI
{
    public class OpenScreenButton : MonoBehaviour
    {

        public UIScreenID screenToOpen;

        public bool isBackButton;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (isBackButton)
                GameManager.Instance.uIManager.LoadLastPanel();
            else
                GameManager.Instance.uIManager.ChangeScreen(screenToOpen);
        }
    }
}