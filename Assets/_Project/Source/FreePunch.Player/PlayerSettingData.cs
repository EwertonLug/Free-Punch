using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch
{
    [CreateAssetMenu(menuName = "Player/Create Player Settings")]
    public class PlayerSettingData : ScriptableObject
    {
        [field: Header("Back Stack Settings")]
        [field: SerializeField] public int InitMaxSlots { get; private set; }
        [field: Header("Running State Settings")]
        [field: SerializeField] public float MoveSpeed { get; private set; }

        [field:Header("Punching State Settings")]
        [field: SerializeField] public float StopPunchDelay { get; private set; }
        [field: SerializeField] public LayerMask NpcLayerMask { get; private set; }
        [field: SerializeField] public int PunchDamage { get; private set; }
    }
}
