using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
	public enum HeartOfGoldMode
	{
		Hyperspace,
		Appearing,
		RealSpace
	}
	
    /// <summary>
    /// The Heart of Gold appears and disappears 
    /// </summary>
    public class HeartOfGoldMote : MovingMote
    {
        public HeartOfGoldMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject, MoveMethod.Elliptical)
        {
            m_worth = 150;
            m_startOffScreen = false;
            m_textureSize = textureSize;

            //calculateRandomStartPositionAndVelocity();

            // Store local scale
            m_originalLocalScale = m_gameObject.transform.localScale;

            // Initial set inactive
            //
            m_gameObject.SetActive(false);
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public HeartOfGoldMote(float weight)
            : base(weight, 150, true, MoveMethod.Elliptical)
        {
        }
		
		/// <summary>
		/// In our override doMove we have to work out appearance position and then scale the ship at that
		/// position and then we go on to the super doMove.
		/// </summary>
		public override void doMove()
		{
            // Set max radius to be larger than default for this mote
            //
            m_maxRadius = Screen.height / 2.0f;

			// If we're in hyper space then we may appear
			if (m_mode == HeartOfGoldMode.Hyperspace)
			{
				if (Random.value < m_chanceToAppear)
				{
                    // Activate the game object
                    //
                    m_gameObject.SetActive(true);

                    m_mode = HeartOfGoldMode.Appearing;
					m_appearanceTime = Time.time;

                    // Position needs to be in a box around the edge of the screen - choose left/right/up/down and position within that
                    //
                    bool left = (Random.value < 0.5f);
                    bool up = (Random.value < 0.5f);

                    if (left)
                        m_position.x = (0.2f + (0.15f * Random.value)) * Screen.width;
                    else
                        m_position.x = (0.8f + (0.15f * Random.value)) * Screen.width;

                    if (up)
                        m_position.y = (0.8f + (0.15f * Random.value)) * Screen.height;
                    else
                        m_position.y = (0.2f + (0.15f * Random.value)) * Screen.height;

                    // Z always 0
                    m_position.z = 0;

                    // Reset texture using new position
                    //
                    setTexture(m_gameObject.guiTexture.texture, true);

                    //Debug.Log("HeartOfGold:doMove() - now appearing..");
				}

			}
			// If we're appearing then we need to scale over time
			else if ( m_mode == HeartOfGoldMode.Appearing)
			{
                //Debug.Log("HeartOfGold:doMove() - doing appear scale = " + m_gameObject.transform.localScale.magnitude);

				// Grow as percentage of appearanceDuration
				if (Time.time < m_appearanceTime + m_appearanceDuration)
				{
					//float scale = (m_appearanceTime - Time.time) / m_appearanceDuration;
					//m_gameObject.transform.localScale = new Vector3(scale, scale, scale);
                    m_gameObject.transform.localScale = Vector3.one * 0.1f * ((Time.time - m_appearanceTime) / m_appearanceDuration);
				}
				else
				{
					// When does the ship disappear again?
					m_realSpaceDuration = Time.time + 5 + 8 * Random.value;
					m_mode = HeartOfGoldMode.RealSpace;
                    m_gameObject.transform.localScale = Vector3.one * 0.1f;
				}
			}
			// If we're in real space then we need to wobble until it's time to disappear again
			else if (m_mode == HeartOfGoldMode.RealSpace)
			{
                //Debug.Log("HeartOfGold - in RealSpace and waiting to disappear, scale = " + m_gameObject.transform.localScale.magnitude);
				if (Time.time < m_realSpaceDuration)
				{
					base.doMove();
				}
				else
				{
					m_mode = HeartOfGoldMode.Hyperspace;
                    m_gameObject.SetActive(false);
				}
			}
		}
		
		
		/// <summary>
		/// The time the ship appears.
		/// </summary>
		protected float m_appearanceTime;
		
		/// <summary>
		/// How long the ship takes to appear
		/// </summary>
		protected float m_appearanceDuration = 0.3f;
		
		/// <summary>
		/// Time spent in real space before disappearing.
		/// </summary>
		protected float m_realSpaceDuration;
		
		/// <summary>
		/// What chance to appear.
		/// </summary>
		protected float m_chanceToAppear = 0.1f;
        
        /// <summary>
        /// Store original local scale
        /// </summary>
        protected Vector3 m_originalLocalScale;

		/// <summary>
		/// Mode of the HeartOfGold
		/// </summary>
		protected HeartOfGoldMode m_mode = HeartOfGoldMode.Hyperspace;	
		
    }
}