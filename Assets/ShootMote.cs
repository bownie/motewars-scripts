using UnityEngine;
using System.Collections.Generic;


namespace Xyglo.Unity
{
    /// <summary>
    /// Something we can shoot
    /// </summary>
	public class ShootMote : MovingMote
	{
        /// <summary>
        /// Meta constructor
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="vector"></param>
        public ShootMote(Texture texture, Vector2 sourcePoint, Vector2 destinationPoint):base(0, sourcePoint, MoveMethod.Line)
        {
            m_attackTexture = texture;
            m_attackVector = destinationPoint - sourcePoint;
            Debug.Log("ShootMote at position x = " + m_position.x + ", y = " + m_position.y);
        }

        /// <summary>
        /// Game constructor
        /// </summary>
        /// <param name="gameObject"></param>
        public ShootMote(GameObject gameObject)
            : base(gameObject, MoveMethod.Line)
        {
        }

        /// <summary>
        /// Get the attack texture
        /// </summary>
        /// <returns></returns>
        public Texture getAttackTexture()
        {
            return m_attackTexture;
        }

        /// <summary>
        /// Direction we're going
        /// </summary>
        /// <returns></returns>
        public Vector2 getAttackVector()
        {
            return m_attackVector;
        }

        /// <summary>
        /// The texture used to attack
        /// </summary>
        protected Texture m_attackTexture;

        /// <summary>
        /// Where are we aiming?
        /// </summary>
        protected Vector2 m_attackVector;
	}
}
