using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{

    /// <summary>
    /// Build certain Xyglo Unity components 
    /// </summary>
    public class Factory
    {
        public enum Buildable
        {
            SlideMenu
        }

        protected GameObject m_baseObject;

        public Factory(GameObject baseObject)
        {
            m_baseObject = baseObject;
        }

        /*
        public SlideMenu buildSlideMenu()
        {
            GameObject g = new GameObject("SlideMenu");
        }

        /// <summary>
        /// Do all the Unity boilerplate in this method
        /// </summary>
        /// <param name="buildableObject"></param>
        public void buildObject(Buildable buildableObject)
        {

            GameObject g = new GameObject("Mote");
            g.AddComponent("GUITexture");
            g.guiTexture.texture = StaticMoteTexture;
            g.transform.position = Vector3.zero;

            //float widthMulti = 0.0f;
            //float heightMulti = widthMulti * Screen.width / Screen.height;
            g.transform.localScale = Vector3.zero; // new Vector3(widthMulti, heightMulti, 0.0f);
            g.guiTexture.pixelInset = new Rect(newPos.x, newPos.y, StaticMoteTexture.width, StaticMoteTexture.height);

            //Debug.Log("Adding texture at x = " + newPos.x + ", y = " + newPos.y);

            Mote newMote = new StaticMote(g);
            MoteManager.m_moteList.Add(newMote);
        }
        */
    }


}