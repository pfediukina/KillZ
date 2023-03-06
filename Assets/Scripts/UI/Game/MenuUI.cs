using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;

    public Action OnDisconnect;

    public bool IsOpened { get; private set; }

    private void Start()
    {
        Cursor.visible = false;
    }

    public void EnableMenu(bool enable)
    {
        IsOpened = enable;
        _menu.alpha = enable ? 1 : 0;
        _menu.blocksRaycasts = enable;
    }

    public void OnExitPressed()
    {
        OnDisconnect?.Invoke();
    }

}