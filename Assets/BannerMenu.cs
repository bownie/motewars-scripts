using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Banner that can slide in and bounce - the GameObject that is assigned to this script must already have a GUIText and
    /// a GUITexture assigned to it.
    /// </summary>
    public class SlideBanner
    {
        /// <summary>
        /// Construct with a new GameObject
        /// </summary>
        public SlideBanner(GameObject gameObject)
        {
            m_menuObject = gameObject;
            initialize();
        }

        /// <summary>
        /// Containing GameObject
        /// </summary>
        protected GameObject m_menuObject;

        /// <summary>
        /// Menu final position
        /// </summary>
        protected Vector3 m_finalPosition = new Vector3(0, 0, 0);

        /// <summary>
        /// Menu start position
        /// </summary>
        protected Vector3 m_startPosition = new Vector3(0, -300, 0);

        /// <summary>
        /// Menu text
        /// </summary>
        protected string m_menuTitle;

        /// <summary>
        /// Body text
        /// </summary>
        protected string m_menuBody;

        /// <summary>
        /// Time for the menu to settle to a standstill
        /// </summary>
        protected float m_timeToComplete = 2.0f;

        /// <summary>
        /// Start time for an animation
        /// </summary>
        protected float m_startTime = 0.0f;

        /// <summary>
        /// Does this menu bounce to a stop?
        /// </summary>
        protected bool m_bounce = true;

        protected void initialize()
        {
            if (m_menuObject == null)
            {
                Debug.Log("Please assign a valid GameObject to this class.");
                return;
            }

            // Set initial position
            //
            m_startPosition = Vector3.zero;
            m_finalPosition = Vector3.zero;

            /*
            m_menuObject.transform.localScale = Vector3.zero; // new Vector3(widthMulti, heightMulti, 0.0f);
            m_menuObject.guiTexture.pixelInset = new Rect(0, 0, 0, 0);
            m_menuObject.guiTexture.transform.position = new Vector3(-1, -1 ,0);
            m_menuObject.guiTexture.transform.localScale = Vector3.zero;
            */
            //m_menuObject.AddComponent("GUIText");

            //setActive(false);
        }

        /// <summary>
        /// Play the slide menu
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="startTime"></param>
        public void playMenu(string title, string body, float startTime)
        {
            m_menuTitle = title;
            m_menuBody = body;
            m_startTime = startTime;
        }

        /// <summary>
        /// Set the active state of this menu
        /// </summary>
        /// <param name="active"></param>
        public void setActive(bool active)
        {
            m_menuObject.SetActive(active);

            if (active)
            {
                m_menuObject.transform.position = m_startPosition;
            }
            else
            {
                m_menuObject.transform.position = new Vector3(-1000, -1000, 0);
            }
        }


        /// <summary>
        /// Perform an update as necessary
        /// </summary>
        public void update(float currentTime)
        {
            // No update if not active and outside of update time
            //
            if (m_menuObject.activeInHierarchy == false || currentTime < m_startTime || currentTime > m_startTime + m_timeToComplete)
                return;


            Debug.Log("UPDATING BANNERMENU");
            
        }

    }
}
