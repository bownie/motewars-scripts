using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Spore motes can generate new ones once they pop
    /// </summary>
    public class SporeMote : Mote
    {
        /// <summary>
        /// SporeMote
        /// </summary>
        /// <param name="worth"></param>
        public SporeMote(float weight)
            : base(weight, 100, true)
        {
            m_weight = weight;
            m_generation = 0;
        }

        /// <summary>
        /// Weight with start side
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="side"></param>
        public SporeMote(float weight, MoteStartSide side)
            : base(weight, 100, true)
        {
            m_weight = weight;
            m_generation = 0;
            m_startSide = side;

            // Set initial acceleration
            //
            accelAwayFromSide(side);
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialVelocity"></param>
        /// <param name="offScreen"></param>
        /// <param name="side"></param>
        public SporeMote(int worth, bool offScreen, int generation)
            : base(worth, offScreen)
        {
            m_worth = worth;
            m_generation = generation;
            m_startOffScreen = offScreen;

            // If we're starting off screen then calculate position and initial accel accordingly
            //
            if (m_startOffScreen)
                calculateRandomStartPositionAndVelocity();
        }*/

        public SporeMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize, int generation)
            : base(gameObject)
        {
            m_worth = 150;
            m_startOffScreen = startOffScreen;
            m_generation = generation;
            m_textureSize = textureSize;
			
			// If we're not a base generation then let's set some initial acceleration in random directions
			//
			if (generation > 0)
			{
				m_accel.x = Random.value * generation;
				m_accel.y = Random.value * generation;
			}
			
            // If we're starting off screen then calculate position and initial accel accordingly
            //
            if (m_startOffScreen)
                calculateRandomStartPositionAndVelocity();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="side"></param>
        /// <param name="textureSize"></param>
        /// <param name="generation"></param>
        public SporeMote(GameObject gameObject, MoteStartSide side, Vector2 textureSize, int generation)
            : base(gameObject)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_generation = generation;
            m_textureSize = textureSize;

            // If we're starting off screen then calculate position and initial accel accordingly
            //
            calculateStartFromSide(side);
        }

        /// <summary>
        /// Set and adjust allowing for m_texture sizing from base class (must be assigned)
        /// </summary>
        /// <param name="position"></param>
        public SporeMote(Vector3 position)
            : base(position)
        {
            position.x = position.x - m_textureSize.x / 2;
            position.y = position.y - m_textureSize.y / 2;
            m_position = position;

            m_worth = 150;
        }

        /// <summary>
        /// Spore mote move
        /// </summary>
        public override void doMove()
        {
            m_moveRect = m_gameObject.guiTexture.pixelInset;
//            m_moveRect.x += Random.ra

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

            // Do some rotation on the fluffy mote
            //
            m_gameObject.transform.rotation = new Quaternion(0, 2 * m_accel.x, 0, 0);
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
        /// Get the generation of this mote
        /// </summary>
        /// <returns></returns>
        public int getGeneration()
        {
            return m_generation;
        }

        /// <summary>
        /// Set current generation of this mote
        /// </summary>
        /// <param name="generation"></param>
        public void setGeneration(int generation)
        {
            m_generation = generation;
        }

        /// <summary>
        /// Is this the last generation
        /// </summary>
        /// <returns></returns>
        public bool isLastGeneration()
        {
            return (m_generation == m_lastGeneration);
        }

        /// <summary>
        /// Last generation of this mote
        /// </summary>
        /// <param name="lastGen"></param>
        /// <returns></returns>
        public void setLastGeneration(int lastGen)
        {
            m_lastGeneration = lastGen;
        }

        /// <summary>
        /// Max regeneration of this mote
        /// </summary>
        /// <returns></returns>
        public int getMaxGenerate()
        {
            return m_maxGenerate;
        }

        /// <summary>
        /// Movement step
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
        /// Generation of this spore mote - they can regenerate
        /// </summary>
        protected int m_generation = 0;

        /// <summary>
        /// What is the last generation of this spore mote?
        /// </summary>
        protected int m_lastGeneration = 1;

        /// <summary>
        /// How many motes can this mote generate max?
        /// </summary>
        protected int m_maxGenerate = 5;
    }
}
