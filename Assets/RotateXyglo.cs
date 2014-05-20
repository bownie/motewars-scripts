using UnityEngine;
using System.Collections;

public class RotateXyglo : MonoBehaviour {

	// Use this for initialization
    //
    IEnumerator Start()
    {
        //NGUIDebug.Log("Starting wait");
        int level = 1;

        AudioSource source = GetComponent<AudioSource>();
        AudioClip clip = source.clip;
        source.PlayOneShot(clip);

        //while (source.active)
        //{
            yield return StartCoroutine(waitFor(clip.length));
        //}

        //NGUIDebug.Log("Loading level");
        Application.LoadLevel(level);
    }

    IEnumerator waitFor(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

	// Update is called once per frame
	void Update () {

        transform.Rotate(0, 20.0f * Time.deltaTime, -5.0f * Time.deltaTime);
	}
}
