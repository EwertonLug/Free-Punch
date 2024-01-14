using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace FreePunch.Screen
{
    public class FPSView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private float _fpsCounterRate;

        private void Start()
        {
            StartCoroutine(CountFps());
        }

        private IEnumerator CountFps()
        {
            while (true)
            {
                yield return new WaitForSeconds(_fpsCounterRate);
                float fps = 1f / Time.deltaTime;
                _fpsText.SetText(Mathf.Floor(fps).ToString());
            }
        }
    }
}
