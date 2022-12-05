using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Manasoup.UI
{
    public class LostMenu : UIScreenBase
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}