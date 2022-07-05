using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace EventHelper
{
    public class EventHelper : MonoBehaviour
    {
    }

    [System.Serializable]
    public class UnityIntEvent : UnityEvent<int>
    {
    }

    public class UnityNetworkConnectionEvent : UnityEvent<NetworkConnection>
    {
    }

    public class UnityQuaternionArrayEvent : UnityEvent<Quaternion[]>
    {
    }
}

