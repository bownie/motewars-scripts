using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// LegoBalloonMote
    /// </summary>
    public class LegoBalloonMote : MovingMote
    {
        public LegoBalloonMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.12f)
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
        public LegoBalloonMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}