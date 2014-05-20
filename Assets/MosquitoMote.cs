using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// A Mosquito
    /// </summary>
    public class MosquitoMote : FluffyMote
    {
        /// <summary>
        /// Default worth 100 with off screen start
        /// </summary>
        public MosquitoMote(float weight)
            : base(weight, 100, true)
        {
        }

        public MosquitoMote(GameObject gameObject, bool startOffScreen, Vector2 textureSize)
            : base(gameObject, startOffScreen, textureSize, 1.0f) // more aggressive mosquito
        {
        }

        /// <summary>
        /// Set and adjust
        /// </summary>
        /// <param name="position"></param>
        public MosquitoMote(Vector3 position)
            : base(position)
        {
        }

        public MosquitoMote(float weight, int worth, bool offScreen):base(weight, worth, offScreen)
        {
        }
    }

}