using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menuCanvas;

    public void SwitchPlayerMenu()
    {
        if(_menuCanvas.blocksRaycasts == true)
            EnablePlayerMenu(false);
        else
            EnablePlayerMenu(true);
    }

    public void EnablePlayerMenu(bool enable)
    {
        _menuCanvas.alpha = enable ? 1 : 0;
        _menuCanvas.blocksRaycasts = enable;
    }
}
