using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// A Mosquito
    /// </summary>
    public class AcornMote : Mote
    {
        /// <summary>
        /// Acorn mote level constructor
        /// </summary>
        /// <param name="weight"></param>
        public AcornMote(float weight)
            : base(weight, 150, true)
        {
        }

        /// <summary>
        /// Acorn mote level constructor with side
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="side"></param>
        public AcornMote(float weight, MoteStartSide side)
            : base(weight, 150, true)
        {
            m_startSide = side;
        }

        public AcornMote(GameObject gameObject, Vector2 textureSize)
            : base(gameObject)
        {
            m_worth = 150;
            m_textureSize = textureSize;
            calculateRandomStartSide();
        }

        public AcornMote(GameObject gameObject, Vector2 textureSize, MoteStartSide side)
            : base(gameObject)
        {
            m_worth = 150;
            m_textureSize = textureSize;
            calculateStartFromSide(side);
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
			float normaliseMovement = 60.0f * Time.smoothDeltaTime;
			
            if (m_startSide == MoteStartSide.Left)
            {
                m_moveRect.x += normaliseMovement * m_step;
                m_moveRect.y = (Screen.height / 2) + m_wobbleHeight * Mathf.Cos(m_randomAngle + m_randomPeriod * m_moveRect.x / (Screen.width / 4));

            }
            else // right
            {
                m_moveRect.x -= normaliseMovement * m_step;
                m_moveRect.y = (Screen.height / 2) + m_wobbleHeight * Mathf.Cos(m_randomAngle + m_randomPeriod * m_moveRect.x / (Screen.width / 4));
            }

            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Acceleration step per frame slightly randomised
        /// </summary>
        protected float m_step = 1 + Random.value * 1.0f;

        /// <summary>
        /// Height of the wobble
        /// </summary>
        protected float m_wobbleHeight = 60.0f + 40.0f * Random.value;

        /// <summary>
        /// Random period
        /// </summary>
        protected float m_randomPeriod = Mathf.PI + Mathf.PI * Random.value;

        /// <summary>
        /// Start with a random angle assigned to the offset
        /// </summary>
        protected float m_randomAngle = Random.value * Mathf.PI * 2.0f;

    }

}