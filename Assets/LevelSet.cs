using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    public class LevelSet
    {
        /// <summary>
        /// Name constructor
        /// </summary>
        /// <param name="name"></param>
        public LevelSet(string name, int levelSetNumber)
        {
            m_name = name;
            m_levelSetNumber = levelSetNumber;
        }

        /// <summary>
        /// Create a new level with click limit
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Level addLevelClickLimit(string name, string summary, int clicks, int gold, int silver, int bronze)
        {
            //Debug.Log("LevelSet::addLevel - adding level " + newLevel.getName() + " with id " + newLevel.getLevelNumber());
            Level newLevel = new Level(LevelType.MotesTapLimit, name, summary, LevelSet.m_levelNumber++, this, clicks, gold, silver, bronze);
            m_levels.Add(newLevel);
            return m_levels[m_levels.Count - 1];
        }

        /// <summary>
        /// Add level with time limit
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="summary"></param>
        /// <param name="timeLimit"></param>
        /// <returns></returns>
        public Level addLevelTimeLimit(string name, string summary, float timeLimit,int gold, int silver, int bronze)
        {
            Level newLevel = new Level(LevelType.ProtectTime, name, summary, LevelSet.m_levelNumber++, this, timeLimit, gold, silver, bronze);
            m_levels.Add(newLevel);
            return m_levels[m_levels.Count - 1];
        }


        /// <summary>
        /// Add a level to the LevelSet
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="summary"></param>
        /// <param name="gold"></param>
        /// <param name="silver"></param>
        /// <param name="bronze"></param>
        /// <returns></returns>
        public Level addLevel(LevelType type, string name, string summary, int gold, int silver, int bronze)
        {
            Level newLevel = new Level(type, name, summary, LevelSet.m_levelNumber++, this, gold, silver, bronze);
            m_levels.Add(newLevel);
            return m_levels[m_levels.Count - 1];
           
        }

        /// <summary>
        /// Count of levels
        /// </summary>
        /// <returns></returns>
        public int getLevelCount()
        {
            return m_levels.Count;
        }

        /// <summary>
        /// Count all non boss levels
        /// </summary>
        /// <returns></returns>
        public int getLevelCountNonBoss()
        {
            int i = 0;

            foreach (Level level in m_levels)
            {
                if (!level.isBoss())
                    i++;
            }

            return i;
        }

        /// <summary>
        /// Get a level
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Level getLevel(int count)
        {
            return m_levels[count];
        }

        /// <summary>
        /// Get level set number
        /// </summary>
        /// <returns></returns>
        public int getLevelSetNumber()
        {
            return m_levelSetNumber;
        }

        /// <summary>
        /// Return all the levels
        /// </summary>
        /// <returns></returns>
        public List<Level> getLevels()
        {
            return m_levels;
        }

        /// <summary>
        /// LevelSet name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return m_name;
        }

        /// <summary>
        /// Set introduction
        /// </summary>
        /// <param name="text"></param>
        public void setIntroductionText(string text)
        {
            m_introductionText = text;
        }

        /// <summary>
        /// Get introduction
        /// </summary>
        /// <returns></returns>
        public string getIntroductionText()
        {
            return m_introductionText;
        }
        /// <summary>
        /// Get the maximum level number
        /// </summary>
        /// <returns></returns>
        static public int getMaxLevelNumber()
        {
            return LevelSet.m_levelNumber;
        }


        /// <summary>
        /// LevelSet name
        /// </summary>
        protected string m_name;

        /// <summary>
        /// List of levels
        /// </summary>
        protected List<Level> m_levels = new List<Level>();

        /// <summary>
        /// Level numbers are incremented when new levels are added via LevelSets
        /// </summary>
        static public int m_levelNumber = 0;

        /// <summary>
        /// LevelSet number
        /// </summary>
        protected int m_levelSetNumber = 0;

        /// <summary>
        /// Introduction to this level
        /// </summary>
        protected string m_introductionText;
    }

}