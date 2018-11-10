
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BallsTest
{
    /// <summary>
    /// Generates balls with given properties
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    internal class BallsGenerator : MonoBehaviour
    {
        [Tooltip("In seconds")]
        [Range(0f, 10f)]
        [SerializeField] protected float ballsAppearanceDelay;

        [Range(10f, 150f)]
        [SerializeField] protected float ballMinSize, ballMaxSize;

        [SerializeField] protected GameObject ballPrefab;

        ///<summary>Part of UI canvas where balls can flow</summary>
        protected RectTransform gameField;

        // don't use it for MonoBehaviour
        private BallsGenerator()
        {
        }

        // Use this for initialization
        protected void Start()
        {
            gameField = GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases(); // make sure that BallsGenerator recalculated its size
            StartCoroutine(GenerateBallsCoroutine());
        }

        protected IEnumerator GenerateBallsCoroutine()
        {
            while (true)
            {
                var ball = Instantiate(ballPrefab, this.transform).AddComponent<Ball>();

                var ballSize = Random.Range(ballMinSize, ballMaxSize);

                // spawns balls below lower gameField border
                // (0,0) is left lower corner of gameField
                var ballPosition = new Vector2(Random.Range(0, gameField.rect.width - ballSize), -ballSize);

                ball.Setup(this.gameField, ballSize, Random.ColorHSV(), ballPosition);

                yield return new WaitForSeconds(ballsAppearanceDelay);
            }
        }
    }
}