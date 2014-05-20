using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// The Heart of Gold
    /// </summary>
    public class AuroraMote : MovingMote
    {
        /// <summary>
        /// Big and slow moving aurorae
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="textureSize"></param>
        /// <param name="aggressiveness"></param>
        public AuroraMote(GameObject gameObject, Vector2 textureSize, float aggressiveness = 0.05f)
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
        public AuroraMote(float weight)
            : base(weight, 150, true, MoveMethod.DefaultDrift)
        {
        }
    }
}