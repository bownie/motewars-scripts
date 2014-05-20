using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class AboutScreen : MonoBehaviour
    {

        /// <summary>
        /// Use this for initialization
        /// </summary>

        protected GameObject m_playerDetails;

        protected GameObject m_xygloDetails;

        protected GameObject m_acknowledgements;

        protected Player m_player;

        protected AchievementCentre m_achievementCentre;

        protected LevelManager m_levelManager;

        /// <summary>
        /// Storage for this object
        /// </summary>
        protected GameObject m_audioGameObject = null;

        protected bool m_fxOn = false;

        protected bool m_musicOn = false;

        void Start()
        {
            XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);

            // Fetch the audio preferences
            //
            m_fxOn = XygloPlayerPrefs.GetBool("MWMMMUT", true);
            m_musicOn = XygloPlayerPrefs.GetBool("MWMMMUS", true);

            // We're abusing the Respawn tag
            //
            m_audioGameObject = GameObject.FindGameObjectWithTag("Respawn");

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


            m_playerDetails = GameObject.FindWithTag("Level1Button");
            m_xygloDetails = GameObject.FindWithTag("Level2Button");
            m_acknowledgements = GameObject.FindWithTag("Level3Button");

            /*
            // Hide any ads on this screeen
            //
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                AdvertisementHandler.HideAds();
                AdvertisementHandler.DisableAds();
            }*/

            // Set up player
            //
            m_player = new Player();

            // Generate level manager again
            //
            m_levelManager = new LevelManager();

            // Set up achievement centre
            //
            m_achievementCentre = new AchievementCentre();

            // Load m_player and m_levelManager and m_achievementCentre
            //
            loadGameState();

            // Populate details
            //
            string playerDetails = "High Score: " + m_player.getHighScore() + "\n";
            playerDetails += "Worlds Unlocked: " + m_player.m_worldsUnlocked + "\n";
            playerDetails += "Current Level: " + m_player.getHighLevel() + "\n";
            playerDetails += "Current Difficulty: " + m_player.getDifficultyLevel() + "\n";
            playerDetails += "Gold stars " + m_player.getTotalMedals(LevelScoreIdentifier.Gold, m_levelManager) + "\n";
            playerDetails += "Silver stars " + m_player.getTotalMedals(LevelScoreIdentifier.Silver, m_levelManager) + "\n";
            playerDetails += "Bronze stars " + m_player.getTotalMedals(LevelScoreIdentifier.Bronze, m_levelManager) + "\n";

            if (m_player.getSecondsShaking() > 0)
            {
                playerDetails += "Shakertronic Time " + m_player.getSecondsShaking().ToString("0.00") +"s\n";
            }

            m_playerDetails.guiText.text = playerDetails;

            string xygloDetails = "Mote Wars created by Xyglo Ltd\n";
            xygloDetails += "(c) 2014 All Rights Reserved.\n";
            xygloDetails += "Support available at http://www.xyglo.com\n";
            xygloDetails += "Privacy policy: http://www.xyglo.com/privacy\n";
            xygloDetails += "Twitter: @xyglo";
            m_xygloDetails.guiText.text = xygloDetails;

            string acknowledge = "";
            acknowledge = "Font 'Villa' by Jake Luedecke\n";
            acknowledge += "Music by Xyglo, Rob Oldham,\n";
            acknowledge += "'Move Forward' by Kevin MacLeod (incompetech.com),\n";
            acknowledge += "'Black Vortex' by Kevin MacLeod (incompetech.com)\n";
            m_acknowledgements.guiText.text = acknowledge;

        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }

        /// <summary>
        /// Load our prefs
        /// </summary>
        protected void loadGameState()
        {
            Debug.Log("loadGameState() - loading");

            // Init by setting the secret key, never ever change this line in your app !
            //
            XygloPlayerPrefs.SetSecretKey(StartButtonScript.m_ignunr);

            // Fetch some stored values
            //
            m_player.setHighScore(XygloPlayerPrefs.GetInt("MWsst", 0));

            // Always at least one world unlocked
            //
            m_player.m_worldsUnlocked = Mathf.Max(XygloPlayerPrefs.GetInt("MWhl", 1), 1);

            m_player.setSoundEffects(XygloPlayerPrefs.GetBool("MWMMMUT", true));
            m_player.setMusic(XygloPlayerPrefs.GetBool("MWMMMUS", true));

            // High level
            //
            m_player.setHighLevel(XygloPlayerPrefs.GetInt("HighLevel", 0));

            // Difficulty level
            //
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

            // Load seconds shaking
            //
            float secondsShaking = XygloPlayerPrefs.GetFloat("ShakertronicTime", 0.0f);
            m_player.setSecondsShaking(secondsShaking);
        }

    }
}