using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Transient graphical object
    /// </summary>
    public class ScoreTransient : Transient
    {
        public ScoreTransient(GameObject gameObject, Vector2 position, Vector2 movement, Texture texture, float lifeTime)
            : base(gameObject, position)
        {
            m_startTime = Time.time;
            m_animation[0] = texture;
            m_lifeTime = lifeTime;
            m_movement = movement; // set the transient movement vector
        }
 
        /// <summary>
        /// Animation of textures
        /// </summary>
        public override void doAnimate()
        {
            
            Rect newRect = new Rect(m_gameObject.guiTexture.pixelInset.x + m_movement.x, m_gameObject.guiTexture.pixelInset.y + m_movement.y, m_gameObject.guiTexture.pixelInset.width, m_gameObject.guiTexture.pixelInset.height);
            //Debug.Log("SCORE ANIMATE = " + newRect);
            m_gameObject.guiTexture.pixelInset = newRect;
            Color newColor = m_gameObject.guiTexture.color;
            newColor.a = newColor.a - 0.02f;
            m_gameObject.guiTexture.color = newColor;
        }

        /// <summary>
        /// Should this mote be kept alive?
        /// </summary>
        /// <returns></returns>
        public override bool isAlive()
        {
            return (Time.time < m_startTime + m_lifeTime);
        }

        /// <summary>
        /// Textures
        /// </summary>
        protected Texture[] m_animation =  new Texture[1];

        /// <summary>
        /// Drop dead time
        /// </summary>
        protected float m_lifeTime;

        /// <summary>
        /// Vector which we're moving the transient in
        /// </summary>
        protected Vector2 m_movement;

        /// <summary>
        /// Time this mote started
        /// </summary>
        protected float m_startTime;
    }
}