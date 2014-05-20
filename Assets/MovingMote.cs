using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class MovingMote : Mote
    {
        /// <summary>
        /// Some movement modes
        /// </summary>
        public enum MoveMethod
        {
            DefaultDrift,
            Circular,
            CircularDrift,
            Elliptical,
            Wiggle,
            Figure8,
            Line,
            BalloonDrift
        }

        public MovingMote(GameObject gameObject, MoveMethod method)
            : base(gameObject)
        {
            m_moveMethod = method;
        }

        public MovingMote(float weight, int worth, bool startOffScreen, MoveMethod method)
            :base(weight, worth, startOffScreen)
        {
            m_moveMethod = method;
            m_worth = worth;
        }

        /// <summary>
        /// Set and adjust
        /// </summary>
        /// <param name="position"></param>
        public MovingMote(int worth, Vector3 position, MoveMethod method)
        {
            m_position = position;
            m_moveMethod = method;
            m_worth = worth;
        }

        /*
        /// <summary>
        /// Initial values for the various modes
        /// </summary>
        protected void initialSetup()
        {
            switch (m_moveMethod)
            {

            }
        }*/


        /// <summary>
        /// How fast our radius expands
        /// </summary>
        protected float m_radiusExpansion = 0.3f;

        /// <summary>
        /// How fast the period changes
        /// </summary>
        protected float m_periodExpansion = 0.1f;


        /// <summary>
        /// Elliptical move
        /// </summary>
        protected void doEllipticalMove(bool figure8 = false)
        {
            // Increment the angle by the framerate and period
            //
            m_angle += 2 * Mathf.PI * m_rotationPeriod / m_frameRate;

            if (m_currentRadius < m_maxRadius)
            {
                m_currentRadius += m_radiusExpansion;
            }

            float random = Random.value;
            if (random > 0.7f)
            {
                m_currentRadius = Mathf.Max(m_currentRadius - m_radiusExpansion, 2.0f);
            }
            else if (random > 0.4f)
            {
                m_currentRadius = Mathf.Max(m_currentRadius - m_radiusExpansion / 2, 2.0f);
            }

            random = Random.value;
            if (random > 0.9f)
            {
                m_rotationPeriod += m_periodExpansion;
            }
            else if (random < 0.1f)
            {
                m_rotationPeriod -= m_periodExpansion;
            }

            // Buffers for rotation
            //
            if (m_rotationPeriod > 2.0f)
                m_rotationPeriod = 2.0f;

            if (m_rotationPeriod < -2.0f)
                m_rotationPeriod = -2.0f;

            // Do a movement relative to the original position
            //
            m_moveRect = m_originalMoveRect;
            m_moveRect.x += m_currentRadius * Mathf.Cos(m_angle);

            if (figure8)
                m_moveRect.y += m_currentRadius * Mathf.Sin(m_angle + Mathf.PI);
            else
                m_moveRect.y += m_currentRadius * Mathf.Sin(m_angle);

            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }


        /// <summary>
        /// Circular move
        /// </summary>
        /// <param name="figure8"></param>
        protected void doCircularMove(bool figure8 = false)
        {
            // Increment the angle by the framerate and period
            //
            m_angle += 2 * Mathf.PI * m_rotationPeriod / m_frameRate;

            if (m_currentRadius < m_maxRadius)
            {
                m_currentRadius += 0.1f;
            }

            float random = Random.value;
            if (random > 0.7f)
            {
                m_currentRadius = Mathf.Max(m_currentRadius - 0.1f, 2.0f);
            }
            else if (random > 0.4f)
            {
                m_currentRadius = Mathf.Max(m_currentRadius - 0.05f, 2.0f);
            }

            random = Random.value;
            if (random > 0.9f)
            {
                m_rotationPeriod += 0.1f;
            }
            else if (random < 0.1f)
            {
                m_rotationPeriod -= 0.1f;
            }

            // Buffers for rotation
            //
            if (m_rotationPeriod > 2.0f)
                m_rotationPeriod = 2.0f;

            if (m_rotationPeriod < -2.0f)
                m_rotationPeriod = -2.0f;

            // Do a movement relative to the original position
            //
            m_moveRect = m_originalMoveRect;
            m_moveRect.x += m_currentRadius * Mathf.Cos(m_angle);


            if (figure8)
                m_moveRect.y += m_currentRadius * Mathf.Sin(m_angle + Mathf.PI);
            else
                m_moveRect.y += m_currentRadius * Mathf.Sin(m_angle);

            m_gameObject.guiTexture.pixelInset = m_moveRect;

        }

        /// <summary>
        /// Wiggle move is for bees
        /// </summary>
        protected void doWiggleMove()
        {
            //Debug.Log("MovingMote::doWiggleMove()");

            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_originalMoveRect;

            // Randomise the accel
            //
            m_accel.x += (Random.value - 0.5f) / 10.0f;
            m_accel.y += (Random.value - 0.5f) / 10.0f;

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;

            //Debug.Log("Position x = " + m_moveRect.x + ", y = " + m_moveRect.y + ", w = " + m_moveRect.width + ", h = " + m_moveRect.height);

            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }


        /// <summary>
        /// When do move is called we want a small movement in some direction - the direction will be managed
        /// by this class according to what type of movement we want.
        /// </summary>
        public override void doMove()
        {
            // Set the framerate
            //
            if (m_frameRate == 0.0f)
            {
                // First time through
                //
                if (m_lastFrameTime == 0.0f)
                {
                    m_lastFrameTime = Time.time;

                    if (m_gameObject.guiTexture != null)
                        m_originalMoveRect = m_gameObject.guiTexture.pixelInset;

                    return;
                }
            }

            // Get frame rate and reset counter
            //
            m_frameRate = 1.0f / (Time.time - m_lastFrameTime);
            m_lastFrameTime = Time.time;

            switch (m_moveMethod)
            {
                case MoveMethod.DefaultDrift:
                    doDefaultDrift();
                    break;

                case MoveMethod.BalloonDrift:
                    doBalloonDrift();
                    break;

                case MoveMethod.Circular:
                    doCircularMove();
                    break;

                case MoveMethod.CircularDrift:
                    // Set up some different values for drift
                    //
                    m_maxRadius = 5.0f;
                    m_rotationPeriod = 0.4f;
                    doCircularMove();
                    break;

                case MoveMethod.Elliptical:
                    doEllipticalMove();
                    break;

                case MoveMethod.Wiggle:
                    doWiggleMove();
                    break;

                    // This doesn't really work as a figure 8
                    //
                case MoveMethod.Figure8:
                    m_maxRadius = 20.0f;
                    m_rotationPeriod = 0.5f;
                    doCircularMove(true);
                    break;

                    // Straight linear movement
                    //
                case MoveMethod.Line:
                    m_moveRect = m_gameObject.guiTexture.pixelInset;
                    m_moveRect.x += m_accel.x;
                    m_moveRect.y += m_accel.y;
                    m_gameObject.guiTexture.pixelInset = m_moveRect;
                    break;

                default:
                    Debug.Log("MovingMote() - doMove() - unhandled movement type");
                    break;
            }
        }

        /// <summary>
        /// MoveMethod
        /// </summary>
        /// <returns></returns>
        public MoveMethod getMovementMethod()
        {
            return m_moveMethod;
        }

        /// <summary>
        /// Lazily drift upwards
        /// </summary>
        protected void doBalloonDrift()
        {
            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;
            m_moveRect = m_gameObject.guiTexture.pixelInset;

            // Adjust accelerations
            //
            m_accel.y *= m_accel.y; // this is our gravity

            // Movement
            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }

        /// <summary>
        /// Default drift movement - can be overridden
        /// </summary>
        protected virtual void doDefaultDrift()
        {
            // Normalise the steps to a 60FPS - if we drop below then the movement is larger
            //
            float normaliseMovement = 60.0f * Time.smoothDeltaTime;

            m_moveRect = m_gameObject.guiTexture.pixelInset;

            float random = Random.value;
            if (random < 0.25f)
                m_accel.x += m_step * normaliseMovement;
            else if (random < 0.5f)
                m_accel.y += m_step * normaliseMovement;
            else if (random < 0.75f)
                m_accel.x -= m_step * normaliseMovement;
            else
                m_accel.y -= m_step * normaliseMovement;

            m_moveRect.x += m_accel.x * normaliseMovement;
            m_moveRect.y += m_accel.y * normaliseMovement;
            m_gameObject.guiTexture.pixelInset = m_moveRect;
        }


        /// <summary>
        /// Acceleration step per frame
        /// </summary>
        protected float m_step = 0.1f;


        /// <summary>
        /// Original position of moverect
        /// </summary>
        protected Rect m_originalMoveRect;

        /// <summary>
        /// Preallocate movement rectangle
        /// </summary>
        protected Rect m_moveRect = new Rect();

        /// <summary>
        /// Store the movement method
        /// </summary>
        protected MoveMethod m_moveMethod;

        /// <summary>
        /// Maximum radius from original point
        /// </summary>
        protected float m_maxRadius = 20.0f;

        /// <summary>
        /// Current radius from initial position
        /// </summary>
        protected float m_currentRadius = 0.0f;

        /// <summary>
        /// Period of rotation in seconds
        /// </summary>
        protected float m_rotationPeriod = 0.5f;

        /// <summary>
        /// Current rotation in rads
        /// </summary>
        protected float m_angle = 0.0f;

        /// <summary>
        /// Frame rate of how often movement is called
        /// </summary>
        protected float m_frameRate = 0.0f;

        /// <summary>
        /// Last frame time
        /// </summary>
        protected float m_lastFrameTime = 0.0f;

    }

}