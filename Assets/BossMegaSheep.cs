﻿using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{

    /// <summary>
    /// A MegaSheep Boss
    /// </summary>
    public class BossMegaSheep : BossMote
    {
        /// <summary>
        /// Default
        /// </summary>
        public BossMegaSheep()
            : base(1000, Vector3.zero, MoveMethod.Figure8)
        {
        }

        /// <summary>
        /// Actual in-game version 
        /// </summary>
        /// <param name="gameObject"></param>
        public BossMegaSheep(GameObject g, Texture leftTexture, Texture rightTexture, Texture centreTexture, Texture angryTexture, Texture hurtTexture, Texture deadTexture, Texture attackTexture, Vector2 centrePoint)
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