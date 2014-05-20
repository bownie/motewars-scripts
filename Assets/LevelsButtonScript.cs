using UnityEngine;
using System.Collections;

public class LevelsButtonScript : MonoBehaviour {

	
    
	void Start () {
	
	}
	
	void Update () {

        Vector2 hitPosition = new Vector2(-1, -1);

        // Test for touch or mouse input position
        //
        if (Input.touches.Length != 0)
            hitPosition = Input.touches[0].position;
        else if (Input.GetMouseButtonDown(0))
            hitPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (hitPosition.x == -1 && hitPosition.y == -1)
            return;

        // With a RotatableGuiItem we need to use transform position and texture sizes rather than
        // guiTexture specific tests.
        //
        GUITexture item = (GUITexture)GetComponent(typeof(GUITexture));

        if (item != null && item.GetScreenRect().Contains(hitPosition))
        {
            Application.LoadLevel(2);
        }
	}
}
