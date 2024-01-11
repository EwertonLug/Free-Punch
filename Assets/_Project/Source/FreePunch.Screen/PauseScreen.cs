using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FreePunch.Screen
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private int _pausedScreenIndex;
        void Start()
        {
            _continueButton.onClick.AddListener(HandleContinueButtonClick);
        }

        private void HandleContinueButtonClick()
        {

            SceneManager.UnloadSceneAsync(_pausedScreenIndex);

        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
        }
    }
}
