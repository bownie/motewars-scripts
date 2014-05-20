using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Manages the levels for our game
    /// </summary>
    public class LevelManager
    {
        public LevelManager()
        {
            Debug.Log("LevelManager - constructing");
            initialise();
        }

        /// <summary>
        /// Return game level we're currently at
        /// </summary>
        /// <returns></returns>
        public int getGameLevelNumber()
        {
            return m_gameLevel;
        }


        /// <summary>
        /// Last available level in game
        /// </summary>
        /// <returns></returns>
        public int getLastLevelAvailable()
        {
            int counter = 0;

            for (int i = 0; i < m_levelSets.Count; i++)
            {
                for (int j = 0; j < m_levelSets[i].getLevelCount(); j++)
                {
                    counter++;
                }
            }

            return counter - 1;
        }


        /// <summary>
        /// Set next level
        /// </summary>
        /// <returns></returns>
        public void nextLevel()
        {
            m_gameLevel++;
        }

        /// <summary>
        /// Set the game level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public void setLevel(int level)
        {
            m_gameLevel = level;
        }

        /// <summary>
        /// Get all the LevelSets
        /// </summary>
        /// <returns></returns>
        public List<LevelSet> getLevelSets()
        {
            return m_levelSets;
        }

        /// <summary>
        /// Get a world number (zero based) for a world level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int getWorldNumberForLevel(int level)
        {
            int levelCounter = 0;

            for (int j = 0; j < m_levelSets.Count; j++)
            {
                for (int i = 0; i < m_levelSets[j].getLevelCount(); i++)
                {
                    if (level == levelCounter)
                        return j;
                    levelCounter++;
                }
            }

            return -1;
        }


        /// <summary>
        /// Return a level number for the world we want to start in
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public Level getLevelForWorld(int world)
        {
            Debug.Log("LevelManager::getLevelForWorld() - levelSets = " + m_levelSets.Count + ", world = " + world);

            // World number is 1 based - we're looking in a 0 based list
            //
            if (m_levelSets.Count > (world))
            {
                return m_levelSets[world].getLevel(0);
            }
            return null;
        }

        /// <summary>
        /// Get the Level we're currently playing - include or exclude boss levels as you need
        /// </summary>
        /// <returns></returns>
        public Level getGameLevel(bool includeBoss = false, int level = -1)
        {
            if (level == -1)
                level = m_gameLevel;

#if GETGAMELEVEL_DEBUG
            Debug.Log("Getting game level from " + m_levelSets.Count + " LevelSets with id " + level);
#endif

            foreach (LevelSet levelSet in m_levelSets)
            {
#if GETGAMELEVEL_DEBUG
                Debug.Log("LevelSet has " + levelSet.getLevelCount() + " levels");
#endif
                for (int i = 0; i < (includeBoss ? levelSet.getLevelCountNonBoss() : levelSet.getLevelCount()); i++)
                {
#if GETGAMELEVEL_DEBUG
                    Debug.Log("Checking level " + levelSet.getLevel(i).getLevelNumber() + " = " + m_gameLevel);
#endif

                    if (levelSet.getLevel(i).getLevelNumber() == level)
                        return levelSet.getLevel(i);
                }
            }

#if GETGAMELEVEL_DEBUG
            Debug.Log("Failed to find level");
#endif
            return null;
        }

        /// <summary>
        /// Get LevelSet for level number
        /// </summary>
        /// <returns></returns>
        public LevelSet getLevelSet(int level = -1)
        {
            if (level == -1)
                level = m_gameLevel;
            foreach (LevelSet levelSet in m_levelSets)
            {
#if GETGAMELEVEL_DEBUG
                Debug.Log("LevelSet has " + levelSet.getLevelCount() + " levels");
#endif
                for (int i = 0; i < levelSet.getLevelCount(); i++)
                {
#if GETGAMELEVEL_DEBUG
                    Debug.Log("Checking level " + levelSet.getLevel(i).getLevelNumber() + " = " + m_gameLevel);
#endif

                    if (levelSet.getLevel(i).getLevelNumber() == level)
                        return levelSet;
                }
            }

            return null;
        }


        /// <summary>
        /// Get a level for a given index number
        /// </summary>
        /// <param name="levelNumber"></param>
        /// <returns></returns>
        public Level getLevel(int levelNumber)
        {
            int counter = 0;
            foreach (LevelSet levelSet in m_levelSets)
            {
                for (int i = 0; i < levelSet.getLevelCount(); i++)
                {
                    if (levelNumber == counter)
                    {
                        return levelSet.getLevel(i);
                    }
                    counter++;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a the number of the first level in a given world
        /// </summary>
        /// <returns></returns>
        public int getFirstLevelNumberForWorld(int world)
        {
            int rL = 0;
            for (int i = 0; i < world; i++)
            {
                rL += m_levelSets[i].getLevelCount();
            }

            return rL;
        }

        /// <summary>
        /// Return the nth level in the game ignoring worlds
        /// </summary>
        /// <param name="levelNUmber"></param>
        /// <returns></returns>
        public Level getLevelForWorldNumber(int levelNumber)
        {
            int counter = 0;

            for (int i = 0; i < m_levelSets.Count; i++)
            {
                for(int j = 0; j < m_levelSets[i].getLevelCount(); j++)
                {
                    if (counter++ == levelNumber)
                    {
                        return m_levelSets[i].getLevel(j);
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Set up the game levels
        /// </summary>
        protected void initialise()
        {
#if GENERATE_DEBUG
            Debug.Log("Initialising LevelManager");
#endif
            // Just comment out the LevelSet - not the levels
            //

            // These are the Lite Levels
            //
            generateMeadow();     // WORLD 1
            generateSpaceCity();  // WORLD 2
			generateLEO();        // WORLD 3

            // These are the full version levels
            //
            generateSaturn();    // WORLD 4
            generateWormhole();  // WORLD 5
            generateMoteWorld(); // WORLD 6
			
			// Add the rest so that the level add stuff works but these are empty levels
            //
            generateForest();
            generateCityGate();
            generateHinterland();
            generateSpaceElevator();
        }


        /// <summary>
        /// ----------------------- WORLD 1 ----------------------------
        /// 
        /// This is the Meadow
        /// </summary>
        protected void generateMeadow()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating the Meadow...");
#endif
            // Welcome level set
            //
            LevelSet welcomeSet = addLevelSet("Free at last!");
            welcomeSet.setIntroductionText("Find your way home.");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = welcomeSet.addLevel(LevelType.ProtectKills, "Protect the Mote Ship", "Pop the fluffies!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new FluffyMote(1));
            level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);

            // Two
            //
            Level newLevel2 = welcomeSet.addLevel(LevelType.ProtectKills, "Seed survival", "Deflect them!", 1000, 500, 250);
            newLevel2.addMote(new SycamoreMote(1.0f, 2.0f));
            newLevel2.setMotesToDestroy(15);
            newLevel2.setGenerateSpacing(0.5f); // half a second between generates
            newLevel2.setGenerateLevel(5);

            // Three
            //
            Level newLevel3 = welcomeSet.addLevel(LevelType.ProtectTime, "More fluffies", "Survive for 30 seconds", 2000, 400, 100);
            newLevel3.addMote(new FluffyMote(1.0f));
            newLevel3.addMote(new SporeMote(0.03f));
            newLevel3.setTimeLimit(30);
            newLevel3.setMotesToDestroy(22);
            newLevel3.setGenerateLevel(20);

            // Four
            //
            Level newLevel4 = welcomeSet.addLevel(LevelType.ProtectKills, "Spores", "A surprise inside..", 2000, 400, 100);
            newLevel4.addMote(new SporeMote(1.0f));
            newLevel4.addMote(new CellMote(0.08f, CellPrize.Shield, CellSide.Auto, true));
            newLevel4.setGenerateSpacing(0.1f);
            newLevel4.setMotesToDestroy(20);
            newLevel4.setGenerateLevel(8);

            // Five
            //
            Level newLevel5 = welcomeSet.addLevel(LevelType.ProtectKills, "What the?", "..wibbly..", 2000, 400, 100);
            newLevel5.addMote(new AcornMote(1.0f));
            newLevel5.setGenerateSpacing(0.5f);
            newLevel5.setMotesToDestroy(10);
            newLevel5.setGenerateLevel(5);

            // Six
            //
            Level newLevel6 = welcomeSet.addLevel(LevelType.ProtectKills, "Getting faster", "Keep the faith", 2000, 400, 100);
            newLevel6.addMote(new SycamoreMote(0.3f, 3.0f)); // faster faster sycamore
            newLevel6.setMotesToDestroy(20);
            newLevel6.setGenerateLevel(5);

            // Seven
            //
            Level newLevel7 = welcomeSet.addLevel(LevelType.ProtectKills, "Mosquitos!", "Dodge 'em", 2000, 400, 100);
            newLevel7.addMote(new MosquitoMote(1.0f));
            newLevel7.addMote(new FluffyMote(0.05f));
            newLevel7.setMotesToDestroy(10);
            newLevel7.setGenerateLevel(15);

            // Eight
            //
            Level newLevel8 = welcomeSet.addLevel(LevelType.MotesTimeLimit, "35 in 30", "Be quick", 2000, 400, 100);
            newLevel8.addMote(new FluffyMote(1));
            newLevel8.setMotesToDestroy(35);
            newLevel8.setGenerateLevel(10);

            // Nine
            //
            Level newLevel9 = welcomeSet.addLevel(LevelType.ProtectKills, "BIG FUN", "Flex your fingers!", 2000, 400, 100);
            newLevel9.addMote(new SycamoreMote(0.3f));
            newLevel9.addMote(new AcornMote(0.3f));
            newLevel9.addMote(new SporeMote(0.3f));
            newLevel9.addMote(new CellMote(0.01f, CellPrize.Shield, CellSide.Auto));
            newLevel9.setGenerateSpacing(0.1f);
            newLevel9.setMotesToDestroy(25);
            newLevel9.setGenerateLevel(10);

            // Ten
            //
            Level newLevel10 = welcomeSet.addLevel(LevelType.ProtectTime, "Take cover!", "45 seconds..", 2000, 400, 100);

            if (WorldScene.m_debugLevels)
                newLevel10.setTimeLimit(1);
            else
                newLevel10.setTimeLimit(30);

            newLevel10.addMote(new FluffyMote(1.0f));
            newLevel10.addMote(new MosquitoMote(0.5f));
            newLevel10.addMote(new AcornMote(0.1f));
            newLevel10.addMote(new SporeMote(0.05f));
            //newLevel10.setMotesToDestroy(30);
            newLevel10.setGenerateLevel(15);

            // Boss
            //
            Level bossLevel =  welcomeSet.addLevel(LevelType.ProtectKills, "Megasheep  must die", "HINT: Tap to ARM", 5000, 4000, 3000);
            bossLevel.setBoss(new BossMegaSheep());
            bossLevel.addMote(new GlueFluffyMote(1));
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);
        }

        /// <summary>
        /// ----------------------- WORLD 2 ----------------------------
        /// 
        /// Generate Space City
        /// </summary>
        protected void generateSpaceCity()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating the City Gate...");
#endif
            float balloonAppear = 0.6f;

            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("You're at Space City");
            levelSet.setIntroductionText("A carnival of a town.");

            // First level - squishmotes make an appearances
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Circus time?", "For real?", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new BalloonMote(BalloonColour.Green, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Yellow, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Pink, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Red, balloonAppear));
            level.addMote(new SquishMote(1));
            level.addMote(new GenericMote(0.8f));
            level.setGenerateSpacing(0.1f);
            level.setCompletionMessage("Great work!\nWhat next?");
            level.setGenerateLevel(5);
            level.setRegenerate(true);


            balloonAppear = 0.1f;

            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Stay alive", "20 seconds", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new FluffyMote(1.0f));
            level.addMote(new SquishMote(0.5f));
            level.addMote(new BalloonMote(BalloonColour.Green, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Yellow, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Pink, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Red, balloonAppear));
            level.addMote(new GenericMote(0.8f));
            level.addMote(new SporeMote(0.05f));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Slithery fun", "Look out...", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SnakeMote(0.2f));
            level.addMote(new GenericMote(0.7f));
            level.addMote(new BalloonMote(BalloonColour.Green, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Yellow, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Pink, balloonAppear));
            level.addMote(new BalloonMote(BalloonColour.Red, balloonAppear));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "This is dirty", "Animals love you..", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new MosquitoMote(1));
            level.addMote(new ButterflyMote(1, 100, true));
            level.addMote(new GenericMote(0.7f));
            level.setGenerateLevel(15);
            level.setGenerateSpacing(0.3f);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "More spores!", "Be quick", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Fluffies are back", "Pop them!", 1000, 850, 550);
            level.setMotesToDestroy(50);
            level.addMote(new FluffyMote(1));
            level.setTimeLimit(40);
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Snake charmer", "Do your thing", 1000, 850, 550);
            level.addMote(new SnakeMote(0.2f));
            level.addMote(new FluffyMote(0.5f));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Woodland things", "HOLD the Pine Cones", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new MushroomMote(1.0f));
            level.addMote(new PineConeMote(0.02f));
			level.setGenerateSpacing(0.8f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Forest floor", "Keep popping!", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(1));
            level.addMote(new PineConeMote(0.08f));
            level.setGenerateSpacing(1.0f);
			level.setGenerateLevel(30);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Must be there..", "Head down!", 1000, 850, 550);
            level.setMotesToDestroy(30);
            level.setGenerateSpacing(0.3f);
            //level.setTimeLimit(1);
            level.addMote(new SporeMote(0.05f));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(16);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Uh oh", "What now?", 5000, 4000, 3000);
            bossLevel.setBoss(new BossBadRobot());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new SporeMote(1.0f, MoteStartSide.Right));
            //bossLevel.addMote(new AcornMote(0.1f, MoteStartSide.Right));
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);
        }

		/// <summary>
        /// ----------------------- WORLD 3 ----------------------------
        /// 
		/// Generates the LEO. 
		/// </summary>
        protected void generateLEO()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating LEO...");
#endif
            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("Low. Earth. Orbit.");
            levelSet.setIntroductionText("floating in space");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Improbable?", "Bling nasty", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new HeartOfGoldMote(0.8f));
            level.addMote(new PineConeMote(0.1f));
            level.addMote(new CellMote(0.08f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.8f);
            //level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);
			
            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Blast from Past", "Pingtastic!", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new SputnikMote(0.2f));
            level.addMote(new CellMote(0.08f, CellPrize.Grenade, CellSide.Auto));
            //level.addMote(new PineConeMote(0.5f));
            //level.addMote(new AcornMote(0.1f));
            //level.addMote(new SporeMote(0.05f));
            level.setGenerateSpacing(0.5f);
            level.setGenerateLevel(15);
            level.setRegenerate(true);


            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "ISS no mess", "Whatever next?", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new ISSMote(1.0f));
            //level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4 - ICBMs plus shields and grenade
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "It's war", "Someone doesn't like you", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new ICBMMote(1.0f));
            level.addMote(new CellMote(0.1f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.05f, CellPrize.Grenade, CellSide.Auto));
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Schools out", "Going experimental!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new LegoBalloonMote(1));
            level.addMote(new ISSMote(0.1f));
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Aurora-some", "Beautiful but deadly", 1000, 850, 550);
            level.setMotesToDestroy(30);
            level.addMote(new AuroraMote(1));
            level.addMote(new ISSMote(0.04f));
            level.addMote(new SputnikMote(0.005f));
            level.addMote(new CellMote(0.02f, CellPrize.Grenade, CellSide.Auto, true));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Supply limited", "Avoid these guys..", 1000, 850, 550);
            level.addMote(new DragonMote(1));
            level.addMote(new CellMote(0.08f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.02f, CellPrize.Grenade, CellSide.Auto, true));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Do some docking", "How difficult?", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new ISSMote(1));
            level.addMote(new DragonMote(1));
            level.addMote(new ICBMMote(0.01f));
            level.addMote(new CellMote(0.08f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Getting tasty", "Just keep popping!", 1000, 850, 550);
            level.setMotesToDestroy(30);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(1));
            level.addMote(new PineConeMote(1));
            level.setGenerateLevel(30);
            level.setRegenerate(true);

            // Level 10 - madness with some respite
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Madness!", "Riptastic", 1000, 850, 550);

            if (WorldScene.m_debugLevels)
                level.setMotesToDestroy(1);
            else
                level.setMotesToDestroy(35);

            //level.setTimeLimit(1);
            level.addMote(new SputnikMote(1));
            level.addMote(new ICBMMote(1));
            level.addMote(new ISSMote(0.5f));
            level.addMote(new DragonMote(0.3f));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.001f, CellPrize.Grenade, CellSide.Auto));
            level.setGenerateLevel(18);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Project Orion", "gone fission", 5000, 4000, 3000);
            bossLevel.setBoss(new BossMegaWolf());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new SporeMote(1.0f, MoteStartSide.Right));
			//bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(1.2f); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);

        }


        /// <summary>
        /// ----------------------- WORLD 4 ----------------------------
        ///  
        /// Generates Saturn level - note that asteroids are used for Fluffies in this level - WORLD 4
        /// </summary>
        protected void generateSaturn()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating Saturn...");
