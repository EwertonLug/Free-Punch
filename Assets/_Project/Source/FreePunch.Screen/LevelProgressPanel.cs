using UnityEngine;
using TMPro;

namespace FreePunch
{
    public class LevelProgressPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelProgresssText;

        public void Setup(int progress, int targetValue)
        {
            _levelProgresssText.SetText($"{progress}/{targetValue}" );
        }
    }
}
