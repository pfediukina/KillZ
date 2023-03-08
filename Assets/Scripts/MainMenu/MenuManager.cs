using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuUI _ui;
    [SerializeField] private int _gameSceneID;

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void OnButtonPressed(bool isConnect)
    {
        var menuInfo = _ui.GetInput(isConnect);
        if (menuInfo.InputText.IsNullOrEmpty()) return;
        StartGame(isConnect, menuInfo.InputText);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame(bool connect, string sessionName)
    {
        SessionInfo.SessionName = sessionName;
        SessionInfo.isConnect = connect;
        SceneManager.LoadScene(_gameSceneID);
    }
}