using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
using Xyglo.Unity;

/// <summary>
/// State of the WorldScene
/// </summary>
public enum WorldSceneState
{
    Interstitial,
    Paused,
    Playing,
    PostLevelSummary,
    DeathScreen,
    GameCompleted
}

/// <summary>
/// Shaking statuses
/// </summary>
public enum ShakingStatus
{
    Bonus1000,
    Bonus2000,
    Bonus5000,
    BonusLife
};

/// <summary>
/// WorldScene is an attempt at a generic version of the WorldScene - we have one WorldScene per level
/// </summary>
[System.Serializable]
public class WorldScene : MonoBehaviour
{
    /// <summary>
    /// Is this version the Lite version?
    /// </summary>
    static public bool m_isLite = false;

    /// <summary>
    /// Enable or disable twitter
    /// </summary>
    ///
    static bool m_twitter = false;

    /// <summary>
    /// Enable all levels even if we've not won them - for debugging
    /// </summary>
    static public bool m_debugLevels = true;

    /// <summary>
    /// Enable Save for GameState - always have this enabled for real games otherwise there might be some angry customers
    /// </summary>
    static public bool m_saveGameState = true;

    /// <summary>
    /// Difficulty level - increases once we've completed the whole game once
    /// </summary>
    //static public int m_difficultyLevel = 1;

    /// <summary>
    /// Which scene is the first - our base value for all worlds
    /// </summary>
    static public int WorldSceneBase = 5;

    /// <summary>
    /// List of active motes - make this static because you never know how many instances you're going to get
    /// of various things.
    /// </summary>
    static protected List<Mote> m_moteList;

    /// <summary>
    /// Transient graphic elements which will animate and time out
    /// </summary>
    static protected List<Transient> m_transientList;

    /// <summary>
    /// Initialise a static LevelManager
    /// </summary>
    protected LevelManager m_levelManager = null;

    /// <summary>
    /// Set the current world for the game as selected from the level screen or
    /// as we progress
    /// </summary>
    static public int CurrentWorld = 0;

    /// <summary>
    /// We can set this externally and set CurrentWorld to -2 to jump to level
    /// </summary>
    static public int JumpToLevel = -1;

    /// <summary>
    /// Simple Static Mote
    /// </summary>
    public Texture StaticMoteTexture;

    /// <summary>
    /// Moves around
    /// </summary>
    public Texture MovingMoteTexture;

    /// <summary>
    /// Fluffy mote
    /// </summary>
    public Texture FluffyMoteTexture;

    /// <summary>
    /// Chick mote
    /// </summary>
    public Texture ChickMoteTexture;

    /// <summary>
    /// Bee mote
    /// </summary>
    public Texture BeeMoteTexture;

    /// <summary>
    /// Mosquito
    /// </summary>
    public Texture MosquitoMoteTexture;

    /// <summary>
    /// Cell
    /// </summary>
    public Texture CellMoteTexture;

    /// <summary>
    /// An acorn texture
    /// </summary>
    public Texture AcornMoteTexture;

    /// <summary>
    /// A mushroom texture
    /// </summary>
    public Texture MushroomMoteTexture;

    /// <summary>
    /// A syncamore texture
    /// </summary>
    public Texture SycamoreMoteTexture;

    /// <summary>
    /// Fluffy mote initial pop texture
    /// </summary>
    public Texture FluffyMoteTexture_InitialPop;

    /// <summary>
    /// Fluffy mote final pop texture
    /// </summary>
    public Texture FluffyMoteTexture_FinalPop;

    /// <summary>
    /// Evades your taps and wastes them
    /// </summary>
    public Texture EvasiveMoteTexture;

    /// <summary>
    /// Generic mote for a level
    /// </summary>
    public Texture GenericMoteTexture;

    /// <summary>
    /// Red balloon
    /// </summary>
    public Texture RedBalloonTexture;

    /// <summary>
    /// Yellow balloon
    /// </summary>
    public Texture YellowBalloonTexture;

    /// <summary>
    /// Green balloon
    /// </summary>
    public Texture GreenBalloonTexture;

    /// <summary>
    /// Blue balloon
    /// </summary>
    public Texture BlueBalloonTexture;

    /// <summary>
    /// Pink balloon
    /// </summary>
    public Texture PinkBalloonTexture;
    
    /// <summary>
    /// Shrink Mote
    /// </summary>
    public Texture PurpleBalloonTexture;

    /// <summary>
    /// This Mote multiplies when tapped
    /// </summary>
    public Texture SporeMoteTexture;

    /// <summary>
    /// Spote more 2nd generation
    /// </summary>
    public Texture SporeMoteTextureGen2;

    /// <summary>
    /// Glue fluffy mote texture
    /// </summary>
    public Texture GlueFluffyMoteTexture;

    /// <summary>
    /// Default pop
    /// </summary>
    public Texture DefaultPopTexture;

    /// <summary>
    /// Grenade power up
    /// </summary>
    public Texture GrenadeTexture;

    /// <summary>
    /// Shield Texture
    /// </summary>
    public Texture ShieldTexture;

    /// <summary>
    /// Invincibility Texture
    /// </summary>
    public Texture InvincibilityTexture;

    /// <summary>
    /// Egg mote texture
    /// </summary>
    public Texture EggMoteTexture;

    /// <summary>
    /// NO fx
    /// </summary>
    public Texture NoFxHudTexture;

    /// <summary>
    /// No Music
    /// </summary>
    public Texture NoMusicHudTexture;

    /// <summary>
    /// Butterfly open
    /// </summary>
    public Texture ButterflyOpenTexture;

    /// <summary>
    /// Butterfly closed
    /// </summary>
    public Texture ButterflyClosedTexture;

    /// <summary>
    /// Normal squishmote
    /// </summary>
    public Texture SquishMoteTexture;

    /// <summary>
    /// Snake
    /// </summary>
    public Texture SnakeMoteTexture;

    /// <summary>
    /// Heli mote
    /// </summary>
    public Texture HeliMoteTexture;

    /// <summary>
    /// Pine cone
    /// </summary>
    public Texture PineConeTexture;

    /// <summary>
    /// Alarmed squishmote
    /// </summary>
    public Texture SquishMoteAlarmedTexture;

    /// <summary>
    /// Muted
    /// </summary>
    public Texture MutedHudTexture;

    /// <summary>
    /// Level completion sound
    /// </summary>
    public AudioClip LevelCompleteClip;

    /// <summary>
    /// Mote pop sound
    /// </summary>
    public AudioClip MotePop;

    /// <summary>
    /// Shield hum
    /// </summary>
    public AudioClip MoteShipShieldNoise;

    /// <summary>
    /// Grenade
    /// </summary>
    public AudioClip GrenadeNoise;

    /// <summary>
    /// Death of the ship
    /// </summary>
    public AudioClip DeathKnell;

    /// <summary>
    /// Buzzing Alarm
    /// </summary>
    public AudioClip BuzzingAlarm;

    /// <summary>
    /// WowAlarm
    /// </summary>
    public AudioClip WowAlarm;

    /// <summary>
    /// Alternative music for a different level
    /// </summary>
    public AudioClip Level2Music;

    /// <summary>
    /// Failure music for game
    /// </summary>
    /// 
    public AudioClip UnhappyCompletion;


    // LEO Textures

    public Texture HeartOfGoldTexture;

    public Texture SputnikTexture;

    public Texture ISSTexture;

    public Texture SatelliteTexture;

    public Texture NorthernLightsTexture;

    public Texture LegoManTexture;

    public Texture DragonCapsuleTexture;

    public Texture ICBMTexture;

    public Texture SatelliteDishTexture;

    public Texture DarkStarAlienTexture;

    public Texture TinaTurnerTexture;

    public Texture MoteBossTexture;

    // MoteWorld Textures

    public Texture CometTexture;

    public Texture BlackHoleTexture;

    // -----------------------------------
    // Now some power ups
    //
    /// <summary>
    /// Timer to explode a trap
    /// </summary>
    public Texture TimerTrap;

    /// <summary>
    /// Shotgun power up - maybe wide spread
    /// </summary>
    public Texture Shotgun;

    /// <summary>
    /// Initial moteship
    /// </summary>
    public Texture MoteShip;

    /// <summary>
    /// Mote ship with shield
    /// </summary>
    public Texture MoteShipShield;


    /// <summary>
    /// Interstital texture for this level
    /// </summary>
    public Texture m_interstitialTexture;

    public AudioClip m_interstitialAudioClip;
    ///
    /// ------------------------------------------
    /// 

    /// <summary>
    /// The m_movatron.
    /// </summary>
    protected Movatron m_movatron = new Movatron();

    /// <summary>
    /// Is this level completed?
    /// </summary>
    protected bool m_levelComplete = false;

    /// <summary>
    /// Time to wait after level completes
    /// </summary>
    protected float m_levelCompletionPause = 1.5f;

    /// <summary>
    /// Level start time
    /// </summary>
    protected float m_startTime;

    /// <summary>
    /// How many lives at the start of the level?  For a bonus if no death.
    /// </summary>
    protected int m_livesAtStart = -1;

    /// <summary>
    /// Bonus for a clean level
    /// </summary>
    protected int m_cleanLevelBonus = 1000;

    /// <summary>
    /// Level complete time
    /// </summary>
    protected float m_completeTime;

    /// <summary>
    /// Completion message object
    /// </summary>
    protected GameObject m_completionMessage;

    /// <summary>
    /// Level start message
    /// </summary>
    protected GameObject m_welcomeMessage;

    /// <summary>
    /// Level hint message
    /// </summary>
    protected GameObject m_hintMessage;

    /// <summary>
    /// Interstitial title message
    /// </summary>
    protected GameObject m_interstitialTitleMessage;

    /// <summary>
    /// Introduction message
    /// </summary>
    protected GameObject m_interstitialIntroductionMessage;

    /// <summary>
    /// Interstitial start time
    /// </summary>
    protected float m_interstitialStart = 0;

    /// <summary>
    /// Length of interstitial
    /// </summary>
    protected float m_interstitialLength = 6.0f; // 6.0f

    /// <summary>
    /// Initial inset
    /// </summary>
    protected Rect m_initialInset = new Rect(0, 0, 0, 0);

    /// <summary>
    /// Hud object
    /// </summary>
    static protected MoteHud m_moteHud;

    /// <summary>
    /// Player object
    /// </summary>
    static public Player m_player;

    /// <summary>
    /// Background texture
    /// </summary>
    protected GameObject m_backgroundObject;

    /// <summary>
    /// Interstitial object
    /// </summary>
    protected GameObject m_interstitialObject;

    /// <summary>
    /// Level details GameObject holds the AudioSource
    /// </summary>
    protected GameObject m_levelDetails;

    /// <summary>
    /// Keep this hint restart position
    /// </summary>
    protected Vector2 m_hintRestartPoint;

    /// <summary>
    /// Welcome restart position
    /// </summary>
    protected Vector2 m_welcomeRestartPoint;

    /// <summary>
    /// Slide message restarting point
    /// </summary>
    protected Vector2 m_slideMessageRestartPoint;

    /// <summary>
    /// Hud control that pops out from the left
    /// </summary>
    protected GameObject m_hudControl;

    /// <summary>
    /// Our MoteShip
    /// </summary>
    protected MoteShip m_moteShip;

    /// <summary>
    /// Track how many touches we have
    /// </summary>
    protected int m_touchCount = 0;

    /// <summary>
    /// We have to cheat I'm afraid as I can't work out this bug
    /// </summary>
    protected Vector2 m_cheatEndPoint = Vector2.zero;

    /// <summary>
    /// Hud animation start time
    /// </summary>
    float m_hudStartTime = 0.0f;

    /// <summary>
    /// Background movement vector
    /// </summary>
    protected Vector2 m_backgroundVector = new Vector2(0, 0);

    /// <summary>
    /// Time to wait before level generation starts
    /// </summary>
    protected float m_levelGenerationPause = 5.0f;

    /// <summary>
    /// New record on this level?
    /// </summary>
    protected LevelScoreIdentifier m_medal = LevelScoreIdentifier.None;

    /// <summary>
    /// If we're starting a world then we need to introduce it
    /// </summary>
    //protected bool m_worldStart = false;

    /// <summary>
    /// The achievement centre
    /// </summary>
    protected AchievementCentre m_achievementCentre = new AchievementCentre();

    /// <summary>
    /// Position of last pop
    /// </summary>
    protected Vector2 m_lastPopPosition = new Vector2();

    /// <summary>
    /// Last pop time
    /// </summary>
    protected float m_lastPopTimeNotify = 0.0f;

    /// <summary>
    /// Run the ending screen
    /// </summary>
    protected bool m_doEnding = false;

    /// <summary>
    /// Transient Manager
    /// </summary>
    protected TransientManager m_transientManager = null;

    /// <summary>
    /// Boss texture manager
    /// </summary>
    protected BossManager m_bossManager = null;

    /// <summary>
    /// Storage for mote ship icons
    /// </summary>
    protected List<GameObject> m_lifeObjects = new List<GameObject>();

	/// <summary>
	/// The m_last shake transient.
	/// </summary>
	protected float m_lastShakeTransient = 0.0f;
	
	/// <summary>
	/// The duration of the m_shake transient.
	/// </summary>
	protected float m_shakeTransientDuration = 0.5f;

#if FACEBOOK_ENABLED
        /// <summary>
        /// Facebook button
        /// </summary>
        protected GameObject m_facebookButton;
#endif

    /// <summary>
    /// Twitter button
    /// </summary>
    protected GameObject m_twitterButton;


    /// <summary>
    /// What state is the WorldScene in?
    /// </summary>
    protected WorldSceneState m_state;

    /// <summary>
    /// Set current shaking rewards
    /// </summary>
    protected ShakingStatus m_shakingStatus = ShakingStatus.Bonus1000;

    /// <summary>
    /// On appl quit here
    /// </summary>
    void OnApplicationQuit()
    {
        // Save the player prefs
        //
        saveGameState();
    }

