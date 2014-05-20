using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class MoteHud
    {
        /// <summary>
        /// One off constructor as we realise this as static in the MoteManager - we need to reset a lot
        /// of references in the initialise.
        /// </summary>
        public MoteHud(Texture defaultText, Texture nofxText, Texture nomusicText, Texture muteText, GameObject hudGameObject)
        {
            // Initialise the positions and final positions according to screen size
            //
            m_hudTextureNoFx = nofxText;
            m_hudTextureNoMusic = nomusicText;
            m_hudTextureDefault = defaultText;
            m_hudTextureMuted = muteText;
            m_hudControlGameObject = hudGameObject;
        }

        /// <summary>
        /// Get a movement vector in the default outgoing direction (negate for incoming)
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public Vector2 getHudMoveVector(float period)
        {
            return (getHudMoveEndPoint() - getHudMoveStartPoint()) / period;
        }

        /// <summary>
        /// Start of hud move
        /// </summary>
        /// <returns></returns>
        public Vector2 getHudMoveStartPoint()
        {
            return new Vector2(m_hudControlInactive.x, m_hudControlInactive.y);
        }

        /// <summary>
        /// End of hud move
        /// </summary>
        /// <returns></returns>
        public Vector2 getHudMoveEndPoint()
        {
            return new Vector2(m_hudControlActive.x, m_hudControlActive.y);
        }

        /// <summary>
        /// Turn off and on some GameObjects
        /// </summary>
        /// <param name="activity"></param>
        public void setGameObjectsActivity(bool activity)
        {
            m_scoreText.gameObject.SetActive(activity);
            m_levelDetails.gameObject.SetActive(activity);
            m_hudControlGameObject.SetActive(activity);
        }
		
		protected float m_fontScaleBasis = 200.0f;
			
        /// <summary>
        /// Set the score, lives and level information according to level type
        /// </summary>
        /// <param name="score"></param>
        public void setHud(int score, int totalHighScore, int levelHighScore, int lives, Level gameLevel)
        {
            // Activate if not active
            if (!m_scoreText.gameObject.activeInHierarchy)
                m_scoreText.gameObject.SetActive(true);

            if (!m_levelDetails.gameObject.activeInHierarchy)
                m_levelDetails.gameObject.SetActive(true);

            if (!m_hudControlGameObject.activeInHierarchy)
                m_hudControlGameObject.SetActive(false);

            string addString = "";
            switch(gameLevel.getType())
            {
                case LevelType.ProtectTime:
                    // Only show if intro countdown has completed and we're still above zero
                    //
                    if (gameLevel.getRemainTime() > 0 && gameLevel.getRemainTime() <= gameLevel.getTimeLimit())
                    {
                        addString = "Time : " + (gameLevel.getRemainTime()).ToString("0");
                    }
                    break;

                case LevelType.MotesTimeLimit:
                    // Only show if intro countdown has completed and we're still above zero
                    //
                    if (gameLevel.getRemainTime() > 0 && gameLevel.getRemainTime() <= gameLevel.getTimeLimit())
                    {
                        addString = "Time : " + (gameLevel.getRemainTime()).ToString("0");
                        addString += "\nMotes left : " + gameLevel.getMotesYetToDestroy();
                    }
                    break;

                case LevelType.MotesTapLimit:
                    addString = "Taps : " + gameLevel.getRemainTaps().ToString("0");
                    addString += "\nMotes left: " + gameLevel.getMotesYetToDestroy();
                    break;

                case LevelType.ProtectArea:
                    break;

                default:
                    break;
            }

            m_levelDetails.guiText.text = addString;
            string fpsString = ""; // "FPS:" + (1.0f / Time.smoothDeltaTime).ToString("0");
			
            m_scoreText.guiText.text = "Score: " + score + "\nHigh Score: " + totalHighScore + "\nLevel Record: " + levelHighScore + "\nLives: " + lives + "\n" + fpsString + m_debugScoreText; // addString;
			
			
        }


        /// <summary>
        /// Set the hud controls texture according to state of the player controls
        /// </summary>
        public void setHudControls(bool music, bool soundfx)
        {
            if (!music && !soundfx)
                m_hudControlGameObject.guiTexture.texture = m_hudTextureMuted;
            else if (!music)
                m_hudControlGameObject.guiTexture.texture = m_hudTextureNoMusic;
            else if (!soundfx)
                m_hudControlGameObject.guiTexture.texture = m_hudTextureNoFx;
            else
                m_hudControlGameObject.guiTexture.texture = m_hudTextureDefault;
        }
		
		/// <summary>
		/// Dos the rescale.
		/// </summary>
		/// <returns>
		/// The rescale.
		/// </returns>
		public float doRescale()
		{
			return Mathf.Max(1, Screen.width / 650.0f);
		}
			

        /// <summary>
        /// Initialise any subobject
        /// </summary>
        public void initialise(GameObject hudControlObject)
        {	
			float rescale = doRescale();

			// Must scale first and then position
			//
			Rect hudRect = m_hudControlGameObject.guiTexture.pixelInset;

            hudControlObject.guiTexture.pixelInset =
				new Rect(-Screen.width / 2 - hudRect.width * 0.7f,
						 -Screen.height / 2 - hudRect.height * 0.7f,
						 hudRect.width,
						 hudRect.height);

            // Set this to something meaningful but bear in mind this is a vector over which we initially
            //
			//float factor = 1.0f; //900.0f / Screen.width;
            m_hudControlActive = new Rect(
				-Screen.width / 2 - hudControlObject.guiTexture.pixelInset.width * 0.5f,
				-Screen.height / 2 - hudControlObject.guiTexture.pixelInset.height * 0.5f,
				hudControlObject.guiTexture.pixelInset.width,
				hudControlObject.guiTexture.pixelInset.height);

            // Original position of the HUD control
            //
            m_hudControlInactive = hudControlObject.guiTexture.pixelInset;

            // Get these objects
            //
            m_scoreText = GameObject.FindWithTag("PlayerScore");
            m_levelDetails = GameObject.FindWithTag("LevelDetails");

            // Store the font size
            //
            m_originalFontSize = m_scoreText.guiText.fontSize;
				
			m_scoreText.guiText.fontSize = (int)(((float)m_scoreText.guiText.fontSize) * rescale);
			m_levelDetails.guiText.fontSize = (int)(((float)m_levelDetails.guiText.fontSize) * rescale);
			
			// Scale font by screen size
			//m_scoreText.guiText.fontSize = (int)((float)m_scoreText.guiText.fontSize * (Screen.width / m_fontScaleBasis));
		
            //m_levelDetails.transform.position = new Vector3(0.2f, 0.2f);
            //m_levelDetails.transform.localScale = new Vector3(0.21f, 0.1f, 0.0f);
            //m_levelDetails.guiTexture.pixelInset = new Rect(0, 0, 0, 0);

            // Disable initially
            //
            m_scoreText.gameObject.SetActive(false);
            m_levelDetails.gameObject.SetActive(false);
            m_hudControlGameObject.SetActive(false);
        }

        /// <summary>
        /// Set the final resting point of the control
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public void setHudControlPositionActive(Rect rect)
        {
            m_hudControlActive = rect;
        }


        /// <summary>
        /// Gameobject for hud control
        /// </summary>
        protected GameObject m_hudControlGameObject;

        /// <summary>
        /// HUD control position in active mode
        /// </summary>
        /// <returns></returns>
        public Rect getHudControlPositionActive() { return m_hudControlActive; }

        /// <summary>
        /// HUD control position in non-active mode
        /// </summary>
        /// <returns></returns>
        public Rect getHudControlPositionInactive() { return m_hudControlInactive; }

        /// <summary>
        /// Is the HUD control active?
        /// </summary>
        /// <returns></returns>
        public bool getHudControlActive() { return m_hudControlStatus; }

        /// <summary>
        /// Set the status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public void setHudControlActive(bool status) { m_hudControlStatus = status; m_hudControlAnimating = true;  }

        /// <summary>
        /// Set status of animation
        /// </summary>
        /// <param name="status"></param>
        public void setHudControlAnimating(bool status) { m_hudControlAnimating = status; }

        /// <summary>
        /// Status of animation
        /// </summary>
        /// <returns></returns>
        public bool isHudControlAnimating() { return m_hudControlAnimating; }

        /// <summary>
        /// Initial font size when we load the game
        /// </summary>
        protected int m_originalFontSize = 0;

        /// <summary>
        /// Developer scale of screen used for scaling all of HUD
        /// </summary>
        protected Vector2 m_developerScale = new Vector2(838, 371);

        /// <summary>
        /// Score text object
        /// </summary>
        protected GameObject m_scoreText;

        /// <summary>
        /// Level details
        /// </summary>
        protected GameObject m_levelDetails;

        /// <summary>
        /// Score outline box
        /// </summary>
        protected GameObject m_scoreBox;

        /// <summary>
        /// Active final position of HUD control
        /// </summary>
        protected Rect m_hudControlActive;

        /// <summary>
        /// Inactive point of HUD control
        /// </summary>
        protected Rect m_hudControlInactive;

        /// <summary>
        /// Is the hud control active on the screen yet - includes being animated to point.
        /// </summary>
        protected bool m_hudControlStatus = false;

        /// <summary>
        /// Used as internal flag on whether to animate the control between states
        /// </summary>
        protected bool m_hudControlAnimating = false;


        /// <summary>
        /// No fx
        /// </summary>
        protected Texture m_hudTextureNoFx;

        /// <summary>
        /// No Music
        /// </summary>
        protected Texture m_hudTextureNoMusic;

        /// <summary>
        /// Muted
        /// </summary>
        protected Texture m_hudTextureMuted;

        /// <summary>
        /// Default texture
        /// </summary>
        protected Texture m_hudTextureDefault;
		
		/// <summary>
		/// The m_debug score text.
		/// </summary>
		protected string m_debugScoreText = "";
    }
}