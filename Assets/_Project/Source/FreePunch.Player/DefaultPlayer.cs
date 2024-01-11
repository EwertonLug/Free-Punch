using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public class DefaultPlayer : PlayerBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerSettingData _settings;

        public override Animator Animator => _animator;
        public override CharacterController CharacterController => _characterController;

        public override PlayerSettingData Settings => _settings;

        protected override void OnInitialized()
        {
            TransitionToState(new IdleState(this));
        }
    }
}
