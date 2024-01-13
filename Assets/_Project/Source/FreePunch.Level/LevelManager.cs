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
        
        [SerializeField] private PlayerBase _currentPlayer;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private NPCManager _npcManager;

        private void Start()
        {
            _playerController.Initialize(_currentPlayer);
            _npcManager.Initialize();

            _npcManager.OnNpcDied += HandleNpcDied;
        }

        private void HandleNpcDied()
        {
            NPCBase npc = _npcManager.CreateNpc();
            _playerController.PushToBackStack(npc);
        }

        private void OnDestroy()
        {
            _npcManager.OnNpcDied -= HandleNpcDied;
        }

    }
}
