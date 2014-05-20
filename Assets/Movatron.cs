using UnityEngine;
using System;
using System.Collections.Generic;


namespace Xyglo.Unity
{
	
	public enum MoveDirection
	{
		None,
		X,
		Y,
		Z
	}
	
	/// <summary>
	/// Used for detection of accelerometer usage
	/// </summary>
	public class Movatron
	{
		public Movatron ()
		{
            m_lowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
		}
		
		
		/// <summary>
		/// Second attempt at shaking - it has an ignore time at the beginning for when we
		/// have shaken for a while, achieved our goal and then need to back off for a few
		/// seconds.
		/// </summary>
		/// <returns>
		/// The sample2.
		/// </returns>
		public bool doSample2()
		{
			// Do nothing if we're ignoring
			if (Time.time < m_shakeIgnore) 
				return false;
			
		    Vector3 acceleration = Input.acceleration;

        	m_lowPassValue = Vector3.Lerp(m_lowPassValue, acceleration, m_lowPassFilterFactor);

            Vector3 deltaAcceleration = acceleration - m_lowPassValue;
			
			float shakeDetectionThreshold = 4.0f;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
    		{

        		// Perform your "shaking actions" here, with suitable guards in the if check above, if necessary to not, to not fire again if they're already being performed.
        		Debug.Log("Shake event detected at time "+Time.time);
				
				if (!m_shaking)
				{
					m_startShake = Time.time;	
					m_shaking = true;
				}
				
					
				if (m_shakePercentage < 100)
					m_shakePercentage++;
				
				return true;
    		}
			
			// Reset shaking
			//
			m_shaking = false;
			if (m_shakePercentage > 0)
				m_shakePercentage--;
			return false;
		}
		
		
		/// <summary>
		/// Tests the shake completed.
		/// </summary>
		/// <returns>
		/// The shake completed.
		/// </returns>
		public bool testShakeCompleted()
		{
			return (m_shaking && Time.time  > m_shakeStartTime + m_shakeDuration);
		}
		
		/// <summary>
		/// Do the sample.
		/// </summary>
		/// <returns>
		/// The sample.
		/// </returns>
		public MoveDirection doSample()
		{
			// Do nothing if we're ignoring
			if (Time.time < m_shakeIgnore) 
				return MoveDirection.None;
			
			if (Time.time > m_lastSample)
			{
				//m_samples.Add(Input.acceleration.magnitude);
				//m_lastSample = Time.time;
				
				float minMove = 0.05f;
				
                Vector3 nowAccel = Input.acceleration;
                Vector3 deltaAccel = LowPassFilter(nowAccel);

                if(Mathf.Abs(deltaAccel.x) > minMove)
                {
                    m_lastDirection = MoveDirection.X;
					m_shakePercentage++;
                }
				/*
				else if (Mathf.Abs(deltaAccel.y) > minMove)
				{
					m_lastDirection = MoveDirection.Y;
				} else if (Mathf.Abs(deltaAccel.z) > minMove)
				{
					m_lastDirection = MoveDirection.Z;
				}*/
				else
				{
					m_lastDirection = MoveDirection.None;
					
					if (m_shakePercentage > 0)
						m_shakePercentage--;
				}
				
			}
			
			
			//while (m_samples.length() > m_sampleListLength / m_samplePeriod)
			//{
				//m_samples.remove();
			//}
			
			return m_lastDirection;
		}
		
		/// <summary>
		/// Sets the shake ignore for a few seconds and reset shakePercentage.
		/// </summary>
		public void setShakeIgnore()
		{
			m_shakeIgnore = Time.time + 5.0f;
			m_shaking = false;
			m_shakePercentage = 0;
		}
		
		
		/// <summary>
		/// Get the shake percentage.
		/// </summary>
		/// <returns>
		/// The percentage.
		/// </returns>
		public int getShakePercentage()
		{
			return m_shakePercentage;
		}
		
		/// <summary>
		/// Sets the shake percentage.
		/// </summary>
		/// <param name='perc'>
		/// Perc.
		/// </param>
		public void setShakePercentage(int perc)
		{
			m_shakePercentage = perc;
		}
		
		/// <summary>
		/// The m_shake percentage.
		/// </summary>
		protected int m_shakePercentage = 0;
		
		/// <summary>
		/// The m_shake start time.
		/// </summary>
		protected float m_shakeStartTime = 0.0f;
		
		/// <summary>
		/// The duration of the m_shake.
		/// </summary>
		protected float m_shakeDuration = 5.0f;
		
		/// <summary>
		/// Ignore shaking until
		/// </summary>
		protected float m_shakeIgnore = 0.0f;
		
		/// <summary>
		/// The m_last direction.
		/// </summary>
		protected MoveDirection m_lastDirection = MoveDirection.None;
		
		/// <summary>
		/// Are we shaking?
		/// </summary>
		/// <returns>
		/// The shaking.
		/// </returns>
		public bool isShaking()
		{
			return m_shaking;
		}
		
		protected bool m_shaking = false;
		
        /// <summary>
        /// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current
        /// input sample (and vice versa). You should be able to use LowPassFilter() function instead of avgSamples().
        /// </summary>
        protected float LowPassKernelWidthInSeconds = 1.0f;
        
        protected float AccelerometerUpdateInterval = 1.0f / 60.0f;


        protected float m_lowPassFilterFactor; // = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 

        protected Vector3 m_lowPassValue = Vector3.zero; // should be initialized with 1st sample

		
		/// <summary>
		/// Lows the pass filter.
		/// </summary>
		/// <returns>
		/// The pass filter.
		/// </returns>
		/// <param name='newSample'>
		/// New sample.
		/// </param>
        protected Vector3 LowPassFilter(Vector3 newSample)
        {
            m_lowPassValue = Vector3.Lerp(m_lowPassValue, newSample, m_lowPassFilterFactor);
            return m_lowPassValue;
        }

		
		protected float m_startShake;
		
		/// <summary>
		/// How often we sample the accelerometer
		/// </summary>
		protected float m_samplePeriod = 0.1f;
		
		protected float m_lastSample = 0.0f;
		
		/// <summary>
		/// The length of the m_sample list in seconds.
		/// </summary>
		protected float m_sampleListLength = 2.0f;
		
		/// <summary>
		/// Last few samples
		/// </summary>
		protected List<float> m_samples = new List<float>();
	}
}

