using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    /// <summary>
    /// Provides a generic interface for player prefs over Unity PlayerPrefs and other methods of obfuscated PlayerPrefs
    /// </summary>
    public class XygloPlayerPrefs
    {
        /// <summary>
        /// Delete all
        /// </summary>
        public static void DeleteAll()
        {
#if UNITY_WINRT
            PlayerPrefs.DeleteAll();
#else
            SecuredPlayerPrefs.DeleteAll();
#endif
        }

        public static void SetSecretKey(string key)
        {
            m_key = key;

            
#if !UNITY_WINRT
            SecuredPlayerPrefs.SetSecretKey(key);
#endif
        }


        /// <summary>
        /// Get a float
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static float GetFloat(string attrName, float def)
        {
#if UNITY_WINRT
            return PlayerPrefs.GetFloat(attrName, def);
#else
            return SecuredPlayerPrefs.GetFloat(attrName, def);
#endif
        }

        /// <summary>
        /// Set a float
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="value"></param>
        public static void SetFloat(string attrName, float value)
        {
#if UNITY_WINRT
            PlayerPrefs.SetFloat(attrName, value);
#else
            SecuredPlayerPrefs.SetFloat(attrName, value);
#endif
        }

        /// <summary>
        /// Get int
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int GetInt(string attrName, int def)
        {
#if UNITY_WINRT
            return PlayerPrefs.GetInt(attrName, def);
#else
            return SecuredPlayerPrefs.GetInt(attrName, def);
#endif
        }

        /// <summary>
        /// Set int
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="value"></param>
        public static void SetInt(string attrName, int value)
        {
#if UNITY_WINRT
            PlayerPrefs.SetInt(attrName, value);
#else
            SecuredPlayerPrefs.SetInt(attrName, value);
#endif
        }

        /// <summary>
        /// Get a bool
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool GetBool(string attrName, bool def)
        {
#if UNITY_WINRT
            return PlayerPrefs.GetInt(attrName, def ? 1 : 0) == 1 ? true : false;
#else
            return SecuredPlayerPrefs.GetBool(attrName, def);
#endif
        }

        /// <summary>
        /// Set bool
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="value"></param>
        public static void SetBool(string attrName, bool value)
        {
#if UNITY_WINRT
            PlayerPrefs.SetInt(attrName, value ? 1 : 0);
#else
            SecuredPlayerPrefs.SetBool(attrName, value);
#endif
        }

        /// <summary>
        /// Get the int array
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] GetIntArray(string attrName, int[] array)
        {
#if UNITY_WINRT
            return PlayerPrefsX.GetIntArray(attrName);
#else
            return SecuredPlayerPrefs.GetIntArray(attrName, array);
#endif
        }

        /// <summary>
        /// Set int array
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="array"></param>
        public static void SetIntArray(string attrName, int[] array)
        {
#if UNITY_WINRT
            PlayerPrefsX.SetIntArray(attrName, array);
#else
            SecuredPlayerPrefs.SetIntArray(attrName, array);
#endif
        }

        /// <summary>
        /// Get String array
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string[] GetStringArray(string attrName, string[] array)
        {
#if UNITY_WINRT
            return PlayerPrefsX.GetStringArray(attrName);
#else
            return SecuredPlayerPrefs.GetStringArray(attrName, array);
#endif
        }

        /// <summary>
        /// Set string array
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name="array"></param>
        public static void SetStringArray(string attrName, string[] array)
        {
#if UNITY_WINRT
            PlayerPrefsX.SetStringArray(attrName, array);
#else
            SecuredPlayerPrefs.SetStringArray(attrName, array);
#endif
        }

        /// <summary>
        /// Force save
        /// </summary>
        public static void Save()
        {
#if UNITY_WINRT
            PlayerPrefs.Save();
#else
            SecuredPlayerPrefs.Save();
#endif
        }

        /// <summary>
        /// Secret key
        /// </summary>
        static protected string m_key;

    }
}