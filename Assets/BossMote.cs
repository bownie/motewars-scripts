using UnityEngine;
using System.Collections.Generic;

namespace Xyglo.Unity
{
    public enum BossFacingSide
    {
        Right,
        Left,
        Center
    }

    /// <summary>
    /// Boss base class
    /// </summary>
	public abstract class BossMote : MovingMote
	{
        public BossMote(GameObject gameObject, MoveMethod method)
            : base(gameObject, method)
        {
        }

        /// <summary>
        /// Set the attack texture
        /// </summary>
        /// <param name="worth"></param>
        /// <param name="position"></param>
        /// <param name="method"></param>
        /// <param name="attackTexture"></param>
        public BossMote(int worth, Vector3 position, MoveMethod method)
            : base(worth, position, method)
        {
        }

        /// <summary>
        /// Loss an HP
        /// </summary>
        public void removeBossHP()
        {
            m_bossHP--;
            m_lastHitTime = Time.time;
            //Debug.Log("BOSS HIT - HP now = " + m_bossHP);
        }

        /// <summary>
        /// Is the boss dead?
        /// </summary>
        /// <returns></returns>
        public bool bossIsDead()
        {
            return (m_bossHP <= 0);
        }

        /// <summary>
        /// Is the boss attacking?
        /// </summary>
        public bool checkAttack()
        {
            if (Time.time > m_lastAttackTime + m_attackPeriod)
            {
                bool attack = (Random.value < m_attackChance);

                if (attack)
                {
                    m_lastAttackTime = Time.time;
                    return attack;
                }
            }

            
            return false;
        }

        /// <summary>
        /// Handle textures
        /// </summary>
        public void doTextures()
        {
			if (bossIsDead())
        	{
				if (m_gameObject.guiTexture.texture != m_deadTexture)
					m_gameObject.guiTexture.texture = m_deadTexture;
				
				return;
			}
			
            // Also check for textures
            //
            if (Time.time > m_lastAttackTime && Time.time < m_lastAttackTime + m_textureDuration)
            {
                m_gameObject.guiTexture.texture = m_angryTexture;
            }
            if (Time.time > m_lastHitTime && Time.time < m_lastHitTime + m_textureDuration)
            {
                m_gameObject.guiTexture.texture = m_hurtTexture;
            }
            else
            {
                // First let's see if we need to change the textures and swap them as necessary
                //
                if (Time.time > m_lastTextureChange + m_changeTexturePeriod)
                {
                    if (m_facingSide == BossFacingSide.Left || m_facingSide == BossFacingSide.Right)
                    {
                        m_leftlast = (m_facingSide == BossFacingSide.Left);
                        m_gameObject.guiTexture.texture = m_centreTexture;
                        m_facingSide = BossFacingSide.Center;
                        //Debug.Log("Changing BOSS texture centre - leftLast = " + m_leftlast);
                    }
                    else
                    {
                        if (m_leftlast)
                        {
                            m_gameObject.guiTexture.texture = m_rightTexture;
                            m_facingSide = BossFacingSide.Right;
                            //Debug.Log("Changing BOSS texture right");
                        }
                        else
                        {
                            m_gameObject.guiTexture.texture = m_leftTexture;
                            m_facingSide = BossFacingSide.Left;
                            //Debug.Log("Changing BOSS texture left");
                        }
                    }
                
                    m_lastTextureChange = Time.time;
                }
            }
        }

        /// <summary>
        /// Get the attack texture
        /// </summary>
        /// <returns></returns>
        public Texture getAttackTexture()
        {
            return m_attackTexture;

        }

        /// <summary>
        /// When did we last attack?
        /// </summary>
        protected float m_lastAttackTime = 0.0f;

        /// <summary>
        /// How many times minimum between attacks?
        /// </summary>
        protected float m_attackPeriod = 5.0f;

        /// <summary>
        /// How much chance of an attack after cooling off period?
        /// </summary>
        protected float m_attackChance = 0.3f;

        /// <summary>
        /// How many simulataneous attacks
        /// </summary>
        protected int m_simultaneousAttacks = 1;

        /// <summary>
        /// How many hit points does the boss have?
        /// </summary>
        protected int m_bossHP = 5;

        /// <summary>
        /// Left texture
        /// </summary>
        protected Texture m_leftTexture;

        /// <summary>
        /// Right texture
        /// </summary>
        protected Texture m_rightTexture;

        /// <summary>
        /// Centre texture
        /// </summary>
        protected Texture m_centreTexture;


        /// <summary>
        /// The texture used to attack
        /// </summary>
        protected Texture m_attackTexture;

        /// <summary>
        /// Angry texture
        /// </summary>
        protected Texture m_angryTexture;

        /// <summary>
        /// Hurt texture
        /// </summary>
        protected Texture m_hurtTexture;

        /// <summary>
        /// Dead texture
        /// </summary>
        protected Texture m_deadTexture;

        /// <summary>
        /// Last time boss has been hit
        /// </summary>
        protected float m_lastHitTime = 0.0f;

        /// <summary>
        /// How long will the texture stay active for?
        /// </summary>
        protected float m_textureDuration = 1.0f;


        /// <summary>
        /// Change texture period
        /// </summary>
        protected float m_changeTexturePeriod = 0.5f;

        /// <summary>
        /// Last texture change time
        /// </summary>
        protected float m_lastTextureChange = 0.0f;

        /// <summary>
        /// Which side is the boss facing?
        /// </summary>
        protected BossFacingSide m_facingSide = BossFacingSide.Center;

        /// <summary>
        /// Did we go left last?
        /// </summary>
        protected bool m_leftlast = false;


	}
}
