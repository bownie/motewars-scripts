using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// SatelliteMote
    /// </summary>
    public class SatelliteMote : MovingMote
    {
        public SatelliteMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_textureSize = textureSize;

            // Reduce step size for movement
            m_step = 0.035f;

            calculateRandomStartPositionAndVelocity();
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public SatelliteMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}