#endif

            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("Saturn Attacks!");
            levelSet.setIntroductionText("Fingers Ready");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Rings!", "Lord them", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new FluffyMote(1));
			level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto, true));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(22);
            level.setRegenerate(true);

            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Contact", "Shoot the ships", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new SycamoreMote(0.4f));
            level.addMote(new FluffyMote(1.0f));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Eye Eye", "Fuzzy", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new MushroomMote(1));   // Mushroom are Gluons
            level.addMote(new CometMote(0.2f));
            level.addMote(new FluffyMote(0.1f));
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(17);
            level.setRegenerate(true);
			
            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Sergeant Pinback?", "You there?", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new MushroomMote(0.4f));   // Mushroom are Gluons
            level.addMote(new DarkStarAlienMote(0.8f));
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(10);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "More spores!", "Be quick", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(0.01f));
            level.addMote(new MushroomMote(1));  // Mushrooms are gluons
			level.addMote(new AcornMote(0.08f)); // Acorns are gluons 
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
			level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(12);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Heroes", "We don't need them", 1000, 850, 550);
            level.setMotesToDestroy(50);
            level.addMote(new TinaTurnerMote(1));
			level.addMote(new AcornMote(0.1f));
            level.addMote(new SporeMote(0.01f));
			level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
            level.setTimeLimit(40);
            level.setGenerateLevel(13);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Angry dude", "Drip", 1000, 850, 550);
            //level.setMotesToDestroy(15);
			level.addMote(new FluffyMote(1.0f));
			level.addMote(new MushroomMote(0.3f));
            level.addMote(new MoteBossMote(0.3f));
            level.addMote(new SporeMote(0.01f));
			level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
			level.setTimeLimit(40);
            level.setGenerateLevel(13);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Confusion", "Like a new order", 1000, 850, 550);
			level.addMote(new FluffyMote(1.0f));
			level.addMote(new MushroomMote(0.3f));
            level.addMote(new MoteBossMote(0.3f));
            level.addMote(new SporeMote(0.01f));
            level.setMotesToDestroy(35);
            level.addMote(new CometMote(1));
            level.addMote(new SnakeMote(1));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
			level.setGenerateLevel(12);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "What the..", "Keep popping!", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new CometMote(1));
            level.addMote(new SnakeMote(1));
			level.addMote(new MoteBossMote(1));
			level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Eclectic", "Fixation", 1000, 850, 550);
            if (WorldScene.m_debugLevels)
                level.setMotesToDestroy(1);
            else
                level.setMotesToDestroy(35);
			
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(30);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Ant Attack", "Arck!", 5000, 4000, 3000);
            bossLevel.setBoss(new BossBadRobot());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);
        }

        /// <summary>
        /// Wormhole - WORLD 5
        /// </summary>
        protected void generateWormhole()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating Wormhole...");
