using UnityEngine;
using UnityEngine.UI;

namespace BallsTest
{
    /// <summary>
    /// Represents ball's behaviour
    /// </summary>
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float size, speed;
        public void Setup(float size, Color color, Vector2 position)
        {
            
            this.size = size;
            var image = gameObject.AddComponent<Image>();
            image.color = color;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.pivot = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

            rectTransform.anchoredPosition = position;
        }

        private void FixedUpdate()
        {
            //todo if outside window - destroy
            //Destroy(this);           
        }
    }
}