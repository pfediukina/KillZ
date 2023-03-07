using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private IWindow _createW;
    private IWindow _connectW;

    [SerializeField]
    private Button[] _skinButtons;

    private void Awake()
    {
        _createW = GetComponentInChildren<CreateWindow>();
        _connectW = GetComponentInChildren<ConnectWindow>();

        for (int i =0; i < _skinButtons.Length; i++)
        {
            int index = i;
            _skinButtons[i].onClick.AddListener(() => SetSkin(index));
        }
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

    public void SetSkin(int index)
    {
        Launcher.SelectedSkin = index;

        for (int i = 0; i < _skinButtons.Length; i++)
        {
            if (index == i)
                _skinButtons[i].image.color = Color.yellow;
            else
                _skinButtons[i].image.color = Color.white;
        }
    }
}