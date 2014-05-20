using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// ICBMMote
    /// </summary>
    public class ICBMMote : RotateMote
    {
        public ICBMMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.2f)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_worth = 150;
            m_startOffScreen = true;
            m_textureSize = textureSize;

            m_step = 0.1f;

            calculateRandomStartPositionAndVelocity(aggressiveness);
        }

        /// <summary>
        /// Start off screen
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public ICBMMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}