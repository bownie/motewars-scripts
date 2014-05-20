using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Transient graphical object
    /// </summary>
    public abstract class Transient
    {
        public Transient(GameObject gameObject, Vector2 position)
        {
            m_position = position;
            m_gameObject = gameObject;
        }

        public Transient(Vector2 position)
        {
            m_position = position;
        }

        /// <summary>
        /// Set mote position
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(Vector2 position)
        {
            m_position = position;
        }

        /// <summary>
        /// Get position
        /// </summary>
        /// <returns></returns>
        public Vector3 getPosition()
        {
            return m_position;
        }

        /// <summary>
        /// Unity object
        /// </summary>
        /// <returns></returns>
        public GameObject getGameObject()
        {
            return m_gameObject;
        }

        /// <summary>
        /// Animate this transient
        /// </summary>
        public abstract void doAnimate();

        /// <summary>
        /// Is this transient still needing to be kept alive?
        /// </summary>
        /// <returns></returns>
        public abstract bool isAlive();

        /// <summary>
        /// Mote position
        /// </summary>
        protected Vector3 m_position;

        /// <summary>
        /// Mote GameObject holding texture
        /// </summary>
        protected GameObject m_gameObject;

        /// <summary>
        /// Store a reference to the loaded texture within this instance
        /// </summary>
        public static Texture m_texture;

    }
}