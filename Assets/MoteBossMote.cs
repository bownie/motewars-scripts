using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// SputnikMote
    /// </summary>
    public class MoteBossMote : MovingMote
    {
        public MoteBossMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.1f)
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
        public MoteBossMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}