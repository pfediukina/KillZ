using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SessionInfo : MonoBehaviour
{
    public static string SessionName;
    public static bool isConnect;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
