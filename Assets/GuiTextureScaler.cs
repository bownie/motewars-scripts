using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Perform generic guitexture scaling according to some default parameters.  This allows you to
    /// position your GuiTextures as per normal in Unity (providing you use position rather than pixelInset)
    /// and then this script - attached to a base object in your scene hierarchy - will rescale all the
    /// GUITextures according to the factors you specify.
    /// </summary>
    [System.Serializable]
    public class GuiTextureScaler : MonoBehaviour
    {
        /// <summary>
        /// Default width of the application from which we will scale our textures
        /// </summary>
        public float defaultWidth = 800;

        /// <summary>
        /// Default height of the application from which we will scale our textures
        /// </summary>
        public float defaultHeight = 360;

        /// <summary>
        /// Do we want to maintain or stretch the textures?
        /// </summary>
        public bool maintainAspect = true;
		
		protected float xScale = 0.0f;
		
		protected float yScale = 0.0f;
		
		public float getXScale()
		{
			return xScale;
		}
		
		public float getYScale()
		{
			return yScale;
		}
		
        /// <summary>
        /// Only rescale once
        /// </summary>
        void Start()
        {
            if (defaultWidth == 0)
                defaultWidth = Screen.width;

            if (defaultHeight == 0)
                defaultHeight = Screen.height;

            xScale = Screen.width / defaultWidth;
            yScale = Screen.height / defaultHeight;

            // Keep the scaling in proportion accordingly
            //
            if (maintainAspect)
            {
                if (xScale < yScale)
                    yScale = xScale;
                else
                    xScale = yScale;
            }

            //GameObject gO = GameObject.FindWithTag("Player");

            foreach (Object obj in GameObject.FindObjectsOfType(typeof(GUITexture)))
            {
                GUITexture texture = (GUITexture)obj;

                if (texture)
                {
                    Rect size = texture.pixelInset;

                    size.x *= xScale;
                    size.width *= xScale;
                    size.y *= yScale;
                    size.height *= yScale;

                    texture.pixelInset = size;
                }
            }

            // Do the same for RotableGUIItem
            //
            foreach(Object obj in GameObject.FindObjectsOfType(typeof(RotatableGuiItem)))
            {
				//if (obj.name == "MoteWars")
				//{
					//Debug.Log("GOT MOTE WARS LOGO multipler = " + xScale);
					//xScale *= 4;
					//continue;
				//}
				
                RotatableGuiItem texture = (RotatableGuiItem)obj;

                if (texture)
                {
                    Vector2 size = texture.size; 
                    Vector2 offset = texture.offset;

                    size.x *= xScale;
                    size.y *= yScale;
                    offset.x *= xScale;
                    offset.y *= yScale;

                    texture.size = size;
                    texture.offset = offset;
				
					// We have to call this after we update the size
					//
					texture.UpdateSettings();
                }
            }
        }
    }
}