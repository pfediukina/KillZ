using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _timer;

    private void Awake()
    {
        _name.text = SessionInfo.SessionName;
    }

    public void UpdateTime(int time)
    {
        string text = TimeSpan.FromSeconds(time).ToString("mm':'ss");
        _timer.text = text;
    }
}