#endif
            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("The way home?");
            levelSet.setIntroductionText("let's hope so");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Far-scape", "Going to frell", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new BalloonMote(BalloonColour.Red, 1)); // Leviathan
			level.addMote(new AcornMote(0.2f)); // Alien spider
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Airing Sun", "It's shining", 1000, 850, 550);
            level.setTimeLimit(20);
            //level.addMote(new CometMote(0.2f));
            level.addMote(new BalloonMote(BalloonColour.Red, 1));
            level.addMote(new SporeMote(0.1f));
            level.setGenerateSpacing(1);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Scorchius", "Hey lumpy!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SnakeMote(1));
            level.addMote(new SquishMote(1));
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Dragon", "Trek", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new BalloonMote(BalloonColour.Yellow, 1));  // Dragon
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto, true));
            level.addMote(new AcornMote(0.2f)); // Alien spider
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Leviathan", "and on", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(1));
            level.addMote(new BalloonMote(BalloonColour.Red, 1)); // Leviathan
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(16);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Moody", "Blues", 1000, 850, 550);
            level.setMotesToDestroy(60);
            level.addMote(new SporeMote(1));
            level.addMote(new BalloonMote(BalloonColour.Red, 1)); // Leviathan
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.addMote(new CellMote(0.01f, CellPrize.Grenade, CellSide.Auto));
            level.setTimeLimit(40);
            level.setGenerateLevel(19);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Borgers", "in arms", 1000, 850, 550);
            //level.setMotesToDestroy(15);
            level.addMote(new PineConeMote(1));
            level.addMote(new HeartOfGoldMote(1.0f));
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto, true));
            level.setTimeLimit(40);
            level.setGenerateLevel(14);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Resupply", "all you can eat", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new SatelliteMote(1.0f));
            level.addMote(new DragonMote(1.0f));
            level.addMote(new ICBMMote(1));
            level.addMote(new BlackHoleMote(1));
            level.addMote(new CellMote(0.02f,  CellPrize.Grenade, CellSide.Auto, true));
            level.setGenerateLevel(13);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Corba", "mark two", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new HeartOfGoldMote(1));
            level.addMote(new BalloonMote(BalloonColour.Red, 1)); // Leviathan
            level.addMote(new CellMote(0.02f, CellPrize.Shield, CellSide.Auto, true));
            level.addMote(new AcornMote(0.2f)); // Alien spider
            level.setGenerateLevel(24);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Space junk", "dodge it", 1000, 850, 550);
            if (WorldScene.m_debugLevels)
                level.setMotesToDestroy(1);
            else
                level.setMotesToDestroy(35);

            //level.setTimeLimit(1);
            level.addMote(new SatelliteMote(1.0f));
            level.addMote(new DragonMote(1.0f));
            level.addMote(new ICBMMote(1));
            level.addMote(new BlackHoleMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Grenade, CellSide.Auto, true));
            level.setGenerateLevel(18);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Like a", "SPACE WOLF", 5000, 4000, 3000);
            bossLevel.setBoss(new BossBadRobot());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new AcornMote(1, MoteStartSide.Right)); // alien spiders
            bossLevel.addMote(new SporeMote(1, MoteStartSide.Right)); // spore motes
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(20);
            bossLevel.setGenerateSpacing(0.2f); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setGenerateSpacingGood(2.0f);
            bossLevel.setRegenerate(true);
        }
				
		/// <summary>
        ///  ----------------------- WORLD 6 ----------------------------
		/// 
        /// Generates the mote world.
		/// </summary>
        protected void generateMoteWorld()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating MoteWorld...");
