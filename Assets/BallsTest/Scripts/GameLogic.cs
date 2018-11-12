using System;
using UnityEngine;

namespace BallsTest
{
    internal class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance { get; internal set; }

        [Tooltip("Linear modifier")] [SerializeField] protected float scoresModifier = 5f;
        [Tooltip("Linear modifier")] [SerializeField] protected float speedModifier = 5f;

        [Range(0f, 1000f)]
        [Tooltip("In seconds")] [SerializeField] protected float roundDurationTime;

        internal float SpeedModifier { get { return speedModifier * TimeFromRoundStart; } }

        protected float roundStartedTime;

        public int PlayerScores { get; internal set; }

        internal float TimeFromRoundStart { get { return Time.time - roundStartedTime; } }

        internal float TimeLeft
        {
            get
            {
                var time = roundDurationTime - TimeFromRoundStart;
                return time > 0f ? time : 0f;
            }
        }

        // don't use it for MonoBehaviour
        protected GameLogic()
        {
        }

        protected void Awake()
        {
            //singleton pattern
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.Log("Game logic already created. Exterminating...");
                Destroy(this);
            }

            PrepareNewRound();
        }

        public void PrepareNewRound()
        {
            roundStartedTime = Time.time;
            PlayerScores = 0;
        }

        internal void AddScores(float size)
        {
            PlayerScores += (int)Math.Round(1f / size * scoresModifier);
        }

        protected void OnTimeLeft()
        {
            ModalWindow.Show("Game is over\n You achieved " + PlayerScores + " Scores");            
        }

        protected void FixedUpdate()
        {
            if (TimeLeft == 0f)
                OnTimeLeft();
        }
    }
}