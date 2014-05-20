using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Power ups
    /// </summary>
    public enum PlayerPowerup
    {
        Shield,
        Grenade,
        Armour,
        ShipLevelX
    }


    /// <summary>
    /// Our player class holding lives and score details as well as player preferences
    /// </summary>
    public class Player
    {
        public Player()
        {
            m_name = "Default";
            m_lives = 3;
        }

        public Player(string name, int lives)
        {
            m_name = name;
            m_lives = lives;
        }

        /// <summary>
        /// Get name
        /// </summary>
        /// <returns></returns>
        public string getName() { return m_name; }

        /// <summary>
        /// Is music supposed to be playing
        /// </summary>
        /// <returns></returns>
        public bool wantsMusic() { return m_music; }

        /// <summary>
        /// Are sound effects playing?
        /// </summary>
        /// <returns></returns>
        public bool wantsSoundEffects() { return m_soundEffects; }

        /// <summary>
        /// Set music playing
        /// </summary>
        /// <param name="music"></param>
        public void setMusic(bool music) { m_music = music; }

        /// <summary>
        /// Set sound effects playing
        /// </summary>
        /// <param name="fx"></param>
        public void setSoundEffects(bool fx) { m_soundEffects = fx; } 

        /// <summary>
        /// Total score
        /// </summary>
        /// <returns></returns>
        public int getTotalScore()
        {
            return m_totalScore;
        }

        /// <summary>
        /// Set total score
        /// </summary>
        /// <param name="score"></param>
        public void setTotalScore(int score)
        {
            m_totalScore = score;
        }


        /// <summary>
        /// Count up all types of medal awarded
        /// </summary>
        /// <param name="medal"></param>
        /// <param name="levelManager"></param>
        /// <returns></returns>
        public int getTotalMedals(LevelScoreIdentifier medal, LevelManager levelManager)
        {
            int counter = 0;

            foreach(int level in m_scoreHistory.Keys)
            {
                Level levelObj = levelManager.getLevel(level);

                if (medal == levelObj.getMedal(m_scoreHistory[level]))
                    counter++;
            }

            /*
            foreach (LevelSet levelSet in levelManager.getLevelSets())
            {
                foreach (Level level in levelSet.getLevels())
                {
                    level.getMedalAwarded(getHistoryLevelScore(
                    m_medal = m_levelManager.getGameLevel().getMedalAwarded(m_player.getHistoryLevelScore(m_levelManager.getGameLevelNumber()), m_player.getLevelScore());
                    m_player.setHistoryLevelScore(m_levelManager.getGameLevelNumber(), m_player.getLevelScore());
                }
            }*/
            return counter;
        }

        /// <summary>
        /// Get score for a level - if it doesn't exist then add the level and return it
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int getHistoryLevelScore(int level)
        {
            if (!m_scoreHistory.ContainsKey(level))
                m_scoreHistory[level] = 0;

            return m_scoreHistory[level];
        }

        /// <summary>
        /// Return a list of levels that we've got a high score for
        /// </summary>
        /// <returns></returns>
        public List<int> getHistoryLevels()
        {
            return new List<int>(m_scoreHistory.Keys);
        }

        /// <summary>
        /// Set the high score
        /// </summary>
        /// <param name="score"></param>
        public void setHighScore(int score)
        {
            m_highScore = score;
        }

        /// <summary>
        /// Get the high score
        /// </summary>
        /// <returns></returns>
        public int getHighScore()
        {
            return m_highScore;
        }

        /// <summary>
        /// Get the highest level we've reached
        /// </summary>
        /// <returns></returns>
        public int getHighLevel()
        {
            return m_highLevel;
        }

        /// <summary>
        /// Set the highest level we've reached
        /// </summary>
        /// <param name="level"></param>
        public void setHighLevel(int level)
        {
            if (level > m_highLevel)
            {
                m_highLevel = level;
                //Debug.Log("Player::setHighLevel() - Setting high level = " + level);
            }
        }

        /// <summary>
        /// Get history levels in array for serialisation
        /// </summary>
        /// <returns></returns>
        public int[] getHistoryLevelsIntArray()
        {
            // Get history levels for writing
            //
            int[] historyLevels = new int[m_scoreHistory.Count];
            
            int i = 0;
            foreach (int historyLevel in m_scoreHistory.Keys)
                historyLevels[i++] = historyLevel;

            return historyLevels;
        }

        /// <summary>
        /// Get history level scores array for serialisation
        /// </summary>
        /// <returns></returns>
        public int[] getHistoryLevelsScoresIntArray()
        {
            int[] historyLevelScores = new int[m_scoreHistory.Count];

            int i = 0;
            foreach (int historyLevel in m_scoreHistory.Keys)
                historyLevelScores[i++] = getHistoryLevelScore(historyLevel);

            return historyLevelScores;
        }

        /// <summary>
        /// Load the history levels from the flattened lists
        /// </summary>
        /// <param name="levels"></param>
        /// <param name="levelScores"></param>
        public void loadHistoryLevels(int[] levels, int[] levelScores)
        {
            m_scoreHistory.Clear();

            for (int i = 0; i < levels.Length; i++)
            {
                m_scoreHistory[levels[i]] = levelScores[i];
            }
        }


        /// <summary>
        /// Set current level score
        /// </summary>
        /// <param name="score"></param>
        public void setLevelScore(int score)
        {
            m_levelScore = score;
        }

        /// <summary>
        /// Current level score
        /// </summary>
        /// <returns></returns>
        public int getLevelScore()
        {
            return m_levelScore;
        }

        /// <summary>
        /// Add Score to level
        /// </summary>
        /// <param name="level"></param>
        /// <param name="score"></param>
        public void addScore(int level, int score)
        {
            if (!m_scoreHistory.ContainsKey(level))
                m_scoreHistory[level] = 0;

            m_totalScore += score;
            m_levelScore += score;
        }

        /// <summary>
        /// Set the historical record
        /// </summary>
        /// <param name="level"></param>
        /// <param name="score"></param>
        public void setHistoryLevelScore(int level, int score)
        {
            m_scoreHistory[level] = score;
        }
        


        /// <summary>
        /// Decrement number of lives
        /// </summary>
        public void decrementLives()
        {
            m_lives--;

            // Score that the player loses per lost life
            //
            int scoreLostPerLife = 100;

            // Adjust scores
            //
            m_totalScore = Mathf.Max(m_totalScore - scoreLostPerLife, 0);
            m_levelScore = Mathf.Max(m_levelScore - scoreLostPerLife, 0);
        }

        /// <summary>
        /// Add some lives
        /// </summary>
        /// <param name="lives"></param>
        public void addLives(int lives)
        {
            m_lives += lives;
        }

        /// <summary>
        /// Set number of lives
        /// </summary>
        /// <param name="lives"></param>
        public void setLives(int lives)
        {
            m_lives = lives;
        }

        /// <summary>
        /// Add a powerup
        /// </summary>
        /// <param name="powerup"></param>
        public void addPowerup(PlayerPowerup powerup)
        {
            if (!m_powerups.Contains(powerup))
                m_powerups.Add(powerup);
        }

        /// <summary>
        /// Remove a power up
        /// </summary>
        /// <param name="powerup"></param>
        public void removePowerup(PlayerPowerup powerup)
        {
            m_powerups.Remove(powerup);
        }

        /// <summary>
        /// Test for powerup
        /// </summary>
        /// <param name="powerup"></param>
        /// <returns></returns>
        public bool testPowerUp(PlayerPowerup powerup)
        {
            return m_powerups.Contains(powerup);
        }

        /// <summary>
        /// Get the list of powerups
        /// </summary>
        /// <returns></returns>
        public List<PlayerPowerup> getPowerups()
        {
            return m_powerups;
        }

        /// <summary>
        /// Get the difficulty level
        /// </summary>
        /// <returns></returns>
        public int getDifficultyLevel()
        {
            return m_difficultyLevel;
        }

        /// <summary>
        /// Set the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void setDifficultyLevel(int difficultyLevel)
        {
            m_difficultyLevel = difficultyLevel;
        }

        /// <summary>
        /// Total seconds spent shaking
        /// </summary>
        /// <returns></returns>
        public float getSecondsShaking()
        {
            return m_secondsShaking;
        }

        /// <summary>
        /// Add seconds shaking
        /// </summary>
        /// <param name="seconds"></param>
        public void addSecondsShaking(float seconds)
        {
            m_secondsShaking += seconds;
        }

        /// <summary>
        /// Set shaking time
        /// </summary>
        /// <param name="seconds"></param>
        public void setSecondsShaking(float seconds)
        {
            m_secondsShaking = seconds;
        }


        /// <summary>
        /// Lives
        /// </summary>
        /// <returns></returns>
        public int getLives() { return m_lives; }

        /// <summary>
        /// Player name
        /// </summary>
        protected string m_name;

        /// <summary>
        /// Lives
        /// </summary>
        protected int m_lives;

        /// <summary>
        /// Score on current level
        /// </summary>
        protected int m_levelScore = 0;

        /// <summary>
        /// Keep a track of total score
        /// </summary>
        protected int m_totalScore = 0;

        /// <summary>
        /// Keep a track of high score
        /// </summary>
        protected int m_highScore = 0;

        /// <summary>
        /// Current worlds unlocked
        /// </summary>
        public int m_worldsUnlocked = 0;

        /// <summary>
        /// Music on
        /// </summary>
        protected bool m_music = false;

        /// <summary>
        /// Sound effects on
        /// </summary>
        protected bool m_soundEffects = false;

        /// <summary>
        /// List of scores
        /// </summary>
        protected Dictionary<int, int> m_scoreHistory = new Dictionary<int, int>();

        /// <summary>
        /// Powerups enabled
        /// </summary>
        protected List<PlayerPowerup> m_powerups = new List<PlayerPowerup>();

        /// <summary>
        /// Highest level we've reached
        /// </summary>
        protected int m_highLevel = 0;

        /// <summary>
        /// Which difficulty level are we on?
        /// </summary>
        protected int m_difficultyLevel = 1;

        /// <summary>
        /// Number of seconds shaking
        /// </summary>
        protected float m_secondsShaking = 0.0f;
    }

}