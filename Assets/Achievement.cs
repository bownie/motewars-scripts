using UnityEngine;
using System;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// An achievement in the game needs to subtype this class
    /// </summary>
    public abstract class Achievement
    {
        /// <summary>
        /// Achievement name
        /// </summary>
        /// <param name="name"></param>
        public Achievement(string name, int level, int bonus)
        {
            m_name = name;
            m_time = DateTime.Now;
            m_gameLevel = level;
            m_bonus = bonus;
        }

        /// <summary>
        /// Points bonus for getting this achievement
        /// </summary>
        /// <returns></returns>
        public int getBonus()
        {
            return m_bonus;
        }

        /// <summary>
        /// Get a flattened version of this achievement
        /// </summary>
        /// <returns></returns>
        public abstract string getSerialisedString();

        /// <summary>
        /// Get name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return m_name;
        }

        /// <summary>
        /// Get game level
        /// </summary>
        /// <returns></returns>
        public int getGameLevel()
        {
            return m_gameLevel;
        }

        /// <summary>
        /// Get the creation time
        /// </summary>
        /// <returns></returns>
        public DateTime getDateTime()
        {
            return m_time;
        }

        /// <summary>
        /// Set the achievement view status
        /// </summary>
        /// <param name="viewed"></param>
        public void setViewed(bool viewed)
        {
            m_viewed = viewed;
        }

        /// <summary>
        /// Has been viewed?
        /// </summary>
        /// <returns></returns>
        public bool isViewed()
        {
            return m_viewed;
        }

        /// <summary>
        /// Achievement name
        /// </summary>
        protected string m_name;

        /// <summary>
        /// Time of achievement
        /// </summary>
        protected DateTime m_time;

        /// <summary>
        /// Bonus score for this achievement
        /// </summary>
        protected int m_bonus = 0;

        /// <summary>
        /// Level this achievement made earned on
        /// </summary>
        protected int m_gameLevel = 0;

        /// <summary>
        /// Has this achievement been shown to the player yet?
        /// </summary>
        protected bool m_viewed = false;
    }

}