#endif

            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("Mote World");
            levelSet.setIntroductionText("still work to do");

            // First level - squishmotes make an appearances
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Sirens", "(so it goes)", 1000, 850, 550);
            level.setMotesToDestroy(1);
            level.addMote(new BlackHoleMote(1));
            level.addMote(new BalloonMote(BalloonColour.Purple, 1.0f, true));   // So it Goes
            //level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "DentArth", "urDent", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new BalloonMote(BalloonColour.Red, 1.0f, true));  // Penguin
            level.addMote(new BalloonMote(BalloonColour.Yellow, 1.0f, true));  // Snowflake
            level.setGenerateSpacing(1);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Snow", "Crash", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SquishMote(1));
            level.addMote(new MushroomMote(1));
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Visor", "Down", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new BalloonMote(BalloonColour.Green, 1, true)); // Viper
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Ion", "Drive", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(1));
            level.addMote(new BalloonMote(BalloonColour.Pink, 1, true)); // Last Starfighter
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(21);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Blast", "Doors", 1000, 850, 550);
            level.setMotesToDestroy(50);
            level.addMote(new SporeMote(1));
            level.addMote(new BalloonMote(BalloonColour.Pink, 1, true)); // Last Starfighter
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setTimeLimit(40);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Minion", "and on", 1000, 850, 550);
            //level.setMotesToDestroy(15);
            level.addMote(new MosquitoMote(1)); // Crunchettes
            level.setTimeLimit(40);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Adaptable", "Corruptable", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new CometMote(1));
            level.addMote(new BalloonMote(BalloonColour.Blue, 1, true)); // Alien Spiders
            level.setGenerateLevel(17);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Death", "Blosson", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new BalloonMote(BalloonColour.Blue, 1, true)); // Alien Spiders
            level.addMote(new BalloonMote(BalloonColour.Pink, 1, true)); // Last Starfighter
            level.setGenerateLevel(21);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Final", "Battle", 1000, 850, 550);
            if (WorldScene.m_debugLevels)
                level.setMotesToDestroy(1);
            else
                level.setMotesToDestroy(35);

            //level.setTimeLimit(1);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new BalloonMote(BalloonColour.Blue, 1, true)); // Alien Spiders
            level.addMote(new BalloonMote(BalloonColour.Pink, 1, true)); // Last Starfighter
            level.addMote(new BalloonMote(BalloonColour.Purple, 1.0f, true));   // So it Goes
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(18);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Commander", "Crunch", 5000, 4000, 3000);
            bossLevel.setBoss(new BossBadRobot());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new AcornMote(1, MoteStartSide.Right));
            bossLevel.addMote(new SporeMote(1, MoteStartSide.Right));
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);
        }
		

		
		// -------------------------- OLD LEVELS -----------------------------
		//
        //
		
        /// <summary>
        /// Forest is the second level
        /// </summary>
        protected void generateForest()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating the Forest...");
