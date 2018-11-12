
using UnityEngine;
using UnityEngine.UI;

namespace BallsTest
{
    /// <summary>
    /// Implements simple modal window with given text. Can be only 1 instance. Overwrites text on second call.
    /// </summary>
    public class ModalWindow : Hideable
    {
        [SerializeField]
        protected Text generalText;

        public static ModalWindow Instance { get; protected set; }

        // don't use it for MonoBehaviour
        protected ModalWindow()
        {
        }

        protected void Awake()
        {
            //singleton pattern
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.Log("Singleton object already created. Exterminating " + this);
                Destroy(this);
            }
            Hide();
        }

        /// <summary>
        /// Overwrites previous modal window if it was open
        /// </summary>    
        internal static void Show(string text)
        {
            Instance.generalText.text = text;            
            Instance.transform.SetAsLastSibling();
            Instance.Show();
        }
    }    
}