using UnityEngine;
using System.Collections;

namespace Xyglo.Unity
{
    public class JiggleMessage
    {
        public JiggleMessage(GameObject baseObject)
        {
            m_baseGameObect = baseObject;
        }

        protected GameObject m_baseGameObect;

    }

}