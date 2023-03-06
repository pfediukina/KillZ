using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectWindow : MonoBehaviour, IWindow
{
    [SerializeField] CanvasGroup _canvas;
    [SerializeField] TMP_InputField _input;

    public WindowInput GetWindowInput()
    {
        WindowInput input = new WindowInput();
        input.InputText = _input.text;
        return input;
    }

    public void SetWindowActive(bool active)
    {
        _canvas.alpha = active ? 1 : 0;
        _canvas.blocksRaycasts = active;
    }
}
