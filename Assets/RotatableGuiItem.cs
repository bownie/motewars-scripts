using UnityEngine;
using Xyglo.Unity;

// From
//
// http://answers.unity3d.com/questions/11022/how-to-rotate-gui-textures.html



/// <summary>
/// Rotatable and animatable texture - you can set an original angle and a wobble factor.
/// </summary>
[ExecuteInEditMode()]
public class RotatableGuiItem : MonoBehaviour
{
    /// <summary>
    /// Initial texture
    /// </summary>
    public Texture texture = null;

    /// <summary>
    /// Initial Angle
    /// </summary>
    public float InitialAngle = 0.0f;

    /// <summary>
    /// Angle
    /// </summary>
    protected float m_angle = 0.0f;

    /// <summary>
    /// Scale
    /// </summary>
    public float scale = 1.0f;

    /// <summary>
    /// Texture size
    /// </summary>
    public Vector2 size = new Vector2(128, 128);

    /// <summary>
    /// Offset for the texture
    /// </summary>
    public Vector2 offset = Vector2.zero;

    /// <summary>
    /// Position of item
    /// </summary>
    protected Vector2 m_position = new Vector2(0, 0);

    /// <summary>
    /// Scaling vector
    /// </summary>
    protected Vector2 m_scaleVector = new Vector2(1, 1);

    /// <summary>
    /// Rectangle
    /// </summary>
    protected Rect m_rect;

    /// <summary>
    /// Pivot point
    /// </summary>
    protected Vector2 m_pivot;

    /// <summary>
    /// An animation frame
    /// </summary>
    public Texture2D AnimationFrame1;

    /// <summary>
    /// Second frame
    /// </summary>
    public Texture2D AnimationFrame2;

    /// <summary>
    /// Third frame
    /// </summary>
    public Texture2D AnimationFrame3;

    /// <summary>
    /// Fourth frame
    /// </summary>
    public Texture2D AnimationFrame4;

    /// <summary>
    /// Fifth frame
    /// </summary>
    public Texture2D AnimationFrame5;

    /// <summary>
    /// How likely is an animation on this texture?
    /// </summary>
    public float AnimationProbability = 0.0f;

    /// <summary>
    /// How much to make this gui item wobble by in radians
    /// </summary>
    public float wobbleAngle = 0.0f;

    /// <summary>  
    /// Time over which to vary wobble angle
    /// </summary>
    public float wobblePeriod = 2.0f;


    /// <summary>
    /// Are we animating?
    /// </summary>
    protected bool m_isAnimating = false;

    /// <summary>
    /// Length of animation frame
    /// </summary>
    protected float m_animationFrameLenth = 0.03f;

    /// <summary>
    /// Which animation frame are we showing?
    /// </summary>
    protected int m_animationFrame = 0;

    /// <summary>
    /// Animation done
    /// </summary>
    protected bool m_animationIncreasing = true;

    /// <summary>
    /// Time to show the next animation frame?
    /// </summary>
    protected float m_animationTime = 0.0f;

    /// <summary>
    /// Don't accept any input for this amount of time inside level to prevent bounces
    /// </summary>
    protected float m_bouncePause = 0.2f;

    /// <summary>
    /// Level start time
    /// </summary>
    protected float m_bounceStartTime = 0.0f;

    /// <summary>
    /// Launch this URL
    /// </summary>
    public string LaunchURL;

    /// <summary>
    /// Is this object rotating rather than wobbling?
    /// </summary>
    public bool Rotating = false;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        m_bounceStartTime = Time.time;

