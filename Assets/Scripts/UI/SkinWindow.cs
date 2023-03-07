using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkinWindow : MonoBehaviour, IWindow
{
    [SerializeField] private CanvasGroup _canvas;
    [HideInInspector] public int SkinID; 

    public WindowInput GetWindowInput() 
    { 
        WindowInput input = new WindowInput();
        input.InputText = SkinID.ToString();
        return input;
    }

    public void SetWindowActive(bool active)
    {
        _canvas.alpha = active ? 1 : 0;
        _canvas.blocksRaycasts = active;
    }

    public void SwitchWindow()
    {
        bool active = _canvas.blocksRaycasts;
        SetWindowActive(!active);
    }
}