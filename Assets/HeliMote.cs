using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class HeliMote : Mote
    {
        /// <summary>
        /// Default worth 50 and start off screen
        /// </summary>
        public HeliMote(float weight):base(weight, 50, true)
        {
        }

        public HeliMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject)
        {
            m_worth = 50;
            m_startOffScreen = startOffScreen;
            m_textureSize = textureSize;

            // If we're starting off screen then calculate position and initial accel accordingly
            //
            if (m_startOffScreen)
                calculateRandomStartPositionAndVelocity(aggressiveness); // low aggressiveness
        }

        /// <summary>
        /// Set and adjust allowing for m_texture sizing from base class (must be assigned)
        /// </summary>
        /// <param name="position"></param>
        public HeliMote(Vector3 position)
            : base(position)
        {
            m_worth = 50;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialVelocity"></param>
        /// <param name="offScreen"></param>
        /// <param name="side"></param>
        public HeliMote(float weight, int worth, bool offScreen):base(weight, worth, offScreen)
        {
        }

        /// <summary>
        /// Override the movement
        /// </summary>
        public override void doMove()
        {
            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_gameObject.guiTexture.pixelInset;

            float random = Random.value;
            if (random < 0.25f)
                m_accel.x += m_step * normaliseMovement;
            else if (random < 0.5f)
                m_accel.y += m_step * normaliseMovement;
            else if (random < 0.75f)
                m_accel.x -= m_step * normaliseMovement;
            else
                m_accel.y -= m_step * normaliseMovement;

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Start off screen?
        /// </summary>
        /// <returns></returns>
        public bool startOffScreen()
        {
            return m_startOffScreen;
        }

        /// <summary>
        /// Initial velocity
        /// </summary>
        /// <returns></returns>
        public Vector2 initialVelocity()
        {
            return m_initialVelocity;
        }


        /// <summary>
        /// Acceleration step per frame
        /// </summary>
        protected float m_step = 0.1f;

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();

        /// <summary>
        /// Any initial velocity
        /// </summary>
        protected Vector2 m_initialVelocity = Vector2.zero;
    }
}
