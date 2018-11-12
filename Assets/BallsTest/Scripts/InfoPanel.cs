using UnityEngine;
using UnityEngine.UI;

namespace BallsTest
{
    /// <summary>
    /// Represents UI panel with scores and timer
    /// </summary>
    internal class InfoPanel : MonoBehaviour
    {
        [SerializeField] protected Text timeLeft, scores;

        // don't use it for MonoBehaviour
        protected InfoPanel()
        {
        }

        private void Update()
        {
            timeLeft.text = GameLogic.Instance.TimeLeft.ToString("0.00");
            scores.text = GameLogic.Instance.PlayerScores.ToString();
        }
    }
}