    /// <summary>
    /// Load our prefs
    /// </summary>
    protected void loadGameState()
    {
        Debug.Log("loadGameState() - loading");

        // Fetch some stored values
        //
        m_player.setHighScore(XygloPlayerPrefs.GetInt("MWsst", 0));

        // Always at least one world unlocked
        //
        m_player.m_worldsUnlocked = Mathf.Max(XygloPlayerPrefs.GetInt("MWhl", 1), 1); 

#if LOAD_DEBUG
        Debug.Log("Got highscore = " + m_player.m_highScore);
        Debug.Log("Got levels unlocked = " + m_player.m_worldsUnlocked);
        Debug.Log("Current World = " + CurrentWorld);
#endif

        m_player.setSoundEffects(XygloPlayerPrefs.GetBool("MWMMMUT", true));
        m_player.setMusic(XygloPlayerPrefs.GetBool("MWMMMUS", true));

        // High level
        //
        m_player.setHighLevel(XygloPlayerPrefs.GetInt("HighLevel", 0));
        //m_player.setHighLevel(99);

        // Fetch the difficulty level and set it in the player
        //
        //int difficultyLevel = XygloPlayerPrefs.GetInt("DifficultyLevel", 1);
        //Debug.Log("GOT DIFFICULTY LEVEL = " + difficultyLevel);
        m_player.setDifficultyLevel(XygloPlayerPrefs.GetInt("DifficultyLevel", 1));

        // Load the achievements
        //
        int[] achievementLevels = XygloPlayerPrefs.GetIntArray("AchievementLevels", new int[0]);
        string[] achievements = XygloPlayerPrefs.GetStringArray("Achievements", new string[0]);
        m_achievementCentre.loadAchievements(achievementLevels, achievements);

        // Load the high scores
        //
        int[] historyLevels = XygloPlayerPrefs.GetIntArray("HistoryLevels", new int[0]);
        int[] historyScores = XygloPlayerPrefs.GetIntArray("HistoryLevelScores", new int[0]);
        m_player.loadHistoryLevels(historyLevels, historyScores);


        // Set shaking time
        //
        float secondsShaking = XygloPlayerPrefs.GetFloat("ShakertronicTime", 0.0f);
        m_player.setSecondsShaking(secondsShaking);

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
            m_pauseMusicTime = m_levelDetails.audio.time;
        }
        else
        {
            // Resume music
            //
            m_levelDetails.audio.Play();
            m_levelDetails.audio.time = m_pauseMusicTime;
        }
    }


    /// <summary>
    /// Save game state
    /// </summary>
    protected void saveGameState()
    {
        // Don't save if we're debugging the levels
        //
        if (!WorldScene.m_saveGameState)
            return;

        Debug.Log("saveGameState() - storing");

        // Top slide save
        //
        int highScore = (int)Mathf.Max(m_player.getTotalScore(), m_player.getHighScore());
        XygloPlayerPrefs.SetInt("MWsst", highScore);

        // Highlevel could be m_worldsUnlocked (last value) or perhaps we've unlocked a new
        // one in CurrentWorld.
        //
        //Debug.Log("saveGameState - CurrentWorld = " + CurrentWorld);
        XygloPlayerPrefs.SetInt("MWhl", Mathf.Max(m_player.m_worldsUnlocked, CurrentWorld + 1));

        // High level
        //
        XygloPlayerPrefs.SetInt("HighLevel", m_player.getHighLevel());

        // Sound and music
        //
        XygloPlayerPrefs.SetBool("MWMMMUT", m_player.wantsSoundEffects());
        XygloPlayerPrefs.SetBool("MWMMMUS", m_player.wantsMusic());

        // Levels and achievements
        //
        //Debug.Log("+++ Achievement log = " + m_achievementCentre.getAchievementString());
        XygloPlayerPrefs.SetIntArray("AchievementLevels", m_achievementCentre.getAchievementLevelList());
        XygloPlayerPrefs.SetStringArray("Achievements", m_achievementCentre.getAchievementList());

        // Store the historic score arrays
        //
        XygloPlayerPrefs.SetIntArray("HistoryLevels", m_player.getHistoryLevelsIntArray());
        XygloPlayerPrefs.SetIntArray("HistoryLevelScores", m_player.getHistoryLevelsScoresIntArray());

        // Difficulty level
        //
        XygloPlayerPrefs.SetInt("DifficultyLevel", m_player.getDifficultyLevel());

        // Set shaking time
        //
        XygloPlayerPrefs.SetFloat("ShakertronicTime", m_player.getSecondsShaking());

        // Save everything
        //
        XygloPlayerPrefs.Save();
    }


    /// <summary>
    /// Reset some important statics so that we get a clean restart
    /// </summary>
    protected void resetStatics()
    {
        // m_player stays as it is
        WorldScene.m_moteHud = null;
        WorldScene.m_moteList = null;
        WorldScene.m_transientList = null;

        // Reset this
        //
        LevelSet.m_levelNumber = 0;

        // And this
        //
        m_interstitialStart = 0;

        // Clear levels and level sets
        //
        //if (WorldScene.m_levelManager != null)
        //{
        //WorldScene.m_levelManager.m_levelSets.Clear();
        //}
        //WorldScene.m_levelManager = null;

        // Reset any lives
        //
        if (WorldScene.m_player != null)
        {
            WorldScene.m_player.setLives(3);
        }
    }

    /// <summary>
    /// Setup some stuff for the game
    /// </summary>
    private void initializeGameComponents()
    {
        // Need to set this up before we create a MoteHud
        //
        m_hudControl = GameObject.FindWithTag("HudControl");
        if (m_hudControl == null)
        {
            Debug.Log("Couldn't get HUD control");
        }
		/*
		else
		{
			// Scale HUD control by screensize
			//
            Debug.Log("Scaling HUD = Screen.width = " + Screen.width);

			if (Screen.width > 1024)
			{
				// factor
				float factor = doScreenScale(true);
				Rect initial = m_hudControl.guiTexture.pixelInset;
                Debug.Log("initializeGameComponents() - HUD resize resizing with factor = " + factor);
				Rect newSize = new Rect(initial.x * factor, initial.y * factor, initial.width * factor, initial.height * factor);
				m_hudControl.guiTexture.pixelInset = newSize;
			}
		}*/

        if (WorldScene.m_moteHud == null)
        {
            WorldScene.m_moteHud = new MoteHud(m_hudControl.guiTexture.texture, NoFxHudTexture, NoMusicHudTexture, MutedHudTexture, m_hudControl);

        }

        if (WorldScene.m_player == null)
        {
            WorldScene.m_player = new Player("Me", 3);
        }

        if (WorldScene.m_transientList == null)
        {
            WorldScene.m_transientList = new List<Transient>();
        }

        // Fetch this object which needs to be attached alongside the WorldScene
        //
        if (m_transientManager == null)
        {
            m_transientManager = (TransientManager)this.gameObject.GetComponent(typeof(TransientManager));
        }

        // Fetch the BossManager
        //
        if (m_bossManager == null)
        {
            m_bossManager = (BossManager)this.gameObject.GetComponent(typeof(BossManager));
        }

        // Social buttons
        //
#if FACEBOOK_ENABLED
            m_facebookButton = GameObject.FindWithTag("FacebookButton");
#endif
        m_twitterButton = GameObject.FindWithTag("TwitterButton");

#if FACEBOOK_ENABLED
            if (m_facebookButton != null)
            {
                Rect rect = m_facebookButton.guiTexture.pixelInset;
                rect.x = Screen.width / 3;
                rect.y = Screen.height / 3;

                m_facebookButton.SetActive(false);
                

                m_facebookButton.guiTexture.transform.localScale = Vector3.zero;
                m_facebookButton.guiTexture.transform.localPosition = Vector3.zero;
                m_facebookButton.guiTexture.pixelInset = rect;
                //m_facebookButton.guiTexture.pixelInset
            }
#endif
        if (m_twitterButton != null)
        {
            m_twitterButton.SetActive(false);
            Rect rect = m_twitterButton.guiTexture.pixelInset;
            rect.x = 2 * Screen.width / 3;
            rect.y = Screen.height / 3;

            m_twitterButton.SetActive(false);

            m_twitterButton.guiTexture.transform.localScale = Vector3.zero;
            m_twitterButton.guiTexture.transform.localPosition = Vector3.zero;
            m_twitterButton.guiTexture.pixelInset = rect;
        }

        // Initialise MoteHud
        //
        m_moteHud.initialise(m_hudControl);
    }
	
	/// <summary>
	/// Protect from bouncing.
	/// </summary>
	protected float m_bounceProtect = 0.0f;
	
    /// <summary>
    /// When Unity initalises this class
    /// </summary>
    void Start()
    {
		// Don't process any clicks or prods until this timer has passed
		//
		m_bounceProtect = Time.time + 0.2f;
		
        // Debug level override some timings to make level access quicker
        //
        if (m_debugLevels)
        {
            m_interstitialLength = 1.0f;
            
            // Changing this currently breaks the positioning of the ship - not very good
            //
            //m_levelGenerationPause = 1.0f;
        }

        // Init by setting the secret key, never ever change this line in your app !
        //
        XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);

        // Ensure any statics are reset when we reload the game scene
        //
        resetStatics();

#if DEBUG_START
        NGUIDebug.Log("WorldScene - starting");
