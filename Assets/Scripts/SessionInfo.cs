using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SessionInfo : MonoBehaviour
{
    public static string SessionName;
    public static bool isConnect;

    //test
    //[SerializeField] private string testName;
    //[SerializeField] private bool connect;
    //end test

    private void Awake()
    {
        DontDestroyOnLoad(this);
        //test
        //if (SessionName == null)
        //{
        //    Debug.Log("Heer");
        //    SessionName = testName;
        //    isConnect = connect;
        //}

    }
}
