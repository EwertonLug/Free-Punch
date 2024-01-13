using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FreePunch.Screen
{
    public class EndLevelPanel : MonoBehaviour
    {
        public event Action OnContinueRequested;
        public event Action OnImproveRequested;

        [SerializeField] private TextMeshProUGUI _currentMoney;
        [SerializeField] private TextMeshProUGUI _improvePrice;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _improveStackButton;

        public void Initialize()
        {
            _nextLevelButton.onClick.AddListener(()=> OnContinueRequested?.Invoke());
            _improveStackButton.onClick.AddListener(()=> { OnImproveRequested?.Invoke();
            });
        }
        public void Setup(RuntimePlayerData runtimePlayerData, int improveStackPrice)
        {
            _currentMoney.SetText(runtimePlayerData.Money.ToString());
            _improvePrice.SetText(improveStackPrice.ToString());
            _improveStackButton.interactable = runtimePlayerData.Money >= improveStackPrice;
        }
    }
}