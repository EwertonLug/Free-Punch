using FreePunch.Level;
using FreePunch.Screen;
using System;
using UnityEngine;

namespace FreePunch
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RuntimePlayerData _playerData;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameScreen _gameScreen;

        private void Start()
        {
            _levelManager.Initialize();
            _gameScreen.Initialize();
            _levelManager.OnLevelCompleted += HandleLevelCompleted;
            _levelManager.OnLevelUpdated += HandleLevelUpdated;
            _levelManager.OnLevelStarted += HandleLevelStarted;
            _gameScreen.OnStartNewRequested += HandleStartNewLevelRequest;
            _gameScreen.OnImproveRequested += HandleImproveRequest;
        }

        private void HandleImproveRequest()
        {
            _playerData.IncreasePowerUp();
            _playerData.DecreaseMoney(_levelManager.ImproveStackPrice);
            _gameScreen.RefresEndLevelPanel(_playerData, _levelManager.ImproveStackPrice);
        }

        private void HandleLevelStarted(LevelManager.RuntimeProgress progress)
        {
            _gameScreen.OnLevelStarted(progress);
        }

        private void HandleLevelUpdated(LevelManager.RuntimeProgress progress)
        {
            _gameScreen.OnLevelProgressUpdated(progress);
        }

        private void HandleStartNewLevelRequest()
        {
            _levelManager.StartNewLevel(_playerData.StackSize);
        }

        private void HandleLevelCompleted()
        {
            _playerData.IncreaseMoney();
            _gameScreen.OnLevelCompleted(_playerData, _levelManager.ImproveStackPrice);
            
        }

        private void OnDestroy()
        {
            _levelManager.OnLevelCompleted -= HandleLevelCompleted;
            _levelManager.OnLevelUpdated -= HandleLevelUpdated;
            _levelManager.OnLevelStarted -= HandleLevelStarted;
            _gameScreen.OnStartNewRequested -= HandleStartNewLevelRequest;
            _gameScreen.OnImproveRequested -= HandleImproveRequest;
        }
    }
}
