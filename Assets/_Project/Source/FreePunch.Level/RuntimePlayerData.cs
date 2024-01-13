using System;
using UnityEngine;

namespace FreePunch
{
    public class RuntimePlayerData : MonoBehaviour
    {
        public int Money { get; private set; }
        public int StackSize { get; private set; }


        public void IncreaseMoney()
        {
            Money+= 10;
        }
        public void IncreasePowerUp()
        {
            StackSize++;
        }

        public void DecreaseMoney(int improveStackPrice)
        {
            Money -= improveStackPrice;
        }
    }
}