using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// A MovingMote with a rotatable guiitem - going to have to replace a lot of shared functions
    /// </summary>
    public class RotateMote : MovingMote
    {
        public RotateMote(GameObject gameObject, MoveMethod method):base(gameObject, method)
        {
            //m_rotatableGuiItem = new RotatableGuiItem();
        }

        public RotateMote(float weight, int worth, bool startOffScreen, MoveMethod method):base(weight, worth, startOffScreen, method)
        {
        }

        public RotateMote(int worth, Vector3 position, MoveMethod method):base(worth, position, method)
        {
        }

        /*
        /// <summary>
        /// Override the setTexture
        /// </summary>
        /// <param name="newTexture"></param>
        /// <param name="maxSize"></param>
        public override void setTexture(Texture newTexture, float maxSize = 128.0f)
        {
            // Initially assuming width > height
            //
            float width = Mathf.Min(newTexture.width, maxSize);
            float height = newTexture.height * width / newTexture.width;

            // If the proportions are the other way around
            //
            if (newTexture.height > newTexture.width)
            {
                height = Mathf.Min(newTexture.height, maxSize);
                width = newTexture.width * height / newTexture.height;
            }
            m_rotatableGuiItem.texture = newTexture;

            // Set the pixel inset
            //
            m_rotatableGuiItem.setRect(new Rect(m_position.x - width / 2, m_position.y - height / 2, width, height));
        }

        /// <summary>
        /// Texture width override
        /// </summary>
        /// <returns></returns>
        public override float getWidth()
        {
            return m_rotatableGuiItem.getScreenRect().width;
        }

        /// <summary>
        /// Texture height override
        /// </summary>
        /// <returns></returns>
        public override float getHeight()
        {
            return m_rotatableGuiItem.getScreenRect().height;
        }

        /// <summary>
        /// Intersect override
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public override bool intersects(Rect rect)
        {
            return (m_rotatableGuiItem.getScreenRect().xMax >= rect.xMin && m_rotatableGuiItem.getScreenRect().xMin <= rect.xMax &&
                    m_rotatableGuiItem.getScreenRect().yMax >= rect.yMin && m_rotatableGuiItem.getScreenRect().yMin <= rect.yMax);
        }

        /// <summary>
        /// Contains override
        /// </summary>
        /// <param name="touchPosition"></param>
        /// <returns></returns>
        public virtual bool contains(Vector2 touchPosition)
        {
            return (m_rotatableGuiItem.getScreenRect().xMin <= touchPosition.x && m_rotatableGuiItem.getScreenRect().xMax >= touchPosition.x &&
                    m_rotatableGuiItem.getScreenRect().yMin <= touchPosition.y && m_rotatableGuiItem.getScreenRect().yMax >= touchPosition.y);
        }

        /// <summary>
        /// Override the MovingMote method
        /// </summary>
        protected override void doDefaultDrift()
        {
            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_rotatableGuiItem.getScreenRect();

            float random = Random.value;
            if (random < 0.25f)
                m_accel.x += m_step * normaliseMovement;
            else if (random < 0.5f)
                m_accel.y += m_step * normaliseMovement;
            else if (random < 0.75f)
                m_accel.x -= m_step * normaliseMovement;
            else
                m_accel.y -= m_step * normaliseMovement;

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_rotatableGuiItem.setRect(m_moveRect);
        }

        /// <summary>
        /// Actual texture position - adjusted for any inset
        /// </summary>
        /// <returns></returns>
        public override Vector2 getInsetPosition()
        {
            return new Vector2(m_rotatableGuiItem.getScreenRect().x, m_rotatableGuiItem.getScreenRect().y);
        }
        */
       
        /// <summary>
        ///  Override OnGUI
        /// </summary>
        void OnGUI()
        {
            Matrix4x4 matrixBackup = GUI.matrix;

            float thisAngle = Time.frameCount * 2;

            Vector2 pos = new Vector2(425,425);

            GUIUtility.RotateAroundPivot(thisAngle, pos);

            Rect thisRect= new Rect(400,400,50,50);

            GUI.DrawTexture(thisRect, m_gameObject.guiTexture.texture); 

            GUI.matrix = matrixBackup;
        }

    

        /// <summary>
        /// Allow accessibility in a similar manner to guiTexture
        /// </summary>
        //public RotatableGuiItem m_rotatableGuiItem;
    }
}