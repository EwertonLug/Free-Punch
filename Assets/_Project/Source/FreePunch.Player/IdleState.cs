namespace FreePunch.Player
{
    public sealed class IdleState : IPlayerState
    {
        public const string AnimationTrigger = "Idle";

        private PlayerBase _sourcePlayer;
        public string Name => nameof(IdleState);

        public IdleState(PlayerBase sourcePlayer)
        {
            _sourcePlayer = sourcePlayer;
        }

        public void EnterState()
        {
            _sourcePlayer.Animator.SetTrigger(AnimationTrigger);
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
          
        }
    }
}