using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class RussianMote : FluffyMote
    {
        public RussianMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize)
            : base(gameObject, startOffScreen, textureSize)
        {
            m_worth = 250;
        }

        public RussianMote(Vector3 position)
            : base(position)
        {
            m_worth = 250;
        }

        public RussianMote(float weight, int worth, bool offScreen)
            : base(weight, worth, offScreen)
        {
            m_worth = 250;
        }

        /// <summary>
        /// weight constructor
        /// </summary>
        /// <param name="weight"></param>
        public RussianMote(float weight)
            : base(weight, 250, true)
        {
        }


    }
}
