using UnityEngine;
using System.Collections;

public class RemoveLite : MonoBehaviour {

	/// <summary>
	/// Just remove this GUITexture for the lite version
	/// </summary>
	void Start () {
	    if (!WorldScene.m_isLite)
            this.gameObject.SetActive(false);
	}
	
	
}
