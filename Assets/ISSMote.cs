using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// ICBMMote
    /// </summary>
    public class ISSMote : MovingMote
    {
        public ISSMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.1f)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_textureSize = textureSize;

            // Modify initial acceleration by aggressiveness
            //
            //m_accel *= aggressiveness;

            // Reduce step size for movement
            m_step *= aggressiveness;

            calculateRandomStartPositionAndVelocity(aggressiveness);
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public ISSMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}