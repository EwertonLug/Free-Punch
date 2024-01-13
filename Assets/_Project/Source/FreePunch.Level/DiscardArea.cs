using System;
using UnityEngine;

namespace FreePunch.Level
{
    public class DiscardArea : MonoBehaviour
    {
        public event Action OnPlayerInsideArea;

        [SerializeField] private string _playerTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                OnPlayerInsideArea?.Invoke();
                Debug.Log("Playe Inside Area");
            }
        }

    }
}