
using UnityEngine;
using System.Collections;

/// <summary>
/// Text auto scaler.
/// 
/// To auto scale text according to screen size
/// 
/// </summary>
public class TextAutoScaler : MonoBehaviour {

	void Start () {
	 
	//	GUIText.pixelOffset.x = Screen.width/2; // If your gui text transform positions are set to 0 this will be in the middle of the view

    
	//	GUIText.pixelOffset.y = Screen.height/15; // Yeah you guessed it, it is near the bottom of the rendered view
		
		// Scale the font according to standard width of 800
		//
		
	    guiText.fontSize = (int)((float)guiText.fontSize * (Screen.width / 900.0f));
		
	}

	void Update () {
	
	}
}
