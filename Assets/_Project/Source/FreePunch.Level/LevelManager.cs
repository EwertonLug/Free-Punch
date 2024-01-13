using FreePunch.AI;
using FreePunch.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Level
{
    public class LevelManager : MonoBehaviour
    {
        public struct RuntimeProgress
        {
            public int LevelProgressTarget;
            public int LevelProgress;
        }
        public event Action<RuntimeProgress> OnLevelStarted;
        public event Action OnLevelCompleted;
        public event Action<RuntimeProgress> OnLevelUpdated;

        [SerializeField] private DiscardArea _discardArea;
        [SerializeField] private PlayerBase _currentPlayer;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private NPCManager _npcManager;
        [SerializeField] private int _improveStatckPrice;
        [SerializeField] private int _startLevelProgressTarget = 1;

        private int _levelProgress;

        public int ImproveStackPrice => _improveStatckPrice;

        public void Initialize()
        {
            _playerController.Initialize(_currentPlayer);
            _npcManager.Initialize();

            _npcManager.OnNpcDied += HandleNpcDied;
            _discardArea.OnPlayerInsideArea += HandlePlayerInsideDiscardArea;
        }
        public void StartNewLevel(int stackSize)
        {
            _levelProgress = 0;
            _startLevelProgressTarget += stackSize;
            var progress = new RuntimeProgress();
            progress.LevelProgress = _levelProgress;
            progress.LevelProgressTarget = _startLevelProgressTarget;
            _playerController.ResetPosition();
            _npcManager.Generate(_startLevelProgressTarget);
            OnLevelStarted?.Invoke(progress);
        }

        private void HandlePlayerInsideDiscardArea()
        {
            int removeCount = _playerController.EmptyBackStatck();
            _levelProgress += removeCount;
            var progress = new RuntimeProgress();
            progress.LevelProgress = _levelProgress;
            progress.LevelProgressTarget = _startLevelProgressTarget;
            OnLevelUpdated?.Invoke(progress);
            CheckIfLevelCompleted();
        }

        private void CheckIfLevelCompleted()
        {
            if (_levelProgress >= _startLevelProgressTarget)
            {
                LevelCompleted();
            }
        }

        private void LevelCompleted()
        {
            OnLevelCompleted?.Invoke();
        }

        private void Update()
        {
            _npcManager.OnUpdate();
        }

        private void FixedUpdate()
        {
            _playerController.OnFixedUpdate();
        }

        private void HandleNpcDied()
        {
            NPCBase npc = _npcManager.CreateNpc();
            _playerController.PushToBackStack(npc);
        }

        private void OnDestroy()
        {
            _npcManager.OnNpcDied -= HandleNpcDied;
            _discardArea.OnPlayerInsideArea -= HandlePlayerInsideDiscardArea;
        }

    }
}
