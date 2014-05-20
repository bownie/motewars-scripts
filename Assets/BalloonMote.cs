using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Balloon colours
    /// </summary>
    public enum BalloonColour
    {
        Blue,
        Red,
        Yellow,
        Green,
        Pink,
        Purple
    };

    /// <summary>
    /// It's a bee
    /// </summary>
    public class BalloonMote : MovingMote
    {
        public BalloonMote(GameObject gameObject, Vector2 textureSize, bool ignoreGravity)
            : base(gameObject, MoveMethod.DefaultDrift)
        {
            m_textureSize = textureSize;

            // Launch from bottom or normal random position
            //
            if (!ignoreGravity)
                launchAtSide(MoteStartSide.Bottom);
            else
                calculateRandomStartPositionAndVelocity();
        }

        /// <summary>
        /// Start off screen bee
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public BalloonMote(BalloonColour colour, float weight, bool ignoreGravity = false)
            : base(weight, 150, true, MoveMethod.Wiggle)
        {
            m_ignoreGravity = ignoreGravity;
            m_balloonColour = colour;
        }

        /// <summary>
        /// Colour
        /// </summary>
        /// <returns></returns>
        public BalloonColour getBalloonColour()
        {
            return m_balloonColour;
        }

        /// <summary>
        /// Are we ignoring gravity?
        /// </summary>
        /// <returns></returns>
        public bool getIgnoreGravity()
        {
            return m_ignoreGravity;
        }


        /// <summary>
        /// Colour
        /// </summary>
        protected BalloonColour m_balloonColour;

        /// <summary>
        /// Are we ignoring gravity for this balloon?
        /// </summary>
        protected bool m_ignoreGravity = false;
    }

}