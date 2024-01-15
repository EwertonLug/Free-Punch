using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using FreePunch.Player;

namespace FreePunch.Screen
{
    public class EndLevelScreen : MonoBehaviour
    {
        public event Action OnContinueRequested;
        public event Action OnImproveRequested;
        public event Action<PlayerColorType> OnChangeColorRequested;

        [SerializeField] private Button _nextLevelButton;
        [Header("Store")]
        [SerializeField] private TextMeshProUGUI _currentMoney;
        [SerializeField] private TextMeshProUGUI _improvePrice;
        [SerializeField] private Button _improveStackButton;
        [SerializeField] private TextMeshProUGUI _colorPrice;
        [SerializeField] private Button _colorButton;

        private PlayerColorType _colorType = PlayerColorType.Orange;

        public void Initialize()
        {
            _nextLevelButton.onClick.AddListener(()=> OnContinueRequested?.Invoke());
            _improveStackButton.onClick.AddListener(()=> { 
                OnImproveRequested?.Invoke();
            });
            _colorButton.onClick.AddListener(() => {
                OnChangeColorRequested?.Invoke(_colorType);
            });
        }

        public void Setup(RuntimePlayerData runtimePlayerData, int improvePrice)
        {
            _currentMoney.SetText(runtimePlayerData.Money.ToString());
            _improvePrice.SetText(improvePrice.ToString());
            _colorPrice.SetText(improvePrice.ToString());
            _improveStackButton.interactable = runtimePlayerData.Money >= improvePrice;
            _colorButton.interactable = runtimePlayerData.Color == PlayerColorType.Blue && runtimePlayerData.Money >= improvePrice;
        }

    }
}