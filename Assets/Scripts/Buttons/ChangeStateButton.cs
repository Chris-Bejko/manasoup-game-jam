using UnityEngine;
using UnityEngine.UI;

namespace Manasoup.UI
{
    public class ChangeStateButton : MonoBehaviour
    {

        public GameManager.GameState stateToHandle;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            GameManager.Instance.ChangeState(stateToHandle);
        }
    }
}