using UnityEngine;
using System.Collections;

public class JumpToLevel : MonoBehaviour {

    public string Tag;

    /// <summary>
    /// Level that we will jump to on click
    /// </summary>
    public int level;

    /// <summary>
    /// Alternate destination for a Full rather than Lite link
    /// </summary>
    public int alternateLevel = -1;

    GUITexture m_texture;

    /// <summary>
    /// Don't accept any input for this amount of time inside level to prevent bounces
    /// </summary>
    protected float m_bouncePause = 0.2f;

    /// <summary>
    /// Level start time
    /// </summary>
    protected float m_bounceStartTime = 0.0f;

	void Start () {

        GameObject go = GameObject.FindWithTag(Tag);

        if (go != null)
        {
            m_texture = (GUITexture)go.guiTexture;
        }
        else
        {
            Debug.Log("JumpToLevel() - Start - No tag found with this level \"" + Tag + "\"");
        }

        m_bounceStartTime = Time.time;
	}

    /// <summary>
    /// Music pause time
    /// </summary>
    protected float m_pauseMusicTime = 0.0f;

    /// <summary>
    /// On application pause and resume
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            // Store time the music paused
            //
            m_pauseMusicTime = GameObject.FindWithTag("Respawn").audio.time;
        }
        else
        {
            // Resume music
            //
            GameObject.FindWithTag("Respawn").audio.Play();
            GameObject.FindWithTag("Respawn").audio.time = m_pauseMusicTime;
        }
    }

    /// <summary>
    /// Update
    /// </summary>
	void Update () {

        if (m_texture == null)
            return;

        // Need to handle back button in WP8
        //
#if UNITY_WINRT
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (WorldScene.m_isLite || alternateLevel == -1)
                Application.LoadLevel(level);
            else
                Application.LoadLevel(alternateLevel);
        }
#endif

        Vector2 hitPosition = new Vector2(-1, -1);

        // Test for touch or mouse input position
        //
        if (Input.touches.Length != 0)
            hitPosition = Input.touches[0].position;
        else if (Input.GetMouseButtonDown(0))
            hitPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (hitPosition.x == -1 && hitPosition.y == -1)
            return;

        // Check for timer
        //
        if (m_texture.GetScreenRect().Contains(hitPosition) && Time.time > m_bounceStartTime + m_bouncePause)
        {
            if (WorldScene.m_isLite || alternateLevel == -1)
                Application.LoadLevel(level);
            else
                Application.LoadLevel(alternateLevel);
        }
	}
}
