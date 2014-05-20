using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
using Xyglo.Unity;

/// <summary>
/// WorldScene is an attempt at a generic version of the WorldScene - we have one WorldScene per level
/// </summary>
[System.Serializable]
public class CompletionScene : MonoBehaviour
{
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
	/// The cup texture.
	/// </summary>
	public Texture CupTexture;
	
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
    /// If you tap on it it steals your remaining taps
    /// </summary>
    public Texture AttackMoteTexture;

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
    static protected Player m_player;

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
    /// Link to full version GameObject
    /// </summary>
    protected GameObject m_fullVersionLink;

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
    protected Vector2 m_cheatEndPoint;

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

        //if (m_player != null)
            //return;

        // Fetch some stored values
        //
        m_player.setHighScore(XygloPlayerPrefs.GetInt("MWsst", 0));

        // Always at least one world unlocked
        //
        m_player.m_worldsUnlocked = Mathf.Max(XygloPlayerPrefs.GetInt("MWhl", 1), 1); 
        //m_player.m_worldsUnlocked = 10; // Mathf.Max(XygloPlayerPrefs.GetInt("MWhl", 1), 1); 

#if LOAD_DEBUG
        Debug.Log("Got highscore = " + m_player.m_highScore);
        Debug.Log("Got levels unlocked = " + m_player.m_worldsUnlocked);
        Debug.Log("Current World = " + CurrentWorld);
#endif

        m_player.setSoundEffects(XygloPlayerPrefs.GetBool("MWMMMUT", true));
        m_player.setMusic(XygloPlayerPrefs.GetBool("MWMMMUS", true));
        Debug.Log("MUSIC IS " + m_player.wantsMusic());

        // High level
        //
        m_player.setHighLevel(XygloPlayerPrefs.GetInt("HighLevel", 0));
        //m_player.setHighLevel(99);

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

    }

    /// <summary>
    /// Save game state
    /// </summary>
    protected void saveGameState()
    {
        Debug.Log("Completion - saveGameState() - storing");

#if UNITY_WINRT
        // Do something else
#else
        // Top slide save
        //
        int highScore = (int)Mathf.Max(m_player.getTotalScore(), m_player.getHighScore());
        XygloPlayerPrefs.SetInt("MWsst", highScore);

        // Highlevel could be m_worldsUnlocked (last value) or perhaps we've unlocked a new
        // one in CurrentWorld.
        //
        //Debug.Log("saveGameState - CurrentWorld = " + CurrentWorld);

        // Fix this to only save three world total
        //
        int worldSet = Mathf.Min(Mathf.Max(m_player.m_worldsUnlocked, CurrentWorld + 1), 3);
        Debug.Log("WORLD SET = " + worldSet);
        XygloPlayerPrefs.SetInt("MWhl", worldSet);

        //XygloPlayerPrefs.SetInt("LatestPlayedLevel", m_levelManager.getGameLevelNumber());

        // Reset high level to 0
        //
        XygloPlayerPrefs.SetInt("HighLevel", 0);

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

        // Save everything
        //
        XygloPlayerPrefs.Save();
#endif
    }


    /// <summary>
    /// Reset some important statics so that we get a clean restart
    /// </summary>
    protected void resetStatics()
    {
        // m_player stays as it is
        //WorldScene.m_moteHud = null;
        //WorldScene.m_moteList = null;
        //WorldScene.m_transientList = null;

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
        //if (m_player != null)
        //{
            //m_player.setLives(3);
        //}
    }

    protected void setupObjects()
    {

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
                float factor = Screen.width / 1024;
                Rect initial = m_moteShip.getGameObject().guiTexture.pixelInset;
                Rect newSize = new Rect(initial.x * factor, initial.y * factor, initial.width * factor, initial.height * factor);
                m_moteShip.getGameObject().guiTexture.pixelInset = newSize;
            }
        }

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
    }

    /// <summary>
    /// Completion Message
    /// </summary>
    protected void completionMessage()
    {
        m_completionMessage.guiText.anchor = TextAnchor.MiddleCenter;

        if (WorldScene.m_isLite)
        {
            m_completionMessage.guiText.text = "Well done - you've completed Mote Wars Lite.\nClick below to get full version and enjoy\nloads more levels and new Motes!";
            m_fullVersionLink.SetActive(true);
        }
        else
        {
            // Turn off the full version message
            //
            m_fullVersionLink.SetActive(false);

            if (m_player.getDifficultyLevel() < 2)
            {
                m_completionMessage.guiText.text = "You've completed MoteWars. Well done!\nWanna try again? It's going to be harder this time!";
            }
            else
            {
                m_completionMessage.guiText.text = "You've completed MoteWars at difficulty " + m_player.getDifficultyLevel() + ".\nCool!  Wanna try again?";
            }
        }
        m_completionMessage.guiText.fontSize = (int)Screen.width / 40;
        m_completionMessage.guiText.pixelOffset = new Vector2(0, 80);
        m_completionMessage.SetActive(true);
    }

    /// <summary>
    /// When Unity initalises this class
    /// </summary>
    void Start()
    {
        // Init by setting the secret key, never ever change this line in your app !
        //
#if UNITY_WINRT
        // Do something else
#else
        XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);
