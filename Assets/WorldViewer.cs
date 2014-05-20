using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// View the levels we have available and allow navigation to those levels if they
    /// are unlocked
    /// </summary>
    public class WorldViewer : MonoBehaviour
    {

        /// <summary>
        /// Store active textures here
        /// </summary>
        public Texture TextureLevel2;
        public Texture TextureLevel3;
        public Texture TextureLevel4;
        public Texture TextureLevel5;
        public Texture TextureLevel6;
        public Texture TextureLevel7;
        public Texture TextureLevel8;
        public Texture TextureLevel9;
        public Texture TextureLevel10;

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
        /// Background image
        /// </summary>
        protected GameObject m_backgroundObject;

        protected Vector2 m_backgroundOriginalSize = new Vector2();

        /// <summary>
        /// Object that holds a cloud
        /// </summary>
        protected GameObject m_cloudObject;

        /// <summary>
        /// World currently showing
        /// </summary>
        protected int m_showingWorld = 1;

        /// <summary>
        /// Level to show next
        /// </summary>
        protected int m_maxWorld = 1;

        /// <summary>
        /// List of button objects
        /// </summary>
        protected List<GameObject> m_buttonList = new List<GameObject>();

        /// <summary>
        /// Cloud objects storage
        /// </summary>
        List<GameObject> m_cloudObjects = new List<GameObject>();

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
        /// Used to getting the achievement list
        /// </summary>
        protected AchievementCentre m_achievementCentre;

        /// <summary>
        /// Player used for getting the score matirx
        /// </summary>
        protected Player m_player;

        /// <summary>
        /// LevelManager
        /// </summary>
        protected LevelManager m_levelManager;

        /// <summary>
        /// Initialise
        /// </summary>
        void Start()
        {
            // You can use this to reset all the player prefs
            //
            XygloPlayerPrefs.DeleteAll();
            //SecuredPlayerPrefs.DeleteAll();

            XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);

            // Fetch the max world
            //
            m_maxWorld = XygloPlayerPrefs.GetInt("MWhl", 1);

            Debug.Log("START - m_maxWorld = " + m_maxWorld);

            //m_maxWorld = 1;

            // If we want to debug then we must cheat
            if (WorldScene.m_debugLevels)
            {
                if (WorldScene.m_isLite)
                    m_maxWorld = 3;
                else
                    m_maxWorld = 6;
            }

            // Hide any ads on this screeen
            //
            //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            //{
                //AdvertisementHandler.HideAds();
                //AdvertisementHandler.DisableAds();
            //}

            // Add the 'Lite' World buttons
            //
            m_buttonList.Add(GameObject.FindWithTag("Level1Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level4Button"));
			m_buttonList.Add(GameObject.FindWithTag("Level7Button"));

            // Add the full verison World buttons
            m_buttonList.Add(GameObject.FindWithTag("Level8Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level9Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level10Button"));

			// And the rest which are currently unused
			//
			m_buttonList.Add(GameObject.FindWithTag("Level2Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level3Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level5Button"));
            m_buttonList.Add(GameObject.FindWithTag("Level6Button"));
            
            
            // Fetch the audio preferences
            //
            m_fxOn = XygloPlayerPrefs.GetBool("MWMMMUT", true);
            m_musicOn = XygloPlayerPrefs.GetBool("MWMMMUS", true);

            // We're abusing the Respawn tag
            //
            m_audioGameObject = GameObject.FindGameObjectWithTag("Respawn");

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

            // Need to star up the levelmanager to get world/level details
            //
            m_levelManager = new LevelManager();

            // Google analytics
            //
            if (GoogleAnalytics.instance)
                GoogleAnalytics.instance.LogScreen("World Selection Screen");

            // Draw the achievements/stars on levels
            //
            drawAchievementsStars();

            // Do the world textures
            //
            if (m_showingWorld != m_maxWorld)
            {
                setWorldTextures();
            }
        }


        /// <summary>
        /// Update called once per frame
        /// </summary>
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
                //Debug.Log("APP PLATFORM = " + Application.platform.ToString());
                doTouch();
            }
            else
            {
                doMouse();
            }
        }

        /// <summary>
        /// Create some stars for a given level - we have to work out what level the world is at - so we need to
        /// count bronze and silver levels and rate that world based on the levels completed in that world.
        /// </summary>
        /// <param name="level"></param>
        protected void drawAchievementsStars()
        {
            // Total levels counter
            //
            int levelCounter = 0;
            LevelScoreIdentifier levelHighMedal;
            bool atLeastBronze = false;
            bool atLeastSilver = false;

            for (int i = 0; i < m_maxWorld; i++)
            {
                LevelSet levelSet = m_levelManager.getLevelSets()[i];
                levelHighMedal = LevelScoreIdentifier.None;
                atLeastBronze = false;
                atLeastSilver = false;


                // Initial medal position for a given world
                //
                Vector2 medalPosition = Vector2.zero;

                // Position according to number of worlds
                //
                if (WorldScene.m_isLite)
                {
                    medalPosition = new Vector2(0.71f, 0.75f - (i % m_maxWorld) * 0.25f);
                }
                else
                {
                    medalPosition = new Vector2(0.42f, 0.75f - (i % (Mathf.Max(m_maxWorld / 2, 1))) * 0.25f);

                    if (i >= 3)
                        medalPosition.x = 0.83f;
                }

                // Iterate through levels
                //
                for (int j = 0; j < levelSet.getLevelCount(); j++)
                {
                    Level level = levelSet.getLevel(j);

                    int historicScore = m_player.getHistoryLevelScore(levelCounter);
                    LevelScoreIdentifier medal = level.getMedal(historicScore);

                    if (medal > LevelScoreIdentifier.None)
                        atLeastBronze = true;

                    if (medal > LevelScoreIdentifier.Bronze)
                        atLeastSilver = true;

                    // Test for medal height in this level
                    if (j == 0)
                        levelHighMedal = medal;
                    else
                    {
                        if (medal > levelHighMedal)
                            levelHighMedal = medal;
                    }


                    //Debug.Log("Level " + levelCounter + " score = " + historicScore);
                    levelCounter++;
                }

                if (atLeastSilver)
                    levelHighMedal = LevelScoreIdentifier.Silver;
                else if (atLeastBronze)
                    levelHighMedal = LevelScoreIdentifier.Bronze;


                if (levelHighMedal == LevelScoreIdentifier.Bronze)
                {
                    createStar(medalPosition, BronzeStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, EmptyStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, EmptyStar);
                }
                else if (levelHighMedal == LevelScoreIdentifier.Silver)
                {
                    createStar(medalPosition, BronzeStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, SilverStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, EmptyStar);
                }
                else if (levelHighMedal == LevelScoreIdentifier.Gold)
                {
                    createStar(medalPosition, BronzeStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, SilverStar);
                    medalPosition.x += 0.05f;
                    createStar(medalPosition, GoldStar);
                }
            }
        }

        /// <summary>
        /// Create a star
        /// </summary>
        /// <param name="position"></param>
        /// <param name="starTexture"></param>
        protected void createStar(Vector2 position, Texture starTexture)
        {
            //Debug.Log("Creating star at position x = " + position.x + ", y = " + position.y);
            GameObject go = new GameObject("World Star");
            GUITexture texture = (GUITexture)go.AddComponent(typeof(GUITexture));
            texture.transform.localScale = new Vector3(0.05f, 0.05f, 1);
            texture.transform.position = new Vector3(position.x, position.y, 1);
            texture.guiTexture.pixelInset = new Rect(-15, -15, 30, 30);
            texture.guiTexture.texture = starTexture;
            m_temporaryStars.Add(go);
        }

        /// <summary>
        /// Not used
        /// </summary>
        void createCloudLevels()
        {
            List<Vector2> cloudPosition = new List<Vector2>();

            //float originalHeight = 1600;
            /*
            cloudPosition.Add(new Vector2(92,  1344));
            cloudPosition.Add(new Vector2(636, originalHeight - 986));
            cloudPosition.Add(new Vector2(964, originalHeight - 1390));
            cloudPosition.Add(new Vector2(1220, originalHeight - 1076));
            cloudPosition.Add(new Vector2(144, originalHeight - 908));
            cloudPosition.Add(new Vector2(1724, originalHeight - 510));
            cloudPosition.Add(new Vector2(1414, originalHeight - 402));
            cloudPosition.Add(new Vector2(778, originalHeight - 540));
            cloudPosition.Add(new Vector2(312, originalHeight - 380));
            cloudPosition.Add(new Vector2(1200, originalHeight - 766));
            */
            cloudPosition.Add(new Vector2(0, 0));
            cloudPosition.Add(new Vector2(1666, 800));
            foreach (Vector2 position in cloudPosition)
            {

                GameObject g = new GameObject("CloudLevel");
                g.AddComponent("GUITexture");
                g.guiTexture.texture = m_cloudObject.guiTexture.texture;
                g.transform.position = new Vector3(0, 0, 10);
                g.transform.localScale = Vector3.zero;

                // Because we're normally wider than taller (fix if not)
                //
                float scale = m_backgroundObject.guiTexture.pixelInset.width / m_backgroundOriginalSize.x;
                //Vector2 cloudSize = new Vector2(m_cloudObject.guiTexture.pixelInset.width * scale, m_cloudObject.guiTexture.pixelInset.height * scale);
                Vector2 cloudSize = new Vector2(m_cloudObject.guiTexture.pixelInset.width, m_cloudObject.guiTexture.pixelInset.height);
                Rect newRect = new Rect(position.x * scale, position.y * scale, cloudSize.x, cloudSize.y);

                //Debug.Log("Adding cloud at x = " + position.x + ", y = " + position.y);
                g.guiTexture.pixelInset = newRect;
                m_cloudObjects.Add(g);
            }

        }

        /// <summary>
        /// Update the level show
        /// </summary>
        /// <param name="levelToShow"></param>
        public void setShowingLevel(int levelToShow)
        {
            m_maxWorld = levelToShow;
        }

        /// <summary>
        /// Enable these buttons
        /// </summary>
        protected void setWorldTextures()
        {
            Debug.Log("setWorldTextures : m_maxWorld = " + m_maxWorld);

            if (m_maxWorld >= 2)
                m_buttonList[1].guiTexture.texture = TextureLevel4;

            if (m_maxWorld >= 3)
                m_buttonList[2].guiTexture.texture = TextureLevel7;

            // We add the extra guard for when we're changing from Lite to Full and we end up with
            // the wrong buttons enabled
            if (m_maxWorld >= 4 && m_buttonList[3].guiTexture.texture != null)
                m_buttonList[3].guiTexture.texture = TextureLevel8;

            if (m_maxWorld >= 5 && m_buttonList[4].guiTexture.texture != null)
                m_buttonList[4].guiTexture.texture = TextureLevel9;

            if (m_maxWorld >= 6)
                m_buttonList[5].guiTexture.texture = TextureLevel10;

            /*
            if (m_maxWorld >= 7)
                m_buttonList[6].guiTexture.texture = TextureLevel7;

            if (m_maxWorld >= 8)
                m_buttonList[7].guiTexture.texture = TextureLevel8;

            if (m_maxWorld >= 9)
                m_buttonList[8].guiTexture.texture = TextureLevel9;

            if (m_maxWorld >= 10)
                m_buttonList[9].guiTexture.texture = TextureLevel10;
			 */
			
            m_showingWorld = m_maxWorld;
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
            // Guard for not all the buttons activated
            //
            for (int i = 0; i < Mathf.Min(m_showingWorld, m_buttonList.Count); i++)
            {
                if (m_buttonList[i].guiTexture.GetScreenRect().Contains(position))
                {
                    //NGUIDebug.Log("WorldViewer::handleClick - Loading scene/level 4 with world " + i);
                    LevelsViewer.WorldSelected = i;
                    Application.LoadLevel(3);
                }
            }

            return true;
        }
    }
}