#endif

            //newLevel2.addMote(new FluffyMote(1));
            //newLevel2.addMote(new MosquitoMote(1.0f));
            //level.addMote(new SycamoreMote(1));
            //level.addMote(new AcornMote(1));
            //level.addMote(new RussianMote(1));
            //level.addMote(new CellMote(0.5f));
            //level.addMote(new SporeMote(1));
            //newLevel4.addMote(new FluffyMote(0.3f));
            //newLevel4.addMote(new MosquitoMote(0.1f));
            //newLevel4.addMote(new ButterflyMote(1, 100, true));


            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("You made it through the Meadow!");
            levelSet.setIntroductionText("Try the forest but be careful in there...");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Are these things for real?", "Look after yourself...", 1000, 850, 550);
            level.setMotesToDestroy(15);
            //level.addMote(new AcornMote(1));
			level.addMote(new CellMote(0.1f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(1.0f);
            level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Keep alive for 20 seconds!", "Some new type of Mote is incoming..", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new FluffyMote(1.0f));
            level.addMote(new PineConeMote(0.5f));
            level.addMote(new AcornMote(0.1f));
            level.addMote(new SporeMote(0.05f));
            level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(15);
            level.setRegenerate(true);


            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Slithery fun", "Look after yourself...", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SnakeMote(1));
            //level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "More animals are in these trees", "Don't get stung.", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new MosquitoMote(1));
            level.addMote(new ButterflyMote(1, 100, true));
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "More spores!", "Be quick", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Fluffies", "Pop 50 of them!", 1000, 850, 550);
            level.setMotesToDestroy(50);
            level.addMote(new FluffyMote(1));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "More forest madness", "Look after yourself...", 1000, 850, 550);
            //level.setMotesToDestroy(15);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(0.5f));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Pine Cones", "You wouldn't think they'd hurt?", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new MushroomMote(1));
            level.addMote(new PineConeMote(1));
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Getting tasty again", "Just keep popping!", 1000, 850, 550);
            level.setMotesToDestroy(30);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(1));
            level.addMote(new PineConeMote(1));
            level.setGenerateLevel(18);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Must be close?", "Head down!", 1000, 850, 550);
            level.setMotesToDestroy(35);
            //level.setTimeLimit(1);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(17);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "They still don't want you to leave", "New boss, same formula!", 5000, 4000, 3000);
            bossLevel.setBoss(new BossMegaWolf());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new SporeMote(1, MoteStartSide.Right));
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);
        }


        /// <summary>
        /// City Gate is the third level
        /// </summary>
        protected void generateCityGate()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating the City Gate...");
