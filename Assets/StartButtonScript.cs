using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class StartButtonScript : MonoBehaviour
    {
        /// <summary>
        /// Audio tone to play on press
        /// </summary>
        public AudioClip PlayTone;

        /// <summary>
        /// Secret!  Sh!
        /// </summary>
        static public string m_ignunr = @"XUJSHI029384nkshd829387476930-4ksdojsaksoojsa7";

        /// <summary>
        /// Music playing
        /// </summary>
        protected bool m_musicOn = true;

        /// <summary>
        /// Effects on
        /// </summary>
        protected bool m_fxOn = true;

        /// <summary>
        /// Load the level
        /// </summary>
        protected bool m_loadLevel = false;

        /// <summary>
        /// Storage for this object
        /// </summary>
        protected GameObject m_audioGameObject = null;

        /// <summary>
        /// A level manager
        /// </summary>
        protected LevelManager m_levelManager;

        /// <summary>
        /// Facebook initialisation
        /// </summary>
        private void SetInit()
        {
            enabled = true; // "enabled" is a magic global
        }

        /// <summary>
        /// Facebook initialisation
        /// </summary>
        /// <param name="isGameShown"></param>
        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            { // pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            { // start the game back up - we're getting focus again
                Time.timeScale = 1;
            }
        }

        /// <summary>
        /// Load our prefs here
        /// </summary>
        void Start()
        {
            XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);
            //Player.m_levelsUnlocked = XygloPlayerPrefs.GetInt("MWhl", 0);

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
                GoogleAnalytics.instance.LogScreen("Main Menu");

            // Hide any ads on this screeen
            //
            //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            //{
                //AdvertisementHandler.HideAds();
            //}

            // Turn off the lite label if we're not lite
            //
            GameObject liteLabel = GameObject.FindGameObjectWithTag("Lite");
            if (liteLabel != null && !WorldScene.m_isLite)
                liteLabel.SetActive(false);

            // Initialise levelmanager for later querying
            //
            m_levelManager = new LevelManager();
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
        /// On appl quit here
        /// </summary>
        void OnApplicationQuit()
        {
            // Good practise to save Prefs when application quit
            XygloPlayerPrefs.Save();
        }

        // Update is called once per frame
        void Update()
        {

            // Need to handle back button in WP8
            //
#if UNITY_WINRT
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
#endif
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

            Vector2 hitPosition = new Vector2(-1, -1);

            // Test for touch or mouse input position
            //
            if (Input.touches.Length != 0)
                hitPosition = Input.touches[0].position;
            else if (Input.GetMouseButtonDown(0))
                hitPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (hitPosition.x == -1 && hitPosition.y == -1)
                return;

            // With a RotatableGuiItem we need to use transform position and texture sizes rather than
            // guiTexture specific tests.
            //
            RotatableGuiItem item = (RotatableGuiItem)GetComponent("RotatableGuiItem");

            if (item != null)
            {
                if (item.getScreenRect().Contains(hitPosition))
                {
                    // -1 means go and find the latest level we're on
                    //
                    Debug.Log("StartButtonScript - Going straight to latest level to play = " + XygloPlayerPrefs.GetInt("HighLevel", 0));

                    // Fetch the latest level
                    //
                    int world = m_levelManager.getWorldNumberForLevel(XygloPlayerPrefs.GetInt("HighLevel", 0));

                    if (world == -1)
                    {
                        Debug.Log("StartButtonScript - got a world of " + world + " resetting to 0 and starting at first world");
                        world = 0;
                    }

                    // World debug
                    Debug.Log("StartButtonScript - jumping to world " + world);

                    // Game scenes start at 6
                    //
                    WorldScene.CurrentWorld = -1;  // start from latest level
                    Application.LoadLevel(WorldScene.WorldSceneBase + world);
                }
            }
        }
    }
}