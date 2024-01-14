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
        [SerializeField] private LevelData _levelSettings;
        [SerializeField] private DiscardArea _discardArea;
        [SerializeField] private PlayerBase _currentPlayer;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private NPCManager _npcManager;


        private int _discardedNpcs;
        private int _levelNpcsAmount;
        public LevelData LevelSettings => _levelSettings;

        public void Initialize()
        {
            _playerController.Initialize(_currentPlayer);
            _npcManager.Initialize(_levelSettings.InitNpcs);
            _npcManager.OnNpcDied += HandleNpcDied;
            _discardArea.OnPlayerInsideArea += HandlePlayerInsideDiscardArea;
            _levelNpcsAmount = _levelSettings.InitNpcs;
        }
        public void StartNewLevel(int playerStackModifier)
        {
            _discardedNpcs = 0;
            _levelNpcsAmount = Mathf.Clamp(_levelSettings.InitNpcs + playerStackModifier, 0, _levelSettings.MaxNPCs);
            var progress = new RuntimeProgress();
            progress.LevelProgress = _discardedNpcs;
            progress.LevelProgressTarget = _levelNpcsAmount;
            _playerController.OnStartNewLevel(_levelNpcsAmount);
            _npcManager.OnStartNewLevel(_levelNpcsAmount);
            OnLevelStarted?.Invoke(progress);
        }

        private void HandlePlayerInsideDiscardArea()
        {
            var discardedNpcs = _playerController.ClearBackStatck();
            _discardedNpcs += discardedNpcs.Count;
            var progress = new RuntimeProgress();
            progress.LevelProgress = _discardedNpcs;
            progress.LevelProgressTarget = _levelNpcsAmount;
            _npcManager.CacheDistardedNpcs(discardedNpcs);
            OnLevelUpdated?.Invoke(progress);
            CheckIfLevelCompleted();
        }

        private void CheckIfLevelCompleted()
        {
            if (_discardedNpcs >= _levelNpcsAmount)
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
