using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    public float xRotation;
    public float yRotation;
    public float zRotation;

    // Update is called once per frame
    void Update()
    {
        //Quaternion rotate = Quaternion.Euler(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime);
        Quaternion rotate = new Quaternion(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime, 0.0f);
        //transform.Rotate(transform.rotation);
        transform.Rotate(rotate.x, rotate.y, rotate.z);
    }
}
