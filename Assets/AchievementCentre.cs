using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{

    /// <summary>
    /// Type and time of mote hit
    /// </summary>
    public class MoteHit
    {
        public MoteHit(System.Type type, float gameTime)
        {
            m_type = type;
            m_gameTime = gameTime;
        }

        /// <summary>
        /// Mote hit type
        /// </summary>
        /// <returns></returns>
        public System.Type getType() { return m_type; }

        /// <summary>
        /// Time of hit
        /// </summary>
        /// <returns></returns>
        public float getTime() { return m_gameTime; }

        /// <summary>
        /// Mote type
        /// </summary>
        System.Type m_type;

        /// <summary>
        /// Hit time
        /// </summary>
        protected float m_gameTime;

    }


    /// <summary>
    /// This class keeps a tab on achievements and unlocks/provides feedback on progress
    /// during gameplay.
    /// </summary>
    public class AchievementCentre    
    {
        public AchievementCentre()
        {
        }

        /// <summary>
        /// Return number of achievements
        /// </summary>
        /// <param name="achievementType"></param>
        /// <returns></returns>
        public int getNumberOfAchievements(System.Type achievementType)
        {
            int counter = 0;
            foreach (Achievement ach in m_achievements)
            {
                if (ach.GetType() == achievementType)
                    counter++;
            }

            return counter;
        }

        /// <summary>
        /// Register a hit with the achievement centre
        /// </summary>
        /// <param name="moteType"></param>
        /// <param name="time"></param>
        public void registerHit(Mote mote, float gameTime, int gameLevel)
        {
            // Store the mote type in the dictionary
            //
            m_moteHits.Add(new MoteHit(mote.GetType(), gameTime));

            // Calculate if any achievement has been created with last hit
            //
            calculateAchievement(gameTime, gameLevel);

            //Debug.Log("ADDED hit at time " + gameTime + ", total count = " + m_moteHits.Count);
        }

        /// <summary>
        /// Work out if something has been unlocked or awarded.  We need to call this method
        /// regularly from the update() script to ensure that mote hits are translated into
        /// achievements.
        /// </summary>
        protected void calculateAchievement(float gameTime, int gameLevel)
        {
            // Don't always process this
            //
            if (Time.time < m_lastAchievementCheckTime + m_calulateAchievementInterval)
                return;

            // Reset this immediately
            //
            m_lastAchievementCheckTime = Time.time;

            // First see if any hits are outside the period in which they score
            //
            List<MoteHit> safeList = new List<MoteHit>();

            // Scan for removal
            //
            foreach (MoteHit hit in m_moteHits)
            {
                if (hit.getTime() + m_scorePeriod > gameTime)
                    safeList.Add(hit);
            }

            //Debug.Log("m_moteHits size = " + m_moteHits.Count);
            //Debug.Log("safeList size = " + safeList.Count);

            // Reload list
            m_moteHits.Clear();
            m_moteHits.AddRange(safeList);

            // Do nothing if less than three
            //
            if (m_moteHits.Count < 3)
                return;

            int currentHit = m_moteHits.Count - 1;

            // Now start calculating achievements
            //
            if (m_moteHits.Count >= 5 && m_moteHits[currentHit].getTime() - m_moteHits[currentHit - 4].getTime() < m_runOfFive)
            {
                RunAchievement run5 = new RunAchievement(gameLevel, 5);
                m_nonViewedAchievements.Add(run5);
                m_moteHits.RemoveRange(currentHit - 4, 5);
            }
            else if (m_moteHits.Count >= 4 && m_moteHits[currentHit].getTime() - m_moteHits[currentHit - 3].getTime() < m_runOfFour)
            {
                RunAchievement run4 = new RunAchievement(gameLevel, 4);
                m_nonViewedAchievements.Add(run4);
                m_moteHits.RemoveRange(currentHit - 3, 4);
            }
            else if (m_moteHits.Count >= 3 && m_moteHits[currentHit].getTime() - m_moteHits[currentHit - 2].getTime() < m_runOfThree)
            {
                RunAchievement run3 = new RunAchievement(gameLevel, 3);
                m_nonViewedAchievements.Add(run3);
                m_moteHits.RemoveRange(currentHit - 2, 3);
            }
        }

        /// <summary>
        /// Clear it
        /// </summary>
        public void clearHitDictionary()
        {
            m_moteHits.Clear();
        }

        /// <summary>
        /// Non view list
        /// </summary>
        protected List<Achievement> m_nonViewedAchievements = new List<Achievement>();

        /// <summary>
        /// Build a duplicate list of non-viewed achievements, then append the list
        /// to the history, clear the non-viewed achievement list and return the
        /// original copy.
        /// 
        /// The act of pulling this list tidies up the achivement history
        /// </summary>
        /// <returns></returns>
        public List<Achievement> getNonViewedAchievements()
        {
            List<Achievement> rL = new List<Achievement>();
            rL.AddRange(m_nonViewedAchievements);

            foreach (Achievement achievement in m_nonViewedAchievements)
            {
                achievement.setViewed(true);
            }

            m_achievements.AddRange(m_nonViewedAchievements);
            m_nonViewedAchievements.Clear();

            return rL;
        }

        /// <summary>
        /// Get achievement string
        /// </summary>
        /// <returns></returns>
        public string getAchievementString()
        {
            string rS = "";
            foreach (Achievement ach in m_achievements)
            {
                rS += ach.getName() + ":" + ach.getGameLevel() + "\n";
            }

            return rS;
        }

        /// <summary>
        /// Get the levels associated the achievement list for storage in array SavedPreferences
        /// </summary>
        public int[] getAchievementLevelList()
        {
            int[] rI = new int[m_achievements.Count];
            int i =0;
            foreach (Achievement ach in m_achievements)
            {
                rI[i++] = ach.getGameLevel();
            }

            return rI;
        }

        /// <summary>
        /// Get the matching list of achievements using serialised strings
        /// </summary>
        /// <returns></returns>
        public string[] getAchievementList()
        {
            string[] rS = new string[m_achievements.Count];
            int i = 0;
            foreach (Achievement ach in m_achievements)
            {
                rS[i++] = ach.getSerialisedString();
            }

            return rS;
        }

        /// <summary>
        /// Load achievements
        /// </summary>
        /// <param name="levelList"></param>
        /// <param name="achievements"></param>
        public void loadAchievements(int[] levelList, string[] achievements)
        {
            m_achievements.Clear();

#if GENERATE_DEBUG
            Debug.Log("Loading " + levelList.Length + " achievements");
#endif
            for (int i = 0; i < levelList.Length; i++)
            {
                //Debug.Log("ACHIEVEMENT = " + achievements[i]);
                //Debug.Log("ACHIEVEMENT LENGTH = " + achievements[i].Length);
                if (achievements[i].Substring(0, 14) == "RunAchievement")
                {
                    int level = int.Parse(achievements[i].Substring(15, 1));
                    m_achievements.Add(new RunAchievement(levelList[i], level)); 
                }
            }
        }

        /// <summary>
        /// Dictionary of the latest hits
        /// </summary>
        protected List<MoteHit> m_moteHits = new List<MoteHit>();

        /// <summary>
        /// List of achievements gathered
        /// </summary>
        protected List<Achievement> m_achievements = new List<Achievement>();

        /// <summary>
        /// Drop dead period for any achievements - they can be cleared after this time
        /// </summary>
        protected float m_scorePeriod = 5.0f;

        /// <summary>
        /// Pause in between checking for achievements
        /// </summary>
        protected float m_calulateAchievementInterval = 0.3f;

        /// <summary>
        /// When did we last check for an achievement?
        /// </summary>
        protected float m_lastAchievementCheckTime = 0.0f;

        /// <summary>
        /// Run of 3 kills within this time
        /// </summary>
        protected float m_runOfThree = 0.6f;

        /// <summary>
        /// Run of four within this
        /// </summary>
        protected float m_runOfFour = 0.8f;

        /// <summary>
        /// Run of five within this
        /// </summary>
        protected float m_runOfFive = 1.0f;
    }
}