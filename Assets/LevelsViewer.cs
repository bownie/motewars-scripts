using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    public class LevelsViewer : MonoBehaviour
    {
        /// <summary>
        /// Which world is selected to inspect the levels for
        /// </summary>
        static public int WorldSelected = -1;

        /// <summary>
        /// Effects on
        /// </summary>
        protected bool m_fxOn = true;

        /// <summary>
        /// Music playing
        /// </summary>
        protected bool m_musicOn = true;

        /// <summary>
        /// Storage for this object
        /// </summary>
        protected GameObject m_audioGameObject = null;

        /// <summary>
        /// Instantiate the LevelManager locally
        /// </summary>
        protected LevelManager m_levelManager;

        /// <summary>
        /// List of some level buttons
        /// </summary>
        protected List<GUIText> m_levelButtons;

        /// <summary>
        /// Font for the level buttons
        /// </summary>
        public Font ButtonFont;

        /// <summary>
        /// Used to getting the achievement list
        /// </summary>
        protected AchievementCentre m_achievementCentre;

        /// <summary>
        /// Player used for getting the score matirx
        /// </summary>
        protected Player m_player;

        /// <summary>
        /// Level to show next
        /// </summary>
        protected int m_maxWorld = 1;

        /// <summary>
        /// Gold star
        /// </summary>
        public Texture GoldStar;

        /// <summary>
        /// Silver star
        /// </summary>
        public Texture SilverStar;

        /// <summary>
        /// Bronze star
        /// </summary>
        public Texture BronzeStar;

        /// <summary>
        /// Empty star
        /// </summary>
        public Texture EmptyStar;

        /// <summary>
        /// Temporary star game objects
        /// </summary>
        public List<GameObject> m_temporaryStars = new List<GameObject>();

        /// <summary>
        /// Text colour active
        /// </summary>
        public Color m_activeColour = Color.green;

        /// <summary>
        /// Text colour inactive
        /// </summary>
        public Color m_inactiveColour = Color.grey;


        // Use this for initialization
        void Start()
        {
            //NGUIDebug.Log("LevelViewer - Start");
            XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);
            m_maxWorld = XygloPlayerPrefs.GetInt("MWhl", 1);

            // If we want to debug then we must cheat
            //
            if (WorldScene.m_debugLevels)
            {
                Debug.Log("LevelsViewer - setting debug worlds");
                m_maxWorld = 3;
            }

            // Hide any ads on this screeen
            //
            //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            //{
                //AdvertisementHandler.HideAds();
            //}

            // Fetch the audio preferences
            //
            m_fxOn = XygloPlayerPrefs.GetBool("MWMMMUT", true);
            m_musicOn = XygloPlayerPrefs.GetBool("MWMMMUS", true);

            // We're abusing the Respawn tag
            //
            m_audioGameObject = GameObject.FindGameObjectWithTag("Respawn");

            // Google analytics
            //
            if (GoogleAnalytics.instance)
                GoogleAnalytics.instance.LogScreen("Level Selection Screen");

            // Check and play music as necessary
            //
            if (m_musicOn)
            {
                if (!m_audioGameObject.audio.isPlaying)
                    m_audioGameObject.audio.Play();
            }
            else
            {
                if (m_audioGameObject.audio.isPlaying)
                    m_audioGameObject.audio.Pause();
            }

            // Load the achievements from storage and generate an object to manage them
            //
            int[] achievementLevels = XygloPlayerPrefs.GetIntArray("AchievementLevels", new int[0]);
            string[] achievements = XygloPlayerPrefs.GetStringArray("Achievements", new string[0]);

            m_achievementCentre = new AchievementCentre();
            m_achievementCentre.loadAchievements(achievementLevels, achievements);

            // Load the high scores from storage
            //
            int[] historyLevels = XygloPlayerPrefs.GetIntArray("HistoryLevels", new int[0]);
            int[] historyScores = XygloPlayerPrefs.GetIntArray("HistoryLevelScores", new int[0]);
            m_player = new Player();
            m_player.loadHistoryLevels(historyLevels, historyScores);

            // Initialise level buttons
            //
            m_levelButtons = new List<GUIText>();

            // Now initialise buttons from the world
            //
            if (WorldSelected != -1)
            {
                m_levelManager = new LevelManager();
            }

            // And set the high level - with debug cheat
            //
            if (WorldScene.m_debugLevels)
            {
                m_player.setHighLevel(m_levelManager.getLastLevelAvailable());
            }
            else
            {
                m_player.setHighLevel(XygloPlayerPrefs.GetInt("HighLevel", 0));
            }

            //NGUIDebug.Log("LevelViewer - Setting up levels");
            setupLevels();
        }

        // Update is called once per frame
        void Update()
        {
            // Check and play music as necessary
            //
            if (m_musicOn)
            {
                if (!m_audioGameObject.audio.isPlaying)
                    m_audioGameObject.audio.Play();
            }
            else
            {
                if (m_audioGameObject.audio.isPlaying)
                    m_audioGameObject.audio.Pause();
            }

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

        /// <summary>
        /// Set up the levels for the world
        /// </summary>
        protected void setupLevels()
        {
            LevelSet levelSet = m_levelManager.getLevelSets()[WorldSelected];

            // Get the level number for the beginning of this world - levels are ordered
            // sequentially but inhabit different worlds.
            //
            int worldLevel = m_levelManager.getFirstLevelNumberForWorld(WorldSelected);
            int originalWorldLevel = worldLevel;

            Debug.Log("First level number for world = " + worldLevel);
            Debug.Log("LEVEL TEST High level = " + (m_player.getHighLevel() - worldLevel));

            // Generate some GameObjects and GUITexts for these levels - we ignore the boss level at the end of each world
            //
            for (int i = 0; i < levelSet.getLevelCountNonBoss(); i++)
            {
                Debug.Log("Generating level " + i + " from levelSet " + levelSet.getName());

                GameObject go = new GameObject("GUIText " + i);
                
                m_levelButtons.Add((GUIText)go.AddComponent(typeof(GUIText)));

                m_levelButtons[i].font = ButtonFont;
                m_levelButtons[i].text = (worldLevel + 1) + ". " + levelSet.getLevel(i).getName();
                m_levelButtons[i].transform.position = new Vector3(i < 5 ? 0.3f : 0.7f, 0.8f - (i % 5) * 0.15f, 0);
                m_levelButtons[i].transform.localScale = Vector3.zero;
                m_levelButtons[i].guiText.anchor = TextAnchor.MiddleCenter;
                m_levelButtons[i].guiText.fontSize = (int)Screen.width / 42;

                //Debug.Log("i = " + i + ", high = " + (m_player.getHighLevel() - originalWorldLevel));

                // Set colour according to if this level is unlocked or not - it's unlocked if there is
                // a valid high score for it or the m_player.getHighLevel limit is set to the value of
                // the level.
                //
                if (m_player.getHistoryLevelScore(worldLevel) > 0 || i <= (m_player.getHighLevel() - originalWorldLevel))
                {
                    m_levelButtons[i].guiText.color = m_activeColour;
                    drawAchievementsStars(worldLevel, m_levelButtons[i].transform.position);
                }
                else
                    m_levelButtons[i].guiText.color = m_inactiveColour;

                // Increment world level
                //
                worldLevel++;
            }
        }

        /// <summary>
        /// Show some level information
        /// </summary>
        protected void printLevels()
        {
            foreach (LevelSet levelSet in m_levelManager.getLevelSets())
            {
                Debug.Log("LevelSet " + levelSet.getLevelSetNumber() + " = " + levelSet.getName());

                for (int i = 0; i < levelSet.getLevelCount(); i++)
                {
                    Debug.Log("  Level " + i + " = " + levelSet.getLevel(i).getName());
                }
            }
        }

        /// <summary>
        /// Do mouse processing
        /// </summary>
        protected void doMouse()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            // Handle click
            //
            handleClick(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }


        /// <summary>
        /// Do touch processing
        /// </summary>
        protected void doTouch()
        {
            // If no input touches
            //
            if (Input.touches.Length == 0)
                return;

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (handleClick(touch.position))
                            continue;

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
            for (int i = 0; i < m_levelButtons.Count; i++)
            {
                if (m_levelButtons[i].GetScreenRect().Contains(position) && m_levelButtons[i].guiText.color == m_activeColour)
                {
                    // Set attributes in in the MoteManager before jumping to it
                    //
                    WorldScene.CurrentWorld = -2;
                    WorldScene.JumpToLevel = m_levelManager.getFirstLevelNumberForWorld(WorldSelected) + i;
                    Application.LoadLevel(WorldScene.WorldSceneBase + WorldSelected);
                }
            }

            return true;
        }


        /// <summary>
        /// Create some stars for a given level
        /// </summary>
        /// <param name="level"></param>
        protected void drawAchievementsStars(int levelNumber, Vector2 position)
        {
            // Initial medal position for a given world
            //
            Vector2 medalPosition = new Vector2(position.x + 0.1f, position.y + 0.03f);

            //Debug.Log("drawAchievementsStars - searching for level " + levelNumber);
            int historicScore = m_player.getHistoryLevelScore(levelNumber);
            Level level = m_levelManager.getLevelForWorldNumber(levelNumber);

            // Do nothing if no level found
            //
            if (level == null)
            {
                Debug.Log("Didn't find level " + levelNumber);
                return;
            }

            LevelScoreIdentifier medal = level.getMedal(historicScore);

            if (medal == LevelScoreIdentifier.Bronze)
            {
                createStar(medalPosition, BronzeStar, 1);
                medalPosition.x += 0.03f;
                createStar(medalPosition, EmptyStar, 2);
                medalPosition.x += 0.03f;
                createStar(medalPosition, EmptyStar, 3);
            }
            else if (medal == LevelScoreIdentifier.Silver)
            {
                createStar(medalPosition, BronzeStar, 1);
                medalPosition.x += 0.03f;
                createStar(medalPosition, SilverStar, 2);
                medalPosition.x += 0.03f;
                createStar(medalPosition, EmptyStar, 3);
            }
            else if (medal == LevelScoreIdentifier.Gold)
            {
                createStar(medalPosition, BronzeStar, 1);
                medalPosition.x += 0.03f;
                createStar(medalPosition, SilverStar, 2);
                medalPosition.x += 0.03f;
                createStar(medalPosition, GoldStar, 3);
            }
        }

        /// <summary>
        /// Create a star
        /// </summary>
        /// <param name="position"></param>
        /// <param name="starTexture"></param>
        protected void createStar(Vector2 position, Texture starTexture, float z)
        {
            //Debug.Log("Creating star at position x = " + position.x + ", y = " + position.y);
            GameObject go = new GameObject("World Star");
            GUITexture texture = (GUITexture)go.AddComponent(typeof(GUITexture));
            texture.transform.localScale = new Vector3(0.02f, 0.02f, z);
            texture.transform.position = new Vector3(position.x, position.y, 1);
            texture.guiTexture.pixelInset = new Rect(-15, 2, 30, 30);
            texture.guiTexture.texture = starTexture;
            m_temporaryStars.Add(go);
        }
    }
}
