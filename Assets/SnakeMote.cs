using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Squish it and see what happens!
    /// </summary>
    public class SnakeMote : Mote
    {

        /// <summary>
        /// Weight constructor
        /// </summary>
        /// <param name="weight"></param>
        public SnakeMote(float weight)
            : base(weight, 250, true)
        {
        }

        /// <summary>
        /// Game play object
        /// </summary>
        /// <param name="gameObject"></param>
        public SnakeMote(GameObject gameObject, bool startOffScreen, Texture originalTexture, Texture alarmedTexture, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject)
        {
            m_worth = 250;
            m_startOffScreen = startOffScreen;
            m_textureSize = textureSize;

            // Store textures
            //
            m_originalTexture = originalTexture;
            m_alarmedTexture = alarmedTexture;

            // If we're starting off screen then calculate position and initial accel accordingly
            //
            if (m_startOffScreen)
                calculateRandomStartPositionAndVelocity(aggressiveness); // low aggressiveness
        }

        /// <summary>
        /// Position constructor
        /// </summary>
        /// <param name="position"></param>
        public SnakeMote(Vector3 position)
            : base(position)
        {
            m_worth = 250;
        }

       
        /// <summary>
        /// Override the movement
        /// </summary>
        public override void doMove()
        {
            // Handle the texture and change as necessary
            //
            if (m_inSquish)
            {
                if (Time.time > m_squishStartTime + m_squishLength)
                {
                    m_inSquish = false;
                    m_gameObject.guiTexture.texture = m_alarmedTexture;
                    m_squishStartTime = -1;
                }
            }
            else
            {
                // Reset the texture if we've just finished squishing
                if (m_squishStartTime == -1)
                {
                    m_gameObject.guiTexture.texture = m_originalTexture;
                    m_squishStartTime = 0;
                }
            }



            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;
            m_moveRect = m_gameObject.guiTexture.pixelInset;

            if (m_inSquish)
            {
                m_moveRect.x += m_alarmVector.x * normaliseMovement;
                m_moveRect.y += m_alarmVector.y * normaliseMovement;
                m_gameObject.guiTexture.pixelInset = m_moveRect;
            }
            else
            {
                //float random = Random.value;
                //if (random < 0.25f)
                //m_accel.x += m_step * normaliseMovement;
                //else if (random < 0.5f)
                //m_accel.y += m_step * normaliseMovement;
                //else if (random < 0.75f)
                //m_accel.x -= m_step * normaliseMovement;
                //else
                //m_accel.y -= m_step * normaliseMovement;

                m_moveRect.x += m_accel.x * normaliseMovement;
                m_moveRect.y += m_accel.y * normaliseMovement;
                m_gameObject.guiTexture.pixelInset = m_moveRect;
            }
        }

        /// <summary>
        /// Can we squish this mote and not have it die?
        /// </summary>
        /// <returns></returns>
        public bool isSquishable()
        {
            return (m_squishes == 0);
        }

        /// <summary>
        /// Get a vector to escape along
        /// </summary>
        /// <returns></returns>
        protected Vector2 getAlarmDirection()
        {
            Vector2 rV = new Vector2();
            float speed = 6.0f;

            float decide = Random.value;

            if (decide < 0.25f)
            {
                rV.x = speed;
                rV.y = 0;
            }
            else if (decide < 0.5f)
            {
                rV.x = -speed;
                rV.y = 0;
            }
            else if (decide < 0.75f)
            {
                rV.x = 0;
                rV.y = speed;
            }
            else
            {
                rV.x = 0;
                rV.y = -speed;
            }

            return rV;
        }


        /// <summary>
        /// Do a squish
        /// </summary>
        public void doSquish()
        {
            // Only squish if we have a squish in the bank and we're not already squishing
            //
            if (m_squishes > 0 && !m_inSquish)
            {
                m_squishStartTime = Time.time;
                m_inSquish = true;
                
                // Decrement
                //
                m_squishes--;

                // Fetch an alarm vector for this alarm
                //
                m_alarmVector = getAlarmDirection();
            }
        }

        /// <summary>
        /// Can we squish?
        /// </summary>
        /// <returns></returns>
        public bool canSquish()
        {
            return (m_squishes > 0);
        }

        /// <summary>
        /// Original texture
        /// </summary>
        protected Texture m_originalTexture;

        /// <summary>
        /// Alarmed texture
        /// </summary>
        protected Texture m_alarmedTexture;

        /// <summary>
        /// Alarm vector
        /// </summary>
        protected Vector2 m_alarmVector;


        /// <summary>
        /// Are we in a squish?
        /// </summary>
        protected bool m_inSquish = false;

        /// <summary>
        /// Length of squish
        /// </summary>
        protected float m_squishLength = 1.5f;

        /// <summary>
        /// Squish start time
        /// </summary>
        protected float m_squishStartTime = 0.0f;

        /// <summary>
        /// Acceleration step per frame
        /// </summary>
        protected float m_step = 0.1f;

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();

        /// <summary>
        /// How many squishes can we survive?
        /// </summary>
        protected int m_squishes = 1;



    }
}
