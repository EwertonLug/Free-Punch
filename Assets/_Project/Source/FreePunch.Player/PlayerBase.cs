using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public abstract class PlayerBase : MonoBehaviour
    {
        private IPlayerState _currentState;
       
        public abstract Animator Animator { get; }
        public abstract CharacterController CharacterController { get; }
        public abstract PlayerSettingData Settings { get; }
        public IPlayerState CurrentState => _currentState;
        public PlayerInput Input { get; private set; }
        
        public void TransitionToState(IPlayerState newState)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            _currentState = newState;
            _currentState.EnterState();
        }

        public void Initialize(PlayerInput input)
        {
            Input = input;
            OnInitialized();
        }

        public void UpdateCurrentState()
        {

            if (_currentState != null)
            {
                _currentState.UpdateState();
            }

        }

        protected abstract void OnInitialized();

    }
}
