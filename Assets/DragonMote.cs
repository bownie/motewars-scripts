using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// The Heart of Gold
    /// </summary>
    public class DragonMote : MovingMote
    {
        public DragonMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.3f)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_textureSize = textureSize;

            calculateRandomStartPositionAndVelocity(aggressiveness);
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public DragonMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}