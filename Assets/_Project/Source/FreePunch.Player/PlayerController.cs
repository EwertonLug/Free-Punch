using FreePunch.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FreePunch.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private PlayerBase _currentPlayer;

        private bool _isInitialized;

        public void Initialize(PlayerBase currentPlayer)
        {
            if (!_isInitialized)
            {
                SetupEvents();
                _currentPlayer = currentPlayer;
                _currentPlayer.Initialize(_playerInput);
                _isInitialized = true;
            }

        }

        public void PushToBackStack(NPCBase npc)
        {
            _currentPlayer.BackStack.AddNpc(npc);
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                _currentPlayer.UpdateCurrentState();

            }
        }

        private void HandlePunchingActionRequest(InputAction.CallbackContext ctx)
        {
            if (_currentPlayer.CurrentState.GetType() != typeof(PunchingState))
            {
                _currentPlayer.TransitionToState(new PunchingState(_currentPlayer));
            }
        }

        private void HandleMoveActionCancel(InputAction.CallbackContext obj)
        {
            _currentPlayer.TransitionToState(new IdleState(_currentPlayer));
        }

        private void HandleMoveActionStart(InputAction.CallbackContext ctx)
        {
            _currentPlayer.TransitionToState(new RunningState(_currentPlayer));
        }

        private void OnDestroy()
        {
            DestroyEvents();
        }

        private void SetupEvents()
        {
            _playerInput.PunchingAction.performed += HandlePunchingActionRequest;
            _playerInput.RunningAction.started += HandleMoveActionStart;
            _playerInput.RunningAction.canceled += HandleMoveActionCancel;

        }

        private void DestroyEvents()
        {
            _playerInput.PunchingAction.performed += HandlePunchingActionRequest;
            _playerInput.RunningAction.started += HandleMoveActionStart;
            _playerInput.RunningAction.canceled += HandleMoveActionCancel;
        }
    }
}
