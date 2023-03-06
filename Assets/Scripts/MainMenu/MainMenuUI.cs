using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private IWindow _createW;
    private IWindow _connectW;

    private void Awake()
    {
        _createW = GetComponentInChildren<CreateWindow>();
        _connectW = GetComponentInChildren<ConnectWindow>();
    }

    public void ShowWindow(bool isConnect)
    {
        _createW.SetWindowActive(!isConnect);
        _connectW.SetWindowActive(isConnect);
    }


    public void HideAllWindows()
    {
        _createW.SetWindowActive(false);
        _connectW.SetWindowActive(false);
    }

    public WindowInput GetInput(bool isConnect)
    {
        if (isConnect) return _connectW.GetWindowInput();
        else return _createW.GetWindowInput();
    }
}