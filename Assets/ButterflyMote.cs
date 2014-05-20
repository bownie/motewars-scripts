using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// A Mosquito
    /// </summary>
    public class ButterflyMote : FluffyMote
    {

        public ButterflyMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize)
            : base(gameObject, startOffScreen, textureSize)
        {
            // Slow the movement steps up
            m_step = 0.02f;
        }


        public ButterflyMote(float weight, int worth, bool offScreen)
            : base(weight, worth, offScreen)
        {
            // Slow the movement steps up
            m_step = 0.02f;
            m_worth = worth;

            // If we're starting off screen then calculate position and initial accel accordingly
            //
            if (m_startOffScreen)
                calculateRandomStartPositionAndVelocity(0.1f);
        }

        /// <summary>
        /// Set the textures
        /// </summary>
        /// <param name="openTexture"></param>
        /// <param name="closedTexture"></param>
        public void setTextures(Texture openTexture, Texture closedTexture)
        {
            /*
            if (openTexture == null)
                Debug.Log("OPEN TEXTURE NULL");

            if (closedTexture == null)
                Debug.Log("CLOSED TEXTURE NULL");
            */
            m_openTexture = openTexture;
            m_closedTexture = closedTexture;
        }

        /// <summary>
        /// Movement and other updates
        /// </summary>
        public override void doMove()
        {
            // Store the movemement rectangle
            //
            m_moveRect = m_gameObject.guiTexture.pixelInset;

            // Occasional flap - only update the textures if they've been set
            //
            if (m_closedTexture != null && m_openTexture != null && Time.time > m_flapTime)
            {
                if (Random.value < 0.1f)
                    setTexture(m_closedTexture);
                else
                    setTexture(m_openTexture);

                // Store the flap time
                //
                m_flapTime = Time.time + 0.2f + (0.5f * Random.value);
            }

            float random = Random.value;
            if (random < 0.25f)
                m_accel.x += m_step;
            else if (random < 0.5f)
                m_accel.y += m_step;
            else if (random < 0.75f)
                m_accel.x -= m_step;
            else
                m_accel.y -= m_step;

            m_moveRect.x += m_accel.x;
            m_moveRect.y += m_accel.y;
            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }


        /// <summary>
        /// Open texture
        /// </summary>
        protected Texture m_openTexture;

        /// <summary>
        /// Closed texture
        /// </summary>
        protected Texture m_closedTexture;

        /// <summary>
        /// Last flap time
        /// </summary>
        protected float m_flapTime = 0;
    }

}