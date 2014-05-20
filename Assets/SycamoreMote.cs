using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Sycamore flies like a boomarang
    /// </summary>
    public class SycamoreMote : Mote
    {
        /// <summary>
        /// This is meta data constructor so we shouldn't have to call the startup code
        /// </summary>
        /// <param name="position"></param>
        public SycamoreMote(float weight, float speed = 2.0f)
            : base(weight, 150, true)
        {
            // Store the speed and transfer it to any GameObject constructed instances
            m_speed = speed;
        }

        /// <summary>
        /// GameObject constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="moteship"></param>
        public SycamoreMote(GameObject gameObject, MoteShip moteship, Vector2 textureSize, float speed)
            : base(gameObject)
        {
            //Debug.Log("Generating Sycamore with speed " + speed + ", texture width = " + textureSize.x + ", height = " + textureSize.y);
            m_textureSize = textureSize;

            // Setting this gap means that we won't (or shouldn't) regenerate a sycamore mote on the same side within
            // this time limit.
            //
            m_regenerateSideGap = 0.5f;
            calculateRandomStartSideCenter(speed);
        }

        /// <summary>
        /// Calculate an ellipse arc through or near the mote ship
        /// </summary>
        public void initialSetup(MoteShip moteShip)
        {
            if (m_gameObject.guiTexture == null)
                return;

            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_gameObject.guiTexture.pixelInset;

            //Debug.Log("ACCEL x = " + m_accel.x + ", y = " + m_accel.y);

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();

        /// <summary>
        /// Doing the move from the left or the right
        /// </summary>
        public override void doMove()
        {
            m_moveRect = m_gameObject.guiTexture.pixelInset;

            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_gameObject.guiTexture.pixelInset;

            //Debug.Log("ACCEL x = " + m_accel.x + ", y = " + m_accel.y);

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;

            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Fetch speed
        /// </summary>
        /// <returns></returns>
        public float getSpeed()
        {
            return m_speed;
        }

        /// <summary>
        /// Acceleration step per frame slightly randomised
        /// </summary>
        protected float m_step = 1;

        /// <summary>
        /// Speed of AcornMote
        /// </summary>
        protected float m_speed = 1.0f;
    }
}