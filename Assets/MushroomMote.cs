using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Mushrooms - we weight these lightly as we don't want too many of them
    /// </summary>
    public class MushroomMote : FluffyMote
    {
        /// <summary>
        /// Default is worth of 75 and starts offscreen
        /// </summary>
        public MushroomMote(float weight)
            : base(weight, 100, true)
        {
            m_weight = 0.1f;
        }

        public MushroomMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize)
            : base(gameObject, startOffScreen, textureSize)
        {
            MosquitoMote.AnimationFrames = 3;
            m_worth = 100;
            m_weight = 0.1f;
        }

        /// <summary>
        /// Set and adjust
        /// </summary>
        /// <param name="position"></param>
        public MushroomMote(Vector3 position)
            : base(position)
        {
            m_worth = 100;
            m_weight = 0.1f;
        }
    }

}