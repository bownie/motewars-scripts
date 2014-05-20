using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class EggMote : FluffyMote
    {
        public EggMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize)
            : base(gameObject, startOffScreen, textureSize)
        {
            m_worth = 250;
        }

        public EggMote(Vector3 position)
            : base(position)
        {
            m_worth = 250;
        }

        public EggMote(float weight, int worth, bool offScreen)
            : base(weight, worth, offScreen)
        {
            m_worth = 250;
        }

        /// <summary>
        /// weight constructor
        /// </summary>
        /// <param name="weight"></param>
        public EggMote(float weight)
            : base(weight, 250, true)
        {
        }


    }
}
