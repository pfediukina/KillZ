using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;

    public static LoadingScreen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void EnableLoading(bool enable)
    {
        Instance._canvas.alpha = enable ? 1 : 0;
        Instance._canvas.blocksRaycasts = enable;
    }
}
