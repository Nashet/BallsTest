using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace BallsTest
{
    /// <summary>
    /// Represents ball's behaviour
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class Ball : MonoBehaviour, IPointerDownHandler
    {
        protected float size;

        protected float Speed { get { return 1f / size * GameLogic.Instance.SpeedModifier; } }

        ///<summary>Area where balls can flow</summary>
        protected RectTransform gameField;

        ///<summary>This object boundaries</summary>
        protected RectTransform rectTransform;

        protected bool isSetupDone;

        // don't use it for MonoBehaviour
        protected Ball()
        {
        }

        /// <summary>
        /// Initialize Ball. After that method ball will start living
        /// </summary>        
        public void Setup(RectTransform gameField, float size, Color color, Vector2 position)
        {
            this.size = size;
            this.gameField = gameField;

            var image = gameObject.GetComponent<Image>();
            image.color = color;

            rectTransform = GetComponent<RectTransform>();

            rectTransform.pivot = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            rectTransform.anchoredPosition = position;

            isSetupDone = true;
        }

        private void FixedUpdate()
        {
            if (isSetupDone)
            {
                // if get outside upper border of gameField
                if (rectTransform.anchoredPosition.y > gameField.rect.height)
                    Destroy(this.gameObject);
                rectTransform.anchoredPosition += Vector2.up * Speed;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isSetupDone)
            {                
                GameLogic.Instance.AddScores(size);

                Destroy(this.gameObject);
                //todo add explosion animation?       
            }
        }
    }
}