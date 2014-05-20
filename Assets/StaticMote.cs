using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class StaticMote : Mote
    {
        public StaticMote(GameObject gameObject)
            : base(gameObject)
        {
            m_worth = 50;
        }

        /// <summary>
        /// Set and adjust allowing for m_texture sizing from base class (must be assigned)
        /// </summary>
        /// <param name="position"></param>
        public StaticMote(int worth, Vector3 position)
            : base(position)
        {
            position.x = position.x - m_textureSize.x / 2;
            position.y = position.y - m_textureSize.y / 2;
            m_position = position;

            m_worth = worth;
        }

        public StaticMote(int worth, bool offScreen)
        {
            m_startOffScreen = offScreen;
            m_worth = worth;
        }

        public override void doMove()
        {
        }

        /// <summary>
        /// Store the static texture here
        /// </summary>
        //public static Texture m_staticMoteTexture;
    }
}
