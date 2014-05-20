using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// It's a bee
    /// </summary>
    public class BeeMote : MovingMote
    {
        public BeeMote(GameObject gameObject)
            : base(gameObject, MoveMethod.Wiggle)
        {
        }

        /// <summary>
        /// Start off screen bee
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="?"></param>
        public BeeMote(float weight)
            : base(weight, 150, true, MoveMethod.Wiggle)
        {
            calculateRandomStartPositionAndVelocity(2.0f);
        }

    }

}