using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch
{
    [CreateAssetMenu(menuName = "Player/Create Player Settings")]
    public class PlayerSettingData : ScriptableObject
    {
       [field: SerializeField] public float MoveSpeed;
       [field: SerializeField] public float StopPunchDelay;
    }
}
