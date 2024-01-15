using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FreePunch.Player
{
    public class DefaultPlayer : PlayerBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerSettingData _settings;
        [SerializeField] private Transform _forwardBoxSensor;

        private Vector3 _playerVelocity;
        public override Animator Animator => _animator;
        public override CharacterController CharacterController => _characterController;

        public override PlayerSettingData Settings => _settings;

        public override Transform FowardBoxSensor => _forwardBoxSensor;


        public override void UpdateBackStackSize(int levelNpcAmount)
        {
            BackStack.UpdateSlotsToProcess(levelNpcAmount);
        }

        public override void UpdateColor(PlayerColorType colorType)
        {
            switch (colorType)
            {
                case PlayerColorType.Orange:
                    foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        renderer.material.mainTexture = _settings.OrangeTexture;
                    }
                    Debug.Log("Change Color");
                    break;
                default:
                    break;
            }
            
        }

        protected override void OnInitialized()
        {
            TransitionToState(new IdleState(this));
        }

        protected override void OnUpdate()
        {
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!IsInitialized)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(_forwardBoxSensor.localPosition, _forwardBoxSensor.localScale);
            Handles.Label(transform.position - transform.localScale / 2, $"State: {CurrentState.Name}");
#endif
        }
    }
}
