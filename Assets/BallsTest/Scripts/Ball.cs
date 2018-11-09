using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace BallsTest
{
    /// <summary>
    /// Represents ball's behaviour
    /// </summary>
    public class Ball : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float speed, size, speedModifier = 5f;

        ///<summary>Area where balls can flow</summary>
        protected RectTransform gameField;

        ///<summary>This object boundaries</summary>
        protected RectTransform rectTransform;

        // don't use it for MonoBehaviour
        private Ball()
        {
        }

        public void Setup(RectTransform gameField, float size, Color color, Vector2 position)
        {
            this.size = size;
            speed = 1f / size * speedModifier;

            this.gameField = gameField;

            var image = gameObject.AddComponent<Image>();
            image.color = color;

            rectTransform = GetComponent<RectTransform>();
            rectTransform.pivot = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

            rectTransform.anchoredPosition = position;
        }

        private void FixedUpdate()
        {
            if (rectTransform.anchoredPosition.y > gameField.rect.height)
                Destroy(this.gameObject);
            rectTransform.anchoredPosition += Vector2.up * speed;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Destroy(this.gameObject);
            //todo add explosion animation?            

            //May use some service locater instead of singleton
            GameLogic.Instance.AddScores(size);
        }
    }
}