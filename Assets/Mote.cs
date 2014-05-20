using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Position an off-screen mote can start from
    /// </summary>
    public enum MoteStartSide
    {
        None,
        Top,
        Bottom,
        Left,
        Right
    };

    /// <summary>
    /// A Mote in God's Eye
    /// 
    /// This is the base Mote class for specialisation
    /// </summary>
    public abstract class Mote
    {
        /// <summary>
        /// Default constructor with no GameObject defined yet
        /// </summary>
        public Mote()
        {
        }

        /// <summary>
        /// Constructor with position
        /// </summary>
        /// <param name="position"></param>
        public Mote(Vector3 position)
        {
            m_position = position;
        }

        /// <summary>
        /// Construct from GameObject
        /// </summary>
        /// <param name="gameObject"></param>
        public Mote(GameObject gameObject)
        {
            m_gameObject = gameObject;

            if (m_gameObject.guiTexture != null)
            {
                m_originalAlpha = m_gameObject.guiTexture.color.a;
            }
        }

        /// <summary>
        /// Set weight and worth
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="worth"></param>
        /// <param name="startOffScreen"></param>
        public Mote(float weight, int worth, bool startOffScreen)
        {
            m_weight = weight;
            m_worth = worth;
            m_startOffScreen = startOffScreen;
        }

        /// <summary>
        /// Constructor with X and Y and worth
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Mote(int worth, float x, float y)
        {
            m_position = new Vector3(x, y, 0);
            m_worth = worth;
        }

        /// <summary>
        /// For generated motes that may start off screen
        /// </summary>
        /// <param name="initialVelocity"></param>
        /// <param name="startOffScreen"></param>
        public Mote(int worth, bool startOffScreen)
        {
            m_startOffScreen = startOffScreen;
            m_worth = worth;
        }

        /// <summary>
        /// Do any movement for this mote
        /// </summary>
        public abstract void doMove();

        /// <summary>
        /// A random start side - slap in the middle
        /// </summary>
        public void calculateRandomStartSide()
        {
            // Guard against no texture
            //
            if (m_textureSize == Vector2.zero)
                return;

            float randSide = Random.value;
            if (randSide < 0.5f)
            {
                m_startSide = MoteStartSide.Left;
                m_position.x = -m_textureSize.x * 1.5f;
                m_position.y = Screen.height / 2;
                m_accel.x = 0;
                m_accel.y = 0;
            }
            else
            {
                m_startSide = MoteStartSide.Right;
                m_position.x = Screen.width + 5;
                m_position.y = Screen.height / 2;
                m_accel.x = 0;
                m_accel.y = 0;
            }
        }

        /// <summary>
        /// Start side with random position on that side
        /// </summary>
        /// <param name="side"></param>
        public void calculateStartFromSide(MoteStartSide side)
        {
            // Guard against no texture
            //
            if (m_textureSize == Vector2.zero)
                return;

            switch(side)
            {
                case MoteStartSide.Right:
                    m_position.x = Screen.width + 5;
                    m_position.y = Screen.height * Random.value;
                    m_accel.x = -Random.value * 3.0f;
                    m_accel.y = 0;
                    break;


                case MoteStartSide.Left:
                    m_position.x = -m_textureSize.x * 1.5f;
                    m_position.y = Screen.height * Random.value;
                    m_accel.x = Random.value * 3.0f;
                    m_accel.y = 0;
                    break;

                case MoteStartSide.Top:
                    m_position.x = Screen.width * Random.value;
                    m_position.y = Screen.height + 5;
                    m_accel.x = 0;
                    m_accel.y = -Random.value * 3.0f;
                    break;

                case MoteStartSide.Bottom:
                    m_position.x = Screen.width * Random.value;
                    m_position.y = -m_textureSize.y * 1.5f;
                    m_accel.x = 0;
                    m_accel.y = Random.value * 3.0f;
                    break;

                default:
                    break;
            }
        }

        

        /// <summary>
        /// Any side but in the middle
        /// </summary>
        public void calculateRandomStartSideCenter(float speed = 3.5f)
        {
            // Do nothing if we have no texture - this can happen if we're just generating the
            // level for information purposes (world and level menus)
            //
            if (m_textureSize == Vector2.zero)
                return;

            bool sideCheckFail = false;

            // Keep a coutner so we don't get stuck in this loop for too many iterations
            //
            int counter = 0;

            do
            {
                float randSide = Random.value;
                float shortSideFactor = ((float)Screen.height) / ((float)Screen.width);
                if (randSide < 0.25f)
                {
                    m_startSide = MoteStartSide.Top;
                    m_position.x = Screen.width / 2 - m_textureSize.x/ 2;
                    m_position.y = Screen.height;
                    m_accel.x = 0;
                    m_accel.y = -speed * shortSideFactor;
                    //Debug.Log("Creating mote Top y = " + m_accel.y);
                }
                else if (randSide < 0.5f)
                {
                    m_startSide = MoteStartSide.Bottom;
                    m_position.x = Screen.width / 2 - m_textureSize.x / 2;
                    m_position.y = -m_textureSize.y * 1.1f;
                    m_accel.x = 0;
                    m_accel.y = speed * shortSideFactor;
                    //Debug.Log("Creating mote Bottom y = " + m_accel.y);
                }
                else if (randSide < 0.75f)
                {
                    m_startSide = MoteStartSide.Left;
                    m_position.x = -m_textureSize.x* 1.5f;
                    m_position.y = Screen.height / 2 - m_textureSize.y / 2;
                    m_accel.x = speed;
                    m_accel.y = 0;
                    //Debug.Log("Creating mote Left");
                }
                else
                {
                    m_startSide = MoteStartSide.Right;
                    m_position.x = Screen.width + 5f;
                    m_position.y = Screen.height / 2 - m_textureSize.y/ 2;
                    m_accel.x = -speed;
                    m_accel.y = 0;
                    //Debug.Log("Creating mote Right");
                }


                // Check dictionary for type
                //
                Pair<System.Type, MoteStartSide> testPair = new Pair<System.Type, MoteStartSide>(this.GetType(), m_startSide);
                if (Mote.m_sideDict.ContainsKey(testPair))
                {
                    float lastTime = Mote.m_sideDict[testPair];

                    // Only run this loop again if we have set a regeneration step
                    //
                    if (m_regenerateSideGap > 0)
                    {
                        // This will block is there are too many motes and the gap is too long...
                        if (Time.time < lastTime + m_regenerateSideGap)
                        {
                            sideCheckFail = true;
#if GEN_DEBUG
                            Debug.Log("Failed side check counter for side " + m_sideDict.ToString() + " at time " + Time.time);
#endif

                        }
                    }
                }                

            } while (++counter < 10 && sideCheckFail);

            // Ensure we don't get stuck in the loop with the counter
            //

            // Only store for a non-zero gap
            //
            if (m_regenerateSideGap > 0)
            {
#if GEN_DEBUG
                Debug.Log("Adding to dictionary mote type = " + this.GetType() + " with side " + m_startSide.ToString() + " at time " + Time.time);
#endif
                Mote.m_sideDict[new Pair<System.Type, MoteStartSide>(this.GetType(), m_startSide)] = Time.time;
            }
#if GEN_DEBUG
            Debug.Log("Generating mote at side " + m_startSide.ToString() + " with position x = " + m_position.x + ", y = " + m_position.y + " at time " + Time.time);
#endif
        }

        /// <summary>
        /// Launch from a specific side - this is useful
        /// </summary>
        /// <param name="side"></param>
        /// <param name="aggressiveNess"></param>
        public void launchAtSide(MoteStartSide side, float aggressiveNess = 1.0f)
        {
            // Do nothing if we have no texture - this can happen if we're just generating the
            // level for information purposes (world and level menus)
            //
            if (m_textureSize == Vector2.zero)
                return;

            switch (side)
            {
                case MoteStartSide.Top:
                    m_position.x = Screen.width * (Random.value * 0.8f) + 0.1f;
                    m_position.y = Screen.height; // +m_texture.height * 1.5f;
                    m_accel.x = 0;
                    m_accel.y = -(Random.value + aggressiveNess);
                    break;

                case MoteStartSide.Bottom:
                    m_position.x = Screen.width * (Random.value * 0.8f) + 0.1f;
                    m_position.y = -m_textureSize.y * 1.1f;
                    m_accel.x = 0;
                    m_accel.y = Random.value + aggressiveNess;
                    break;

                case MoteStartSide.Left:
                    m_position.x = -m_textureSize.x * 1.5f;
                    m_position.y = Screen.height * (Random.value * 0.8f) + 0.1f;
                    m_accel.x = Random.value + aggressiveNess;
                    m_accel.y = 0;
                    break;

                case MoteStartSide.Right:
                    m_position.x = Screen.width + 5f;
                    m_position.y = Screen.height * (Random.value * 0.8f) + 0.1f;
                    m_accel.x = -(Random.value + aggressiveNess);
                    m_accel.y = 0;
                    break;

                default:
                    break;
            }

            m_startSide = side;
        }

        /// <summary>
        /// Generate random position outside of the viewpane
        /// </summary>
        /// <param name="aggressiveNess"></param>
        public void calculateRandomStartPositionAndVelocity(float aggressiveNess = 1.0f)
        {
            // Do nothing if we have no texture - this can happen if we're just generating the
            // level for information purposes (world and level menus)
            //
            if (m_textureSize == Vector2.zero)
                return;

            float randSide = Random.value;
            if (randSide < 0.25f)
            {
                m_startSide = MoteStartSide.Top;
                m_position.x = Screen.width * (Random.value * 0.8f) + 0.1f;
                m_position.y = Screen.height; // +m_texture.height * 1.5f;
                m_accel.x = 0;
                m_accel.y = -(Random.value + aggressiveNess);
            }
            else if (randSide < 0.5f)
            {
                m_startSide = MoteStartSide.Bottom;
                m_position.x = Screen.width * (Random.value * 0.8f) + 0.1f;
                m_position.y = -m_textureSize.y * 1.1f;
                m_accel.x = 0;
                m_accel.y = Random.value + aggressiveNess;
            }
            else if (randSide < 0.75f)
            {
                m_startSide = MoteStartSide.Left;
                m_position.x = -m_textureSize.x * 1.5f;
                m_position.y = Screen.height * (Random.value * 0.8f) + 0.1f;
                m_accel.x = Random.value + aggressiveNess;
                m_accel.y = 0;
            }
            else
            {
                m_startSide = MoteStartSide.Right;
                m_position.x = Screen.width + 5f;
                m_position.y = Screen.height * (Random.value * 0.8f) + 0.1f;
                m_accel.x = -(Random.value + aggressiveNess);
                m_accel.y = 0;
            }
        }

        /// <summary>
        /// Set accceleration away from side
        /// </summary>
        /// <param name="side"></param>
        protected void accelAwayFromSide(MoteStartSide side)
        {
            switch (side)
            {
                case MoteStartSide.Right:
                    m_accel.x = -Random.value * 2.0f;
                    m_accel.y = 0;
                    break;

                case MoteStartSide.Left:
                    m_accel.x = Random.value * 2.0f;
                    m_accel.y = 0;
                    break;

                case MoteStartSide.Top:
                    m_accel.y = -Random.value * 2.0f;
                    m_accel.x = 0;
                    break;

                case MoteStartSide.Bottom:
                    m_accel.y = Random.value * 2.0f;
                    m_accel.x = 0;
                    break;
            }

        }
        /// <summary>
        /// Acceleration on the current mote
        /// </summary>
        /// <returns></returns>
        public Vector2 getAcceleration()
        {
            return m_accel;
        }

        /// <summary>
        /// Set the acceleration
        /// </summary>
        /// <param name="accel"></param>
        /// <returns></returns>
        public void setAcceleration(Vector2 accel)
        {
            m_accel = accel;
        }

        /// <summary>
        /// Set mote position
        /// </summary>
        /// <param name="position"></param>
        public virtual void setPosition(Vector2 position)
        {
            m_position = position;
        }

        /// <summary>
        /// Get position
        /// </summary>
        /// <returns></returns>
        public Vector3 getPosition()
        {
            return m_position;
        }

        /// <summary>
        /// 2D vector
        /// </summary>
        /// <returns></returns>
        public Vector2 getPositionV2()
        {
            return new Vector2(m_position.x, m_position.y);
        }

        /// <summary>
        /// Actual texture position - adjusted for any inset
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 getInsetPosition()
        {
            return new Vector2(m_gameObject.guiTexture.pixelInset.x, m_gameObject.guiTexture.pixelInset.y);
        }

        /// <summary>
        /// Centre position of the inset texture
        /// </summary>
        /// <returns></returns>
        public Vector2 getInsetCentrePosition()
        {
            return new Vector2(m_gameObject.guiTexture.pixelInset.x + m_gameObject.guiTexture.pixelInset.width / 2, m_gameObject.guiTexture.pixelInset.y + m_gameObject.guiTexture.pixelInset.height / 2);
        }

        /// <summary>
        /// Get the screen rectangle
        /// </summary>
        /// <returns></returns>
        public Rect getScreenRect()
        {
            return m_gameObject.guiTexture.GetScreenRect();
        }

        /// <summary>
        /// Set a new texture on this mote and ensure sizing is correct
        /// </summary>
        /// <param name="newTexture"></param>
        public virtual void setTexture(Texture newTexture, bool useExistingPosition = false, float maxSize = 128.0f)
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

            //Debug.Log("Mote::setTexture() - TEXTURE WIDTH = " + width + ", HEIGHT = " + height);

            m_gameObject.guiTexture.texture = newTexture;
            m_originalAlpha = m_gameObject.guiTexture.color.a;

            // Set the pixel inset
            //
            if (useExistingPosition)
            {
                m_gameObject.guiTexture.pixelInset = new Rect(m_position.x - width / 2, m_position.y - height / 2, width, height);
            }
            else
            {
                // Use the existing pixelinset position
                Rect rect = m_gameObject.guiTexture.pixelInset;
                m_gameObject.guiTexture.pixelInset = new Rect(rect.center.x - width / 2, rect.center.y - height / 2, width, height);
            }
        }


        /// <summary>
        /// Test for hit on this object - we could do trig here if we wanted to be more accurate
        /// </summary>
        /// <returns></returns>
        public virtual bool contains(Vector2 touchPosition)
        {
            //Rect testRect = m_gameObject.guiTexture.pixelInset;
            return (m_gameObject.guiTexture.pixelInset.xMin <= touchPosition.x && m_gameObject.guiTexture.pixelInset.xMax >= touchPosition.x &&
                    m_gameObject.guiTexture.pixelInset.yMin <= touchPosition.y && m_gameObject.guiTexture.pixelInset.yMax >= touchPosition.y && m_gameObject.activeInHierarchy);
        }


        /// <summary>
        /// Does this mote interset the rect provided?
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public virtual bool intersects(Rect rect)
        {
            return (m_gameObject.guiTexture.pixelInset.xMax >= rect.xMin &&  m_gameObject.guiTexture.pixelInset.xMin <= rect.xMax &&
                    m_gameObject.guiTexture.pixelInset.yMax >= rect.yMin && m_gameObject.guiTexture.pixelInset.yMin <= rect.yMax);
        }

        /// <summary>
        /// Position of screen position of the more taking into account transform and inset - can be overriden
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 getScreenPosition()
        {
            return new Vector2(m_gameObject.guiTexture.transform.position.x * Screen.width, m_gameObject.guiTexture.transform.position.y * Screen.height) + getInsetPosition();
        }

        /// <summary>
        /// Texture width
        /// </summary>
        /// <returns></returns>
        public virtual float getWidth()
        {
            return m_gameObject.guiTexture.pixelInset.width;
        }

        /// <summary>
        /// Texture height
        /// </summary>
        /// <returns></returns>
        public virtual float getHeight()
        {
            return m_gameObject.guiTexture.pixelInset.height;
        }

        /// <summary>
        /// Hide this mote
        /// </summary>
        public void hideMote()
        {
            m_gameObject.SetActive(false);
        }

        /// <summary>
        /// Is this mote active?
        /// </summary>
        /// <returns></returns>
        public bool isActive()
        {
            return m_gameObject.activeInHierarchy;
        }

        /// <summary>
        /// Get gameobject
        /// </summary>
        public GameObject getGameObject()
        {
            return m_gameObject;
        }

        /// <summary>
        /// Set a GameObject
        /// </summary>
        /// <param name="gameObject"></param>
        public void setGameObject(GameObject gameObject)
        {
            m_gameObject = gameObject;
            m_originalAlpha = m_gameObject.guiTexture.color.a;
        }

        /// <summary>
        /// Get the worth of this mote
        /// </summary>
        /// <returns></returns>
        public int getWorth()
        {
            return m_worth;
        }

        /// <summary>
        /// Set the worth
        /// </summary>
        /// <param name="worth"></param>
        public void setWorth(int worth)
        {
            m_worth = worth;
        }

        /// <summary>
        /// Get generation weighting
        /// </summary>
        /// <returns></returns>
        public float getWeight()
        {
            return m_weight;
        }

        /// <summary>
        /// Set weighting
        /// </summary>
        /// <param name="weight"></param>
        public void setWeight(float weight)
        {
            m_weight = weight;
        }

        /// <summary>
        /// Original alpha
        /// </summary>
        /// <returns></returns>
        public float getOriginalAlpha()
        {
            return m_originalAlpha;
        }

        /// <summary>
        /// Is this mote dead?
        /// </summary>
        /// <returns></returns>
        public bool isDead()
        {
            return m_dead;
        }

        /// <summary>
        /// Set this mote to dead - only if we're not a hold mote (hold down to destroy)
        /// </summary>
        /// <param name="dead"></param>
        public void setDead(bool alwaysKill = false)
        {
			// Always kill overrides any other manner of death
			if (alwaysKill)
			{
				m_dead = true;
				return;
			}
			
            if (m_holdTimeToDestuction > 0)
            {
                // Set the eventual destruction time if not yet set
                //
                if (m_destructionTime == 0)
                {
                    m_destructionTime = Time.time + m_holdTimeToDestuction;
                    return;
                }

                if (Time.time < m_destructionTime)
                    return;
            }
            

            m_dead = true;
        }

        /// <summary>
        /// How long do we have to hold this mote down to destroy it?
        /// </summary>
        /// <param name="time"></param>
        public void setHoldTimeToDestruction(float time)
        {
            m_holdTimeToDestuction = time;
        }

        /// <summary>
        /// Called when we lift a touch on the mote - reset the timer
        /// </summary>
        public void resetDestructionTime()
        {
            m_destructionTime = 0.0f;
        }

        /// <summary>
        /// Return the destruction time
        /// </summary>
        /// <returns></returns>
        public float getDestructionTime()
        {
            return m_destructionTime;
        }

        /// <summary>
        /// Number of active motes allowed of this type - 0 is unlimited
        /// </summary>
        /// <returns></returns>
        public int getAliveLimit()
        {
            return m_aliveLimit;
        }

        /// <summary>
        /// Active motes allowed
        /// </summary>
        /// <param name="limit"></param>
        public void setAliveLimit(int limit)
        {
            m_aliveLimit = limit;
        }

        /// <summary>
        /// Is this a good mote?  Good motes don't kill the moteship.
        /// </summary>
        public bool isGoodMote()
        {
            return m_isGoodMote;
        }


        /// <summary>
        /// Set armed
        /// </summary>
        /// <param name="armed"></param>
        public void setArmed(bool armed)
        {
            m_armed = armed;
        }

        /// <summary>
        /// Is this mote armed?
        /// </summary>
        /// <returns></returns>
        public bool isArmed()
        {
            return m_armed;
        }

        /// <summary>
        /// Get the StartSide
        /// </summary>
        /// <returns></returns>
        public MoteStartSide getStartSide()
        {
            return m_startSide;
        }

        /// <summary>
        /// Add a pop texture to this mote
        /// </summary>
        /// <param name="texture"></param>
        public void addPopTexture(Texture texture)
        {
            m_popTextures.Add(texture);
        }

        /// <summary>
        /// Get list of pop textures
        /// </summary>
        /// <returns></returns>
        public List<Texture> getPopTextures()
        {
            return m_popTextures;
        }

        /// <summary>
        /// Original alpha value of the texture
        /// </summary>
        protected float m_originalAlpha = 0.5f;

        /// <summary>
        /// Score when we pop this Mote
        /// </summary>
        protected int m_worth = 0;

        /// <summary>
        /// Store texture size for positioning
        /// </summary>
        public Vector2 m_textureSize;

        /// <summary>
        /// Weighting for generation
        /// </summary>
        protected float m_weight = 1.0f;

        /// <summary>
        /// Does this mote generate off screen initially
        /// </summary>
        protected bool m_startOffScreen = false;

        /// <summary>
        /// Define a number of animation frames per mote type
        /// </summary>
        static public int AnimationFrames;

        /// <summary>
        /// Acceleration vector
        /// </summary>
        protected Vector2 m_accel = new Vector2();

        /// <summary>
        /// Mote position
        /// </summary>
        protected Vector3 m_position;

        /// <summary>
        /// Mote GameObject holding texture
        /// </summary>
        protected GameObject m_gameObject;

        /// <summary>
        /// Starting side for offscreen mote
        /// </summary>
        protected MoteStartSide m_startSide = MoteStartSide.None;

        /// <summary>
        /// Mark this mote as dead and ready to be removed
        /// </summary>
        protected bool m_dead = false;

        /// <summary>
        /// Alive limit is the number of active motes of this type we're allowed
        /// at one time.
        /// </summary>
        protected int m_aliveLimit = 0;

        /// <summary>
        /// We use this gap to specify if we want to have gaps between motes being generated/place on the
        /// same side within a certain time period.  For example the SycamoreMote is placed at the centre of
        /// each side so we want to avoid overlapping motes.
        /// </summary>
        protected float m_regenerateSideGap = 0.0f;

        /// <summary>
        /// Dictionary of when the last 
        /// </summary>
        static protected Dictionary<Pair<System.Type, MoteStartSide>, float> m_sideDict = new Dictionary<Pair<System.Type, MoteStartSide>, float>();

        /// <summary>
        /// Is this a good mote?
        /// </summary>
        protected bool m_isGoodMote = false;

        /// <summary>
        /// For GoodMotes we need to find out if it's armed or not
        /// </summary>
        protected bool m_armed = false;

        /// <summary>
        /// List of pop textures in descending order (least popped first)
        /// </summary>
        List<Texture> m_popTextures = new List<Texture>();

        /// <summary>
        /// How long to hold this mote before destruction
        /// </summary>
        protected float m_holdTimeToDestuction = 0.0f;

        /// <summary>
        /// Start time at which destruction is tested
        /// </summary>
        protected float m_destructionTime = 0.0f;

        /// <summary>
        /// How long after popping accept do we actually destroy ourselves?
        /// </summary>
        protected float m_timeToDestruction = 0.0f;

    }
}