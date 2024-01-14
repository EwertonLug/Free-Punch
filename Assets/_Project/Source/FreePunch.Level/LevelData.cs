using UnityEngine;

namespace FreePunch.Level
{
    [CreateAssetMenu(menuName = "FreePunch/Settings/Create Level Settings")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public int InitNpcs { get; private set; }
        [field: SerializeField] public int MoneyReward { get; private set; }
        [field: SerializeField] public int ImprovePrice { get; private set; }
        [field: SerializeField] public int MaxNPCs { get; private set; }
    }
}
