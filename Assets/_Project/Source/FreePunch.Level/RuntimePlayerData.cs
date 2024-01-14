using FreePunch.Level;
using System;
using UnityEngine;

namespace FreePunch
{
    public class RuntimePlayerData : MonoBehaviour
    {
        private LevelData _levelSettings;
        public int Money { get; private set; }
        public int StackSize { get; private set; }

        public void IncreaseMoney()
        {
            Money+= _levelSettings.MoneyReward;
        }
        public void IncreasePowerUp()
        {
            StackSize++;
        }

        public void DecreaseMoney()
        {
            Money -= _levelSettings.ImprovePrice;
        }

        public void Initialize(LevelData levelSettings)
        {
            _levelSettings = levelSettings;
        }
    }
}