using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
using Xyglo.Unity;

//namespace Xyglo.Unity
//{

/// <summary>
/// State of the MoteManager
/// </summary>
public enum MoteManagerState
{
    Interstitial,
    Paused,
    Playing,
    PostLevelSummary,
    DeathScreen,
    GameCompleted
}

/// <summary>
/// Manage some motes
/// </summary>
[System.Serializable]
public class MoteManager : MonoBehaviour
{
#if MOTEMANAGER_OLD_SCRIPT

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
    public AudioClip UnhappyCompletion;

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
    /// List of textures
    /// </summary>
    protected List<Texture> m_interstitialTextures = new List<Texture>();

    /// <summary>
    /// Interstitial start time
    /// </summary>
    protected float m_interstitialStart = 0;

    /// <summary>
    /// Length of interstitial
    /// </summary>
    protected float m_interstitialLength = 2.0f; // 6.0f

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
        //protected GameObject m_facebookButton;
#endif

    /// <summary>
    /// Twitter button
    /// </summary>
    protected GameObject m_twitterButton;


    /// <summary>
    /// What state is the MoteManager in?
    /// </summary>
    protected MoteManagerState m_state;

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
        m_player.setHighScore(SecuredPlayerPrefs.GetInt("MWsst", 0));

        // Always at least one world unlocked
        //
        m_player.m_worldsUnlocked = Mathf.Max(SecuredPlayerPrefs.GetInt("MWhl", 1), 1); 
        //m_player.m_worldsUnlocked = 10; // Mathf.Max(SecuredPlayerPrefs.GetInt("MWhl", 1), 1); 

        //Debug.Log("Got highscore = " + m_player.m_highScore);
        //Debug.Log("Got levels unlocked = " + m_player.m_worldsUnlocked);
        //Debug.Log("Current World = " + CurrentWorld);

        m_player.setSoundEffects(SecuredPlayerPrefs.GetBool("MWMMMUT", true));
        m_player.setMusic(SecuredPlayerPrefs.GetBool("MWMMMUS", true));

        // High level
        //
        m_player.setHighLevel(SecuredPlayerPrefs.GetInt("HighLevel", 0));
        //m_player.setHighLevel(99);

        // Load the achievements
        //
        int[] achievementLevels = SecuredPlayerPrefs.GetIntArray("AchievementLevels", new int[0]);
        string[] achievements = SecuredPlayerPrefs.GetStringArray("Achievements", new string[0]);
        m_achievementCentre.loadAchievements(achievementLevels, achievements);

