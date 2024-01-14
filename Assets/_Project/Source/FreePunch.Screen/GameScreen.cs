using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using FreePunch.Level;
using System.Threading.Tasks;

namespace FreePunch.Screen
{
    public sealed class GameScreen : MonoBehaviour
    {
        public event Action OnImproveRequested;
        public event Action OnStartNewRequested;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private LevelProgressPanel _levelProgress;
        [SerializeField] private int _joystickScreenIndex;
        [SerializeField] private int _pausedScreenIndex;
        [SerializeField] private int _endLevelScreenIndex;

        private EndLevelScreen _endLevelPanel;
        private RuntimePlayerData _playerData;
        private LevelData _levelSettings;

        public void Initialize(LevelData levelSettings, RuntimePlayerData playerData)
        {
            _playerData = playerData;
            _levelSettings = levelSettings;
            _pauseButton.onClick.AddListener(HandlePlayButtonClick);
            _levelProgress.gameObject.SetActive(true);
            _levelProgress.Setup(0, levelSettings.InitNpcs);
            SceneManager.LoadSceneAsync(_joystickScreenIndex, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.buildIndex == _endLevelScreenIndex)
            {
                var rootObjects = scene.GetRootGameObjects();
                _endLevelPanel = rootObjects[0].GetComponent<EndLevelScreen>();
                _endLevelPanel.Initialize();
                _endLevelPanel.Setup(_playerData, _levelSettings.ImprovePrice);
                _endLevelPanel.OnContinueRequested += HandleContinueRequested;
                _endLevelPanel.OnImproveRequested += HandleImproveRequested;
            }
        }

        private void HandleImproveRequested()
        {
            OnImproveRequested?.Invoke();
        }

        private void HandleContinueRequested()
        {
            _endLevelPanel.OnContinueRequested -= HandleContinueRequested;
            _endLevelPanel.OnImproveRequested -= HandleImproveRequested;
            SceneManager.UnloadSceneAsync(_endLevelScreenIndex);
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

        public async void OnLevelCompletedAsync()
        {
            await SceneManager.UnloadSceneAsync(_joystickScreenIndex);
            await SceneManager.LoadSceneAsync(_endLevelScreenIndex, LoadSceneMode.Additive);

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
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }
    }
}
