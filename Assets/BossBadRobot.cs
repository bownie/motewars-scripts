using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{

    /// <summary>
    /// A MegaSheep Boss
    /// </summary>
    public class BossBadRobot : BossMote
    {
        /// <summary>
        /// Default
        /// </summary>
        public BossBadRobot()
            : base(1000, Vector3.zero, MoveMethod.Figure8)
        {
            // More aggressive attack period
            m_attackPeriod = 2.5f;
        }

        /// <summary>
        /// Actual in-game version 
        /// </summary>
        /// <param name="gameObject"></param>
        public BossBadRobot(GameObject g, Texture leftTexture, Texture rightTexture, Texture centreTexture, Texture angryTexture, Texture hurtTexture, Texture deadTexture, Texture attackTexture, Vector2 centrePoint)
            : base(g, MoveMethod.Figure8)
        {
            // Store the boss centre point
            //
            m_bossPoint = centrePoint;

            // Textures
            //
            m_leftTexture = leftTexture;
            m_rightTexture = rightTexture;
            m_centreTexture = centreTexture;
            m_attackTexture = attackTexture;
            m_angryTexture = angryTexture;
            m_hurtTexture = hurtTexture;
            m_deadTexture = deadTexture;

            // More aggressive attack period
            //
            m_attackPeriod = 2.5f;
        }

        /// <summary>
        /// Movement and other updates
        /// </summary>
        public override void doMove()
        {
            // Call super
            //
            base.doMove();
        }

        /// <summary>
        /// The point at which the boss will be rotating
        /// </summary>
        protected Vector2 m_bossPoint;

    }

}