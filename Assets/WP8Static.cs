using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xyglo.Unity
{
    /// <summary>
    /// Used to connect up the URL hack workaround:
    /// 
    /// http://forum.unity3d.com/threads/192097-Application-OpenURL-causing-application-on-WP8-phone-to-stop?p=1320101&viewfull=1#post1320101
    /// </summary>
    public static class WP8Static
    {
        public static event EventHandler OpenUrlHandle;

        public static void FireOpenUrl(string url)
        {
            if (OpenUrlHandle != null)
            {
                OpenUrlHandle(url, null);
            }
        }
    }
}