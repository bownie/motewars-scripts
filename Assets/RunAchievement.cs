using UnityEngine;
using System;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// An achievement in the game needs to subtype this class
    /// </summary>
    public class RunAchievement : Achievement
    {
        /// <summary>
        /// Achievement name
        /// </summary>
        /// <param name="name"></param>
        public RunAchievement(int gameLevel, int numberInRun) : base("RunAchievement", gameLevel, 300)
        {
            m_numberInRun = numberInRun;
        }

        /// <summary>
        /// Number in the run
        /// </summary>
        /// <returns></returns>
        public int getNumberInRun()
        {
            return m_numberInRun;
        }

        /// <summary>
        /// Serialised version of this achievement
        /// </summary>
        /// <returns></returns>
        public override string getSerialisedString()
        {
            return m_name + ":" + m_numberInRun;
        }

        protected int m_numberInRun = 0;
        
    }

}