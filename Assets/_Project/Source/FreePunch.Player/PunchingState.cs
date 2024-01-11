using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public class PunchingState : IPlayerState
    {
        public const string AnimationTrigger = "Punching";
        private PlayerBase _playerBase;

        private DateTime _onEnterTime;

        public PunchingState(PlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public void EnterState()
        {
            _playerBase.Animator.SetTrigger(AnimationTrigger);
            _onEnterTime = DateTime.UtcNow;
        }

        public void ExitState()
        {
           
        }

        public void UpdateState()
        {
            if (ShouldStop())
            {
                _playerBase.TransitionToState(new IdleState(_playerBase));
            }
        }

        public bool ShouldStop()
        {
            TimeSpan diff = DateTime.UtcNow - _onEnterTime;
            return diff.Seconds > _playerBase.Settings.StopPunchDelay;
        }
    }
}
