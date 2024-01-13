using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

namespace FreePunch.Screen
{
    public sealed class GameScreen : MonoBehaviour
    {
        public event Action OnImproveRequested;
        public event Action OnStartNewRequested;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private EndLevelPanel _endLevelPanel;
        [SerializeField] private LevelProgressPanel _levelProgress;
        [SerializeField] private int _joystickScreenIndex;
        [SerializeField] private int _pausedScreenIndex;

        public void Initialize()
        {
            _endLevelPanel.Initialize();
            _pauseButton.onClick.AddListener(HandlePlayButtonClick);
            _endLevelPanel.OnContinueRequested += HandleContinueRequested;
            _endLevelPanel.OnImproveRequested += HandleImproveRequested;
            _levelProgress.gameObject.SetActive(true);
            _levelProgress.Setup(0, 1);
            SceneManager.LoadSceneAsync(_joystickScreenIndex, LoadSceneMode.Additive);
        }

        private void HandleImproveRequested()
        {
            OnImproveRequested?.Invoke();
        }

        private void HandleContinueRequested()
        {
            _endLevelPanel.gameObject.SetActive(false);
            OnStartNewRequested?.Invoke();
        }

        public void OnLevelStarted(Level.LevelManager.RuntimeProgress progress)
        {
            _pauseButton.gameObject.SetActive(true);
            _levelProgress.gameObject.SetActive(true);
            _levelProgress.Setup(progress.LevelProgress, progress.LevelProgressTarget);
            SceneManager.LoadSceneAsync(_joystickScreenIndex, LoadSceneMode.Additive);
        }

        public void RefresEndLevelPanel(RuntimePlayerData _playerMoney, int improveStackPrice)
        {
            _endLevelPanel.Setup(_playerMoney, improveStackPrice);
        }

        public void OnLevelProgressUpdated(Level.LevelManager.RuntimeProgress progress)
        {
            _levelProgress.gameObject.SetActive(true);
            _levelProgress.Setup(progress.LevelProgress, progress.LevelProgressTarget);
        }

        public void OnLevelCompleted(RuntimePlayerData _playerMoney, int improveStackPrice)
        {
            SceneManager.UnloadSceneAsync(_joystickScreenIndex);
            _endLevelPanel.gameObject.SetActive(true);
            _endLevelPanel.Setup(_playerMoney, improveStackPrice);
            _pauseButton.gameObject.SetActive(false);
            _levelProgress.gameObject.SetActive(false);
        }

        private void HandlePlayButtonClick()
        {
            SceneManager.LoadSceneAsync(_pausedScreenIndex, LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _endLevelPanel.OnContinueRequested -= HandleContinueRequested;
            _endLevelPanel.OnImproveRequested -= HandleImproveRequested;
        }
    }
}
