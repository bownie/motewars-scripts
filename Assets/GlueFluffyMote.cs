using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class GlueFluffyMote : Mote
    {
        /// <summary>
        /// Default worth 50 and start off screen
        /// </summary>
        public GlueFluffyMote(float weight):base(weight, 0, true)
        {
            m_isGoodMote = true; // is a good mote!
        }

        public GlueFluffyMote(GameObject gameObject, Vector2 textureSize)
            : base(gameObject)
        {
            m_isGoodMote = true; // is a good mote!
            m_worth = 50;
            m_startOffScreen = true;
            m_textureSize = textureSize;
            
            calculateStartPosition();
        }

        /// <summary>
        /// Where this mote will start
        /// </summary>
        protected void calculateStartPosition()
        {
            m_position = new Vector2(0, Random.value * Screen.height);
            m_accel = new Vector2(2, 0.5f);
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

            // Modify acceleration to point to boss by a fraction
            //
            if (m_trackBoss)
            {
                m_accel += (m_bossPosition - getInsetCentrePosition()) * normaliseMovement / 500.0f;

                //Debug.Log("INSET x = " + getInsetCentrePosition().x + ", y = " + getInsetCentrePosition().y);
                //Debug.Log("BOSS x = " + m_bossPosition.x + ",y = " + m_bossPosition.y);
                //Debug.Log("GlueFluffyMote - accel x = " + accel.x + ", y = " + accel.y);

                // Set a max value for acceleration
                m_accel.x = Mathf.Min(m_accel.x, 3.0f);
                m_accel.y = Mathf.Min(m_accel.y, 3.0f);
            }

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_gameObject.guiTexture.pixelInset = m_moveRect;

            // Pulsate alpha
            pulsateAlpha();
            
        }

        /// <summary>
        /// Pulsate the alpha
        /// </summary>
        protected void pulsateAlpha()
        {
            Color color = m_gameObject.guiTexture.color;

            if (m_alphaRising)
            {
                if (color.a >= m_maxAlpha)
                {
                    m_alphaRising = false;
                }
                else
                {
                    color.a += m_alphaStep;
                }
            }
            else
            {
                if (color.a <= m_minAlpha)
                {
                    m_alphaRising = true;
                }
                else
                {
                    color.a -= m_alphaStep;
                }
            }

            m_gameObject.guiTexture.color = color;

        }

        /// <summary>
        /// Set the position that this mote is heading to
        /// </summary>
        /// <param name="bossPosition"></param>
        public void setBossPosition(Vector2 bossPosition)
        {
            m_bossPosition = bossPosition;
            //Debug.Log("Boss position x = " + bossPosition.x + ", y = " + bossPosition.y);
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
        /// Are we tracking the boss?
        /// </summary>
        /// <returns></returns>
        public bool isTrackingBoss()
        {
            return m_trackBoss;
        }

        /// <summary>
        /// Are we tracking the boss?
        /// </summary>
        /// <param name="trackBoss"></param>
        public void setTrackBoss(bool trackBoss)
        {
            m_trackBoss = trackBoss;
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

        /// <summary>
        /// Boss position
        /// </summary>
        protected Vector2 m_bossPosition = Vector2.zero;

        /// <summary>
        /// Are we tracking the boss position or have we given up?
        /// </summary>
        protected bool m_trackBoss = true;

        /// <summary>
        /// Min alpha
        /// </summary>
        protected float m_minAlpha = 0.2f;

        /// <summary>
        /// Max alpha
        /// </summary>
        protected float m_maxAlpha = 1.0f;

        /// <summary>
        /// Alpha step
        /// </summary>
        protected float m_alphaStep = 0.03f;

        /// <summary>
        /// Is alpha rising or falling?
        /// </summary>
        protected bool m_alphaRising = true;
    }
}

