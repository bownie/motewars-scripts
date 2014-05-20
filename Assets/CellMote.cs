using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Cell mote state
    /// </summary>
    public enum CellMoteState
    {
        Question,
        Opened
    };
    
    /// <summary>
    /// Prize inside the Cell
    /// </summary>
    public enum CellPrize
    {
        Shield,
        Grenade,
        Laser,
        TimeBomb
    };

    /// <summary>
    /// CellSide that it comes o
    /// </summary>
    public enum CellSide
    {
        None,
        Left,
        Right,
        Auto
    }

    /// <summary>
    /// A cell mote holds a power up and needs to be opened in a particular way
    /// </summary>
    public class CellMote : Mote
    {
        /// <summary>
        /// Weight constructor
        /// </summary>
        /// <param name="weight"></param>
        public CellMote(float weight, CellPrize prize, CellSide side, bool straightMovement = false)
            : base(weight, 0, true)
        {
            m_prize = prize;
            m_side = side;
            m_straightMovement = straightMovement;
			m_isGoodMote = true;
			
			//Debug.Log("CellMote level constructor");
			
            setAliveLimit(1); // only one of these allowed alive at any one time
        }
        
        /// <summary>
        /// GameObject constructor
        /// </summary>
        /// <param name="gameObject"></param>
        public CellMote(GameObject gameObject, Vector2 textureSize, CellPrize prize, CellSide side, bool straightMovement = false)
            : base(gameObject)
        {
			m_side = side;
			m_prize = prize;
            m_worth = 100;
            m_textureSize = textureSize;
            m_straightMovement = straightMovement;
			m_isGoodMote = true;
			
			Debug.Log("CellMote GameObject constructor");
            // Starting side
            calculateSide();

            setAliveLimit(1); // only one of these allowed alive at any one time
        }

        protected void calculateSide()
        {
            if (m_side == CellSide.Auto)
            {
                calculateRandomStartSide();
				
				Debug.Log("Calculating randomside");

            }
            else if (m_side == CellSide.Left)
            {
                m_startSide = MoteStartSide.Left;
                m_position.x = -m_textureSize.x * 1.5f;
                m_position.y = Screen.height / 2;
                m_accel.x = 0;
                m_accel.y = 0;
				
				Debug.Log("CellSide.Left");
            }
            else if (m_side == CellSide.Right)
            {
                m_startSide = MoteStartSide.Right;
                m_position.x = Screen.width + 5;
                m_position.y = Screen.height / 2;
                m_accel.x = 0;
                m_accel.y = 0;
				Debug.Log("CellSide.Right");
            }
			
			
        }

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();

        /// <summary>
        /// Is movement just straight?
        /// </summary>
        /// <returns></returns>
        public bool getStraightMovement()
        {
            return m_straightMovement;
        }

        /// <summary>
        /// Doing the move from the left or the right
        /// </summary>
        public override void doMove()
        {
			// Normalise
			//
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;
			
            m_moveRect = m_gameObject.guiTexture.pixelInset;

            if (m_startSide == MoteStartSide.Left)
            {
                /*
                float random = Random.value;
                if (random < 0.25f)
                    m_accel.x += m_step;
                else if (random < 0.5f)
                    m_accel.y += m_step;
                else if (random < 0.75f)
                    m_accel.x -= m_step;
                else
                    m_accel.y -= m_step;
                */


                m_moveRect.x += m_step * normaliseMovement;

                if (!m_straightMovement)
                    m_moveRect.y = (Screen.height / 2) + m_wobbleHeight * Mathf.Cos(2 * Mathf.PI * m_moveRect.x / (Screen.width / 4));
                
            }
            else // right
            {
                m_moveRect.x -= m_step * normaliseMovement;

                if (!m_straightMovement)
                    m_moveRect.y = (Screen.height / 2) + m_wobbleHeight * Mathf.Cos(2 * Mathf.PI * m_moveRect.x / (Screen.width / 4));
            }

            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Get the cell state
        /// </summary>
        /// <returns></returns>
        public CellMoteState getCellState()
        {
            return m_cellState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void setCellState(CellMoteState state)
        {
            m_cellState = state;
        }

        /// <summary>
        /// Cell prize
        /// </summary>
        /// <returns></returns>
        public CellPrize getPrize()
        {
            return m_prize;
        }
		
		/// <summary>
		/// Gets the side.
		/// </summary>
		/// <returns>
		/// The side.
		/// </returns>
		public CellSide getSide()
		{
			return m_side;
		}

        /// <summary>
        /// Acceleration step per frame
        /// </summary>
        protected float m_step = 2f;

        /// <summary>
        /// Height of the wobble
        /// </summary>
        protected float m_wobbleHeight = 80.0f;

        /// <summary>
        /// What state is the mote in?
        /// </summary>
        protected CellMoteState m_cellState = CellMoteState.Question;

        /// <summary>
        /// Prize
        /// </summary>
        protected CellPrize m_prize = CellPrize.Shield;

        /// <summary>
        /// Are we autogenerating a random side or specifying one?
        /// </summary>
        protected CellSide m_side = CellSide.None;

        /// <summary>
        /// Straight or curved?
        /// </summary>
        protected bool m_straightMovement = false;
    }
}