using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public class RunningState : IPlayerState
    {
        public const string AnimationTrigger = "Running";
        private PlayerBase _playerBase;
        private Vector3 _playerVelocity;
        public RunningState(PlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public void EnterState()
        {
            _playerBase.Animator.SetTrigger(AnimationTrigger);
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            Vector2 moveAmount = _playerBase.Input.RunningAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(moveAmount.x, 0, moveAmount.y);

            if (move != Vector3.zero)
            {
                _playerBase.CharacterController.transform.forward = move;
            }

            _playerBase.CharacterController.Move(_playerBase.Settings.MoveSpeed * Time.deltaTime * move);

            if (_playerBase.CharacterController.isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            _playerBase.CharacterController.Move(_playerVelocity * Time.deltaTime);

        }
    }
}