#endif

            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("You're at the City Gate");
            levelSet.setIntroductionText("Find a way into Space City!");

            // First level - squishmotes make an appearances
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Protect the Mote Ship", "Destroy 15 dandelions!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new FluffyMote(1));
            level.addMote(new SquishMote(1));
            level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);


            // Level 2
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "Keep alive for 20 seconds!", "Some new type of Mote is incoming..", 1000, 850, 550);
            level.setTimeLimit(20);
            level.addMote(new FluffyMote(1.0f));
            level.addMote(new SquishMote(0.5f));
            //level.addMote(new AcornMote(0.1f));
            level.addMote(new SporeMote(0.05f));
            level.setGenerateSpacing(1);
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 3
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Slithery fun", "Look after yourself...", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SquishMote(1));
            level.addMote(new SnakeMote(1));
            //level.setGenerateSpacing(0.2f);
            level.setGenerateLevel(20);
            level.setRegenerate(true);

            // Level 4
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Trying to escape the forest still?", "The animals still love you..", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new MosquitoMote(1));
            level.addMote(new ButterflyMote(1, 100, true));
            level.setGenerateLevel(15);
            level.setRegenerate(true);

            // Level 5
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "More spores!", "Be quick", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateSpacing(0.1f);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 6
            //
            level = levelSet.addLevel(LevelType.MotesTimeLimit, "Fluffies", "Pop 50 of them!", 1000, 850, 550);
            level.setMotesToDestroy(50);
            level.addMote(new FluffyMote(1));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 7
            //
            level = levelSet.addLevel(LevelType.ProtectTime, "More forest madness", "Look after yourself...", 1000, 850, 550);
            //level.setMotesToDestroy(15);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(0.5f));
            level.setTimeLimit(40);
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 8
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Pine Cones", "You wouldn't think they'd hurt?", 1000, 850, 550);
            level.setMotesToDestroy(35);
            level.addMote(new MushroomMote(1));
            level.addMote(new PineConeMote(1));
            level.setGenerateLevel(25);
            level.setRegenerate(true);

            // Level 9
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Getting tasty again", "Just keep popping!", 1000, 850, 550);
            level.setMotesToDestroy(20);
            level.addMote(new SnakeMote(1));
            level.addMote(new FluffyMote(1));
            level.addMote(new PineConeMote(1));
            level.setGenerateLevel(30);
            level.setRegenerate(true);

            // Level 10
            //
            level = levelSet.addLevel(LevelType.ProtectKills, "Must be almost there now?", "Head down!", 1000, 850, 550);
            level.setMotesToDestroy(30);
            //level.setTimeLimit(1);
            level.addMote(new SporeMote(1));
            level.addMote(new MushroomMote(1));
            level.addMote(new CellMote(0.05f, CellPrize.Shield, CellSide.Auto));
            level.setGenerateLevel(30);
            level.setRegenerate(true);

            // Boss level
            //
            Level bossLevel = levelSet.addLevel(LevelType.ProtectKills, "Commander Crunch", "Alien footfall", 5000, 4000, 3000);
            bossLevel.setBoss(new BossBadRobot());
            bossLevel.addMote(new GlueFluffyMote(1));
            bossLevel.addMote(new SporeMote(1, MoteStartSide.Right));
            //bossLevel.addMote(new CellMote(0.5f, CellPrize.Grenade, CellSide.Right));
            bossLevel.setGenerateLevel(5);
            bossLevel.setGenerateSpacing(2); // ensure that glue mote don't get generated too often for the boss
            bossLevel.setRegenerate(true);

        }


        /// <summary>
        /// Hinterland Not used
        /// </summary>
        protected void generateHinterland()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating Hinterland...");
