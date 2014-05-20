using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Transient graphical object
    /// </summary>
    public class BannerTransient : Transient
    {
        public BannerTransient(GameObject gameObject, Vector2 position, Texture texture, float lifeTime, float alpha = 1.0f)
            : base(gameObject, position)
        {
            m_startTime = Time.time;
            m_texture = texture;
            m_lifeTime = lifeTime;
            m_initial = gameObject.guiTexture.pixelInset;
			
			// Possible override of alpha
			//
			m_alpha = alpha;
        }

        /// <summary>
        /// Animation of textures
        /// </summary>
        public override void doAnimate()
        {
            // Scaling factor increases
            //
            m_scaling += 0.1f;

            float newWidth = m_initial.width * m_scaling;
            float newHeight = m_initial.height * m_scaling;

            m_newSize.width = newWidth;
            m_newSize.height = newHeight;
            m_newSize.x = m_initial.x - ((newWidth - m_initial.width) / 2);
            m_newSize.y = m_initial.y - ((newHeight - m_initial.height) / 2);
            //m_newSize.x -= ((newWidth - m_initial.width) / 2);
            //m_newSize.y -= ((newHeight - m_initial.height) / 2);

            m_gameObject.guiTexture.pixelInset = m_newSize;

            //Debug.Log("BANNER TRANSIENT newSize = " + m_newSize + " at alpha " + m_alpha);

            // Fade
            //
            Color newColour = new Color(m_gameObject.guiTexture.color.r, m_gameObject.guiTexture.color.g, m_gameObject.guiTexture.color.b, m_alpha);
            m_gameObject.guiTexture.color = newColour;
		
            m_alpha -= 0.05f;
			
			if (m_alpha < 0.0f)
				m_alpha = 0;
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
        /// Scaled sized
        /// </summary>
        protected Rect m_newSize = new Rect();

        // Scaling changes over time
        //
        protected float m_scaling = 1.0f;

        /// <summary>
        /// Alpha diminishes
        /// </summary>
        protected float m_alpha = 1.0f;

        /// <summary>
        /// Textures
        /// </summary>
        //protected Texture m_texture;

        /// <summary>
        /// Drop dead time
        /// </summary>
        protected float m_lifeTime;

        /// <summary>
        /// Size of original texture
        /// </summary>
        protected Rect m_initial;

        /// <summary>
        /// Time this mote started
        /// </summary>
        protected float m_startTime;
    }
}