#endif
        if (m_player == null)
        {
            m_player = new Player("Me", 3);
        }

        // Load the game state
        //
        loadGameState();

        // Set up some objects
        //
        setupObjects();

        // Ensure any statics are reset when we reload the game scene
        //
        resetStatics();

        // Enable audio as necessary
        //
        checkAudio();

        // Set initial background texture and color perhaps?
        //
        setBackgroundTexture();

        // Google analytics
        //
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("Completion screen");
            //Debug.Log("GoogleAnalytics - Starting Game First Time");
        }

        // Set start time
        //
        m_startTime = Time.time;

        // Activate moteship
        //
        m_moteShip.getGameObject().SetActive(true);

        // Set completion
        completionMessage();

        // Increment difficulty level in the WorldScene (only for non-lite) - this will get saved out when we quit this level
        //
        if (!WorldScene.m_isLite)
        {
            m_player.setDifficultyLevel(m_player.getDifficultyLevel() + 1);
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
            ///createMote(m_levelManager.getGameLevel().getBoss(), false);
            m_levelManager.getGameLevel().setBossGenerated(true);
        }
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
    protected void checkForWholeGameRestart(Vector2 hitPosition)
    {
        /*
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
        */


        m_completionMessage.SetActive(false);
        //m_levelManager.setLevel(0);
        Application.LoadLevel(1);

        // Save prefs
        //
        saveGameState();

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
    /// Heartbeat called by unity
    /// </summary>
    void Update()
    {
        animateMoteShip();

        // After X seconds then handle touches
        //
        if (Time.time > m_startTime + 1)
        {
            // Handle any touches
            //
            if (WorldScene.isMobilePlatform())
            {
                doTouch();
            }
            else
            {
                doMouse();
            }
        }
		
		animateCups();
    }
	
	/// <summary>
	/// Animates the cups.
	/// </summary>
	protected void animateCups()
	{
		
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
                    launchTransient(m_lastPopPosition, m_transientManager.m_niceOne, 1.0f);
                else
                    launchTransient(m_lastPopPosition, m_transientManager.m_killStreak, 1.0f);

                // Add the score
                m_player.addScore(m_levelManager.getGameLevelNumber(), ach.getBonus());
            }
        }
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

        // Fetch this object as well
        //
        if (m_fullVersionLink == null)
        {
            m_fullVersionLink = GameObject.FindGameObjectWithTag("HudControl");
        }

        // Check and set music playing status
        //
        if (m_player.wantsMusic())
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

        //m_levelDetails.audio.Play();
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
            gameObject.guiText.fontSize = (int)Screen.width / 40;
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
    /// Display a level finished hud and return true if the level is now completed and we don't
    /// want further updates
    /// </summary>
    /// <returns></returns>
    protected bool levelFinishedHud()
    {

        if (m_levelComplete && m_player.getLives() == 0)
        {
            slideMessage(m_completionMessage, m_completeTime, "Your mission is over...", new Vector2(-500, 70), new Vector2(0, 70), new Vector2(500, 70), 1.0f, 10.0f, 0.0f);

            // Start twitter string
            //
            m_twitterString = "I'm+playing+Mote+Wars+and+just+got+a+";

            string missonStats = "You scored " + m_player.getTotalScore();

            if (m_player.getTotalScore() > m_player.getHighScore())
            {
                missonStats += " which is a NEW RECORD!";
                m_twitterString += "new+record+score+of" + m_player.getTotalScore();
            }
            else
            {
                m_twitterString += "+score+of+" + m_player.getTotalScore();
            }

            // Complete twitter string
            //
            m_twitterString += ".+Can+you+beat+that?";
            slideMessage(m_welcomeMessage, m_completeTime, missonStats, new Vector2(500, -70), new Vector2(0, -70), new Vector2(-500, -70), 1.0f, 10.0f, 0.0f);

#if FACEBOOK_ENABLED
                m_facebookButton.SetActive(true);
#endif
            
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

            // Level number and name
            //
            m_welcomeMessage.guiText.text = (m_levelManager.getGameLevel().getLevelNumber() + 1) + ". " + m_levelManager.getGameLevel().getName();
            m_welcomeMessage.guiText.fontSize = (int)Screen.width / 40;
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
            m_hintMessage.guiText.fontSize = (int)Screen.width / 40;
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

        // Do some kind of countdown clock
        //
    }

    /// <summary>
    /// Animate the hud control
    /// </summary>
    protected void hudControlAnimation()
    {
        // Animate the hud control if we need to - check activity state and positions
        //
        if (!m_moteHud.isHudControlAnimating())
            return;

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
        }
        else if (!m_moteHud.getHudControlActive() && Time.time < m_hudStartTime + hudDuration)
        {
            // If the control is inactive and the position is not inactive then carry on animating as well
            //
            Vector2 result = Tweening.easeInCubic(m_cheatEndPoint, -m_moteHud.getHudMoveVector(hudDuration), Time.time - m_hudStartTime, hudDuration);
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

            //m_moteHud.setHudControlPositionActive(m_hudControl.guiTexture.pixelInset);
            //Debug.Log("RESET HUD START TIME");
            //Debug.Log("FINISH POINT = x " + m_hudControl.guiTexture.pixelInset.x + ", y = " + m_hudControl.guiTexture.pixelInset.y);
            //Debug.Log("RESTART POINT x = " + m_moteHud.getHudControlPositionActive().x + ", y = " + m_moteHud.getHudControlPositionActive().y);
            //Debug.Log("FINAL POINT x = " + m_moteHud.getHudControlPositionInactive().x + ", y = " + m_moteHud.getHudControlPositionInactive().y);

            m_cheatEndPoint = new Vector2(m_hudControl.guiTexture.pixelInset.x, m_hudControl.guiTexture.pixelInset.y);
        }
    }

    /// <summary>
    /// Fire off a bounce menu
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="timeToSettle"></param>
    /// <param name="bounce"></param>
    protected void fireBounceBanner(string title, string body, Vector2 startPosition, Vector2 endPosition, float timeToSettle, bool bounce)
    {
        //GameObject m = GameObject.FindWithTag("SlideMenu");

        //m.MenuTitle = title;
        //m.MenuBody = body;
        //m.StartPosition = startPosition;
        //m.FinalPosition = endPosition;

        //m_slideBanner.playMenu(title, body, Time.time);
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
    /*
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
            //doDeath();

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
                    //Debug.Log("FOUND BOSS MOTE in ALIVE LIST");
                    bossAlive = true;
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
                m_completionMessage.guiText.fontSize = (int)Screen.width / 40;
                m_completionMessage.guiText.pixelOffset = new Vector2(0, 0);
                m_completionMessage.SetActive(true);
            }
        }
    }
    */

    /// <summary>
    /// Game failure message and you're out of here
    /// </summary>
    protected void gameFailure()
    {
        Debug.Log("gameFailure");
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
        foreach (Mote mote in m_moteList)
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

    protected void handleSpecialMotes(Mote mote)
    {
        // Special case for boss motes and glue fluffies
        //
        if (mote.GetType() == typeof(GlueFluffyMote))
        {
            GlueFluffyMote glue = (GlueFluffyMote)mote;
            if (getBossFromActiveList() != null)
                glue.setBossPosition(getBossFromActiveList().getInsetCentrePosition());
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

        //Debug.Log("Animating Moteship");
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
            //if (m_levelManager.getGameLevel().isBoss())
            //{
                //original = new Vector2(m_moteShip.getOriginalPosition().x, m_moteShip.getOriginalPosition().y);
            //}

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
    /// Do mouse processing
    /// </summary>
    protected void doMouse()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        Vector2 clickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (WorldScene.m_isLite && m_fullVersionLink.guiTexture.GetScreenRect().Contains(clickPoint))
        {
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				Application.OpenURL("https://itunes.apple.com/app/mote-wars/id766649977");
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				Application.OpenURL("https://play.google.com/store/apps/details?id=com.xyglo.motewars");
			}
			else // todo for WP8 and Windows Store
			{
            	Application.OpenURL("http://www.xyglo.com/mote-wars/");
			}
        }
        else
        {
            checkForWholeGameRestart(clickPoint);
        }
    }

    /// <summary>
    /// Do touch processing
    /// </summary>
    protected void doTouch()
    {
        // If no input touches or the completion time is within half a second then ignore
        if (Input.touches.Length == 0) // || Time.time < m_completeTime + 0.5f)
            return;

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Go back to start screen
                    //
                    if (WorldScene.m_isLite && m_fullVersionLink.guiTexture.GetScreenRect().Contains(touch.position))
                    {
                        Application.OpenURL("http://www.xyglo.com/mote-wars/");
                    }
                    else
                    {
                        checkForWholeGameRestart(touch.position);
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
                    Debug.Log("Got moved");
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
                    break;

                default:
                    break;
            }
        }
    }
    
    /// <summary>
    /// Handle a click at a position
    /// </summary>
    /// <param name="position"></param>
    protected bool handleClick(Vector2 position)
    {
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
        foreach (Mote mote in m_moteList)
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
                // Send the event
                //
                //m_player.addScore(m_levelManager.getGameLevelNumber(), mote.getWorth());

                // Check type of mote
                //
                if (mote.GetType() == typeof(SporeMote))
                {
                    // We're going to destroy the original but spawn a random number more at the same point
                    //
                    //spawnSporeMites((SporeMote)mote);
                }
                else if (mote.GetType() == typeof(EggMote))
                {
                    //breakEggMote((EggMote)mote);
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
                        // correct InsetPosition.
                        //
                        mote.setPosition(mote.getInsetPosition());

 						if (cM.getPrize() == CellPrize.Shield)
						{
	                        mote.setTexture(ShieldTexture);
							Debug.Log("Converting from Question to Shield");
						}
						else if (cM.getPrize() == CellPrize.Grenade)
						{
							mote.setTexture(GrenadeTexture);
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
                            //explodeGrenade(cM.getPosition());
                            return true;
                        }

                        // else we destroy a single mote by falling through
                        ignoreScore = true;
                    }
                }

                // Pop the mote, handle scoring and transients and return
                //
				Debug.Log("Pop mote");
                popMote(mote, ignoreScore);

                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Pop a mote and sort out the scoring and transients as necessary
    /// </summary>
    /// <param name="mote"></param>
    /// <param name="ignoreScore"></param>
    protected void popMote(Mote mote, bool ignoreScore)
    {
		Debug.Log("Popmote for " + mote.GetType());
		
        // Keep track of motes hit
        if (!ignoreScore)
            m_levelManager.getGameLevel().incrementMotesDestroyed();

        // Always do these
        mote.hideMote();
        mote.setDead(true);

        if (!ignoreScore)
        {
            launchScoreTransient(mote.getInsetPosition(), mote.getWorth(), 0.5f);
            launchTransient(mote.getInsetPosition(), FluffyMoteTexture_InitialPop, FluffyMoteTexture_FinalPop, 0.05f);

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
    }


    /// <summary>
    /// Break an Egg Mote and create a Chick Mote
    /// </summary>
    /// <param name="mote"></param>
    protected void breakEggMote(EggMote mote)
    {
        Debug.Log("New Chick Mote at x = " + mote.getPosition().x + ", y = " + mote.getPosition().y);
        //createMote(new ChickMote(mote.getInsetPosition()), false);
    }


    /// <summary>
    /// Play a sound if we're supposed to
    /// </summary>
    /// <param name="clip"></param>
    protected void playSound(AudioClip clip)
    {
//        if (m_player.wantsSoundEffects())
  //      {
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
    //    }
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
    protected void launchTransient(Vector2 position, Texture texture1, Texture texture2, float dropDead)
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
        g.guiTexture.pixelInset = new Rect(position.x, position.y, texture1.width, texture1.height);

        m_transientList.Add(new PoppedMote(g, position, texture1, texture2, dropDead));
    }

    /// <summary>
    /// Launch a transient message
    /// </summary>
    /// <param name="position"></param>
    /// <param name="texture"></param>
    /// <param name="dropDead"></param>
    protected void launchTransient(Vector2 position, Texture texture, float dropDead)
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
        float width = texture.width / 2;
        float height = texture.height / 2;
        g.guiTexture.pixelInset = new Rect(position.x - (width / 2), position.y - (height / 2), width, height);

        m_transientList.Add(new BannerTransient(g, position, texture, dropDead));
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
        g.transform.position = new Vector3(0, 0, 100); // give z a high value so it comes to the front
        g.transform.localScale = Vector3.zero;
        g.guiTexture.texture = texture;
        g.guiTexture.pixelInset = new Rect(position.x, position.y, texture.width / 2, texture.height / 2);
        m_transientList.Add(new ScoreTransient(g, position, m_transientManager.getTransientMovementVector(position), texture, dropDead));
    }
}
//}