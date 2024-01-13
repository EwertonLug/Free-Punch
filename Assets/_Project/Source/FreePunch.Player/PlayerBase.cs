using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public abstract class PlayerBase : MonoBehaviour
    {
        [field: SerializeField] public PlayerBackStack BackStack { get; private set; }

        public abstract Animator Animator { get; }
        public abstract CharacterController CharacterController { get; }
        public abstract PlayerSettingData Settings { get; }
        public abstract Transform FowardBoxSensor { get; }
        public IPlayerState CurrentState { get; private set; }
        public PlayerInput Input { get; private set; }
        public bool IsInitialized { get; private set; }
       

        public void TransitionToState(IPlayerState newState)
        {
            if (!IsInitialized)
            {
                return;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }

            newState.EnterState();
            CurrentState = newState;
        }

        public void Initialize(PlayerInput input)
        {
            if (!IsInitialized)
            {
                Input = input;
                IsInitialized = true;
                BackStack.Setup(Settings.InitMaxSlots);
                OnInitialized();
            }
        }

        public void UpdateCurrentState()
        {
            if (!IsInitialized)
            {
                return;
            }
            if (CurrentState != null)
            {
                CurrentState.UpdateState();
            }
            BackStack.OnUpdate();
            OnUpdate();
        }

        protected abstract void OnInitialized();
        protected abstract void OnUpdate();

    }
}