        // Load the high scores
        //
        int[] historyLevels = SecuredPlayerPrefs.GetIntArray("HistoryLevels", new int[0]);
        int[] historyScores = SecuredPlayerPrefs.GetIntArray("HistoryLevelScores", new int[0]);
        m_player.loadHistoryLevels(historyLevels, historyScores);
    }

    /// <summary>
    /// Save game state
    /// </summary>
    protected void saveGameState()
    {
        Debug.Log("saveGameState() - storing");

        // Top score save
        //
        int highScore = (int)Mathf.Max(m_player.getTotalScore(), m_player.getHighScore());
        SecuredPlayerPrefs.SetInt("MWsst", highScore);

        // Highlevel could be m_worldsUnlocked (last value) or perhaps we've unlocked a new
        // one in CurrentWorld.
        //
        //Debug.Log("saveGameState - CurrentWorld = " + CurrentWorld);
        SecuredPlayerPrefs.SetInt("MWhl", Mathf.Max(m_player.m_worldsUnlocked, CurrentWorld + 1));
        //SecuredPlayerPrefs.SetInt("LatestPlayedLevel", m_levelManager.getGameLevelNumber());

        // High level
        //
        SecuredPlayerPrefs.SetInt("HighLevel", m_player.getHighLevel());

        // Sound and music
        //
        SecuredPlayerPrefs.SetBool("MWMMMUT", m_player.wantsSoundEffects());
        SecuredPlayerPrefs.SetBool("MWMMMUS", m_player.wantsMusic());

        // Levels and achievements
        //
        //Debug.Log("+++ Achievement log = " + m_achievementCentre.getAchievementString());
        SecuredPlayerPrefs.SetIntArray("AchievementLevels", m_achievementCentre.getAchievementLevelList());
        SecuredPlayerPrefs.SetStringArray("Achievements", m_achievementCentre.getAchievementList());

        // Store the historic score arrays
        //
        SecuredPlayerPrefs.SetIntArray("HistoryLevels", m_player.getHistoryLevelsIntArray());
        SecuredPlayerPrefs.SetIntArray("HistoryLevelScores", m_player.getHistoryLevelsScoresIntArray());

        // Save everything
        //
        SecuredPlayerPrefs.Save();
    }


    /// <summary>
    /// Reset some important statics so that we get a clean restart
    /// </summary>
    protected void resetStatics()
    {
        // m_player stays as it is
        MoteManager.m_moteHud = null;
        MoteManager.m_moteList = null;
        MoteManager.m_transientList = null;

        // Reset this
        //
        LevelSet.m_levelNumber = 0;

        // And this
        //
        m_interstitialStart = 0;

        // Clear levels and level sets
        //
        //if (MoteManager.m_levelManager != null)
        //{
        //MoteManager.m_levelManager.m_levelSets.Clear();
        //}
        //MoteManager.m_levelManager = null;

        // Reset any lives
        //
        if (MoteManager.m_player != null)
        {
            MoteManager.m_player.setLives(3);
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

        if (MoteManager.m_moteHud == null)
        {
            MoteManager.m_moteHud = new MoteHud(m_hudControl.guiTexture.texture, NoFxHudTexture, NoMusicHudTexture, MutedHudTexture, m_hudControl);

        }

        if (MoteManager.m_player == null)
        {
            MoteManager.m_player = new Player("Me", 3);
        }

        if (MoteManager.m_transientList == null)
        {
            MoteManager.m_transientList = new List<Transient>();
        }

        // Fetch this object which needs to be attached alongside the MoteManager
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
    /// When Unity initalises this class
    /// </summary>
    void Start()
    {
        // Init by setting the secret key, never ever change this line in your app !
        //
        SecuredPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);

        // Ensure any statics are reset when we reload the game scene
        //
        resetStatics();

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
                //Debug.Log("CurrentWorld == -2 : Jumping to level " + JumpToLevel);

                // And set the CurrentWorld from LevelSet
                //
                CurrentWorld = m_levelManager.getGameLevel().getLevelSet().getLevelSetNumber();
            }
            else if (CurrentWorld == -1) // -1 denotes that we want to start from the last played level
            {
                //int level = SecuredPlayerPrefs.GetInt("LatestPlayedLevel", 0);
                int level = SecuredPlayerPrefs.GetInt("HighLevel", 0);
                //Debug.Log("CurrentWorld == -1 : Starting with level " + level);

                // Find the latest played level
                //
                m_levelManager.setLevel(level);

                // And set the CurrentWorld from LevelSet
                CurrentWorld = m_levelManager.getGameLevel().getLevelSet().getLevelSetNumber();
            }
            else  // We're just selecting a level by world number - get the first level for that world
            {
                //Debug.Log("Setting start level by CurrentWorld == " + CurrentWorld);

                Level startingLevel = m_levelManager.getLevelForWorld(CurrentWorld);
                if (startingLevel != null)
                {
                    //Debug.Log("OTHER - Got starting level = " + startingLevel.getLevelNumber());
                    m_levelManager.setLevel(startingLevel.getLevelNumber());
                }
            }
        }

        if (MoteManager.m_moteList == null)
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

        // Setup the interstitial textures
        //
        setupInterstitials();

        //  Starting this world
        //
        //m_worldStart = true;

        // Google analytics
        //
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("Starting Game First Time");
            //Debug.Log("GoogleAnalytics - Starting Game First Time");
        }

        // For the first time we play a level straight from the menu then we want to see the interstitial
        //
        m_state = MoteManagerState.Interstitial;
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
        m_interstitialTitleMessage.guiText.material.color = Color.magenta;

        m_interstitialIntroductionMessage = GameObject.FindWithTag("InterstitialIntroduction");
        m_interstitialIntroductionMessage.guiText.material.color = Color.blue;

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

        // For boss motes we create those once
        //
        if (m_levelManager.getGameLevel().isBoss() && m_levelManager.getGameLevel().hasBossBeenGenerated() == false)
        {
            createMote(m_levelManager.getGameLevel().getBoss(), false);
            m_levelManager.getGameLevel().setBossGenerated(true);
        }

        // If the active moteList is less than the total number of Motes in level then keep generating
        //
        if ((MoteManager.m_moteList.Count < m_levelManager.getGameLevel().getMotes().Count) ||
            (MoteManager.m_moteList.Count < m_levelManager.getGameLevel().getGenerateLevel()))
        {
            //Debug.Log("REGENS = " + m_levelManager.getGameLevel().regenerations());
            int moteIndex = 0;

            // Test for regenerative level - if we have one then we don't generate straight for the
            // mote list.
            //
            Mote levelMote = null;

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
                moteIndex = MoteManager.m_moteList.Count;
                levelMote = m_levelManager.getGameLevel().getMotes()[moteIndex];
            }

            //Debug.Log("Level = " + m_levelManager.getGameLevelNumber() + ", MOTE INDEX = " + moteIndex);

            if (levelMote != null)
            {
                createMote(levelMote, false);
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
        foreach (Mote mote in MoteManager.m_moteList)
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
        float maxSize = 128.0f;

        if (moteObject is StaticMote)
        {
            g = new GameObject("StaticMote");
            newTexture = StaticMoteTexture;
            activeMote = new StaticMote(g);
            activeMote.setPosition(moteObject.getPosition());
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
        else if (moteObject.GetType() == typeof(SquishMote))
        {
            g = new GameObject("SquishMote");
            newTexture = SquishMoteTexture;
            activeMote = new SquishMote(g, true, SquishMoteTexture, SquishMoteAlarmedTexture, rescaleTexture(SquishMoteTexture, maxSize));
            //Debug.Log("Creating new SquishMote at x = " + moteObject.getPosition().x + ", y = " + moteObject.getPosition().y);
            //activeMote.setPosition(moteObject.getPosition());
        }
        else if (moteObject.GetType() == typeof(BeeMote))
        {
            g = new GameObject("BeeMote");
            newTexture = BeeMoteTexture;

            FluffyMote fluffyMote = (FluffyMote)moteObject;
            activeMote = new FluffyMote(g, fluffyMote.startOffScreen(), rescaleTexture(BeeMoteTexture, maxSize));
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
            g = new GameObject("CellMote");
            newTexture = CellMoteTexture;
            //CellMote cellMote = (CellMote)moteObject;
            activeMote = new CellMote(g, rescaleTexture(ShieldTexture, maxSize));
        }
        else if (moteObject.GetType() == typeof(AcornMote))
        {
            g = new GameObject("AcornMote");
            newTexture = AcornMoteTexture;
            //AcornMote acornMote = (AcornMote)moteObject;
            activeMote = new AcornMote(g);
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
                SporeMote sporeMote = (SporeMote)moteObject;
                activeMote = new SporeMote(g, sporeMote.startOffScreen(), rescaleTexture(SporeMoteTexture, maxSize), spore.getGeneration());
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
                activeMote.setPosition(spore.getPosition());
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
            activeMote.setWorth(moteObject.getWorth());

            // Add the mote
            //
            MoteManager.m_moteList.Add(activeMote);

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
            activeMote.setAcceleration(shoot.getAttackVector() / 50);



            Debug.Log("Acceleration x = " + activeMote.getAcceleration().x + ", y = " + activeMote.getAcceleration().y);
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
            g.transform.position = Vector3.zero;
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
                //if (activeMote.GetType() == typeof(BossMegaSheep))
                //{
                //Debug.Log("Positioning MEGA NOW at x = " + activeMote.getPosition().x + ", y = " + activeMote.getPosition().y);
                //}

                //Debug.Log("Positioning mote NOW at x = " + activeMote.getPosition().x + ", y = " + activeMote.getPosition().y + " with width = " + width + " and height = " + height);
                g.guiTexture.pixelInset = new Rect(activeMote.getPosition().x, activeMote.getPosition().y, width, height);
            }

            // set the worth of the mote
            //
            activeMote.setWorth(moteObject.getWorth());

            // Add the mote
            //
            MoteManager.m_moteList.Add(activeMote);
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
            Debug.Log("Setting highscore = " + m_player.getTotalScore());
            Debug.Log("World unlocked = " + m_player.getTotalScore());

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
        }
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
    /// Setup the interstitials
    /// </summary>
    protected void setupInterstitials()
    {
        //Debug.Log("setupInterstitials - setting up the textures");
        TextureManager tm = (TextureManager)m_backgroundObject.GetComponent(typeof(TextureManager));
        m_interstitialTextures.Add(tm.MeadowInterTexture);
        //m_interstitialTextures.Add(tm.ForestInterTexture);
        //m_interstitialTextures.Add(tm.CityGateInterTexture);
        /*
         * m_interstitialTextures.Add(tm.SpaceCityInterTexture);
        m_interstitialTextures.Add(tm.HinterlandInterTexture);
        m_interstitialTextures.Add(tm.SpaceElevatorInterTexture);
        m_interstitialTextures.Add(tm.LEOInterTexture);
        m_interstitialTextures.Add(tm.SaturnInterTexture);
        m_interstitialTextures.Add(tm.WormholeInterTexture);
        m_interstitialTextures.Add(tm.MoteWorldInterTexture);
         * */
    }

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
            //Debug.Log("--> Initialise interstitial with current world = " + CurrentWorld);

            m_interstitialStart = Time.time;
            m_moteHud.setGameObjectsActivity(false);

            // Initialise the interstitial object
            m_interstitialObject.guiTexture.texture = m_interstitialTextures[CurrentWorld];
            m_interstitialObject.transform.localScale = Vector3.zero;
            m_interstitialObject.transform.localPosition = Vector3.zero;

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
            float x = Screen.width - (Screen.width * (timeNow / expandPeriod));
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
        slideMessage(m_interstitialTitleMessage, m_interstitialStart + 0.3f, m_levelManager.getLevelSet().getName(), new Vector2(-500, 70), Vector2.zero, new Vector2(500, 70), 1.0f, 10.0f, 0.0f);
        slideMessage(m_interstitialIntroductionMessage, m_interstitialStart + 0.3f, m_levelManager.getLevelSet().getIntroductionText(), new Vector2(Screen.width + 300, -70), new Vector2(0, -70), new Vector2(-500, -70), 1.0f, 10.0f, 0.0f);

        // If the interstitial has completed then prepare the game for playing
        //
        if (Time.time - m_interstitialStart > m_interstitialLength)
        {
            //Debug.Log("Starting to Play Game at time " + Time.time);
            m_state = MoteManagerState.Playing;
            m_startTime = Time.time;
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
    /// Heartbeat called by unity
    /// </summary>
    void Update()
    {
        if (m_state == MoteManagerState.Interstitial)
        {
            doInterstitial();
        }
        else if (m_state == MoteManagerState.Playing)
        {
            // Check for game completion
            //
            if (m_levelManager.isGameCompleted())
            {
                m_state = MoteManagerState.GameCompleted;
                return;
            }

            // We're playing the level - so keep animating everything
            //
            doPlayingLevel();

            // Handle any touches
            //
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                doTouch();
            }
            else
            {
                doMouse();
            }

            // Hud update
            //
            //if (m_worldStart == false)
            //{
            updateHud();
            //}
            //else
            //{
            // Spend a few seconds if we want to explain the world a little
            //
            //if (Time.time > m_startTime + 0)
            //{
            //m_worldStart = false;
            //m_startTime = Time.time;
            //}
            //}

            // Pan the background
            //
            updateBackground();

            // Check and set playing status
            //
            checkAudio();

            // Now let's see if we've scored any achievements and handle that
            //
            checkAchievements();
        }
        else if (m_state == MoteManagerState.DeathScreen)
        {
            // Handle any touches
            //
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                doTouch();
            }
            else
            {
                doMouse();
            }

            // Keep the HUD updated and any animations going
            //
            updateHud();

            // Pan the background
            //
            updateBackground();
        }
        else if (m_state == MoteManagerState.GameCompleted)
        {
            doVictoryScreen();

            // Handle any touches
            //
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                doTouch();
            }
            else
            {
                doMouse();
            }

            // Keep the HUD updated and any animations going
            //
            updateHud();

            // Pan the background
            //
            updateBackground();
        }
        else if (m_state == MoteManagerState.PostLevelSummary)
        {
            // Sum up the achievements on this level - we might not use this state yet
            //
        }

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
    /// Check for mote collisions
    /// </summary>
    protected void checkCollisions()
    {
        foreach (Mote mote in MoteManager.m_moteList)
        {
            if (!mote.getGameObject().activeInHierarchy)
                continue;
            Rect moteRect = mote.getGameObject().guiTexture.GetScreenRect();
            Rect moteShipRect = m_moteShip.getGameObject().guiTexture.GetScreenRect();

            // Adjust mote ship size a bit as we're intersecting with rectangles
            //
            moteShipRect.x += 30;
            moteShipRect.y += 50;
            moteShipRect.width -= 60;
            moteShipRect.height -= 100;

            //if (rectIntersect(mote.getGameObject().guiTexture.GetScreenRect(), m_moteShip.getGameObject().guiTexture.GetScreenRect()))
            if (betterRectIntersect(moteRect, moteShipRect))
            {
                // Depends on mote type here
                //
                if (mote.GetType() == typeof(CellMote))
                {
                    CellMote cM = (CellMote)mote;

                    if (cM.getCellState() == CellMoteState.Opened)
                    {
                        Debug.Log("Get prize!!");

                        switch (cM.getPrize())
                        {
                            // Activate the shield
                            //
                            case CellPrize.Shield:
                                m_player.addPowerup(PlayerPowerup.Shield);
                                m_moteShip.setTexture(MoteShipShield);

                                mote.setDead(true);
                                mote.hideMote();
                                break;

                            case CellPrize.Grenade:
                                m_player.addPowerup(PlayerPowerup.Grenade);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else // death
                {

                    // Check for shield
                    //
                    if (m_player.testPowerUp(PlayerPowerup.Shield))
                    {
                        m_player.removePowerup(PlayerPowerup.Shield);
                        m_moteShip.setTexture(MoteShip);

                        // Pop the mote and ensure scoring and transients are still launched
                        popMote(mote, false);
                    }
                    else
                    {
                        // Do ship death
                        //
                        doDeath();
                        return;
                    }
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
        MoteManager.m_player.decrementLives();
        playSound(DeathKnell);

        // Reset and escape
        //
        resetLevel(false);
    }

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
        if (MoteManager.m_player.wantsMusic())
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
        m_moteHud.setHudControls(MoteManager.m_player.wantsMusic(), MoteManager.m_player.wantsSoundEffects());
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
    /// Update the hud messages as necessary
    /// </summary>
    protected void updateHud()
    {
        if (m_levelManager.getGameLevel() == null)
        {
            Debug.Log("MoteManager::updateHud() - null game level " + m_levelManager.getGameLevelNumber());
            return;
        }

        if (MoteManager.m_moteHud != null)
        {
            // Keep high score current
            //if (m_player.getTotalScore() > m_player.getTotalScore())
                //m_player.m_highScore = _player.m_totalScore;

            // Set the hud
            //
            //MoteManager.m_moteHud.setHud(m_player.m_totalScore, m_player.m_highScore, m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()), m_player.getLives(), m_levelManager.getGameLevel());
        }

        // Check and display level finished hud and return if we have completed
        //
        if (levelFinishedHud())
            return;

        if (m_state != MoteManagerState.Interstitial)
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
            slideMessage(m_completionMessage, m_completeTime, "Your mission is over...", new Vector2(-500, -70), Vector2.zero, new Vector2(500, 70), 1.0f, 10.0f, 0.0f);

//#if FACEBOOK_ENABLED
                //m_facebookButton.SetActive(true);
//#endif
            m_twitterString = "I'm+playing+Mote+Wars+and+just+got+a+total+score+of+" + m_player.getTotalScore();
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
        Debug.Log("doLevelMessage");

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
            for (int i = 0; i < MoteManager.m_player.getLives() - 1; i++)
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
            //Debug.Log("GET LVES = " + MoteManager.m_player.getLives() + ", LIFE OBJECTS = " + (m_lifeObjects.Count + 1));
            // Ensure lives decrement on HUD
            //
            if (MoteManager.m_player.getLives() - 1 < m_lifeObjects.Count)
            {
                DestroyObject(m_lifeObjects[MoteManager.m_player.getLives() - 1]);
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
        m_state = MoteManagerState.DeathScreen;
    }

    /// <summary>
    /// Do any movement for motes
    /// </summary>
    protected void animateMotes()
    {
        foreach (Mote mote in MoteManager.m_moteList)
        {
            mote.doMove();

            // Dim motes as necessary
            //
            Color newColour = mote.getGameObject().guiTexture.color;
            newColour.a = (m_levelComplete ? 0.1f : mote.getOriginalAlpha());
            mote.getGameObject().guiTexture.color = newColour;
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
        foreach (Mote mote in MoteManager.m_moteList)
            Destroy(mote.getGameObject());

        MoteManager.m_moteList.Clear();

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

                //m_worldStart = true;

                // Now change state and queue this up
                //
                queueInterstitial();

                // Google analytics
                //
                if (GoogleAnalytics.instance)
                {
                    GoogleAnalytics.instance.LogScreen("Completed World");
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
        m_state = MoteManagerState.Interstitial;
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
            MoteManager.m_moteList.Remove(mote);
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

        // Temporary check
        //
        if (checkForWholeGameRestart(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
            return;

        if (m_levelComplete && Time.time > m_completeTime + m_levelCompletionPause)
        {
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
    /// Do touch processing
    /// </summary>
    protected void doTouch()
    {
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
            Rect exitButton = new Rect(43, 150, 50, 24);
            Rect musicButton = new Rect(96, 119, 65, 18);
            Rect fxButton = new Rect(136, 71, 50, 24);
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
        m_levelManager.getGameLevel().incrementClicks();
        bool ignoreScore = false;

        //Debug.Log("Got touch");
        foreach (Mote mote in MoteManager.m_moteList)
        {
            // Used for ignoring scoring for removal of some objects
            //
            ignoreScore = false;

            if (mote.contains(position))
            {
                // Send the event
                //
                MoteManager.m_player.addScore(m_levelManager.getGameLevelNumber(), mote.getWorth());

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
                else if (mote.GetType() == typeof(CellMote))
                {
                    CellMote cM = (CellMote)mote;

                    if (cM.getCellState() == CellMoteState.Question)
                    {
                        cM.setCellState(CellMoteState.Opened);
                        mote.setTexture(ShieldTexture);
                        return true;
                    }
                    else
                    {
                        // else we destroy it by falling through
                        ignoreScore = true;
                    }
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
    /// Pop a mote and sort out the scoring and transients as necessary
    /// </summary>
    /// <param name="mote"></param>
    /// <param name="ignoreScore"></param>
    protected void popMote(Mote mote, bool ignoreScore)
    {

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
        if (MoteManager.m_player.wantsSoundEffects())
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
#endif
}
//}