#endif
        if (m_levelManager == null)
        {
            m_levelManager = new LevelManager();

            // Check and set starting level according to world
            //

            // -2 denotes a level selected from the level selection screen - go straight to that level
            //
            if (CurrentWorld == -2)
            {
                // Jump to this level
                //
                m_levelManager.setLevel(JumpToLevel);

//#if DEBUG_START
                Debug.Log("CurrentWorld == -2 : Jumping to level " + JumpToLevel);
//#endif
                // And set the CurrentWorld from LevelSet
                //
                CurrentWorld = m_levelManager.getGameLevel().getLevelSet().getLevelSetNumber();

//#if DEBUG_START
                Debug.Log("WorldScene - CurrentWorld has been set to " + CurrentWorld);
//#endif
            }
            else if (CurrentWorld == -1) // -1 denotes that we want to start from the last played level
            {

                int level = XygloPlayerPrefs.GetInt("HighLevel", 0);

#if DEBUG_START
                NGUIDebug.Log("WorldScene - CurrentWorld == -1 : Starting with level " + level);
#endif

                if (level > m_levelManager.getLastLevelAvailable())
                {
                    //NGUIDebug.Log("WorldScene - Stored level is too high (" + level + ") resetting to " + m_levelManager.getLastLevelAvailable());
                    level = m_levelManager.getLastLevelAvailable();
                }
                // Find the latest played level
                //
                m_levelManager.setLevel(level);

                // If we've stored a boss level than back up one level as we don't jump straight
                // to a boss level.
                //
                if (m_levelManager.getGameLevel().isBoss())
                {
                    m_levelManager.setLevel(level - 1);
                }

                // And set the CurrentWorld from LevelSet
                CurrentWorld = m_levelManager.getGameLevel().getLevelSet().getLevelSetNumber();
            }
            else  // We're just selecting a level by world number - get the first level for that world
            
            {
#if DEBUG_START
                NGUIDebug.Log("Setting start level by CurrentWorld == " + CurrentWorld);
#endif
                Level startingLevel = m_levelManager.getLevelForWorld(CurrentWorld);
                if (startingLevel != null)
                {
#if DEBUG_START
                    NGUIDebug.Log("OTHER - Got starting level = " + startingLevel.getLevelNumber());
#endif
                    m_levelManager.setLevel(startingLevel.getLevelNumber());
                }
            }
        }

        if (WorldScene.m_moteList == null)
        {
            m_moteList = new List<Mote>();
        }

        // Player setup etc
        //
        initializeGameComponents();

        // Do this
        //
        initialiseAnimatedHudComponents();

        // Start time for initial call and set up level for initial call 
        //
        //m_startTime = Time.time;
        //m_levelManager.getGameLevel().resetClock(m_levelGenerationPause);

        // Reset the total score
        //
        m_player.setTotalScore(0);

        // Load the game state
        //
        loadGameState();

        // Set initial background texture and color perhaps?
        //
        setBackgroundTexture();

        // Google analytics
        //
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("Starting Game First Time");
            //Debug.Log("GoogleAnalytics - Starting Game First Time");
        }

        // For the first time we play a level straight from the menu then we want to see the interstitial
        //
        m_state = WorldSceneState.Interstitial;
    }


    /// <summary>
    /// Animated HUD components
    /// </summary>
    void initialiseAnimatedHudComponents()
    {

        if (!StaticMoteTexture)
        {
            Debug.LogError("Please assign a StaticMote texture on the inspector");
            return;
        }

        m_completionMessage = GameObject.FindWithTag("CompletionMessage");

        if (m_completionMessage == null)
        {
            Debug.Log("Can't find CompletionMessage object");
        }
        m_completionMessage.guiText.material.color = Color.green;

        m_welcomeMessage = GameObject.FindWithTag("WelcomeMessage");

        if (m_welcomeMessage == null)
        {
            Debug.Log("Can't find LevelMessage object");
        }
        m_welcomeMessage.guiText.material.color = Color.magenta;


        m_hintMessage = GameObject.FindWithTag("HintMessage");
        if (m_hintMessage == null)
        {
            Debug.Log("Can't find HintMessage object");
        }
        m_hintMessage.guiText.material.color = Color.green;

        // Fetch this reference
        //
        m_backgroundObject = GameObject.FindWithTag("Background");
        if (m_backgroundObject == null)
        {
            Debug.Log("Background image couldn't be loaded");
        }
        else
        {
            // Set the alpha
            //
            //Debug.Log("updateBackground alpha");
            float newAlpha = 0.1f;
            Color newColour = new Color(m_backgroundObject.guiTexture.color.r, m_backgroundObject.guiTexture.color.g, m_backgroundObject.guiTexture.color.b, newAlpha);
            m_backgroundObject.guiTexture.color = newColour;
        }

        m_interstitialObject = GameObject.FindWithTag("Interstitial");
        if (m_interstitialObject == null)
        {
            Debug.Log("Couldn't load interstitial GameObject");
        }

        m_interstitialTitleMessage = GameObject.FindWithTag("InterstitialTitle");
        m_interstitialTitleMessage.guiText.material.color = Color.yellow;

        m_interstitialIntroductionMessage = GameObject.FindWithTag("InterstitialIntroduction");
        m_interstitialIntroductionMessage.guiText.material.color = Color.cyan;

        m_moteShip = new MoteShip(GameObject.FindWithTag("MoteShip"));
        if (m_moteShip == null)
        {
            Debug.Log("Can't find the MOTESHIP!");
        }
        else
        {
            m_moteShip.getGameObject().transform.localScale = Vector3.zero;
            m_moteShip.getGameObject().transform.localPosition = Vector3.zero;
            m_moteShip.getGameObject().SetActive(false);
			
			// Scale moteship by screensize
			//
			if (Screen.width > 1024)
			{
				// factor
				Rect initial = m_moteShip.getGameObject().guiTexture.pixelInset;
				float factor = doScreenScale(true, initial.width); //s Screen.width / 1024;
				Rect newSize = new Rect(initial.x * factor, initial.y * factor, initial.width * factor, initial.height * factor);
				m_moteShip.getGameObject().guiTexture.pixelInset = newSize;
			}
        }
    }


    /// <summary>
    /// Perform mote population and ensure that dead motes can be cleared up
    /// </summary>
    protected void levelPopulation()
    {
        if (m_levelManager.getGameLevel() == null)
        {
            Debug.Log("Could not get next level");
            return;
        }

        // Reset clock once per level - this is taken care of by logic inside the level resetclock method
        //
        //m_levelManager.getGameLevel().resetClock(m_levelGenerationPause);

        // For boss motes we create those once - always do this before other mote types
        //
        //
        if (m_levelManager.getGameLevel().isBoss() && m_levelManager.getGameLevel().hasBossBeenGenerated() == false)
        {
            createMote(m_levelManager.getGameLevel().getBoss(), false);
            m_levelManager.getGameLevel().setBossGenerated(true);

            // We also need to change the music to boss music - if we're playing then stop, change music
            // and restart.
            //
            if (m_levelDetails.audio.isPlaying)
            {
                m_levelDetails.audio.Stop();
                m_levelDetails.audio.clip = m_bossManager.B01MegaSheepBossMusic;
                m_levelDetails.audio.Play();
            }
        }

        Level level = m_levelManager.getGameLevel();

        //Debug.Log("WorldScene MoteList Count = " + WorldScene.m_moteList.Count);
        //Debug.Log("LEVEL MOTE COUNT = " + level.getMotes().Count);

        // If the active moteList is less than the total number of Motes in level then keep generating - also check for generation timing
        // (which defaults to zero)
        //
        if (((WorldScene.m_moteList.Count < level.getMotes().Count) || WorldScene.m_moteList.Count < level.getGenerateLevel()))
        {
            // Test for regenerative level - if we have one then we don't generate straight for the
            // mote list.
            //
            Mote levelMote = null;
            int moteIndex = 0;
            bool createdGoodMote = false;

            if (Time.time > m_lastMoteGenerationTime + level.getGenerateSpacingGood())
            {
                if (m_levelManager.getGameLevel().getGenerateLevel() > 0)
                {
                    // Randomise next mote to generate
                    //
					//Debug.Log("GOOD MOTE generate ATTEMPT");
                    levelMote = m_levelManager.getGameLevel().getRandomMote(m_moteList, true); // only good motes
					//Debug.Log("LEVEL MOTE = " + levelMote);
                }
				
                //Debug.Log("Level = " + m_levelManager.getGameLevelNumber() + ", MOTE INDEX = " + moteIndex);

                // Create the mote - check for either being a not good mote (and create) or if good then check time with
                // other spacing constraint.
                //
                if (levelMote != null)
                {
					CellMote cellMote = null;
					
					// Test for activated things like shields - if not then we don't need this extra powerup
					//
					if (levelMote is CellMote)
						cellMote = (CellMote)levelMote;
					
					if (cellMote == null || (cellMote.getPrize() == CellPrize.Shield && m_player.testPowerUp(PlayerPowerup.Shield) == false))
					{
						//Debug.Log("CREATE GOOD MOTE");
					
						createMote(levelMote, false);

	                    // Update the last generation time
	                    //
	                    m_lastMoteGenerationTime = Time.time;
	
	                    // Created
	                    //
	                    createdGoodMote = true;
					}
                }
            }

            // Check now for baddy mote
            if (!createdGoodMote && Time.time > m_lastMoteGenerationTime + level.getGenerateSpacing())
            {
                if (m_levelManager.getGameLevel().getGenerateLevel() > 0)
                {
                    // Randomise next mote to generate
                    //
                    //moteIndex = (int)Mathf.Max((Random.value * (float)m_levelManager.getGameLevel().getMotes().Count) - 1, 0);
                    levelMote = m_levelManager.getGameLevel().getRandomMote(m_moteList);
                }
                else
                {
                    // Set mote index to the next mote to generate
                    //
                    moteIndex = WorldScene.m_moteList.Count;
                    levelMote = m_levelManager.getGameLevel().getMotes()[moteIndex];
                }

                //Debug.Log("Level = " + m_levelManager.getGameLevelNumber() + ", MOTE INDEX = " + moteIndex);
			
			    // Create the mote - check for either being a not good mote (and create) or if good then check time with
                // other spacing constraint.
			    //
                if (levelMote != null)
                {
					//Debug.Log("CREATING BADDY MOTE");
                    createMote(levelMote, false);

                    // Update the last generation time
                    //
                    m_lastMoteGenerationTime = Time.time;
                }
            }
        }
        //else
        //{
        // We have all the motes but we need to now test for escape from the playing area.
        // If motes have flown outside the outer area of the screen we want to regenerate
        // these to keep them in play.  This is NOT the same as regeneration of motes that
        // are set to regenerate at the Level level.  Ken?
        //
        // Also any motes marked as dead can be cleared up at this point.
        //

        // Remove list
        //
        List<Mote> removeList = new List<Mote>();
        float buffer = 10.0f;
        foreach (Mote mote in WorldScene.m_moteList)
        {
            //if (!mote.intersects(new Rect(-mote.getWidth() * 0.6f, -mote.getHeight() * 0.6f, Screen.width + (mote.getWidth() * 1.2f), Screen.height + (mote.getHeight() * 1.2f))))

            if (mote.isDead() || !mote.intersects(new Rect(-mote.getWidth() - buffer, -mote.getHeight() - buffer, Screen.width + mote.getWidth() + 2 * buffer, Screen.height + mote.getHeight() + 2 * buffer)))
            {
                removeList.Add(mote);
            }
        }

        // Remove motes from the active list so that they will be regenerated
        //
        removeMotes(removeList);
        //}

    }

    /// <summary>
    /// Time last mote was generated
    /// </summary>
    protected float m_lastMoteGenerationTime = 0.0f;

    /// <summary>
    /// Get a boss mote - includes derived boss classes
    /// </summary>
    /// <returns></returns>
    protected BossMote getBossFromActiveList()
    {
        foreach (Mote mote in m_moteList)
        {
            if (mote is BossMote)
                return (BossMote)mote;
        }

        return null;
    }
	
	/// <summary>
	/// Does the screen scaling of .
	/// </summary>
	/// <returns>
	/// The screen scale.
	/// </returns>
	protected float doScreenScale(bool factor = false, float inMaxSize = 0.0f)
	{
		// Default max size but we might want to change this/scale according to screen size
        //
        float maxSize = 128.0f;
		
		// Reset maxSize 
		if (factor && inMaxSize > 0.0f)
			maxSize = inMaxSize;
		
		float initialSize = maxSize;
		
		// Modify max size by screen size
		//
		if (Screen.width <= 480)
		{
			maxSize = 48.0f;
		}
		else if (Screen.width <= 640)
		{
			maxSize = 96.0f;
		}
		else if (Screen.width <= 1024)
		{
			maxSize = 128.0f;
		}
		else if (Screen.width <= 1600)
		{
			maxSize = 160.0f;
		}
		else
		{
			// Scale according to new screen size
			//maxSize *= Screen.width / 1024;
			maxSize = 360.0f;
		}
		
		if (factor)
			return maxSize / initialSize;
		else
			return maxSize;
	}
	
    /// <summary>
    /// A Mote Factory within the Manager - we might want to split this out at some point to its
    /// own class.
    /// </summary>
    /// <param name="moteObject"></param>
    /// <returns></returns>
    protected void createMote(Mote moteObject, bool nextGeneration)
    {
        Texture newTexture = null;
        Mote activeMote = null;
        GameObject g = null;

        // Default max size but we might want to change this/scale according to screen size
        //
        float maxSize = doScreenScale();

        if (moteObject is StaticMote)
        {
            g = new GameObject("StaticMote");
            newTexture = StaticMoteTexture;
            activeMote = new StaticMote(g);
            activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(BalloonMote))
        {
            g = new GameObject("BallonMote");
            BalloonMote balloonMote = (BalloonMote)moteObject;

            switch (balloonMote.getBalloonColour())
            {
                case BalloonColour.Blue:
                    newTexture = BlueBalloonTexture;
                    break;

                case BalloonColour.Red:
                    newTexture = RedBalloonTexture;
                    break;

                case BalloonColour.Yellow:
                    newTexture = YellowBalloonTexture;
                    break;

                case BalloonColour.Pink:
                    newTexture = PinkBalloonTexture;
                    break;

                case BalloonColour.Purple:
                    newTexture = PurpleBalloonTexture;
                    break;

                case BalloonColour.Green:
                    newTexture = GreenBalloonTexture;
                    break;

                default:
                    break;
            }
            
            activeMote = new BalloonMote(g, rescaleTexture(newTexture, maxSize), balloonMote.getIgnoreGravity());
            //activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(GenericMote))
        {
            g = new GameObject("GenericMote");
            newTexture = GenericMoteTexture;
            //GenericMote gMote = (GenericMote)moteObject;
            activeMote = new GenericMote(g, true, rescaleTexture(GenericMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(MovingMote))
        {
            g = new GameObject("MovingMote");
            newTexture = MovingMoteTexture;
            MovingMote movingMote = (MovingMote)moteObject;
            activeMote = new MovingMote(g, movingMote.getMovementMethod());
            activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(FluffyMote))
        {
            g = new GameObject("FluffyMote");
            newTexture = FluffyMoteTexture;

            FluffyMote fluffyMote = (FluffyMote)moteObject;
            activeMote = new FluffyMote(g, fluffyMote.startOffScreen(), rescaleTexture(FluffyMoteTexture, maxSize));

            // Add two pop textures
            activeMote.addPopTexture(FluffyMoteTexture_InitialPop);
            activeMote.addPopTexture(FluffyMoteTexture_FinalPop);
        }
        else if (moteObject.GetType() == typeof(ChickMote))
        {
            g = new GameObject("ChickMote");
            newTexture = ChickMoteTexture;
            activeMote = new ChickMote(g);
            //EggMote eggMote = (EggMote)moteObject;
            Debug.Log("Creating new ChickMote at x = " + moteObject.getPosition().x + ", y = " + moteObject.getPosition().y);
            activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(PineConeMote))
        {
            // Works like an Acorn
            //
            g = new GameObject("PineConeMote");
            newTexture = PineConeTexture;
            activeMote = new PineConeMote(g, rescaleTexture(PineConeTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(HeliMote))
        {
            g = new GameObject("HeliMote");
            newTexture = HeliMoteTexture;
            activeMote = new HeliMote(g, true, rescaleTexture(HeliMoteTexture, maxSize));
            //EggMote eggMote = (EggMote)moteObject;
            //Debug.Log("Creating new ChickMote at x = " + moteObject.getPosition().x + ", y = " + moteObject.getPosition().y);
            activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(SquishMote))
        {
            g = new GameObject("SquishMote");
            newTexture = SquishMoteTexture;
            activeMote = new SquishMote(g, true, SquishMoteTexture, SquishMoteAlarmedTexture, rescaleTexture(SquishMoteTexture, maxSize));
            //Debug.Log("Creating new SquishMote at x = " + moteObject.getPosition().x + ", y = " + moteObject.getPosition().y);
            //activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(SnakeMote))
        {
            g = new GameObject("SnakeMote");
            newTexture = SnakeMoteTexture;
            activeMote = new SquishMote(g, true, SnakeMoteTexture, SnakeMoteTexture, rescaleTexture(SnakeMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(BeeMote))
        {
            g = new GameObject("BeeMote");
            newTexture = BeeMoteTexture;

            //BeeMote fluffyMote = (BeeMote)moteObject;
            activeMote = new BeeMote(g);
        }
        else if (moteObject.GetType() == typeof(MosquitoMote))
        {
            // Adding MosquitoMote
            //
            //Debug.Log("+++++++++++++++ Adding MosquitoMote");
            g = new GameObject("MosquitoMote");
            newTexture = MosquitoMoteTexture;

            MosquitoMote mosquitoMote = (MosquitoMote)moteObject;
            activeMote = new MosquitoMote(g, mosquitoMote.startOffScreen(), rescaleTexture(MosquitoMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(ButterflyMote))
        {
            g = new GameObject("ButterflyMote");
            newTexture = ButterflyOpenTexture;
            ButterflyMote butterflyMote = (ButterflyMote)moteObject;
            activeMote = new ButterflyMote(g, butterflyMote.startOffScreen(), rescaleTexture(ButterflyOpenTexture, maxSize));

            // Set the textures on the butterfly
            //
            ButterflyMote instance = (ButterflyMote)activeMote;
            instance.setTextures(ButterflyOpenTexture, ButterflyClosedTexture);
        }
        else if (moteObject.GetType() == typeof(CellMote))
        {
			// For cell motes do a quick check for activated features
			//
			CellMote cm = (CellMote)moteObject;
            g = new GameObject("CellMote");
            newTexture = CellMoteTexture;

			if ((cm.getPrize() == CellPrize.Shield && m_player.testPowerUp(PlayerPowerup.Shield) == false) || (cm.getPrize() == CellPrize.Grenade)) 
			{
	            activeMote = new CellMote(g, rescaleTexture(ShieldTexture, maxSize), cm.getPrize(), cm.getSide(), cm.getStraightMovement());
			}
		}
        else if (moteObject.GetType() == typeof(AcornMote))
        {
            g = new GameObject("AcornMote");
            newTexture = AcornMoteTexture;
            AcornMote acornMote = (AcornMote)moteObject;

            // Check for side starting
            //
            if (acornMote.getStartSide() == MoteStartSide.None)
                activeMote = new AcornMote(g, rescaleTexture(AcornMoteTexture, maxSize));
            else
                activeMote = new AcornMote(g, rescaleTexture(AcornMoteTexture, maxSize), acornMote.getStartSide());
        }
        else if (moteObject.GetType() == typeof(SycamoreMote))
        {
            g = new GameObject("SycamoreMote");
            newTexture = SycamoreMoteTexture;
            SycamoreMote sycMote = (SycamoreMote)moteObject;
            activeMote = new SycamoreMote(g, m_moteShip, rescaleTexture(SycamoreMoteTexture, maxSize), sycMote.getSpeed());

        }
        else if (moteObject.GetType() == typeof(MushroomMote))
        {
            g = new GameObject("MushroomMote");
            newTexture = MushroomMoteTexture;
            MushroomMote mushMote = (MushroomMote)moteObject;
            activeMote = new MushroomMote(g, mushMote.startOffScreen(), rescaleTexture(MushroomMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(RussianMote))
        {
            g = new GameObject("RussianMote");
            newTexture = EvasiveMoteTexture;
            RussianMote rusMote = (RussianMote)moteObject;
            activeMote = new RussianMote(g, rusMote.startOffScreen(), rescaleTexture(EvasiveMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(EggMote))
        {
            g = new GameObject("EggMote");
            newTexture = EggMoteTexture;
            EggMote eggMote = (EggMote)moteObject;
            activeMote = new EggMote(g, eggMote.startOffScreen(), rescaleTexture(EggMoteTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(SporeMote))
        {
            SporeMote spore = (SporeMote)moteObject;

            // New mote
            //
            g = new GameObject("SporeMote");

            if (!nextGeneration)
            {
                newTexture = SporeMoteTexture;

                // Check for a start side - we could be just starting from one side with this type of mote - might need to generalise this code a bit/
                if (spore.getStartSide() != MoteStartSide.None)
                {
                    Debug.Log("SPAWNING RIGHT SIDE SPORE MOTE");
                    activeMote = new SporeMote(g, spore.getStartSide(), rescaleTexture(SporeMoteTexture, maxSize), spore.getGeneration());
                }
                else
                {
                    activeMote = new SporeMote(g, spore.startOffScreen(), rescaleTexture(SporeMoteTexture, maxSize), spore.getGeneration());
                    //activeMote.setPosition(spore.getPosition());
                }
            }
            else
            {
                // For all other generations use a different texture and spawn
                // at mote position.
                //
                //Debug.Log("2nd Generation creation");
                newTexture = SporeMoteTextureGen2;
                //SporeMote sporeMote = (SporeMote)moteObject;
                activeMote = new SporeMote(g, false, rescaleTexture(SporeMoteTexture, maxSize), spore.getGeneration() + 1);
                //activeMote.setPosition(spore.getPosition());
            }
        }
        else if (moteObject.GetType() == typeof(BossMegaSheep))
        {
            //Debug.Log("Creating MEGASHEEP");
            g = new GameObject("BossMegaSheep");

            // A Boss will take care of it's own szing and scaling rather than default mote sizing
            //
            activeMote = new BossMegaSheep(g, m_bossManager.B01MegaSheepLeft, m_bossManager.B01MegaSheepRight, m_bossManager.B01MegaSheepCentre,
                                           m_bossManager.B01MegaSheepAngry, m_bossManager.B01MegaSheepHurt, m_bossManager.B01MegaSheepDead,
                                           m_bossManager.B01MegaSheepBAAAA, new Vector2(3 * Screen.width / 4, Screen.height / 2));

            // set the worth of the mote
            //
            activeMote.setWorth(moteObject.getWorth() * m_player.getDifficultyLevel());

            // Add the mote
            //
            WorldScene.m_moteList.Add(activeMote);

            newTexture = m_bossManager.B01MegaSheepCentre;

            // Double max size for a boss sheep
            //
            maxSize *= 2;

            // Set position according to texture size and centre vertically
            //
            activeMote.setPosition(new Vector2((3 * Screen.width / 4) - (Mathf.Min(maxSize, newTexture.width) / 2), (Screen.height / 2) - Mathf.Min(maxSize, newTexture.height) / 2));
            //Debug.Log("Positioning MEGA x = " + activeMote.getPosition().x + ", y = " + activeMote.getPosition().y);
        }
        else if (moteObject.GetType() == typeof(BossMegaWolf))
        {
            g = new GameObject("BossMegaWolf");

            // A Boss will take care of it's own szing and scaling rather than default mote sizing
            //
            activeMote = new BossMegaWolf(g, m_bossManager.B01MegaSheepLeft, m_bossManager.B01MegaSheepRight, m_bossManager.B01MegaSheepCentre,
                                           m_bossManager.B01MegaSheepAngry, m_bossManager.B01MegaSheepHurt, m_bossManager.B01MegaSheepDead,
                                           m_bossManager.B01MegaSheepBAAAA, new Vector2(3 * Screen.width / 4, Screen.height / 2));

            // set the worth of the mote
            //
            activeMote.setWorth(moteObject.getWorth() * m_player.getDifficultyLevel());

            // Add the mote
            //
            WorldScene.m_moteList.Add(activeMote);

            newTexture = m_bossManager.B01MegaSheepCentre;

            // Double max size for a boss sheep
            //
            maxSize *= 2;

            // Set position according to texture size and centre vertically
            //
            activeMote.setPosition(new Vector2((3 * Screen.width / 4) - (Mathf.Min(maxSize, newTexture.width) / 2), (Screen.height / 2) - Mathf.Min(maxSize, newTexture.height) / 2));
            //Debug.Log("Positioning MEGA x = " + activeMote.getPosition().x + ", y = " + activeMote.getPosition().y);
        }
        else if (moteObject.GetType() == typeof(BossBadRobot))
        {
            g = new GameObject("BossBadRobot");

            // A Boss will take care of it's own szing and scaling rather than default mote sizing
            //
            activeMote = new BossBadRobot(g, m_bossManager.B01MegaSheepLeft, m_bossManager.B01MegaSheepRight, m_bossManager.B01MegaSheepCentre,
                                           m_bossManager.B01MegaSheepAngry, m_bossManager.B01MegaSheepHurt, m_bossManager.B01MegaSheepDead,
                                           m_bossManager.B01MegaSheepBAAAA, new Vector2(3 * Screen.width / 4, Screen.height / 2));

            // set the worth of the mote
            //
            activeMote.setWorth(moteObject.getWorth() * m_player.getDifficultyLevel());

            // Add the mote
            //
            WorldScene.m_moteList.Add(activeMote);

            newTexture = m_bossManager.B01MegaSheepCentre;

            // Double max size for a boss sheep
            //
            maxSize *= 2;

            // Set position according to texture size and centre vertically
            //
            activeMote.setPosition(new Vector2((3 * Screen.width / 4) - (Mathf.Min(maxSize, newTexture.width) / 2), (Screen.height / 2) - Mathf.Min(maxSize, newTexture.height) / 2));
            //Debug.Log("Positioning MEGA x = " + activeMote.getPosition().x + ", y = " + activeMote.getPosition().y);
        }
        else if (moteObject.GetType() == typeof(ShootMote))
        {
            g = new GameObject("BossAttack");
            activeMote = new ShootMote(g);

            ShootMote shoot = (ShootMote)moteObject;
            newTexture = shoot.getAttackTexture();
            activeMote.setPosition(shoot.getPosition());
            activeMote.setAcceleration(shoot.getAttackVector() / 100);

            Debug.Log("Acceleration x = " + activeMote.getAcceleration().x + ", y = " + activeMote.getAcceleration().y);
        }
        else if (moteObject.GetType() == typeof(GlueFluffyMote))
        {
            Debug.Log("Generating glue fluffy");
            g = new GameObject("GlueFluffyMote");
            newTexture = GlueFluffyMoteTexture;
            activeMote = new GlueFluffyMote(g, rescaleTexture(SporeMoteTextureGen2, maxSize));

            GlueFluffyMote glue = (GlueFluffyMote)moteObject;

            // Set the boss position
            //
            BossMote bossMote = getBossFromActiveList();
            if (bossMote != null)
                glue.setBossPosition(bossMote.getInsetCentrePosition());

            //newTexture = shoot.getAttackTexture();
            //activeMote.setPosition(shoot.getPosition());
            //activeMote.setAcceleration(shoot.getAttackVector() / 50)

        }
        else if (moteObject.GetType() == typeof(HeartOfGoldMote))
        {
            g = new GameObject("HeartOfGoldMote");
            newTexture = HeartOfGoldTexture;
            activeMote = new HeartOfGoldMote(g, rescaleTexture(newTexture, maxSize));
            //HeartOfGoldMote hog = (HeartOfGoldMote)moteObject;
        }
        else if (moteObject.GetType() == typeof(SputnikMote))
        {
            g = new GameObject("SputnikMote");
            newTexture = SputnikTexture;
            activeMote = new SputnikMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(SatelliteMote))
        {
            g = new GameObject("SatelliteMote");
            newTexture = SatelliteTexture;
            activeMote = new SatelliteMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(ICBMMote))
        {
            g = new GameObject("ICBMMote");
            newTexture = ICBMTexture;
            activeMote = new ICBMMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(LegoBalloonMote))
        {
            g = new GameObject("LegoBalloonMote");
            newTexture = LegoManTexture;
            activeMote = new LegoBalloonMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(AuroraMote))
        {
            g = new GameObject("AuroraMote");
            newTexture = NorthernLightsTexture;
            activeMote = new AuroraMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(DragonMote))
        {
            g = new GameObject("DragonMote");
            newTexture = DragonCapsuleTexture;
            activeMote = new DragonMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(ISSMote))
        {
            g = new GameObject("ISSMote");
            newTexture = ISSTexture;
            activeMote = new ISSMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(BlackHoleMote))
        {
            g = new GameObject("BlackHoleMote");
            newTexture = BlackHoleTexture;
            activeMote = new BlackHoleMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(CometMote))
        {
            g = new GameObject("CometMote");
            newTexture = CometTexture;
            activeMote = new CometMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(DarkStarAlienMote))
        {
            g = new GameObject("DarkStarAlienMote");
            newTexture = DarkStarAlienTexture;
            activeMote = new DarkStarAlienMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(TinaTurnerMote))
        {
            g = new GameObject("TinaTurnerMote");
            newTexture = TinaTurnerTexture;
            activeMote = new TinaTurnerMote(g, rescaleTexture(newTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(MoteBossMote))
        {
            g = new GameObject("MoteBossMote");
            newTexture = MoteBossTexture;
            activeMote = new MoteBossMote(g, rescaleTexture(newTexture, maxSize));
        }
        else
        {
            Debug.Log("Got unhandled Mote type");
        }


        // If we have an activeMote to populate then do so and return it
        //
        if (g != null && newTexture != null && activeMote != null)
        {
            g.AddComponent("GUITexture");
            g.guiTexture.texture = newTexture;

			// Force motes behind the screen fauna and flora
			//
			g.transform.position = new Vector3(0, 0, -1);
			//g.transform.position = Vector3.zero;
			
            g.transform.localScale = Vector3.zero;

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

            // Generate next generation at the same point as the passed mote and we want to
            // inhert some of the acceleration.
            //
            if (nextGeneration)
            {
                g.guiTexture.pixelInset = new Rect(moteObject.getGameObject().guiTexture.pixelInset.x, moteObject.getGameObject().guiTexture.pixelInset.y, width, height);

                // Semi random vector of acceleration for the new
                //
                activeMote.setAcceleration(getSimilarVector(moteObject.getAcceleration()));
            }
            else
            {
                g.guiTexture.pixelInset = new Rect(activeMote.getPosition().x, activeMote.getPosition().y, width, height);
            }
        }

        // We may have no active mote at this point
        //
        if (activeMote != null)
        {
            // If there is no poptexture then add a default one
            //
            if (activeMote.getPopTextures().Count == 0)
            {
                activeMote.addPopTexture(m_transientManager.m_defaultPop);
            }

            // set the worth of the mote
            //
            activeMote.setWorth(moteObject.getWorth() * m_player.getDifficultyLevel());

            // Add the mote
            //
            WorldScene.m_moteList.Add(activeMote);
        }
    }


    /// <summary>
    /// Rescale a texture to a max size
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="maxSize"></param>
    /// <returns></returns>
    protected Vector2 rescaleTexture(Texture texture, float maxSize)
    {
        // Initially assuming width > height
        //
        float width = Mathf.Min(texture.width, maxSize);
        float height = texture.height * width / texture.width;

        // If the proportions are the other way around
        //
        if (texture.height > texture.width)
        {
            height = Mathf.Min(texture.height, maxSize);
            width = texture.width * height / texture.height;
        }

        return new Vector2(width, height);
    }

    /// <summary>
    /// Get a similar vector to the one passed
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    protected Vector2 getSimilarVector(Vector2 vector)
    {
        // Half initial vector
        //
        Vector2 rV = vector / 2;

        // Plus a bit of random of the other half
        //
        bool reverse = false;
        if (Random.value < 0.5f)
            reverse = true;

        rV.x += (reverse ? -1 : 1) * Random.value * vector.x;

        if (Random.value < 0.5f)
            reverse = true;

        rV.y += (reverse ? -1 : 1) * Random.value * vector.y;

        return rV;
    }

    /// <summary>
    /// Twitter default string
    /// </summary>
    protected string m_twitterString = "I'm+playing+Mote+Wars+available+on+iOS+and+Android.+It's+awesome!";

    /// <summary>
    /// Reset and restart
    /// </summary>
    protected bool checkForWholeGameRestart(Vector2 hitPosition)
    {
        // Test twitter button first
        //
        if (m_twitterButton.activeInHierarchy)
        {
            if (m_twitterButton.guiTexture.GetScreenRect().Contains(hitPosition))
            {
                string twitString = "http://twitter.com/?status=" + m_twitterString;
                //twitString += "I+just+got+a+top+score+on+Mote+Wars+of+" + m_player.getTotalScore();
                Application.OpenURL(twitString);
                return true;
            }
        }


        if (m_levelManager.isGameCompleted() || m_player.getLives() == 0)
        {
            // Clear
            //
            //Debug.Log("Setting highscore = " + m_player.m_totalScore);
            //Debug.Log("World unlocked = " + m_player.m_worldsUnlocked);

            // Save prefs
            //
            saveGameState();

            m_completionMessage.SetActive(false);
            m_levelManager.setLevel(0);
            Application.LoadLevel(1);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Game completion 
    /// </summary>
    protected void doVictoryScreen()
    {
        // Do thanks and animation and fireworks and everything else
        //
        string gameCompletionMessage = "Congratulations!  You've completed Mote Wars!";
        slideMessage(m_completionMessage, m_completeTime, gameCompletionMessage, new Vector2(-500, 200), Vector2.zero, new Vector2(500, 200), 1.0f, 10.0f, 0.0f);

#if FACEBOOK_ENABLED
            m_facebookButton.SetActive(true);
#endif

        if (m_twitter)
            m_twitterButton.SetActive(true);

    }

    /// <summary>
    /// Is current level completed or have we died?
    /// </summary>
    /// <returns></returns>
    protected bool isLevelCompleted()
    {
        return m_levelManager.getGameLevel().levelCompletedLogic(m_player.getLives());
    }

    /// <summary>
    /// See if the boss needs to do anything - like attack
    /// </summary>
    protected void checkBoss()
    {
        BossMote bossMote = getBossMote();

        if (bossMote == null)
            return;

        // Check and launch an attak
        //
        if (bossMote.checkAttack())
        {
            //Debug.Log("Launching BOSS ATTACK on Mote ship");

            Debug.Log("bossMote inset x = " + bossMote.getInsetPosition().x + ", y = " + bossMote.getInsetPosition().y);
            Debug.Log("bossMote inset centre x = " + bossMote.getInsetCentrePosition().x + ", y = " + bossMote.getInsetCentrePosition().y);

            Debug.Log("moteship inset x = " + m_moteShip.getInsetPosition().x + ", y = " + m_moteShip.getInsetPosition().y);
            Debug.Log("moteship inset centre x = " + m_moteShip.getInsetCentrePosition().x + ", y = " + m_moteShip.getInsetCentrePosition().y);

            createMote(new ShootMote(bossMote.getAttackTexture(), bossMote.getInsetCentrePosition(), m_moteShip.getScreenCentrePosition()), false);
            playSound(m_bossManager.B01MegaSheepBAAAASound);
        }

        // Process texture changes
        //
        bossMote.doTextures();

        // Check and set boss is dead
        //
		/*
        if (bossMote.bossIsDead())
        {
            // Check dead in terms of HP and then set dead in terms of processing
            //
            bossMote.setDead(true);
        }*/
    }


    /// <summary>
    /// Normal level playing
    /// </summary>
    protected void doPlayingLevel()
    {

        // Don't perform game updates when the hud control is active or the level
        // is completed.  We have to check for hud control existence here as it 
        // might not have been constructed yet.
        //
        if (m_moteHud != null && !m_moteHud.getHudControlActive() /* && !m_worldStart*/ )
        {
            // Don't do any level population until m_levelGenerationPause seconds have passed into the level
            //
            if ((Time.time - m_startTime) > (m_levelGenerationPause) && !m_levelComplete)
            {
                levelPopulation();
            }

            // Check ship collisions only if we're still playing
            //
            if (!m_levelComplete)
            {
                checkCollisions();
            }

            // Check to see if we need to do anything with a boss mote
            //
            checkBoss();

            // Do mote animation
            //
            animateMotes();

            // And the mote ship
            //
            animateMoteShip();

            // Transient animations
            //
            animateTransients();

            // Check for completion only if it's not already completed
            //
            if (!m_levelComplete)
            {
                checkForCompletion();
            }
        }

    }

    /// <summary>
    /// Basis for scaling our fonts
    /// </summary>
    protected float m_fontScaleBasis = 200.0f;

    /// <summary>
    /// Do the interstitial until it's completed
    /// </summary>
    protected void doInterstitial()
    {
        //Debug.Log("doInterstitial");
        // Set the start time if it's not been already
        //
        if (m_interstitialStart == 0)
        {
			// Resize interstitial
			//
			Texture2D text2d = m_interstitialTexture as Texture2D;

//#if !UNITY_WINRT
            // This doesn't really work anyway so swap it out for something less needy
			//TextureScale.Bilinear(text2d, Screen.width, Screen.height);
//#endif
			
			//Texture resizeTexture =  m_interstitialTexture.
			
			
            //Debug.Log("--> Initialise interstitial with current world = " + CurrentWorld);

            m_interstitialStart = Time.time;
            m_lastPopTimeNotify = Time.time + 15.0f; // ensure we have no advice during intro
            m_moteHud.setGameObjectsActivity(false);

            // Initialise the interstitial object
            m_interstitialObject.guiTexture.texture = text2d;
            m_interstitialObject.transform.localScale = Vector3.zero;
            m_interstitialObject.transform.localPosition = Vector3.zero;
			
			// Dim the interstitial
			//
			Color tempColour = m_interstitialObject.guiTexture.color;
			tempColour.a = 0.2f;
			m_interstitialObject.guiTexture.color = tempColour;
			
			// Font scaling
			//
            m_interstitialTitleMessage.guiText.fontSize = (int)((float)m_interstitialTitleMessage.guiText.fontSize * (Screen.width / m_fontScaleBasis));
            m_interstitialIntroductionMessage.guiText.fontSize = (int)((float)m_interstitialIntroductionMessage.guiText.fontSize * (Screen.width / m_fontScaleBasis));
				
            // Capture the initial inset value
            //
            m_initialInset = m_interstitialObject.guiTexture.pixelInset;

            // Now reset the aspect of the picture using the vertical length as the absolute measure
            //
            float aspect = Screen.height / m_initialInset.height;
            m_interstitialObject.guiTexture.pixelInset = new Rect(Screen.width, 0, m_initialInset.width * aspect, Screen.height);

            m_backgroundObject.SetActive(false);
            m_interstitialObject.SetActive(true);
            m_interstitialTitleMessage.SetActive(true);
            m_interstitialIntroductionMessage.SetActive(true);

            // Play the interstitial audio clip
            //
            playSound(m_interstitialAudioClip);

            //Debug.Log("doInterstitial - Setting background to CurrentWorld " + CurrentWorld + " with width = " + m_interstitialObject.guiTexture.texture.width + " and height = " + m_interstitialObject.guiTexture.texture.height);
        }

        //float scale = (m_initialInset.width - Screen.width ) / (m_interstitialLength / 2);
        float expandPeriod = m_interstitialLength / 8;
        float timeNow = Time.time - m_interstitialStart;

        // Zoom out on the texture and the in again
        //
        if (timeNow < expandPeriod)
        {


			//float aspect = m_interstitialObject.guiTexture.pixelInset.height / m_interstitialObject.guiTexture.pixelInset.width;
			//float x = Screen.width - (Screen.width * (timeNow / expandPeriod));
			float x = Screen.width - (m_interstitialObject.guiTexture.pixelInset.width * (timeNow / expandPeriod));
			
            //Debug.Log("SLIDING x = " + x);
            Rect newRect = new Rect(x, 0, m_interstitialObject.guiTexture.pixelInset.width, m_interstitialObject.guiTexture.pixelInset.height);
            m_interstitialObject.guiTexture.pixelInset = newRect;

#if SLIDE_IN
                Vector2 startPoint = new Vector2(Screen.width, 0);
                float aspect = m_initialInset.height / m_initialInset.width;
                Vector2 endPoint = new Vector2((Screen.width - Screen.width * aspect) / 2, 0);
                Debug.Log("Start point x = " + startPoint.x + ", y = " + startPoint.y);
                Debug.Log("End point x = " + endPoint.x + ", y = " + endPoint.y);
                Debug.Log("Expand period = " + expandPeriod);

                Vector2 moveVector = (endPoint - startPoint) / expandPeriod;
                Debug.Log("Move vector x = " + moveVector.x + ", y = " + moveVector.y);

                Vector2 position = Tweening.easeOutCubic(startPoint, moveVector, timeNow , expandPeriod);
                Debug.Log("position x = " + position.x + ", y = " + position.y);
                Rect newRect = new Rect(position.x, position.y, m_interstitialObject.guiTexture.pixelInset.width, m_interstitialObject.guiTexture.pixelInset.height);

                //Debug.Log("rect x = " + newRect.x + ", y = " + newRect.y);
                m_interstitialObject.guiTexture.pixelInset = newRect;
#endif
        }

        // Do the slide messages
        //
		float titleY = Screen.height / 10;
        slideMessage(m_interstitialTitleMessage, m_interstitialStart + 0.3f, m_levelManager.getLevelSet().getName(), new Vector2(-500, titleY), new Vector2(0, titleY), new Vector2(500, titleY), 1.0f, 10.0f, 0.0f);
        slideMessage(m_interstitialIntroductionMessage, m_interstitialStart + 0.3f, m_levelManager.getLevelSet().getIntroductionText(), new Vector2(Screen.width + 300, -titleY), new Vector2(0, -titleY), new Vector2(-500, -titleY), 1.0f, 10.0f, 0.0f);

        // If the interstitial has completed then prepare the game for playing
        //
        if (Time.time - m_interstitialStart > m_interstitialLength)
        {
            //Debug.Log("Starting to Play Game at time " + Time.time);

            // Starting to play game at this point
            //
            m_state = WorldSceneState.Playing;
            m_startTime = Time.time;
            m_lastPopTimeNotify = Time.time + 10.0f;

            // Only reset this if it's the first time through
            //
            if (m_livesAtStart == -1)
            {
                m_livesAtStart = m_player.getLives();
            }

            m_levelManager.getGameLevel().resetClock(m_levelGenerationPause);

            m_interstitialTitleMessage.SetActive(false);
            m_interstitialIntroductionMessage.SetActive(false);
            m_interstitialObject.SetActive(false);
            setBackgroundTexture();
            m_moteShip.getGameObject().SetActive(true);
            m_moteHud.setGameObjectsActivity(true);

            // Hide the level/completion message
            //
            m_completionMessage.SetActive(false);
        }
    }

    /// <summary>
    /// Check for back button in game for WP8
    /// </summary>
    protected void checkExit()
    {
        // Need to handle back button in WP8
        //
#if UNITY_WINRT
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            saveGameState();
            Application.LoadLevel(1);  // jump back to starts screen
        }
#endif
    }

    /// <summary>
    /// Heartbeat called by unity
    /// </summary>
    void Update()
    {

#if UNITY_WINRT        
        checkExit();
#endif
        if (m_state == WorldSceneState.Interstitial)
        {
            doInterstitial();
        }
        else if (m_state == WorldSceneState.Playing)
        {
            // Check for game completion
            //
            if (m_levelManager.isGameCompleted())
            {
                m_state = WorldSceneState.GameCompleted;
                return;
            }

            // We're playing the level - so keep animating everything
            //
            doPlayingLevel();

            // Handle any touches
            //
            if (WorldScene.isMobilePlatform())
            {
                doTouch();
				doAccelerometer2();
            }
            else
            {
                doMouse();
            }

            // Hud update
            //
            updateHud();

            // Pan the background
            //
            updateBackground();

            // Check and set playing status
            //
            checkAudio();

            // Now let's see if we've scored any achievements and handle that
            //
            checkAchievements();

            // Check to see if we need to send an encouragment to do something
            //
            checkEncouragements();
            
        }
        else if (m_state == WorldSceneState.DeathScreen)
        {
            // Handle any touches after a nice little wait
            //
			if (Time.time > m_completeTime + 3.0f)
			{
                if (WorldScene.isMobilePlatform())
	            {
	                doTouch();
	            }
	            else
	            {
	                doMouse();
	            }
			}

            // Keep the HUD updated and any animations going
            //
            updateHud();

            // Transient animations
            //
            animateTransients();

            // Pan the background
            //
            updateBackground();
        }
        else if (m_state == WorldSceneState.GameCompleted)
        {
            doVictoryScreen();

            // Handle any touches
            //
		    if (Time.time > m_completeTime + 5.0f)
			{
                if (WorldScene.isMobilePlatform())
	            {
	                doTouch();
	            }
	            else
	            {
	                doMouse();
	            }
			}
            // Keep the HUD updated and any animations going
            //
            updateHud();

            // Transient animations
            //
            animateTransients();

            // Pan the background
            //
            updateBackground();
        }
        else if (m_state == WorldSceneState.PostLevelSummary)
        {
            // Sum up the achievements on this level - we might not use this state yet
            //
        }

    }

    /// <summary>
    /// Check to see if we need to send an encouragement
    /// </summary>
    protected void checkEncouragements()
    {
		// Don't send any encouragments on a boss level - unless it's a boss related one.
		if (m_levelManager.getGameLevel().isBoss() || m_state != WorldSceneState.Playing)
			return;
		
        if (Time.time > m_lastPopTimeNotify)
        {
            Vector2 advicePos = new Vector2(Screen.width / 2, Screen.height / 2);
            if (Random.value < 0.5f)
            {
                //advicePos.x -= m_transientManager.m_popThoseMotes.width / 2;
                launchTransient(advicePos, m_transientManager.m_popThoseMotes, 3.0f, doScreenScale() * 3.0f);
            }
            else
            {
                ///advicePos.x -= m_transientManager.m_shakeIt.width / 2;
                launchTransient(advicePos, m_transientManager.m_shakeIt, 3.0f, doScreenScale() * 3.0f);
            }

            // Update this
            //
            m_lastPopTimeNotify = Time.time + 10.0f;
        }
    }


    /// <summary>
    /// Mobile platform test
    /// </summary>
    /// <returns></returns>
    static public bool isMobilePlatform()
    {
        return (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer ||
                //Application.platform == RuntimePlatform.MetroPlayerX86 ||
                //Application.platform == RuntimePlatform.MetroPlayerX64 ||
                //Application.platform == RuntimePlatform.MetroPlayerARM ||
                Application.platform == RuntimePlatform.WP8Player);
    }


    /// <summary>
    /// Check the achievement manager for anything doing
    /// </summary>
    protected void checkAchievements()
    {
        // Any achievements we pull at this point are also logged for posterity in
        // the achievement manager for future retrieval.  Any achievements fetched
        // here are cleared from the nonviewed queue so we should be confident that
        // this code will only fire once per actual achievement.
        //
        foreach (Achievement ach in m_achievementCentre.getNonViewedAchievements())
        {
            if (ach.GetType() == typeof(RunAchievement))
            {
                RunAchievement runAch = (RunAchievement)ach;
                //Debug.Log("GOT RUN OF " + runAch.getNumberInRun() + " KILLS at " + ach.getDateTime().ToLongTimeString());

                if (runAch.getNumberInRun() == 3)
                {
                    //Debug.Log("NICE ONE position = " + m_lastPopPosition);
                    launchTransient(m_lastPopPosition, m_transientManager.m_niceOne, 1.0f, doScreenScale());
                }
                else
                {
                    //Debug.Log("KILL STREAK position = " + m_lastPopPosition);
                    launchTransient(m_lastPopPosition, m_transientManager.m_killStreak, 1.0f, doScreenScale());
                }

                // Add the score with multiplier
                //
                m_player.addScore(m_levelManager.getGameLevelNumber(), ach.getBonus() * m_player.getDifficultyLevel());
            }
        }
    }


    /// <summary>
    /// Check for mote collisions
    /// </summary>
    protected void checkCollisions()
    {
        foreach (Mote mote in WorldScene.m_moteList)
        {
            // Skip inactive
            if (!mote.getGameObject().activeInHierarchy || mote.isDead())
                continue;

            // Fetch by type
            //
            Rect moteRect;

            //if (mote is RotateMote)
            //{
                //moteRect = ((RotateMote)mote).m_rotatableGuiItem.getScreenRect();
            //}
            //else
            //{
                moteRect = mote.getGameObject().guiTexture.GetScreenRect();
            //}

            Rect moteShipRect = m_moteShip.getGameObject().guiTexture.GetScreenRect();

            // If this is a good mote then we handle it differently
            //
            if (mote.isGoodMote())
            {
				
				if (betterRectIntersect(moteRect, moteShipRect))
				{
	                // Depends on mote type here
	                //
	                if (mote.GetType() == typeof(CellMote))
	                {
	                    CellMote cM = (CellMote)mote;
						
						//Debug.Log("CELL MOTE INTERSECT");
						
	                    if (cM.getCellState() == CellMoteState.Opened)
	                    {
	                        //Debug.Log("Get prize!!");
	
	                        switch (cM.getPrize())
	                        {
	                            // Activate the shield
	                            //
	                            case CellPrize.Shield:
	                                m_player.addPowerup(PlayerPowerup.Shield);
	                                m_moteShip.setTexture(MoteShipShield);
	
	                                // Need to loop this somehow
	                                //
	                                playSound(MoteShipShieldNoise);
	
	                                // Shields up!
	                                //
	                                Vector2 centre = new Vector2(Screen.width / 2, Screen.height / 2);
	                                launchTransient(centre, m_transientManager.m_shieldsUp, 2.0f, doScreenScale());
	
	                                mote.setDead();
	                                mote.hideMote();
									continue;
	                                //break;
	
	                                // Grenade only works when you click on it - it's not collected by intersection
	                            case CellPrize.Grenade:
	                                break;
	
	                            default:
	                                break;
	                        }
	                    }
	                }
				}
    
                // If a GoodMote is not armed then it can't hurt either our moteship or any foes
                //
                if (!mote.isArmed())
                    continue;
                
                // Check for collisions with a boss mote for example
                //
                BossMote bossMote = getBossFromActiveList();

                if (bossMote != null)
                {
                    Rect bossRect = bossMote.getGameObject().guiTexture.GetScreenRect();

                    if (betterRectIntersect(moteRect, bossRect))
                    {
                        // Remove a HP - might also want to make this change textures for a second
                        //
                        bossMote.removeBossHP();
                        mote.setDead();
                        launchTransient(mote.getInsetCentrePosition(), InvincibilityTexture, 2.0f, doScreenScale());
                        playSound(m_bossManager.B01MegaSheepBAAAASound);
                    }
                }

                // No further processing for the moment - need to restructure this later on
                //
                continue;
            }


            // Adjust mote ship size a bit as we're intersecting with rectangles
            //
            moteShipRect.x += 30;
            moteShipRect.y += 50;
            moteShipRect.width -= 60;
            moteShipRect.height -= 100;

            //if (rectIntersect(mote.getGameObject().guiTexture.GetScreenRect(), m_moteShip.getGameObject().guiTexture.GetScreenRect()))
            if (betterRectIntersect(moteRect, moteShipRect))
            {
                // Check for shield
                //
                if (m_player.testPowerUp(PlayerPowerup.Shield))
                {
					Debug.Log("SHIELD POPPED MOTE");
					
                    // Pop the mote and ensure scoring and transients are still launched
                    popMote(mote, false, true);
					
                    m_player.removePowerup(PlayerPowerup.Shield);
                    m_moteShip.setTexture(MoteShip);
                }
                else
                {
					Debug.Log("DEATH");
                    // Do ship death
                    //
                    doDeath();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Death
    /// </summary>
    protected void doDeath()
    {
        // Start off the death spiral
        //
        m_moteShip.doDeath();
        WorldScene.m_player.decrementLives();
        playSound(DeathKnell);

        // Reset and escape
        //
        resetLevel(false);
    }

    /*
    /// <summary>
    /// Rect intersection - this seems to be way wrong...
    /// </summary>
    /// <param name="rectA"></param>
    /// <param name="rectB"></param>
    /// <returns></returns>
    public bool rectIntersect(this Rect rectA, Rect rectB)
    {
        return (Mathf.Abs(rectA.x - rectB.x) < (Mathf.Abs(rectA.width + rectB.width) / 2))
            && (Mathf.Abs(rectA.y - rectB.y) < (Mathf.Abs(rectA.height + rectB.height) / 2));
    }*/

    /// <summary>
    /// Destroy all active motes
    /// </summary>
    protected void explodeGrenade(Vector2 explodePosition)
    {
        Debug.Log("explodeGrenade() - destroy all active motes");
        // Grenade explosion
        //
        //launchTransient(explodePosition, m_transientManager.m_explosion, 2.0f, doScreenScale());
        launchTransient(explodePosition, m_transientManager.m_grenade, 2.0f, doScreenScale());
        playSound(GrenadeNoise);


        // Pop everything
        //
        foreach (Mote mote in m_moteList)
        {
            popMote(mote, false);

            launchTransient(mote.getInsetCentrePosition(), m_transientManager.m_explosion, 1.5f, doScreenScale() * 0.5f);
        }
    }

    /// <summary>
    /// A better intersection test
    /// </summary>
    /// <param name="rectA"></param>
    /// <param name="rectB"></param>
    /// <returns></returns>
    protected bool betterRectIntersect(Rect rectA, Rect rectB)
    {
        return (rectA.xMin <= rectB.xMax &&
      rectB.xMin <= rectA.xMax &&
      rectA.yMin <= rectB.yMax &&
      rectB.yMin <= rectA.yMax);
    }



    /// <summary>
    /// Check whether or not we should be playing music
    /// </summary>
    protected void checkAudio()
    {
        // Fetch this the first time through
        //
        if (m_levelDetails == null)
        {
            m_levelDetails = GameObject.FindGameObjectWithTag("LevelDetails");
        }

        // Check and set music playing status
        //
        if (WorldScene.m_player.wantsMusic())
        {
            if (!m_levelDetails.audio.isPlaying)
            {
                m_levelDetails.audio.Play();
            }
        }
        else
        {
            if (m_levelDetails.audio.isPlaying)
            {
                m_levelDetails.audio.Pause();
            }
        }

        // Set the hud texture according to what is selected
        //
        m_moteHud.setHudControls(WorldScene.m_player.wantsMusic(), WorldScene.m_player.wantsSoundEffects());
    }

    /// <summary>
    /// Pan a background image
    /// </summary>
    protected void updateBackground()
    {
        if (m_backgroundObject == null)
            return;

        Rect pixelInset = m_backgroundObject.guiTexture.pixelInset;
        //pixelInset.x--;
        //pixelInset.y--;

        // Add a random direction to the background vector
        //
        float divisor = 50.0f;
        if (Random.value < 0.5f)
        {
            if (pixelInset.x < -pixelInset.width / 2)
                m_backgroundVector.x += Random.value / divisor;
            else
                m_backgroundVector.x += -Random.value / divisor;
        }
        else
        {
            if (pixelInset.y < -pixelInset.height / 2)
                m_backgroundVector.y += Random.value / divisor;
            else
                m_backgroundVector.y += -Random.value / divisor;
        }

        pixelInset.x += m_backgroundVector.x;
        pixelInset.y += m_backgroundVector.y;

        m_backgroundObject.guiTexture.pixelInset = pixelInset;

    }

    protected float m_fontSizeScreenWidth = 20.0f;

    /// <summary>
    /// Slide a message in over a time frame
    /// </summary>
    /// <param name="?"></param>
    protected void slideMessage(GameObject gameObject, float startTime, string message, Vector2 startPoint, Vector2 pausePoint, Vector2 finishPoint, float messageIn, float messagePause, float messageOut)
    {
        if (Time.time - startTime < messageIn)
        {
            Vector2 linearVector = (pausePoint - startPoint) / messageIn;
            gameObject.guiText.text = message;
            gameObject.guiText.fontSize = (int)(Screen.width / m_fontSizeScreenWidth);
            gameObject.guiText.pixelOffset = Tweening.easeOutCubic(startPoint, linearVector, Time.time - startTime, messageIn);
            gameObject.SetActive(true);
        }
        else if (Time.time - startTime < messageIn + messagePause)
        {
            gameObject.SetActive(true);
            m_slideMessageRestartPoint = gameObject.guiText.pixelOffset;
        }
        else if (Time.time - startTime < messageIn + messagePause + messageOut)
        {
            if (messageOut != 0.0f)
            {
                Vector2 linearVector = (finishPoint - pausePoint) / messageOut;
                gameObject.guiText.pixelOffset = Tweening.easeInCubic(m_slideMessageRestartPoint, linearVector, Time.time - (startTime + messageIn + messagePause), messageOut);
                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(messageOut != 0.0f);
        }
    }

    /// <summary>
    /// Update the hud messages as necessary
    /// </summary>
    protected void updateHud()
    {
        if (m_levelManager.getGameLevel() == null)
        {
            Debug.Log("WorldScene::updateHud() - null game level " + m_levelManager.getGameLevelNumber());
            return;
        }

        if (WorldScene.m_moteHud != null)
        {
            // Keep high score current
            //if (m_player.m_totalScore > m_player.m_highScore)
                //m_player.m_highScore = m_player.m_totalScore;

            // Set the hud
            //
            WorldScene.m_moteHud.setHud(m_player.getTotalScore(), Mathf.Max(m_player.getTotalScore(), m_player.getHighScore()), m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()), m_player.getLives(), m_levelManager.getGameLevel());
        }

        // Check and display level finished hud and return if we have completed
        //
        if (levelFinishedHud())
            return;

        if (m_state != WorldSceneState.Interstitial)
        {
            // Level introductory messages
            //
            doLevelMessage();
        }

        // Do the HUD control animation
        //
        hudControlAnimation();

        // Hud lives showing
        //
        updateHudLives();
    }


    /// <summary>
    /// Display a level finished hud and return true if the level is now completed and we don't
    /// want further updates
    /// </summary>
    /// <returns></returns>
    protected bool levelFinishedHud()
    {

        if (m_levelComplete && m_player.getLives() == 0)
        {
            slideMessage(m_completionMessage, m_completeTime, "GAME OVER", new Vector2(-500, 70), new Vector2(0, 70), new Vector2(500, 70), 1.0f, 10.0f, 0.0f);

            // Build twitter string
            //
            m_twitterString = "I'm+playing+Mote+Wars+";
            
            if (m_isLite)
                m_twitterString += "Lite+";

            m_twitterString += "and+just+got+a+";

            string missonStats = m_player.getTotalScore().ToString();

            if (m_player.getTotalScore() > m_player.getHighScore())
            {
                missonStats += " NEW RECORD!";
                m_twitterString += "new+record+score+of++" + m_player.getTotalScore();
            }
            else
            {
                m_twitterString += "score+of++" + m_player.getTotalScore();
            }

            // Complete twitter string
            //
            m_twitterString += ".+Try+it+for+iOS,Android+WP8!";
            slideMessage(m_welcomeMessage, m_completeTime, missonStats, new Vector2(500, -70), new Vector2(0, -70), new Vector2(-500, -70), 1.0f, 10.0f, 0.0f);

#if FACEBOOK_ENABLED
                m_facebookButton.SetActive(true);
#endif
            
			if (m_twitter)
            	m_twitterButton.SetActive(true);

            if (m_doEnding && Time.time > m_completeTime + DeathKnell.length)
            {
                m_doEnding = false;
                playSound(UnhappyCompletion);
            }

            return true;
        }

        // Completion message
        //
        if (m_levelComplete)
        {
            // Turn off the level message
            //
            m_welcomeMessage.SetActive(false);
            m_hintMessage.SetActive(false);

            string levelCompletionMessage = m_levelManager.getGameLevel().getCompletionMessage(m_completeTime);
            //levelCompletionMessage += "\nScore on this level = " + m_player.getLevelScore();

            //Debug.Log("HISTORY LEVEL SCORE = " + m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()));

            // Notify of a record and set the level score historical
            //
            if (m_player.getLevelScore() > m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()))
            {
                m_medal = m_levelManager.getGameLevel().getMedalAwarded(m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()), m_player.getLevelScore());
                m_player.setHistoryLevelScore(m_levelManager.getGameLevelNumber(), m_player.getLevelScore());
            }

            // Check for a clean level
            //
            //Debug.Log("LIVES AT START = " + m_livesAtStart + ", player lives = " + m_player.getLives());
            if (m_livesAtStart == m_player.getLives())
            {
                levelCompletionMessage += "\nClean level bonus = " + m_cleanLevelBonus;

                if (m_player.getDifficultyLevel() > 1)
                {
                    levelCompletionMessage += " x " + m_player.getDifficultyLevel() + " difficulty bonus";
                }
            }
            //else
            //{
                //levelCompletionMessage += "\nNo level bonus";
            //}

            // Show that it's a new record
            //
            if (m_medal != LevelScoreIdentifier.None)
            {
                levelCompletionMessage += "\nYou've got a new ";

                switch (m_medal)
                {
                    case LevelScoreIdentifier.Gold:
                        levelCompletionMessage += "GOLD";
                        break;

                    case LevelScoreIdentifier.Silver:
                        levelCompletionMessage += "SILVER";
                        break;

                    case LevelScoreIdentifier.Bronze:
                        levelCompletionMessage += "BRONZE";
                        break;

                    default:
                        break;
                }

                levelCompletionMessage += " medal!";

                // Show the stars awards
                //
                m_transientManager.showAwards(m_medal, new Vector2(Screen.width / 2, Screen.height / 5));
            }

            // Note that getGameLevelNumber will get the _next_ level number which is actually the one we want to report
            //
            slideMessage(m_completionMessage, m_completeTime, levelCompletionMessage, new Vector2(-500, -70), Vector2.zero, new Vector2(500, 70), 1.0f, 10.0f, 0.0f);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Animate the level introductory messages
    /// </summary>
    protected void doLevelMessage()
    {
        //Debug.Log("doLevelMessage");

        // Some durations
        //
        float levelMessageInDuration = m_levelGenerationPause / 4; // 1.1f;
        float levelMessagePauseDuration = m_levelGenerationPause / 2; //1.0f;
        float levelMessageOutDuration = m_levelGenerationPause / 4; //0.5f;

        // Level message - THIS NEEDS TIDYING UP - move this code into the slideMessage
        //
        if (Time.time - m_startTime < levelMessageInDuration)
        {
            Vector2 startPoint = new Vector2(500, 80);
            Vector2 endPoint = new Vector2(0, 80);
            Vector2 linearVector = (endPoint - startPoint) / levelMessageInDuration;

            string multiplierMessage = "";

            if (m_player.getDifficultyLevel() > 1)
                multiplierMessage = "\n( x " + m_player.getDifficultyLevel() + " multiplier )";

            // Level number and name
            //
            m_welcomeMessage.guiText.text = (m_levelManager.getGameLevel().getLevelNumber() + 1) + ". " + m_levelManager.getGameLevel().getName() + multiplierMessage;
            m_welcomeMessage.guiText.fontSize = (int)(Screen.width / m_fontSizeScreenWidth);
            m_welcomeMessage.guiText.pixelOffset = Tweening.easeOutCubic(startPoint, linearVector, Time.time - m_startTime, levelMessageInDuration);
            m_welcomeMessage.SetActive(true);
        }
        else if (Time.time - m_startTime < (levelMessageInDuration + levelMessagePauseDuration))
        {
            m_welcomeMessage.SetActive(true);
            m_welcomeRestartPoint = m_welcomeMessage.guiText.pixelOffset;
        }
        else if (Time.time - m_startTime < (levelMessageInDuration + levelMessagePauseDuration + levelMessageOutDuration))
        {
            Vector2 startPoint = m_welcomeRestartPoint;
            Vector2 endPoint = new Vector2(-1000, 80);
            Vector2 linearVector = (endPoint - startPoint) / levelMessageOutDuration;
            m_welcomeMessage.guiText.pixelOffset = Tweening.easeInCubic(startPoint, linearVector, Time.time - (m_startTime + levelMessageInDuration + levelMessagePauseDuration), levelMessageOutDuration);
            m_welcomeMessage.SetActive(true);
        }
        else
        {
            m_welcomeMessage.SetActive(false);
        }

        // Do a hint message
        //
        float hintMessageDelay = m_levelGenerationPause / 8; //0.3f;
        float hintMessageInDuration = m_levelGenerationPause / 4; // 1.1f;
        float hintMessagePauseDuration = m_levelGenerationPause / 2; // 0.5f;
        float hintMessageOutDuration = m_levelGenerationPause / 7; // 0.5f;

        if (Time.time - m_startTime - hintMessageDelay < hintMessageInDuration)
        {
            Vector2 startPoint = new Vector2(100, -500);
            Vector2 endPoint = new Vector2(100, -50);
            Vector2 linearVector = (endPoint - startPoint) / hintMessageInDuration;
            m_hintMessage.guiText.text = m_levelManager.getGameLevel().getHint();
            m_hintMessage.guiText.fontSize = (int)(Screen.width / m_fontSizeScreenWidth);
            m_hintMessage.guiText.pixelOffset = Tweening.easeOutCubic(startPoint, linearVector, Time.time - m_startTime, hintMessageInDuration);
            m_hintMessage.SetActive(true);
        }
        else if (Time.time - m_startTime - hintMessageDelay < hintMessageInDuration + hintMessagePauseDuration)
        {
            m_hintMessage.SetActive(true);
            m_hintRestartPoint = m_hintMessage.guiText.pixelOffset;
        }
        else if (Time.time - m_startTime - hintMessageDelay < hintMessageInDuration + hintMessagePauseDuration + hintMessageOutDuration)
        {
            Vector2 startPoint = m_hintRestartPoint;
            Vector2 endPoint = new Vector2(100, 500);
            Vector2 linearVector = (endPoint - startPoint) / hintMessageOutDuration;
            m_hintMessage.guiText.pixelOffset = Tweening.easeInCubic(startPoint, linearVector, Time.time - (m_startTime + hintMessageDelay + hintMessageInDuration + hintMessagePauseDuration), hintMessageOutDuration);
            m_hintMessage.SetActive(true);
        }
        else
        {
            m_hintMessage.SetActive(false);
        }
    }

    /// <summary>
    /// Show the number of ships/lives we have on the hud
    /// </summary>
    protected void updateHudLives()
    {
        // Initial
        //
        if (m_lifeObjects.Count == 0)
        {
            // Make some moteship icons for lives
            //
            for (int i = 0; i < WorldScene.m_player.getLives() - 1; i++)
            {
                GameObject g = new GameObject("MoteShipLife");
                g.AddComponent("GUITexture");
                g.guiTexture.texture = m_moteShip.getGameObject().guiTexture.texture;
                g.transform.position = Vector3.zero;
                g.transform.localScale = Vector3.zero;
                float scale = 0.05f;
                //g.guiTexture.pixelInset = new Rect(1.0f + m_moteShip.guiTexture.texture.width * scale * i * 1.5f, 0.2f, m_moteShip.guiTexture.texture.width * scale, m_moteShip.guiTexture.texture.height * scale);
                float x = 500 + m_moteShip.getGameObject().guiTexture.texture.width * scale * i * 1.2f;
                float y = 0.2f;
                g.guiTexture.pixelInset = new Rect(x, y, m_moteShip.getGameObject().guiTexture.texture.width * scale, m_moteShip.getGameObject().guiTexture.texture.height * scale);
                m_lifeObjects.Add(g);
            }
        }
        else
        {
            //Debug.Log("GET LVES = " + WorldScene.m_player.getLives() + ", LIFE OBJECTS = " + (m_lifeObjects.Count + 1));
            // Ensure lives decrement on HUD
            //
            if (WorldScene.m_player.getLives() - 1 < m_lifeObjects.Count)
            {
                DestroyObject(m_lifeObjects[WorldScene.m_player.getLives() - 1]);
            }
        }
    }


    /// <summary>
    /// Animate the hud control
    /// </summary>
    protected void hudControlAnimation()
    {
        // Animate the hud control if we need to - check activity state and positions
        //
        if (!m_moteHud.isHudControlAnimating())
		{
			// First time through
			//
			if (m_cheatEndPoint == Vector2.zero)
			{
				m_cheatEndPoint = new Vector2(m_hudControl.guiTexture.pixelInset.x, m_hudControl.guiTexture.pixelInset.y);
				//Debug.Log("CHEAT END POINT FIRST TIME x = " + m_cheatEndPoint.x + ", y = " + m_cheatEndPoint.y);
			}
			
            return;
		}

        if (m_hudStartTime == 0.0f)
            m_hudStartTime = Time.time;

        float hudDuration = 0.5f;

        // If HUD control is active but not in final position then keep animating
        //
        if (m_moteHud.getHudControlActive() && Time.time < m_hudStartTime + hudDuration)
        {
            Vector2 result = Tweening.easeOutCubic(m_moteHud.getHudMoveStartPoint(), m_moteHud.getHudMoveVector(hudDuration), Time.time - m_hudStartTime, hudDuration);
            //Debug.Log("OUT RESULT x = " + result.x + ",y = " + result.y);
            m_hudControl.guiTexture.pixelInset = new Rect(result.x, result.y, m_hudControl.guiTexture.pixelInset.width, m_hudControl.guiTexture.pixelInset.height);
            //m_moteHud.setHudControlPositionActive(m_hudControl.guiTexture.pixelInset);
		}
        else if (!m_moteHud.getHudControlActive() && Time.time < m_hudStartTime + hudDuration)
        {
            // If the control is inactive and the position is not inactive then carry on animating as well
            //
            Vector2 result = Tweening.easeInCubic(m_cheatEndPoint /* m_moteHud.getHudMoveEndPoint() */, -m_moteHud.getHudMoveVector(hudDuration), Time.time - m_hudStartTime, hudDuration);
            //Debug.Log("IN RESULT x = " + result.x + ",y = " + result.y);
            m_hudControl.guiTexture.pixelInset = new Rect(result.x, result.y, m_hudControl.guiTexture.pixelInset.width, m_hudControl.guiTexture.pixelInset.height);
        }


        // Resetting
        //
        if (Time.time > m_hudStartTime + hudDuration)
        {
            // We're not animating any more
            //
            m_moteHud.setHudControlAnimating(false);
            m_hudStartTime = 0.0f;
			
			
			// If control is not active then we're running so resume the clock countdown
			//
			if (!m_moteHud.getHudControlActive())
			{
				m_levelManager.getGameLevel().setGameRunning();
			}
			else
			{
				// Set paused once if we're now paused
				//
				if (!m_levelManager.getGameLevel().isGamePaused())
				{
					m_levelManager.getGameLevel().setGamePaused();
				}
			}
            //m_moteHud.setHudControlPositionActive(m_hudControl.guiTexture.pixelInset);
            //Debug.Log("RESET HUD START TIME");
            //Debug.Log("FINISH POINT = x " + m_hudControl.guiTexture.pixelInset.x + ", y = " + m_hudControl.guiTexture.pixelInset.y);
            //Debug.Log("RESTART POINT x = " + m_moteHud.getHudControlPositionActive().x + ", y = " + m_moteHud.getHudControlPositionActive().y);
            //Debug.Log("FINAL POINT x = " + m_moteHud.getHudControlPositionInactive().x + ", y = " + m_moteHud.getHudControlPositionInactive().y);

			
            m_cheatEndPoint = new Vector2(m_hudControl.guiTexture.pixelInset.x, m_hudControl.guiTexture.pixelInset.y);
			Debug.Log("CHEAT END POINT x = " + m_cheatEndPoint.x + ", y = " + m_cheatEndPoint.y);
        }
    }

    /// <summary>
    /// Can we find the boss mote in the active list?
    /// </summary>
    /// <returns></returns>
    protected BossMote getBossMote()
    {
        if (m_levelManager.getGameLevel().getBoss() == null)
            return null;

        BossMote bossMote = null;

        foreach (Mote mote in m_moteList)
        {
            if (mote.GetType() == m_levelManager.getGameLevel().getBoss().GetType())
            {
                bossMote = (BossMote)mote;
            }
        }
        return bossMote;
    }

    /// <summary>
    /// Check for level completion
    /// </summary>
    protected void checkForCompletion()
    {
        if (m_levelManager == null)
        {
            Debug.Log("Got null m_levelManager");
            return;
        }

        if (m_levelManager.getGameLevel() == null)
        {
            Debug.Log("Got null at m_levelManager.getGameLevel()");
            return;
        }


        // Test for an expired time limit on a time limited level
        //
        if (m_levelManager.getGameLevel().getType() == LevelType.MotesTimeLimit &&
            m_levelManager.getGameLevel().levelTimeLimitExpired())
        {
            // We lose a life but we also reset the clock on this level
            //
            doDeath();

            // Reset clock 
            m_levelManager.getGameLevel().resetClock();
        }

        // Now see if we've completely run out of lives
        //
        if (m_player.getLives() == 0)
        {
            gameFailure();
            return;
        }

        // Level completion
        //
        bool levelCompletion = m_levelManager.getGameLevel().levelCompletedLogic(m_player.getLives());

        // Now do the boss text
        //
        if (m_levelManager.getGameLevel().isBoss() && m_levelManager.getGameLevel().hasBossBeenGenerated())
        {
            //Debug.Log("TESTING BOSS GENERATION");
            bool bossAlive = false;
            // Is the boss still alive?
            //
            foreach (Mote mote in m_moteList)
            {
                // Can we find the boss mote in the active list?
                //
                if (mote.GetType() == m_levelManager.getGameLevel().getBoss().GetType())
                {
					BossMote bossMote = (BossMote)mote;
					bossAlive = !bossMote.bossIsDead();
                    //Debug.Log("FOUND BOSS MOTE in ALIVE LIST");
                    //bossAlive = true;
                }
            }

            if (!bossAlive)
            {
                levelCompletion = true;
            }
        }

        // Completed the level if none are active or boss has been cleared
        //
        //
        if (levelCompletion)
        {
            // Do completion for normal or boss level
            //
            m_levelComplete = true;
            m_completeTime = Time.time;

            // Add a bonus
            //
            WorldScene.m_player.addScore(m_levelManager.getGameLevelNumber(), m_cleanLevelBonus * m_player.getDifficultyLevel());

            playSound(LevelCompleteClip);

            Level level = m_levelManager.getGameLevel();
            if (level == null)
            {
                Debug.Log("Couldn't get new level");
            }

            if (m_levelManager.isGameCompleted())
            {
                m_completionMessage.guiText.anchor = TextAnchor.MiddleCenter;
                m_completionMessage.guiText.text = "You've completed MoteWars!  Well done!";
                m_completionMessage.guiText.fontSize = (int)(Screen.width / m_fontSizeScreenWidth);
                m_completionMessage.guiText.pixelOffset = new Vector2(0, 0);
                m_completionMessage.SetActive(true);
            }
        }
    }


    /// <summary>
    /// Game failure message and you're out of here
    /// </summary>
    protected void gameFailure()
    {
        Debug.Log("gameFailure - run out of lives");
        // Disable the moteship gameobject
        //
        m_moteShip.getGameObject().SetActive(false);
        m_levelComplete = true;
        m_completeTime = Time.time;
        m_doEnding = true;

        // Change state to death screen
        //
        m_state = WorldSceneState.DeathScreen;
    }

    /// <summary>
    /// Do any movement for motes
    /// </summary>
    protected void animateMotes()
    {
        foreach (Mote mote in WorldScene.m_moteList)
        {
            mote.doMove();

            // Dim motes as necessary
            //
            if (m_levelComplete)
            {
                Color newColour = mote.getGameObject().guiTexture.color;
                newColour.a = (m_levelComplete ? 0.1f : mote.getOriginalAlpha());
                mote.getGameObject().guiTexture.color = newColour;
            }

            // Special mote movements
            //
            handleSpecialMotes(mote);
        }
    }

    /// <summary>
    /// Special rules for certain motes
    /// </summary>
    /// <param name="mote"></param>
    protected void handleSpecialMotes(Mote mote)
    {
        // Special case for boss motes and glue fluffies
        //
        if (mote.GetType() == typeof(GlueFluffyMote))
        {
            GlueFluffyMote glue = (GlueFluffyMote)mote;

            BossMote bossMote = getBossFromActiveList();
            if (bossMote != null)
            {
                // Update tracking if we've interacted with the boss - this prevents the GlueFluffy from circling the boss
                //
                if (bossMote.intersects(glue.getScreenRect()))
                    glue.setTrackBoss(false);
                else
                    glue.setBossPosition(bossMote.getInsetCentrePosition());
            }
        }
    }

    /// <summary>
    /// Animate transient graphical items and keep them cleared up
    /// </summary>
    protected void animateTransients()
    {
        List<Transient> removeList = new List<Transient>();

        foreach (Transient transient in m_transientList)
        {
            transient.doAnimate();

            if (!transient.isAlive())
                removeList.Add(transient);
        }

        // Tidy up
        //
        foreach (Transient transient in removeList)
        {
            Destroy(transient.getGameObject());
            m_transientList.Remove(transient);
        }
    }

    /// <summary>
    /// Animate the mote ship
    /// </summary>
    protected void animateMoteShip()
    {
        if (m_moteShip == null)
            return;

        // If we're dying then do something funky
        //
        if (m_moteShip.testDying())
        {
            return;
        }

        // Slide time is the time it takes the ship to slide into the screen - which is less
        // than the whole level generation pause
        //
        float slideTime = m_levelGenerationPause - 3.0f;

        // Do ship intro - slide it into position from off screen
        //
        if (Time.time - m_startTime < slideTime)
        {
            Vector2 offScreenPosition = new Vector2(-0.2f, 0.5f);
            Vector2 original = new Vector2(2.5f * m_moteShip.getOriginalPosition().x, m_moteShip.getOriginalPosition().y);

            // For boss levels come in from the left a little way and let the boss come in from the right
            //
            if (m_levelManager.getGameLevel().isBoss())
            {
                original = new Vector2(m_moteShip.getOriginalPosition().x, m_moteShip.getOriginalPosition().y);
            }

            Vector2 step = (original - offScreenPosition) / slideTime;
            Vector2 newPos = Tweening.easeOutCubic(offScreenPosition, step, Time.time - m_startTime, slideTime);
            m_moteShip.getGameObject().transform.position = new Vector3(newPos.x, newPos.y, 0);
        }
        else
        {
            // Do some wobbling behaviour
            //
            m_moteShip.doMove();
        }

        // Check for level completion and dim the moteship if active
        //
        Color newColour = m_moteShip.getGameObject().guiTexture.color;
        newColour.a = (m_levelComplete ? 0.1f : m_moteShip.getOriginalAlpha());
        m_moteShip.getGameObject().guiTexture.color = newColour;
    }


    /// <summary>
    /// Reset the level or start another one/all over again
    /// </summary>
    protected void resetLevel(bool nextLevel)
    {
        //Debug.Log("Resetting level with next level fetch = " + nextLevel.ToString());

        // Move message off screen
        //
        m_completionMessage.SetActive(false);

        // Clear the mote list and reset
        //
        foreach (Mote mote in WorldScene.m_moteList)
            Destroy(mote.getGameObject());

        WorldScene.m_moteList.Clear();

        // Unset this
        //
        m_levelComplete = false;

        // Reset start time
        //
        m_startTime = Time.time;

        // Get the next level
        //
        if (nextLevel)
        {
            if (GoogleAnalytics.instance)
            {
                GoogleAnalytics.instance.LogScreen("Fetching Next Level");
                //Debug.Log("GoogleAnalytics - Fetching Next Level");
            }

            // Get next level and set the highest level number in the player class
            //
            m_levelManager.nextLevel();
            m_player.setHighLevel(m_levelManager.getGameLevelNumber());

            // Get the number of lives at the start of the level
            //
            m_livesAtStart = m_player.getLives();

            if (m_levelManager.getGameLevel() == null)
            {
                Debug.Log("Game completed");
                m_completeTime = Time.time;
                m_levelManager.setGameCompleted(true);
                return;
            }


            // Ok reset the clock
            //
            m_levelManager.getGameLevel().resetClock(m_levelGenerationPause);

            // Level sets are 0 based and so is CurrentWorld
            //
            int levelSet = m_levelManager.getGameLevel().getLevelSet().getLevelSetNumber();


            if (levelSet != CurrentWorld)
            {
                Debug.Log("We're in a new world - do transition to world " + levelSet);
                CurrentWorld = levelSet;
                setBackgroundTexture();

                // Now change state and queue this up
                //
                queueInterstitial();

                // Google analytics
                //
                if (GoogleAnalytics.instance)
                {
                    GoogleAnalytics.instance.LogScreen("Completed World");
                }

                // Save game state before loading another level
                //
                saveGameState();

                //Debug.Log("CURRENT WORLD = " + CurrentWorld);

                // If we're lite we need to skip a few levels when we hit the 4th world
                if (CurrentWorld == 3 && m_isLite)
                {
                    Application.LoadLevel(WorldScene.WorldSceneBase + CurrentWorld + 3);
                }
                else
                {
                    // Load the next level
                    //
                    Application.LoadLevel(WorldScene.WorldSceneBase + CurrentWorld);
                }
            }
        }
        else
        {

            // Advance the start time of the current level
            //
            m_levelManager.getGameLevel().advanceStartTime(m_levelGenerationPause);
        }

        // set the player level score to zero
        //
        m_player.setLevelScore(0);

        // Not medal awarded for a new level yet
        //
        m_medal = LevelScoreIdentifier.None;

        // Clear any star objects
        //
        m_transientManager.clearStars();

        // If this is a boss level then reset the boss generation flag
        //
        m_levelManager.getGameLevel().setBossGenerated(false);
    }

    /// <summary>
    /// Queue up an interstitial
    /// </summary>
    protected void queueInterstitial()
    {
        // Set both of these to initiate interstitial
        //
        m_state = WorldSceneState.Interstitial;
        m_interstitialStart = 0;

        // Turn off these
        //
        m_welcomeMessage.SetActive(false);
        m_hintMessage.SetActive(false);
    }


    /// <summary>
    /// Set the background texture to a passed value or to a background level texture
    /// </summary>
    protected void setBackgroundTexture(Texture texture = null)
    {
        m_backgroundObject.SetActive(true);

        if (texture != null)
        {
            m_backgroundObject.guiTexture.texture = texture;
        }
        else
        {
            // Fetch the texture manager
            //
            TextureManager tm = (TextureManager)m_backgroundObject.GetComponent(typeof(TextureManager));
            if (tm != null)
            {
                // m_backgroundObject textures as follows
                //
                switch (CurrentWorld)
                {
                    case 0:
                        m_backgroundObject.guiTexture.texture = tm.MeadowTexture;
                        break;
                        /*
                    case 1:
                        m_backgroundObject.guiTexture.texture = tm.ForestTexture;
                        break;

                    case 2:
                        m_backgroundObject.guiTexture.texture = tm.CityGateTexture;
                        break;
                    
                case 3:
                    m_backgroundObject.guiTexture.texture = tm.SpaceCityTexture;
                    break;
                        
                case 4:
                    m_backgroundObject.guiTexture.texture = tm.HinterlandTexture;
                    break;

                case 5:
                    m_backgroundObject.guiTexture.texture = tm.SpaceElevatorTexture;
                    break;

                case 6:
                    m_backgroundObject.guiTexture.texture = tm.LEOTexture;
                    break;

                case 7:
                    m_backgroundObject.guiTexture.texture = tm.SaturnTexture;
                    break;

                case 8:
                    m_backgroundObject.guiTexture.texture = tm.WormholeTexture;
                    break;

                case 9:
                    m_backgroundObject.guiTexture.texture = tm.HomeworldTexture;
                    break;
                    */
                    default:
                        break;
                }

                m_backgroundObject.guiTexture.pixelInset = new Rect(0, 0, m_backgroundObject.guiTexture.texture.width, m_backgroundObject.guiTexture.texture.height);
            }
        }
    }

    /// <summary>
    /// Remove a mote from play completely
    /// </summary>
    /// <param name="mote"></param>
    protected void removeMotes(List<Mote> motes)
    {
        foreach (Mote mote in motes)
        {
            Destroy(mote.getGameObject());
            WorldScene.m_moteList.Remove(mote);
        }
    }

    /// <summary>
    /// Do mouse processing
    /// </summary>
    protected void doMouse()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
		
		        // Temporary fix
        if (checkForWholeGameRestart(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
            return;

		
        if (m_levelComplete && Time.time > m_completeTime + m_levelCompletionPause)
        {
            // Check for this if the level is complete and enable twitter buttons etc - this check used to sit
            // outside of the timer window but not sure this is a good idea?
            //
            if (checkForWholeGameRestart(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
                return;

            resetLevel(true);
            return;
        }

        // Handle click
        //
        if (!m_levelComplete)
        {
            handleClick(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }
    }
	
	/// <summary>
	/// The accelerometer.
	/// </summary>
	protected void doAccelerometer()
	{
		// Don't do this if we're starting or finishing level
		//
		if ((Time.time - m_startTime < m_levelGenerationPause) || m_levelComplete)
            return;
			
		Debug.Log("DoAccelerometer");
		
		MoveDirection direction = m_movatron.doSample();
		
		Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 rightPos = new Vector2(3 * Screen.width / 4, Screen.height / 2);
		Vector2 leftPos = new Vector2(Screen.width / 4, Screen.height / 2);
		
		switch(direction)
		{
			case MoveDirection.X:
				launchTransient(leftPos, m_transientManager.m_score100, 1.0f, doScreenScale());
				break;
			
			case MoveDirection.Y:
				launchTransient(middlePos, m_transientManager.m_score250, 1.0f, doScreenScale());
				break;
			
			case MoveDirection.Z:
				launchTransient(rightPos, m_transientManager.m_niceOne, 1.0f, doScreenScale());
				break;
			
		default:
			break;
		
			
		}
		
		if (m_movatron.getShakePercentage() > 0)
		{
			Debug.Log("Got shake percentage = " + m_movatron.getShakePercentage());
			
			if (m_movatron.getShakePercentage() > 99)
			{
				// Launch transient and reset
				//
				launchTransient(rightPos, m_transientManager.m_niceOne, 1.0f, doScreenScale());
				m_movatron.setShakePercentage(0);
			}
		}
		//if (Input.acceleration.
	}
	
	/// <summary>
	/// Launches some stars in random areas within screen quadrants
	/// </summary>
	protected void launchRandomQuadrantStars()
	{		
		int numberOfStars = (4 + (int)(Random.value * 10));
		for(int i = 0; i < numberOfStars; i++)
		{
			// Get a random quadrant
			//
			bool top = (Random.value < 0.5f);
			bool left = (Random.value < 0.5f);
			
			Vector2 position = Vector2.zero;
			
			if (top)
				position.y = 3 * Screen.height / 4;
			else
				position.y = Screen.height / 4;
			
			if (left)
				position.x = Screen.height / 4;
			else
				position.x = 3 * Screen.height / 4;
			
			float multiplierX = (Random.value < 0.5f ? 1: -1);
			float multiplierY = (Random.value < 0.5f ? 1: -1);
			
			position.x += multiplierX * (Screen.width / 6) * Random.value;
			position.y += multiplierY *  (Screen.height / 6) * Random.value;
			
			launchTransient(position, m_transientManager.m_goldStar, 1.5f, doScreenScale() / 2.0f, 0.1f);
		}
	}
	
	/// <summary>
	/// Dos the accelerometer2.
	/// </summary>
	protected void doAccelerometer2()
	{
		// Don't do this if we're starting or finishing level
		//
		if ((Time.time - m_startTime < m_levelGenerationPause) || m_levelComplete)
            return;
				
		if (m_movatron.doSample2() && Time.time > m_lastShakeTransient + m_shakeTransientDuration)
		{
			Vector2 topPos = new Vector2(Screen.width / 2 - m_transientManager.m_keepShaking.width / 2,
				Screen.height / 2 - m_transientManager.m_keepShaking.height / 2);
		
			launchTransient(topPos, m_transientManager.m_keepShaking, 2.0f, doScreenScale() * 2.5f);
			
			launchRandomQuadrantStars();
			

            // Increment time spent shaking only if we're definitely within a shaking session
            //
            if (Time.time - m_lastShakeTransient < 2 * m_shakeTransientDuration)
            {
                m_player.addSecondsShaking(Time.time - m_lastShakeTransient);
            }

			m_lastShakeTransient = Time.time;
		}
		
		// Award the prize when the percentage gets above a certain threshold
		//
		if (m_movatron.getShakePercentage() > 8)
		{
            doShakePrize();
		}
	}
	
    /// <summary>
    /// Loop around shaking prizes
    /// </summary>
    protected void doShakePrize()
    {
        Vector2 middlePos = new Vector2(Screen.width / 2 - m_transientManager.m_lifeAwarded.width / 2, 
            Screen.height / 2 - m_transientManager.m_lifeAwarded.height / 2);

        switch(m_shakingStatus)
        {
            case ShakingStatus.Bonus1000:
                launchTransient(middlePos, m_transientManager.m_shakeBonus1000 , 2.5f, doScreenScale() * 5.0f);
                playSound(m_transientManager.m_alarmAward);
                m_player.addScore(m_levelManager.getGameLevelNumber(), 1000);
                m_shakingStatus = ShakingStatus.Bonus2000;
                break;

            case ShakingStatus.Bonus2000:
                launchTransient(middlePos, m_transientManager.m_shakeBonus2000, 2.5f, doScreenScale() * 5.0f);
                playSound(m_transientManager.m_alarmAward);
                m_player.addScore(m_levelManager.getGameLevelNumber(), 2000);
                m_shakingStatus = ShakingStatus.Bonus5000;
                break;

            case ShakingStatus.Bonus5000:
                launchTransient(middlePos, m_transientManager.m_shakeBonus5000 , 2.5f, doScreenScale() * 5.0f);
                playSound(m_transientManager.m_alarmAward);
                m_player.addScore(m_levelManager.getGameLevelNumber(), 5000);
                m_shakingStatus = ShakingStatus.BonusLife;
                break;

            case ShakingStatus.BonusLife:
			    launchTransient(middlePos, m_transientManager.m_lifeAwarded, 2.5f, doScreenScale() * 5.0f);
                playSound(m_transientManager.m_alarmAward);
                m_player.addLives(1);
                m_shakingStatus = ShakingStatus.Bonus1000;
                break;


            default:
                break;
        }

		// And ignore shaking for a bit
		//
		m_movatron.setShakeIgnore();
    }

    /// <summary>
    /// Do touch processing
    /// </summary>
    protected void doTouch()
    {
		// Prevent bounces
		//
		if (Time.time < m_bounceProtect)
			return;
		
        // If no input touches or the completion time is within half a second then ignore
        if (Input.touches.Length == 0) // || Time.time < m_completeTime + 0.5f)
            return;

        // Temporary fix
        if (checkForWholeGameRestart(Input.touches[0].position))
            return;

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Do level completion
                    //
                    if (m_levelComplete && Time.time > m_completeTime + m_levelCompletionPause)
                    {
                        resetLevel(true);
                        return;
                    }

                    if (!m_levelComplete)
                    {
                        m_touchCount++;

                        if (handleClick(touch.position))
                            continue;
                    }
                    break;

                case TouchPhase.Canceled:
                    if (m_touchCount > 0)
                        m_touchCount--;
                    break;

                case TouchPhase.Ended:
                    if (m_touchCount > 0)
                        m_touchCount--;
                    Debug.Log("Got ended");
                    break;

                case TouchPhase.Moved:
                    if (!m_levelComplete)
                    {
                        handleMove(touch.position);
                    }
                    //Debug.Log("Got moved");
                    break;

                case TouchPhase.Stationary:
                    /*
                    foreach (TouchTarget target in m_touchTargets)
                    {
                        if (target.contains(touch.position))
                        {
                            // Send the event
                            //
                            NGUIDebug.Log("SEND PAD PRESS");

                            //
                            if (m_lastTarget == target)
                            {
                                m_touchValue = Mathf.Min(m_touchValue + 0.1f, 1.0f);
                            }
                            else
                            {
                                m_touchValue = 0.5f;
                            }
                            OnPadPress(target.getFunction(), m_touchValue);
                            m_lastTarget = target;
                            //handleTouch(target.getFunction());
                        }
                    }*/
                    Debug.Log("Got stationary");
                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Handle clicks in HUD
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    protected bool doHudHandleClick(Vector2 position)
    {
        // Check for click on HUD element - if the HUD is active then deal with clicks on it - if it's
        // inactive see if we clicked on the bottom left corner to activate it
        //
        if (m_moteHud.getHudControlActive())
        {
            if (!m_hudControl.guiTexture.GetScreenRect().Contains(position))
            {
                Debug.Log("Clicked outside HUD - deactivating");
                m_moteHud.setHudControlActive(false);

                // Google analytics
                //
                if (GoogleAnalytics.instance)
                    GoogleAnalytics.instance.LogScreen("Unpausing Game");

                return true;
            }

            // Handle click inside control
            //
			//float rescale = m_moteHud.doRescale();
		
#if MH_DEBUG
			Debug.Log ("RIGHTMOST = " + (Screen.width / 2 /* + m_hudControl.guiTexture.GetScreenRect().x */ + m_hudControl.guiTexture.pixelInset.xMax));
			Debug.Log("TOPMOST = " + (Screen.height / 2 /* + m_hudControl.guiTexture.GetScreenRect().y */ + m_hudControl.guiTexture.pixelInset.yMax));
			Debug.Log("POSITION x = " + position.x + ", y = " + position.y);
			Debug.Log("RESCALE = " + rescale);
#endif
			
			float rightMost = Screen.width / 2 + m_hudControl.guiTexture.pixelInset.xMax;
			//float topMost = Screen.height / 2 + m_hudControl.guiTexture.pixelInset.yMax;
			
			float width = m_hudControl.guiTexture.pixelInset.width;
			float height = m_hudControl.guiTexture.pixelInset.height;
			
#if MH_DEBUG
			Debug.Log("WIDTH = " + width);
			Debug.Log("HEIGHT = " + height);
#endif
            Rect exitButton = new Rect(rightMost - 0.7f * width, 0.43f * height, width / 3, height / 7);
            Rect musicButton = new Rect(rightMost -  0.53f * width, 0.28f * height, width / 3, height / 6);
            Rect fxButton = new Rect(rightMost - 0.33f * width, 0.11f * height, width / 4, height / 6);
      
#if MH_DEBUG			
			Debug.Log ("EXIT BUTTON = " + exitButton);
#endif
			if (exitButton.Contains(position))
            {
                // Clear down a load of stuff
                //
                resetLevel(false);

                // Clear
                //
                //Debug.Log("Setting highscore = " + m_player.m_totalScore);
                //Debug.Log("Levels unlocked = " + m_player.m_worldsUnlocked);

                // Save game state
                //
                saveGameState();

                // Got to the menu
                //
                Application.LoadLevel(1);
            }
            else if (musicButton.Contains(position)) // toggle music
            {
                m_player.setMusic(!m_player.wantsMusic());
            }
            else if (fxButton.Contains(position)) // toggle sound effects
            {
                m_player.setSoundEffects(!m_player.wantsSoundEffects());
            }
			else
			{
				Debug.Log("Click point x = " + position.x + ", y = " + position.y);
			}
			
            return true;
        }
        else
        {
            if (m_hudControl.guiTexture.GetScreenRect().Contains(position))
            {
                Debug.Log("Clicked on HUD control - activating");
                m_moteHud.setHudControlActive(true);

                // Google analytics
                //
                if (GoogleAnalytics.instance)
                    GoogleAnalytics.instance.LogScreen("Paused in Game");

                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Handle a click at a position
    /// </summary>
    /// <param name="position"></param>
    protected bool handleClick(Vector2 position)
    {
        // Do the HD click handler firstly
        //
        if (doHudHandleClick(position))
            return true;

        // Ok we're not on the HUD - handle game clicks by testing all motes
        //

        // Store clicks
        //
		if (m_levelManager.getGameLevel() != null)
		{
        	m_levelManager.getGameLevel().incrementClicks();
		}
		
		bool ignoreScore = false;

        //Debug.Log("Got touch");
        foreach (Mote mote in WorldScene.m_moteList)
        {
            // Used for ignoring scoring for removal of some objects
            //
            ignoreScore = false;

            //if (mote.isGoodMote())
            //{
                // do something with good mote maybe or ignore?
                //continue;
            //}

            // Ignore taps on the boss
            //
            if (mote is BossMote)
            {
                continue;  
            }


            if (mote.contains(position))
            {
                // Send the event to add score with multiplier
                //
                WorldScene.m_player.addScore(m_levelManager.getGameLevelNumber(), mote.getWorth() * m_player.getDifficultyLevel());

                // Check type of mote
                //
                if (mote.GetType() == typeof(SporeMote))
                {
                    // We're going to destroy the original but spawn a random number more at the same point
                    //
                    spawnSporeMites((SporeMote)mote);
                }
                else if (mote.GetType() == typeof(EggMote))
                {
                    breakEggMote((EggMote)mote);
                }
                else if (mote.GetType() == typeof(SquishMote))
                {
                    SquishMote sM = (SquishMote)mote;

                    // If we can squish this mote than squish it - otherwise
                    // we can fall through and destroy it.
                    if (sM.canSquish())
                    {
                        sM.doSquish();
                        return true;
                    }

                }
                else if (mote.GetType() == typeof(SnakeMote))
                {
                    SnakeMote sM = (SnakeMote)mote;

                    // If we can squish this mote than squish it - otherwise
                    // we can fall through and destroy it.
                    if (sM.canSquish())
                    {
                        sM.doSquish();
                        return true;
                    }

                }
                else if (mote.GetType() == typeof(CellMote))
                {
					
                    CellMote cM = (CellMote)mote;

                    if (cM.getCellState() == CellMoteState.Question)
                    {
                        cM.setCellState(CellMoteState.Opened);
                        
                        // Set the position so that when we set the texture again we pick up the
                        // correct InsetPosition.  Reverse Y.
                        //
                        Vector2 inPos = mote.getInsetCentrePosition();
                        //inPos.y = Screen.height - inPos.y;
                        mote.setPosition(inPos);

 						if (cM.getPrize() == CellPrize.Shield)
						{
                            mote.setTexture(ShieldTexture, true);
							Debug.Log("Converting from Question to Shield");
						}
						else if (cM.getPrize() == CellPrize.Grenade)
						{
							mote.setTexture(GrenadeTexture, true);
							Debug.Log("Converting from Question to Grenade");
						}
					
                        return true;
                    }
                    else // we're open
                    {
                        // If the type is a Grenade then blow everything up
                        //
                        if (cM.getPrize() == CellPrize.Grenade)
                        {
                            explodeGrenade(cM.getInsetCentrePosition());
                            return true;
                        }

                        // else we destroy a single mote by falling through
                        ignoreScore = true;
                    }
                }
                else if (mote.GetType() == typeof(GlueFluffyMote))
                {
                    // GlueFluffyMote needs to be armed before it will explode
                    //
                    if (!mote.isArmed())
                    {
                        mote.setPosition(mote.getInsetPosition());
                        mote.setArmed(true);
                        mote.setTexture(StaticMoteTexture, false, 48);
                        // also update the texture
                    }

                    return true;
                }

                // Pop the mote, handle scoring and transients and return
                //
                popMote(mote, ignoreScore);

                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Handle the move/drag action - we keep clicked on a mote
    /// </summary>
    protected void handleMove(Vector2 position)
    {
        bool ignoreScore = false;

        foreach (Mote mote in WorldScene.m_moteList)
        {
            if (mote is BossMote)
                continue;


            if (mote.contains(position) && mote.getDestructionTime() > 0)
            {
                popMote(mote, ignoreScore);
            }
        }
    }

    /// <summary>
    /// Pop a mote and sort out the scoring and transients as necessary
    /// </summary>
    /// <param name="mote"></param>
    /// <param name="ignoreScore"></param>
    protected void popMote(Mote mote, bool ignoreScore, bool alwaysKill = false)
    {
        // Always do this - pass the alwaysKill flag if we want to ensure that this mote dies
        //
        mote.setDead(alwaysKill);

        // Now test for actual death - it might be a hold mote
        //
        if (mote.isDead())
        {
            // Keep track of motes hit
            // 
            if (!ignoreScore)
                m_levelManager.getGameLevel().incrementMotesDestroyed();

            // Hide it and mark for destruction
            //
            mote.hideMote();

            if (!ignoreScore)
            {
                launchScoreTransient(mote.getInsetPosition(), mote.getWorth() * m_player.getDifficultyLevel(), 0.5f);

                if (mote.getPopTextures().Count == 0)
                {
                    launchTransient(mote.getInsetPosition(), DefaultPopTexture, 0.05f, doScreenScale());
                }
                else if (mote.getPopTextures().Count == 1)
                {
                    launchTransient(mote.getInsetPosition(), mote.getPopTextures()[0], 0.05f, doScreenScale());
                }
                else
                {
                    launchTransient(mote.getInsetPosition(), mote.getPopTextures()[0], mote.getPopTextures()[1], 0.05f, doScreenScale());
                    //launchTransient(mote.getInsetPosition(), FluffyMoteTexture_InitialPop, FluffyMoteTexture_FinalPop, 0.05f);
                }

                // Register the hit with the achievement centre and store the last click position for
                // the launching of any transients.
                //
                m_achievementCentre.registerHit(mote, Time.time, m_levelManager.getGameLevelNumber());
            }

            // Last pop position required for launching any achievements
            //
            m_lastPopPosition = mote.getInsetPosition();

            // Play the sounds
            //
            playSound(MotePop);

            // Set last pop time
            //
            m_lastPopTimeNotify = Time.time + 8.0f;
        }
        else
        {
            // Remind user to hold on this mote
            //
            if (Time.time > m_lastHoldTransientTime)
            {
                launchTransient(mote.getInsetPosition(), m_transientManager.m_holdToDestroy, 1.0f, doScreenScale());
                m_lastHoldTransientTime = Time.time + 0.5f;
            }
        }
    }

    /// <summary>
    /// Last hold transient time
    /// </summary>
    protected float m_lastHoldTransientTime = 0.0f;

    /// <summary>
    /// Spawn more motes at the SporeMote spawn point.  A random amount of new spores.
    /// </summary>
    /// <param name="mote"></param>
    protected void spawnSporeMites(SporeMote mote)
    {
        // If the mote is last generation then don't spawn any new ones
        if (mote.isLastGeneration())
            return;

        // Motes to generate shoud always be at least one
        //
        int motesToGenerate = Mathf.CeilToInt(mote.getMaxGenerate() * Random.value);
        //Debug.Log("Spawning " + motesToGenerate + " motes");

        for (int i = 0; i < motesToGenerate; i++)
        {
            // Creating mote from the passed in mote will regenerate at the same position
            //
            createMote(mote, true);
        }
    }

    /// <summary>
    /// Break an Egg Mote and create a Chick Mote
    /// </summary>
    /// <param name="mote"></param>
    protected void breakEggMote(EggMote mote)
    {
        Debug.Log("New Chick Mote at x = " + mote.getPosition().x + ", y = " + mote.getPosition().y);
        createMote(new ChickMote(mote.getInsetPosition()), false);
    }


    /// <summary>
    /// Play a sound if we're supposed to
    /// </summary>
    /// <param name="clip"></param>
    protected void playSound(AudioClip clip)
    {
        if (WorldScene.m_player.wantsSoundEffects())
        {
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
        }
    }

    protected Vector2 getMinimumPosition(Vector2 position)
    {
        // Minimum distances from the edge
        //
        if (position.x < 50)
            position.x = 50;
        if (position.x > Screen.width - 200)
            position.x = Screen.width - 200;

        if (position.y < 50)
            position.y = 50;
        if (position.y > Screen.height - 100)
            position.y = Screen.height - 100;

        return position;
    }

    /// <summary>
    /// Launch a transient
    /// </summary>
    /// <param name="position"></param>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    /// <param name="dropDead"></param>
    protected void launchTransient(Vector2 position, Texture texture1, Texture texture2, float dropDead, float size)
    {
        position = getMinimumPosition(position);

        GameObject g = new GameObject("MotePop");
        g.AddComponent("GUITexture");

        // Set these to zero and leve the transient to sort out the pixelinset (position) and
        // the texture that is being shown.
        //
        g.transform.position = new Vector3(0, 0, 100); // give z a high value so it comes to the front
        g.transform.localScale = Vector3.zero;
        g.guiTexture.texture = texture1;
        //float width = texture1.width * scale / 2;
        //float height = texture1.height * scale / 2;
        g.guiTexture.pixelInset = new Rect(position.x, position.y, size, size);

        m_transientList.Add(new PoppedMote(g, position, texture1, texture2, dropDead));
    }

    /// <summary>
    /// Launch a transient message
    /// </summary>
    /// <param name="position"></param>
    /// <param name="texture"></param>
    /// <param name="dropDead"></param>
    protected void launchTransient(Vector2 position, Texture texture, float dropDead, float size)
    {
        position = getMinimumPosition(position);

        GameObject g = new GameObject("Transient");
        g.AddComponent("GUITexture");

        // Set these to zero and leve the transient to sort out the pixelinset (position) and
        // the texture that is being shown.
        //
        g.transform.position = new Vector3(0, 0, 100); // give z a high value so it comes to the front
        g.transform.localScale = Vector3.zero;
        g.guiTexture.texture = texture;
        //float width = (texture.width * scale / 2;
        //float height = texture.height * scale / 2;
        g.guiTexture.pixelInset = new Rect(position.x - size / 2, position.y - size / 2, size, size);

        m_transientList.Add(new BannerTransient(g, position, texture, dropDead));
    }
	
	
	/// <summary>
	/// Launchs the transient.
	/// </summary>
	/// <param name='position'>
	/// Position.
	/// </param>
	/// <param name='texture'>
	/// Texture.
	/// </param>
	/// <param name='dropDead'>
	/// Drop dead.
	/// </param>
	/// <param name='size'>
	/// Size.
	/// </param>
	/// <param name='alpha'>
	/// Alpha.
	/// </param>
	protected void launchTransient(Vector2 position, Texture texture, float dropDead, float size, float alpha)
    {
        position = getMinimumPosition(position);

        GameObject g = new GameObject("Transient");
        g.AddComponent("GUITexture");

        // Set these to zero and leve the transient to sort out the pixelinset (position) and
        // the texture that is being shown.
        //
        g.transform.position = new Vector3(0, 0, 100); // give z a high value so it comes to the front
        g.transform.localScale = Vector3.zero;
        g.guiTexture.texture = texture;
        g.guiTexture.pixelInset = new Rect(position.x - size / 2, position.y - size / 2, size, size);
		
		// Pass the alpha change
		//
        m_transientList.Add(new BannerTransient(g, position, texture, dropDead, alpha));
    }
	
    /// <summary>
    /// Launch a score transient
    /// </summary>
    /// <param name="position"></param>
    /// <param name="score"></param>
    /// <param name="dropDead"></param>
    protected void launchScoreTransient(Vector2 position, int score, float dropDead)
    {
        //Debug.Log("Launching Score Transient for score " + score);

        // Get texture based on score from the TransientManager object
        //
        //TransientManager tm = (TransientManager)this.gameObject.GetComponent(typeof(TransientManager));
        Texture texture = m_transientManager.getTextureForScore(score);

        // Check to see if we found one
        //
        if (texture == null)
        {
            Debug.Log("There's no transient for score " + score);
            return;
        }

        GameObject g = new GameObject("ScoreTransient");
        g.AddComponent("GUITexture");

        // Set these to zero and leve the transient to sort out the pixelinset (position) and
        // the texture that is being shown.
        //
        g.transform.position = new Vector3(0, 0, 1); // give z a high value so it comes to the front
        g.transform.localScale = Vector3.zero;
        g.guiTexture.texture = texture;
        g.guiTexture.pixelInset = new Rect(position.x, position.y, texture.width / 2, texture.height / 2);
        //Debug.Log("SCORE TRANSIENT SIZE RECT = " + g.guiTexture.pixelInset);
        m_transientList.Add(new ScoreTransient(g, position, m_transientManager.getTransientMovementVector(position), texture, dropDead));
    }
}
//}