#endif
            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("There must be another way home.  Survive the Hinterland.");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Protect the Mote Ship", "Destroy 15 dandelions!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new FluffyMote(1));
            level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);
        }

        protected void generateSpaceElevator()
        {
#if GENERATE_DEBUG
            Debug.Log("Generating Space Elevator...");
#endif
            // Welcome level set
            //
            LevelSet levelSet = addLevelSet("These robots really don't want to let you get into space.");

            // First level - destroy all the dandelions for a basic bronze - do kill streaks for a higher score!
            //
            Level level = levelSet.addLevel(LevelType.ProtectKills, "Protect the Mote Ship", "Destroy 15 dandelions!", 1000, 850, 550);
            level.setMotesToDestroy(15);
            level.addMote(new FluffyMote(1));
            level.setCompletionMessage("Well done.\nWhat else awaits us here?");
            level.setGenerateLevel(15);
            level.setRegenerate(true);
        }

        /// <summary>
        /// Test for game completion
        /// </summary>
        /// <returns></returns>
        public bool isGameCompleted()
        {
            return m_gameCompleted;
        }

        /// <summary>
        /// Is game completed?
        /// </summary>
        /// <param name="completed"></param>
        public void setGameCompleted(bool completed)
        {
            m_gameCompleted = completed;
        }

        /// <summary>
        /// Add LevelSet and return it for use
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected LevelSet addLevelSet(string name)
        {
            //Debug.Log("LevelManager::addLevelSet - adding level set " + name);
            m_levelSets.Add(new LevelSet(name, m_levelSets.Count));
            return m_levelSets[m_levelSets.Count - 1];
        }

        /// <summary>
        /// List of level sets
        /// </summary>
        public List<LevelSet> m_levelSets = new List<LevelSet>();

        /// <summary>
        /// Level that we're currently at
        /// </summary>
        protected int m_gameLevel = 0;

        /// <summary>
        /// Game completion
        /// </summary>
        protected bool m_gameCompleted = false;
    }
}