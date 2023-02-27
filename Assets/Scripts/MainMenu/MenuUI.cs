using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private IWindow _createW;
    private IWindow _connectW;

    private void Awake()
    {
        _createW = GetComponentInChildren<CreateWindow>();
        _connectW = GetComponentInChildren<ConnectWindow>();
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