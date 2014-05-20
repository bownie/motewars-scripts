using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// ICBMMote
    /// </summary>
    public class TinaTurnerMote : MovingMote
    {
        public TinaTurnerMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_textureSize = textureSize;

            calculateRandomStartPositionAndVelocity();
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public TinaTurnerMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}