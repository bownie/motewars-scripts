using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// The MoteShip is the thing we're trying to protect in this game
    /// </summary>
    public class MoteShip : MovingMote
    {
        public MoteShip(GameObject gameObject)
            : base(gameObject, MoveMethod.Figure8)
        {
            m_originalPosition = gameObject.transform.position;
            m_originalAlpha = gameObject.guiTexture.color.a;
        }

        public MoteShip(GameObject gameObject, MoveMethod method)
            : base(gameObject, method)
        {
            m_originalPosition = gameObject.transform.position;
        }

        /// <summary>
        /// Set and adjust
        /// </summary>
        /// <param name="position"></param>
        public MoteShip(int worth, Vector3 position, MoveMethod method)
            : base(worth, position, method)
        {
        }

        /// <summary>
        /// Enable death animation
        /// </summary>
        public void doDeath()
        {
            m_dying = true;
            m_deathStartTime = Time.time;
            m_deathEndTime = m_deathStartTime + 0.5f;
            m_preDeathInset = m_gameObject.guiTexture.pixelInset;
        }

        /// <summary>
        /// Get actual screen position from inset and transform
        /// </summary>
        public override Vector2 getScreenPosition()
        {
            float x = Screen.width * m_gameObject.transform.position.x + m_gameObject.guiTexture.pixelInset.x;
            float y = Screen.height * m_gameObject.transform.position.y + m_gameObject.guiTexture.pixelInset.y;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Get actual screen position from inset and transform
        /// </summary>
        public Vector2 getScreenCentrePosition()
        {
            float x = Screen.width * m_gameObject.transform.position.x + m_gameObject.guiTexture.pixelInset.x + m_gameObject.guiTexture.pixelInset.width / 2;
            float y = Screen.height * m_gameObject.transform.position.y + m_gameObject.guiTexture.pixelInset.y + m_gameObject.guiTexture.pixelInset.height / 2;
            return new Vector2(x, y);
        }


        /// <summary>
        /// Are we dying?
        /// </summary>
        /// <returns></returns>
        public bool testDying()
        {
            if (!m_dying)
                return false;

            // Reset
            //
            if (Time.time > m_deathEndTime)
            {
                m_gameObject.guiTexture.pixelInset = m_preDeathInset;
                m_dying = false;
                return false;
            }
            else
            {
                // Do death spiral
                //
                float factor = Mathf.PI * (Time.time - m_deathStartTime) / (m_deathEndTime - m_deathStartTime);
                float xFactor = 120.0f;
                float yFactor = 80.0f;
                m_gameObject.guiTexture.pixelInset = new Rect(m_preDeathInset.x - Mathf.Sin(factor) * xFactor, m_preDeathInset.y - Mathf.Sin(factor) * yFactor,
                    m_preDeathInset.width + Mathf.Sin(factor) * 2.0f * xFactor, m_preDeathInset.height + Mathf.Sin(factor) * 2.0f * yFactor);
            }
            return m_dying;
        }

        /// <summary>
        /// Original alpha
        /// </summary>
        /// <returns></returns>
        //public float getOriginalAlpha()
        //{
            //return m_originalAlpha;
        //}

        public float getDeathStartTime() { return m_deathStartTime; }

        public float getDeathEndTime() { return m_deathEndTime; }

        /// <summary>
        /// Start time for death spiral
        /// </summary>
        protected float m_deathStartTime = 0.0f;

        /// <summary>
        /// End time for death spiral
        /// </summary>
        protected float m_deathEndTime = 0.0f;

        /// <summary>
        /// Original alpha level
        /// </summary>
        //protected float m_originalAlpha = 0.5f;

        /// <summary>
        /// Dying?
        /// </summary>
        protected bool m_dying = false;

        public Vector3 getOriginalPosition() { return m_originalPosition; }
        protected Vector3 m_originalPosition;

        /// <summary>
        /// Store the inset before death
        /// </summary>
        public Rect m_preDeathInset;
    }

}