        //transform.localScale = Vector3.zero;
        //transform.localPosition = Vector3.zero;
        UpdateSettings();
    }

    /// <summary>
    /// Screen rect - we have to invert the y axis
    /// </summary>
    /// <returns></returns>
    public Rect getScreenRect()
    {
        return new Rect(m_rect.x, Screen.height - m_rect.y - m_rect.height, m_rect.width, m_rect.height);
    }

    /// <summary>
    /// Set the rect
    /// </summary>
    /// <param name="rect"></param>
    public void setRect(Rect rect)
    {
        m_rect = rect;
    }
	
	/// <summary>
	/// Updates the settings.
	/// </summary>
    public void UpdateSettings()
    {
        m_position = new Vector2(transform.position.x * Screen.width + offset.x, transform.position.y * Screen.height + offset.y);
        m_rect = new Rect(m_position.x - size.x * 0.5f, m_position.y - size.y * 0.5f, size.x, size.y);
        m_pivot = new Vector2(m_rect.xMin + m_rect.width * 0.5f, m_rect.yMin + m_rect.height * 0.5f);
    }



    void OnGUI()
    {
        if (Application.isEditor) { UpdateSettings(); }

        if (Rotating)
            calculateRotation();
        else
            calculateWobble();

        Matrix4x4 matrixBackup = GUI.matrix;
        
        m_scaleVector.x = scale;
        m_scaleVector.y = scale;
        GUIUtility.ScaleAroundPivot(m_scaleVector, m_pivot);
        GUIUtility.RotateAroundPivot(InitialAngle + m_angle, m_pivot);

        if (texture != null)
        {
            GUI.DrawTexture(m_rect, texture);
        }
        GUI.matrix = matrixBackup;

        testAnimate();
    }

    /// <summary>
    /// Test to see if we want to animate and do it as necessary
    /// </summary>
    protected void testAnimate()
    { 
        // See if we want to animate
        //
        if (!m_isAnimating && Random.value < AnimationProbability)
        {
            m_isAnimating = true;
            m_animationTime = Time.time + m_animationFrameLenth;
            m_animationIncreasing = true;
            m_animationFrame = 0;
        }

        if (m_isAnimating && Time.time > m_animationTime)
        {
            if (m_animationIncreasing && m_animationFrame < 5)
            {
                texture = getTextureForFrame(m_animationFrame++);

                if (m_animationFrame == 5)
                    m_animationIncreasing = false;
            }
            else
            {
                if (m_animationFrame > 0)
                {
                    texture = getTextureForFrame(--m_animationFrame);
                }
                else
                {
                    //Debug.Log("STOPPED ANIMATING");
                    m_isAnimating = false;
                }
            }

            m_animationTime = Time.time + m_animationFrameLenth;
        }
    }

    /// <summary>
    /// Get animation texture for an animation frame - modified to deal with only the first
    /// animation frame populated.
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    protected Texture2D getTextureForFrame(int frame)
    {
        Texture2D returnFrame = null;

        switch (frame)
        {
            case 0:
                returnFrame = AnimationFrame1;
                break;

            case 1:
                returnFrame = AnimationFrame2;
                break;

            case 2:
                returnFrame = AnimationFrame3;
                break;

            case 3:
                returnFrame = AnimationFrame4;
                break;

            case 4:
                returnFrame = AnimationFrame5;
                break;

            default:
                returnFrame = null;
                break;
        }

        if (returnFrame == null)
            return AnimationFrame1;

        return returnFrame;
    }


    public Vector2 getPosition()
    {
        return m_position;
    }

    protected void calculateRotation()
    {
        m_angle += 0.1f;
    }

    /// <summary>
    /// Calculate the wobble of an animation
    /// </summary>
    protected void calculateWobble()
    {
        if (wobbleAngle == 0.0f)
            return;

        m_angle = wobbleAngle * Mathf.Sin(Time.fixedTime / wobblePeriod * 2 * Mathf.PI);
    }


    /// <summary>
    /// Test for clicks in update
    /// </summary>
    void Update()
    {
        // Do nothing for no texture
        if (texture == null)
            return;


        Vector2 hitPosition = new Vector2(-1, -1);

        // Test for touch or mouse input position
        //
        if (Input.touches.Length != 0)
            hitPosition = Input.touches[0].position;
        else if (Input.GetMouseButtonDown(0))
            hitPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (hitPosition.x == -1 && hitPosition.y == -1)
            return;

        // Need to invert the y axis for testing the GUIRect
        //
        hitPosition.y = Screen.height - hitPosition.y;

        //Debug.Log("m_rect x = " + m_rect.x + ", y = " + m_rect.y + ", width = " + m_rect.width + ", height = " + m_rect.height);
        //Debug.Log("HITPOSITION x = " + hitPosition.x + ", y = " + hitPosition.y);

        // Check for timer
        //
        if (LaunchURL != "" && m_rect.Contains(hitPosition) && Time.time > m_bounceStartTime + m_bouncePause)
        {
#if UNITY_WP8
            // We need to use this for WP8 or 
            WP8Static.FireOpenUrl(LaunchURL);
#else
            Application.OpenURL(LaunchURL);
#endif
        }
    }


}