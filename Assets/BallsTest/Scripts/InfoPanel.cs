using UnityEngine;
using UnityEngine.UI;

namespace BallsTest
{
    internal class InfoPanel : MonoBehaviour
    {
        [SerializeField] protected Text timeLeft, scores;

        private void Update()
        {
            timeLeft.text = GameLogic.Instance.TimeLeft.ToString("0.00");
            scores.text = GameLogic.Instance.PlayerScores.ToString();
        }
    }
}