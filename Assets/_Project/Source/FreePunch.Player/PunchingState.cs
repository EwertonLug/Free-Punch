using FreePunch.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public sealed class PunchingState : IPlayerState
    {
        public const string AnimationTrigger = "Punching";
        private PlayerBase _playerBase;

        private DateTime _onEnterTime;

        public string Name => nameof(PunchingState);

        public PunchingState(PlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public void EnterState()
        {
            _playerBase.Animator.SetTrigger(AnimationTrigger);
            _onEnterTime = DateTime.UtcNow;
            Collider[] hitColliders = Physics.OverlapBox(_playerBase.FowardBoxSensor.position, _playerBase.FowardBoxSensor.localScale / 2, Quaternion.identity, _playerBase.Settings.NpcLayerMask);
            
            foreach (Collider npc in hitColliders)
            {
                NPCBase npcBase = npc.GetComponent<NPCBase>();

                if (npcBase != null)
                {
                    npcBase.TakeDamage(_playerBase.Settings.PunchDamage);
                }
            }

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
