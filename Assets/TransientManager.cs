using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    /// <summary>
    /// Simply a placeholder for transient textures
    /// </summary>
    public class TransientManager : MonoBehaviour
    {
        public Texture m_score50;
        public Texture m_score100;
        public Texture m_score150;
        public Texture m_score200;
        public Texture m_score250;
        public Texture m_score300;
        public Texture m_score400;
        public Texture m_score500;
        public Texture m_score600;
        public Texture m_score800;
        public Texture m_score1000;
        public Texture m_score1500;
        public Texture m_score2000;
        public Texture m_niceOne;
        public Texture m_killStreak;
        public Texture m_explosion;
        public Texture m_shakeIt;
        public Texture m_keepShaking;
        public Texture m_lifeAwarded;
        public Texture m_shieldsUp;
        public Texture m_grenadeAwarded;
        public Texture m_grenade;
        public Texture m_shield;
        public Texture m_goldStar;
        public Texture m_silverStar;
        public Texture m_bronzeStar;
        public Texture m_defaultPop;
        public Texture m_number1;
        public Texture m_number2;
        public Texture m_number3;

        public Texture m_shakeBonus1000;
        public Texture m_shakeBonus2000;
        public Texture m_shakeBonus5000;
        public Texture m_shakeBonusLife;
        public Texture m_holdToDestroy;
        public Texture m_popThoseMotes;

        public AudioClip m_alarmAward;

        /// <summary>
        /// Gameobject for the gold star
        /// </summary>
        protected GameObject m_goldStarRealised = null;

        /// <summary>
        /// GameObject for the silver star
        /// </summary>
        protected GameObject m_silverStarRealised = null;

        /// <summary>
        /// GameObject for the bronze star
        /// </summary>
        protected GameObject m_bronzeStarRealised = null;

        /// <summary>
        /// Facebook graphic realised
        /// </summary>
        protected GameObject m_facebookBoastRealised = null;

        /// <summary>
        /// Twit boat graphic realised
        /// </summary>
        protected GameObject m_twitBoastRealised = null;

        /// <summary>
        /// Boast about your achievements on email
        /// </summary>
        protected GameObject m_emailBoastRealised = null;

        /// <summary>
        /// Gold position
        /// </summary>
        protected Rect m_goldPosition = new Rect();

        /// <summary>
        /// Silver position
        /// </summary>
        protected Rect m_silverPosition = new Rect();

        /// <summary>
        /// Bronze position
        /// </summary>
        protected Rect m_bronzePosition = new Rect();

        /// <summary>
        /// Original alpha value
        /// </summary>
        protected float m_originalAlpha = 1.0f;

        /// <summary>
        /// Grey alpha value
        /// </summary>
        protected float m_greyedAlpha = 0.15f;

        /// <summary>
        /// Animate stars
        /// </summary>
        protected float m_showStarsStartTime = 0.0f;

        /// <summary>
        /// Return a texture for a score
        /// </summary>
        /// <param name="worth"></param>
        /// <returns></returns>
        public Texture getTextureForScore(int worth)
        {
            switch (worth)
            {
                case 50:
                    return m_score50;
                case 100:
                    return m_score100;
                case 150:
                    return m_score150;
                case 250:
                    return m_score250;
                case 500:
                    return m_score500;
                case 1000:
                    return m_score1000;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get a transient movement direction for a screen position - we move into
        /// the screen center with all transients
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 getTransientMovementVector(Vector2 position)
        {
            Vector2 rV = new Vector2();
            float factor = 10.0f;
            rV.x = -((position.x - Screen.width / 2) / Screen.width) * factor;
            rV.y = -((position.y - Screen.height / 2) / Screen.height) * factor;
            //Debug.Log("GetTransientMovementVector x = " + rV.x + ", y = " + rV.y);
            return rV;
        }

        /// <summary>
        /// Show awards for a level
        /// </summary>
        /// <param name="levelScore"></param>
        public void showAwards(LevelScoreIdentifier levelScore, Vector2 centrePosition)
        {
            // Populate stars as necessary
            //
            if (m_goldStarRealised == null)
            {
                m_goldStarRealised = getStar(LevelScoreIdentifier.Gold);
                m_showStarsStartTime = Time.time;
            }

            if (m_silverStarRealised == null)
                m_silverStarRealised = getStar(LevelScoreIdentifier.Silver);

            if (m_bronzeStarRealised == null)
                m_bronzeStarRealised = getStar(LevelScoreIdentifier.Bronze);

            // Position stars
            //
            positionStars(levelScore, centrePosition);

            // Set up grey outedness
            //
            greyedStars(levelScore);
        }


        /// <summary>
        /// Position stars 
        /// </summary>
        /// <param name="levelScore"></param>
        /// <param name="centrePosition"></param>
        protected void positionStars(LevelScoreIdentifier levelScore, Vector2 centrePosition)
        {
            float growTime = 0.5f;
            float shrinkTime = 0.2f;

            // Position silver star first and gold and bronze around them
            //
            if (m_silverStarRealised.guiTexture != null)
            {
                m_silverPosition.width = m_silverStarRealised.guiTexture.pixelInset.width;
                m_silverPosition.height = m_silverStarRealised.guiTexture.pixelInset.height;

                // Do grow/shrink
                //
                if (levelScore == LevelScoreIdentifier.Silver)
                {
                    if (Time.time < m_showStarsStartTime + growTime)
                    {
                        // expand
                        m_silverPosition.width += (Time.time - m_showStarsStartTime * 20.0f);
                        m_silverPosition.height += (Time.time - m_showStarsStartTime * 20.0f);
                    }
                    else if (Time.time < m_showStarsStartTime + growTime + shrinkTime)
                    {
                        // shrink more slowly
                        //
                        m_silverPosition.width -= (Time.time - m_showStarsStartTime * 7.0f);
                        m_silverPosition.height -= (Time.time - m_showStarsStartTime * 7.0f);
                    }
                }

                m_silverPosition.x = centrePosition.x - m_silverStarRealised.guiTexture.pixelInset.width / 2;
                m_silverPosition.y = centrePosition.y;

                // Set
                m_silverStarRealised.guiTexture.pixelInset = m_silverPosition;
            }

            if (m_goldStarRealised.guiTexture != null)
            {
                // Set star positions - wobbling?
                //
                m_goldPosition.x = m_silverPosition.x + m_goldStarRealised.guiTexture.pixelInset.width * 1.5f;
                m_goldPosition.y = centrePosition.y;
                m_goldPosition.width = m_goldStarRealised.guiTexture.pixelInset.width;
                m_goldPosition.height = m_goldStarRealised.guiTexture.pixelInset.height;
                m_goldStarRealised.guiTexture.pixelInset = m_goldPosition;
            }

            if (m_bronzeStarRealised.guiTexture != null)
            {
                m_bronzePosition.width = m_bronzeStarRealised.guiTexture.pixelInset.width;
                m_bronzePosition.height = m_bronzeStarRealised.guiTexture.pixelInset.height;

                // Do grow/shrink
                //
                if (levelScore == LevelScoreIdentifier.Bronze)
                {
                    if (Time.time < m_showStarsStartTime + growTime)
                    {
                        // expand
                        m_bronzePosition.width += (Time.time - m_showStarsStartTime * 20.0f);
                        m_bronzePosition.height += (Time.time - m_showStarsStartTime * 20.0f);
                    }
                    else if (Time.time < m_showStarsStartTime + growTime + shrinkTime)
                    {
                        // shrink more slowly
                        //
                        m_bronzePosition.width -= (Time.time - m_showStarsStartTime * 7.0f);
                        m_bronzePosition.height -= (Time.time - m_showStarsStartTime * 7.0f);
                    }
                }

                m_bronzePosition.x = m_silverPosition.x + m_bronzeStarRealised.guiTexture.pixelInset.width * 1.5f;
                m_bronzePosition.y = centrePosition.y;

                m_bronzeStarRealised.guiTexture.pixelInset = m_bronzePosition;
            }
        }

        protected void greyedStars(LevelScoreIdentifier levelScore)
        {
            // Greying or not
            //
            switch (levelScore)
            {
                case LevelScoreIdentifier.Gold:
                    setAlpha(m_goldStarRealised, m_originalAlpha);
                    setAlpha(m_silverStarRealised, m_originalAlpha);
                    setAlpha(m_bronzeStarRealised, m_originalAlpha);
                    break;

                case LevelScoreIdentifier.Silver:
                    setAlpha(m_goldStarRealised, m_greyedAlpha);
                    setAlpha(m_silverStarRealised, m_originalAlpha);
                    setAlpha(m_bronzeStarRealised, m_originalAlpha);
                    break;

                case LevelScoreIdentifier.Bronze:
                    setAlpha(m_goldStarRealised, m_greyedAlpha);
                    setAlpha(m_silverStarRealised, m_greyedAlpha);
                    setAlpha(m_bronzeStarRealised, m_originalAlpha);
                    break;

                default:
                    setAlpha(m_goldStarRealised, m_greyedAlpha);
                    setAlpha(m_silverStarRealised, m_greyedAlpha);
                    setAlpha(m_bronzeStarRealised, m_greyedAlpha);
                    break;
            }
        }

        /// <summary>
        /// Get a star GameObject for a score level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected GameObject getStar(LevelScoreIdentifier level)
        {
            GameObject g = null;
            switch(level)
            {
                case LevelScoreIdentifier.Gold:
                    g = new GameObject("GoldStar");
                    g.AddComponent("GUITexture");
                    g.guiTexture.texture = m_goldStar;
                    g.transform.position = Vector3.zero;
                    g.transform.localScale = Vector3.zero;
                    g.guiTexture.pixelInset = new Rect(0, 0, m_goldStar.width / 4, m_goldStar.height / 4);
                    break;

                case LevelScoreIdentifier.Silver:
                    g = new GameObject("SilverStar");
                    g.AddComponent("GUITexture");
                    g.guiTexture.texture = m_silverStar;
                    g.transform.position = Vector3.zero;
                    g.transform.localScale = Vector3.zero;
                    g.guiTexture.pixelInset = new Rect(0, 0, m_silverStar.width / 4, m_silverStar.height / 4);
                    break;

                case LevelScoreIdentifier.Bronze:
                    g = new GameObject("BronzeStar");
                    g.AddComponent("GUITexture");
                    g.guiTexture.texture = m_bronzeStar;
                    g.transform.position = Vector3.zero;
                    g.transform.localScale = Vector3.zero;
                    g.guiTexture.pixelInset = new Rect(0, 0, m_bronzeStar.width / 4, m_bronzeStar.height / 4);
                    break;

                default:
                    break;
            }

            // Set the original alpha once
            //
            if (m_originalAlpha == 1.0f)
            {
                m_originalAlpha = g.guiTexture.color.a;
            }

            return g;
        }

        /// <summary>
        /// Set alpha for a GuiTexture on a GameObject
        /// </summary>
        /// <param name="g"></param>
        /// <param name="alpha"></param>
        protected void setAlpha(GameObject g, float alpha)
        {
            Color newColour = g.guiTexture.color;
            newColour.a = alpha;
            g.guiTexture.color = newColour;
        }

        /// <summary>
        /// Clear the stars
        /// </summary>
        public void clearStars()
        {
            DestroyObject(m_goldStarRealised);
            DestroyObject(m_silverStarRealised);
            DestroyObject(m_bronzeStarRealised);
        }
    }
}