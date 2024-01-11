using UnityEngine;
using UnityEngine.InputSystem;

namespace FreePunch.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [field: SerializeField] public InputAction RunningAction { get;  private set; }
        [field: SerializeField] public InputAction PunchingAction { get; private set; }


        private void OnEnable()
        {
            RunningAction.Enable();
            PunchingAction.Enable();
        }

        private void OnDisable()
        {
            RunningAction.Disable();
            PunchingAction.Disable();
        }
    }
}
