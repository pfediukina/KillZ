using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuUI _ui;
    [SerializeField] private int _gameSceneID;

    public void OnButtonPressed(bool isConnect)
    {
        var menuInfo = _ui.GetInput(isConnect);
        if (menuInfo.InputText.IsNullOrEmpty()) return;
        StartGame(isConnect, menuInfo.InputText);
    }

    public void StartGame(bool connect, string sessionName)
    {
        PlayerPrefs.SetString("SessionName", sessionName);
        PlayerPrefs.SetInt("IsConnect", connect ? 1 : 0);
        SceneManager.LoadScene(_gameSceneID);
    }
}