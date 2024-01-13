using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace FreePunch.Screen
{
    public sealed class GameScreen : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _pauseInfoText;
        [SerializeField] private int _joystickScreenIndex;
        [SerializeField] private int _pausedScreenIndex;

        void Start()
        {
            _pauseButton.onClick.AddListener(HandlePlayButtonClick);
            SceneManager.LoadSceneAsync(_joystickScreenIndex, LoadSceneMode.Additive);
        }

        private void HandlePlayButtonClick()
        {
            SceneManager.LoadSceneAsync(_pausedScreenIndex, LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
        }
    }
}
