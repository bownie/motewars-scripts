using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Transient graphical object
    /// </summary>
    public class PoppedMote : Transient
    {
        public PoppedMote(GameObject gameObject, Vector2 position, Texture texture1, Texture texture2, float lifeTime):base(gameObject, position)
        {
            m_startTime = Time.time;
            m_animation[0] = texture1;
            m_animation[1] = texture2;
            m_lifeTime = lifeTime;
        }
 
        /// <summary>
        /// Animation of textures
        /// </summary>
        public override void doAnimate()
        {
            if (Time.time < m_startTime + (m_lifeTime / 2.0f))
            {
                m_gameObject.guiTexture.texture = m_animation[0];
                //Debug.Log("PoppedMote::doAnimate - first texture x = " + m_position.x + ", y = " + m_position.y);
            }
            else if (Time.time < m_startTime + m_lifeTime)
            {
                m_gameObject.guiTexture.texture = m_animation[1];
                //m_gameObject.guiTexture.pixelInset = new Rect(m_position.x, m_position.y, m_animation[1].width, m_animation[1].height);
                //Debug.Log("PoppedMote::doAnimate - second texture");
            }
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
        protected Texture[] m_animation =  new Texture[2];

        /// <summary>
        /// Drop dead time
        /// </summary>
        protected float m_lifeTime;

        /// <summary>
        /// Time this mote started
        /// </summary>
        protected float m_startTime;
    }
}