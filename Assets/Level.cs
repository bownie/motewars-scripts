using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Identify the level of the scoring
    /// </summary>
    public enum LevelScoreIdentifier
    {
        None,
        Bronze,
        Silver,
        Gold
    };

    /// <summary>
    /// Level types
    /// </summary>
    public enum LevelType
    {
        MotesTimeLimit,  // destroy all motes in the level within a time limit
        MotesTapLimit,   // destroy all motes within a tap limit
        CellsTimeLimit,  // destroy cells on level within a time limit
        CellsTapLimit,   // destroy cells on level within a tap limit
        DestroyLimit,       // destroy a limited set of motes in the level
        ProtectArea,        // protect an area for a certain time limit
        ProtectTime,        // protected ship for a time limit
        ProtectKills        // protect ship for number of kills
    };

    /// <summary>
    /// A level in MoteWars.  The Level class is used both for templating levels and also for the live levels
    /// themselves.
    /// </summary>
    public class Level
    {

        /// <summary>
        /// Simple level constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="summary"></param>
        /// <param name="levelNumber"></param>
        /// <param name="levelSet"></param>
        /// <param name="gold"></param>
        /// <param name="silver"></param>
        /// <param name="bronze"></param>
        public Level(LevelType type, string name, string summary, int levelNumber, LevelSet levelSet, int gold, int silver, int bronze)
        {
            m_type = type;
            m_name = name;
            m_levelNumber = levelNumber;
            m_levelSet = levelSet;
            m_summary = summary;
            setScoreLevels(gold, silver, bronze);

            // Default time limit is 30 seconds
            //
            m_timeLimit = 30.0f;
        }

        /// <summary>
        /// Construct a Level with click limit
        /// </summary>
        /// <param name="name"></param>
        /// <param name="levelNumber"></param>
        /// <param name="levelSet"></param>
        public Level(LevelType type, string name, string summary, int levelNumber, LevelSet levelSet, int tapLimit, int gold, int silver, int bronze)
        {
            m_type = type;
            m_name = name;
            m_levelNumber = levelNumber;
            m_levelSet = levelSet;
            m_summary = summary;
            m_totalTaps = tapLimit;

            setScoreLevels(gold, silver, bronze);
        }

        /// <summary>
        /// Construct a level with a time limit
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="summary"></param>
        /// <param name="levelNumber"></param>
        /// <param name="levelSet"></param>
        /// <param name="timeLimit"></param>
        public Level(LevelType type, string name, string summary, int levelNumber, LevelSet levelSet, float timeLimit, int gold, int silver, int bronze)
        {
            m_type = type;
            m_name = name;
            m_levelNumber = levelNumber;
            m_levelSet = levelSet;
            m_summary = summary;
            m_timeLimit = timeLimit;

            setScoreLevels(gold, silver, bronze);
        }

        /// <summary>
        /// Construct a level with a click limit and time limit
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="summary"></param>
        /// <param name="levelNumber"></param>
        /// <param name="levelSet"></param>
        /// <param name="timeLimit"></param>
        /// <param name="clickLimit"></param>
        public Level(LevelType type, string name, string summary, int levelNumber, LevelSet levelSet, float timeLimit, int tapLimit, int gold, int silver, int bronze)
        {
            m_type = type;
            m_name = name;
            m_levelNumber = levelNumber;
            m_levelSet = levelSet;
            m_summary = summary;
            m_totalTaps = tapLimit;
            m_timeLimit = timeLimit;

            setScoreLevels(gold, silver, bronze);
        }

        /// <summary>
        /// Set score levels for qualification
        /// </summary>
        /// <param name="gold"></param>
        /// <param name="silver"></param>
        /// <param name="bronze"></param>
        public void setScoreLevels(int gold, int silver, int bronze)
        {
            setScoreLevel(LevelScoreIdentifier.Gold, gold);
            setScoreLevel(LevelScoreIdentifier.Silver, silver);
            setScoreLevel(LevelScoreIdentifier.Bronze, bronze);
        }

        /// <summary>
        /// Get medal awarded if any for a new score on this level
        /// </summary>
        /// <param name="lastScore"></param>
        /// <param name="newScore"></param>
        /// <returns></returns>
        public LevelScoreIdentifier getMedalAwarded(int lastScore, int newScore)
        {
            LevelScoreIdentifier lastMedal = getMedal(lastScore);
            LevelScoreIdentifier newMedal = getMedal(newScore);

            // Check to see if we have a new medal and return it if so
            //
            if (newMedal > lastMedal)
                return newMedal;

            // If we don't have a new medal - don't report anything
            //
            return LevelScoreIdentifier.None;
        }

        /// <summary>
        /// Get Medal for this level for a given score
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public LevelScoreIdentifier getMedal(int score)
        {
            if (score >= m_scoreLevels[LevelScoreIdentifier.Gold])
                return LevelScoreIdentifier.Gold;

            if (score >= m_scoreLevels[LevelScoreIdentifier.Silver])
                return LevelScoreIdentifier.Silver;

            if (score >= m_scoreLevels[LevelScoreIdentifier.Bronze])
                return LevelScoreIdentifier.Bronze;

            return LevelScoreIdentifier.None;
        }


        /// <summary>
        /// Score levels
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="score"></param>
        public void setScoreLevel(LevelScoreIdentifier identifier, int score)
        {
            m_scoreLevels[identifier] = score;
        }

        /// <summary>
        /// Number of motes required to complete level
        /// </summary>
        /// <returns></returns>
        public int motesRemaining()
        {
            if (m_generateLevel > 0)
                return m_generateLevel - m_motesDestroyed;

            return m_levelMotes.Count - m_motesDestroyed;
        }
        /// <summary>
        /// Get score level
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public int getScoreLevel(LevelScoreIdentifier identifier)
        {
            return m_scoreLevels[identifier];
        }

        /// <summary>
        /// Get the name of the level
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return m_name;
        }

        /// <summary>
        /// Get the level summary
        /// </summary>
        /// <returns></returns>
        public string getHint()
        {
            return m_summary;
        }

        /// <summary>
        /// Return the level number
        /// </summary>
        /// <returns></returns>
        public int getLevelNumber()
        {
            return m_levelNumber;
        }

        /// <summary>
        /// Add a mote to the list
        /// </summary>
        /// <param name="mote"></param>
        public void addMote(Mote mote)
        {
            m_levelMotes.Add(mote);
        }

        /// <summary>
        /// Get the Mote list
        /// </summary>
        /// <returns></returns>
        public List<Mote> getMotes()
        {
            return m_levelMotes;
        }

        /// <summary>
        /// Do Motes regenerate?
        /// </summary>
        /// <returns></returns>
        public bool regenerates()
        {
            return m_regenerate;
        }

        /// <summary>
        /// How many Motes do we populate on this level?  Take regeneration into account - if
        /// we're regenerating then ignore number of motes destroyed.
        /// </summary>
        /// <returns></returns>
        public int getGenerateLevel()
        {
            return (m_regenerate ? m_generateLevel : m_generateLevel - m_motesDestroyed);
        }

        /// <summary>
        /// Set generation limit for this level - number of alive motes at a time
        /// /// </summary>
        /// <param name="regenerate"></param>
        public void setGenerateLevel(int generate, bool applyDifficulty = true)
        {
            m_generateLevel = generate;

            // We add on more motes according to settings and difficulty
            //
            if (applyDifficulty)
            {
                // Now add a sprinkling more for large screens
                //
                if (Screen.width < 480)
                    m_generateLevel -= 8;
                else if (Screen.width < 640)
                    m_generateLevel -= 6;
                else if (Screen.width < 800)
                    m_generateLevel -= 3;
                else if (Screen.width < 1400)
                    m_generateLevel += 3;
                else if (Screen.width < 1600)
                    m_generateLevel += 6;
                else
                    m_generateLevel += 10;

                // Minimum generate level is 5
                //
                if (m_generateLevel < 5)
                    m_generateLevel = 5;
            }   
        }

        /// <summary>
        /// How long between generations of motes?
        /// </summary>
        /// <param name="time"></param>
        public void setGenerateSpacing(float time)
        {
            m_generateSpacing = time;
        }

        /// <summary>
        /// Spacing for good motes
        /// </summary>
        /// <param name="time"></param>
        public void setGenerateSpacingGood(float time)
        {
            m_generateSpacingGood = time;
        }

        /// <summary>
        /// Get the generate spacing
        /// </summary>
        /// <returns></returns>
        public float getGenerateSpacing()
        {
            return m_generateSpacing;
        }

        /// <summary>
        /// Get the generate spacing for good
        /// </summary>
        /// <returns></returns>
        public float getGenerateSpacingGood()
        {
            return m_generateSpacingGood;
        }

        /// <summary>
        /// Do motes regenerate?
        /// </summary>
        /// <param name="regenerate"></param>
        public void setRegenerate(bool regenerate)
        {
            m_regenerate = regenerate;
        }


        /// <summary>
        /// Has the time limit expired
        /// </summary>
        /// <returns></returns>
        public bool levelTimeLimitExpired()
        {
            return (Time.time - m_startTime > m_timeLimit);
        }

        /// <summary>
        /// Has this level been completed logically?  Conditions all satisfied?
        /// </summary>
        /// <returns></returns>
        public bool levelCompletedLogic(int lives)
        {
            // Always a fail
            //
            if (lives == 0)
            {
                Debug.Log("You're dead");
                return true;
            }

            // If we're on the boss level use level specific rules and not a generic mote rule
            if (m_bossMote != null)
            {
                return false;
            }


            switch (m_type)
            {
                    // Have we destroyed all the motes we have been asked to?
                    //
                case LevelType.MotesTimeLimit:
                    return (m_motesDestroyed >= m_motesToDestroy);

                    // Tap limit exceeded
                    //
                case LevelType.MotesTapLimit:
                    return (m_tapLimit - m_totalTaps <= 0);

                case LevelType.ProtectArea:
                    break;

                    // Protect the mote ship against a time limit or number of motes perhaps?
                case LevelType.ProtectTime:
                    return (Time.time - m_startTime > m_timeLimit);

                case LevelType.ProtectKills:
                    return (m_motesToDestroy <= m_motesDestroyed);

                default:
                    return false;
            }

            return false;
        }

        /// <summary>
        /// We can either set this externally or have the level auto-generate a message for us
        /// </summary>
        /// <param name="completionString"></param>
        /// <returns></returns>
        public void setCompletionMessage(string completionString)
        {
            m_congratsString = completionString;
        }

        /// <summary>
        /// Get a level completion message
        /// </summary>
        /// <returns></returns>
        public string getCompletionMessage(float completeTime)
        {
            if (m_congratsString != "")
                return m_congratsString;

            m_congratsString = getCongratulatory() + "!";
            
            // Add timing for non timed levels
            //
            /*
            if (m_type != LevelType.MotesTimeLimit && m_type != LevelType.ProtectTime && m_type != LevelType.CellsTimeLimit)
            {
                m_congratsString += " in " + (completeTime - m_startTime).ToString("0.00") + " seconds.";
            }
            else
            {
                m_congratsString += ".";
            }
            */

            /*
            switch (m_type)
            {
                case LevelType.ProtectKills:
                    m_congratsString += "\n" + getExecutionExpressive() + " " + m_motesDestroyed + " Motes";
                    break;

                default:
                    break;

            }*/

            return m_congratsString;
        }

        protected string[] m_congratulatories = { "Great", "Well done", "Congrats", "Congratulations", "Splendid", "Good show", "Nice one", "Lovin' it" };

        /// <summary>
        /// Get a congratulatory
        /// </summary>
        /// <returns></returns>
        public string getCongratulatory()
        {
            int myValue = (int)(Mathf.Min(Random.value * m_congratulatories.Length, m_congratulatories.Length - 1));
            return m_congratulatories[myValue];
        }

        protected string[] m_executionExpressives = { "Zapped", "Totalled", "Wrecked", "Rocked", "Exploded", "Destroyed", "Incinerated", "Wasted", "Borked", "Fragged", "Fracked", "Thwacked", "Twunked" };

        /// <summary>
        /// A method to return an expressive
        /// </summary>
        /// <returns></returns>
        public string getExecutionExpressive()
        {
            int myValue = (int)(Mathf.Min(Random.value * m_executionExpressives.Length, m_executionExpressives.Length - 1));
            return m_executionExpressives[myValue];
        }

        /// <summary>
        /// Have we failed the level yet?
        /// </summary>
        /// <returns></returns>
        public bool levelFailedLogic()
        {
            return false;
        }

        /// <summary>
        /// Check the clock start time - also we can include an offset if we're sending
        /// any message ahead of the level start time
        /// </summary>
        public void resetClock(float offset = 0.0f)
        {
            //if (!m_resetClock)
            //{
                //Debug.Log("Level::checkClockStart - resetting m_startTime to now");
                m_startTime = Time.time + offset;
                //m_resetClock = true;
            //}
        }

        /// <summary>
        /// When we die during a level we need to increment the start time to allow
        /// for regeneration.
        /// </summary>
        /// <param name="offset"></param>
        public void advanceStartTime(float offset)
        {
            m_startTime += offset;
        }

        /// <summary>
        /// Motes to destory in this level
        /// </summary>
        /// <returns></returns>
        public int getMotesToDestroy()
        {
            return m_motesToDestroy;
        }

        /// <summary>
        /// Motes to Destroy
        /// </summary>
        /// <returns></returns>
        public int getMotesYetToDestroy()
        {
            return m_motesToDestroy - m_motesDestroyed;
        }

        /// <summary>
        /// Motes to destory
        /// </summary>
        /// <param name="motesToDestroy"></param>
        public void setMotesToDestroy(int motesToDestroy, bool applyDifficulty = true)
        {
            m_motesToDestroy = motesToDestroy;

            // Add a mote per difficulty level we add on
            //
            if (applyDifficulty)
                m_motesToDestroy += (XygloPlayerPrefs.GetInt("DifficultyLevel", 1) - 1);
        }


        /// <summary>
        /// Get total number of alive motes of this type
        /// </summary>
        /// <param name="mote"></param>
        /// <returns></returns>
        public int getAliveMotes(Mote mote, List<Mote> moteList)
        {
            int rI = 0;
            foreach (Mote list in moteList)
            {
                if (list.GetType() == mote.GetType())
                    rI++;
            }

            return rI;
        }

        /// <summary>
        /// Get a new mote according to weighting and ensure that we don't overproduce according
        /// to mote "alive limit" and the live list of motes.
        /// </summary>
        /// <returns></returns>
        public Mote getRandomMote(List<Mote> liveMotes, bool onlyGood = false)
        {
            // First calculate the total mote weight
            //
            float totalWeight = 0;
            foreach (Mote mote in m_levelMotes)
            {
                if (!onlyGood || mote.isGoodMote())
				{
					//if (onlyGood)
					//{
						//Debug.Log("GOOD MOTE weight += " + mote.getWeight());
					//}
					
                    totalWeight += mote.getWeight();
				}
            }
			
			//if (onlyGood)
			//{
				//Debug.Log("TOTAL GOOD WEIGHT = " + totalWeight);
			//}
			
            // Get a random value
            //
            float pickAMote = Random.value;

            // Select a mote based on weighting
            //
            float sumProb = 0.0f;
            foreach (Mote mote in m_levelMotes)
            {
                if (onlyGood && !mote.isGoodMote()) // skip this mote for only good
                    continue;

                // Test for total alive motes limit not exceeded before delving into the
                // weighting problem.
                //
                if (mote.getAliveLimit() == 0 || getAliveMotes(mote, liveMotes) < mote.getAliveLimit())
                {
                    if ((sumProb + mote.getWeight() / totalWeight) > pickAMote)
                    {
                        return mote;
                    }
                }

                // Add on to probability sum
                //
                sumProb += (mote.getWeight() / totalWeight);
            }
			
			//Debug.Log("FAILED TO MATCH");
			
            return null;
        }

        /// <summary>
        /// Total number of motes destroyed
        /// </summary>
        /// <returns></returns>
        public int getMotesDestroyed() { return m_motesDestroyed; }

        /// <summary>
        /// Tracking motes destroyed
        /// </summary>
        public void incrementMotesDestroyed()
        {
            m_motesDestroyed++;
            //Debug.Log("MOTES DESTROYED = " + m_motesDestroyed);
        }

        /// <summary>
        /// Get the levelset associated with this level
        /// </summary>
        /// <returns></returns>
        public LevelSet getLevelSet()
        {
            return m_levelSet;
        }

        /// <summary>
        /// Clicks on this level
        /// </summary>
        /// <returns></returns>
        public int getClicksUsed() { return m_clicksUsed; }

        /// <summary>
        /// Increment number of clicks on this level
        /// </summary>
        public void incrementClicks() { m_clicksUsed++; }

        /// <summary>
        /// Set total clicks allowed for this level
        /// </summary>
        /// <param name="clicks"></param>
        public void setTotalTaps(int clicks) { m_totalTaps = clicks; }

        /// <summary>
        /// Total clicks allowed in this level
        /// </summary>
        /// <returns></returns>
        public int returnTotalTaps() { return m_totalTaps; }

        /// <summary>
        /// Get the level type
        /// </summary>
        /// <returns></returns>
        public LevelType getType() { return m_type; }

        /// <summary>
        /// Set the time limit
        /// </summary>
        /// <param name="timeLimit"></param>
        public void setTimeLimit(float timeLimit)
        {
            m_timeLimit = timeLimit;
        }

        /// <summary>
        /// Time limit for level - 0 is unlimited
        /// </summary>
        /// <returns></returns>
        public float getTimeLimit() { return m_timeLimit; }

        /// <summary>
        /// Tap limit for level - 0 is unlimited
        /// </summary>
        /// <returns></returns>
        public float getTapLimit() { return m_tapLimit; }

        /// <summary>
        /// Remaining time in level
        /// </summary>
        /// <returns></returns>
        public float getRemainTime()
		{
			if (m_isPaused)
				return m_pauseTime;
			else
				return m_timeLimit - Time.time + m_startTime;
		}
		
		/// <summary>
		/// Sets the game paused.  Store the time at which we have paused.
		/// </summary>
		public void setGamePaused()
		{
			m_isPaused =  true;
			m_pauseTime = m_timeLimit - Time.time + m_startTime;
		}
		
		/// <summary>
		/// Is the game paused?
		/// </summary>
		/// <returns>
		/// The game paused.
		/// </returns>
		public bool isGamePaused()
		{
			return m_isPaused;
		}
		
		/// <summary>
		/// Sets the game running.
		/// </summary>
		public void setGameRunning()
		{
			m_isPaused = false;
			
			// Now update the start time by the difference between the time we paused and
			// currently reported remaining time.
			//
			m_startTime += m_pauseTime - (m_timeLimit - Time.time + m_startTime);
		}

        /// <summary>
        /// Remaining taps in level
        /// </summary>
        /// <returns></returns>
        public float getRemainTaps() { return m_tapLimit - m_totalTaps; }

        /// <summary>
        /// Has the clock been reset - meaning this level has been started at some point
        /// </summary>
        /// <returns></returns>
        public bool clockBeenReset()
        {
            return m_resetClock;
        }

        /// <summary>
        /// Is this a boss level?
        /// </summary>
        /// <returns></returns>
        public bool isBoss()
        {
            return (m_bossMote != null);
        }

        /// <summary>
        /// Get the boss mote type
        /// </summary>
        /// <returns></returns>
        public Mote getBoss()
        {
            return m_bossMote;
        }


        /// <summary>
        /// Set the boss level
        /// </summary>
        /// <param name="bossLevel"></param>
        public void setBoss(Mote bossMote)
        {
            m_bossMote = bossMote;
        }

        /// <summary>
        /// Has the boss been generated already?
        /// </summary>
        /// <returns></returns>
        public bool hasBossBeenGenerated()
        {
            return m_bossGenerated;
        }

        /// <summary>
        /// Set the boss generation flag
        /// </summary>
        /// <param name="bossGenerated"></param>
        public void setBossGenerated(bool bossGenerated)
        {
            m_bossGenerated = bossGenerated;
        }

        /// <summary>
        /// Time limit for level - 0 is unlimited
        /// </summary>
        protected float m_timeLimit = 0;

        /// <summary>
        /// Tap limit for level - 0 is unlimited
        /// </summary>
        protected float m_tapLimit = 0;

        /// <summary>
        /// Start time for this level
        /// </summary>
        protected float m_startTime = 0;

        /// <summary>
        /// Name of the level
        /// </summary>
        protected string m_name;

        /// <summary>
        /// Summary of this level
        /// </summary>
        protected string m_summary;

        /// <summary>
        /// ID of this level
        /// </summary>
        protected int m_levelNumber;

        /// <summary>
        /// LevelSet we're in
        /// </summary>
        protected LevelSet m_levelSet;

        /// <summary>
        /// If this is set then motes generate up to this level
        /// </summary>
        protected int m_generateLevel = 0;

        /// <summary>
        /// Time between generations - always have a small value here to make things a bit playable
        /// </summary>
        protected float m_generateSpacing = 0.02f;

        /// <summary>
        /// Generate Spacing Good
        /// </summary>
        protected float m_generateSpacingGood = 2.0f;

        /// <summary>
        /// Do Motes regenerate?  Default is true
        /// </summary>
        protected bool m_regenerate = true;

        /// <summary>
        /// Keep a track of motes destroyed in this level
        /// </summary>
        protected int m_motesDestroyed = 0;

        /// <summary>
        /// Keep a track of clicks on this level
        /// </summary>
        protected int m_clicksUsed = 0;

        /// <summary>
        /// Motes in our level
        /// </summary>
        protected List<Mote> m_levelMotes = new List<Mote>();

        /// <summary>
        /// Time to complete this level : -1 for infinite
        /// </summary>
        protected float m_timeToComplete = -1;

        /// <summary>
        /// Total number of taps allowed for this level
        /// </summary>
        protected int m_totalTaps = 10;

        /// <summary>
        /// How many motes to complete the level?
        /// </summary>
        protected int m_levelCompletionMotes = 0;

        /// <summary>
        /// Keep a dictionary of score levels
        /// </summary>
        protected Dictionary<LevelScoreIdentifier, int> m_scoreLevels = new Dictionary<LevelScoreIdentifier, int>();

        /// <summary>
        /// Get level type
        /// </summary>
        protected LevelType m_type;

        /// <summary>
        /// Lives for this level - don't use this for the moment
        /// </summary>
        //protected int m_lives;

        /// <summary>
        /// Total motes to destory for this level
        /// </summary>
        protected int m_motesToDestroy = 0;

        /// <summary>
        /// Level completion congratulatory string
        /// </summary>
        protected string m_congratsString = "";

        /// <summary>
        /// Has the clock been reset on this level yet?
        /// </summary>
        protected bool m_resetClock = false;

        /// <summary>
        /// Is this a boss level?  If so just set the boss type here
        /// </summary>
        protected Mote m_bossMote = null;

        /// <summary>
        /// Has the boss been generated for this level?
        /// </summary>
        protected bool m_bossGenerated = false;
		
		// Is the game paused?
		protected bool m_isPaused = false;
		
		// Time at which we paused
		protected float m_pauseTime;
    }

}