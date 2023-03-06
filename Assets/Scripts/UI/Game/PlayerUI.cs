using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private Image _playerPointer;
    [SerializeField] private TextMeshProUGUI _playerScore;

    public GameUI GameInfoUI { get; private set; }
    public MenuUI MenuUI { get; private set; }
    public HealthUI HealthUI { get; private set; }
    public ChangeGameText GameText { get; set; }


    private void Awake()
    {
        HealthUI = GetComponent<HealthUI>();
        GameInfoUI = GetComponent<GameUI>();
        MenuUI = GetComponent<MenuUI>();
        GameText = GetComponentInChildren<ChangeGameText>();
    }

    private void Start()
    {
        GameManager.OnTimeChanged += GameInfoUI.UpdateTime;
    }

    //public void DisconnectPlayer() => OnDisconnect?.Invoke();

    public void ChangePlayerAmmo(int ammo, int maxAmmo)
    {
        _ammo.text = $"{ammo}/{maxAmmo}";
    }

    public void FollowPoint(Vector2 pointPos)
    {
        if (!_playerPointer.gameObject.activeSelf)
            _playerPointer.gameObject.SetActive(true);
        
        _playerPointer.transform.position = pointPos;
    }

    public void UpdateScore(int score)
    {
        _playerScore.text = score.ToString();
    }
}
