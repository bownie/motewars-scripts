using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// A ChickMote pops out of an Egg
    /// </summary>
    public class ChickMote : Mote
    {
        public ChickMote(GameObject gameObject)
            : base(gameObject)
        {
            m_worth = 250;
        }

        public ChickMote(Vector3 position)
            : base(position)
        {
            m_worth = 250;
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



        /// <summary>
        /// Acceleration step per frame
        /// </summary>
        protected float m_step = 0.1f;

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();
    }
}
