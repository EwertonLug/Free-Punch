using FreePunch.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerBase _currentPlayer;
        [SerializeField] private PlayerController _playerController;

        private void Start()
        {
            _playerController.Initialize(_currentPlayer);
        }

    }
}
