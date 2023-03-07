using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameMaster _GM;

    [SerializeField] private CanvasGroup _menuCanvas;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Button _buttonStartGame;
    [SerializeField] private Image _playerPointer;
    
    //test
    public PlayerHealthUI HealthUI;
    public Action OnDisconnect;

    private bool _inMenu;

    private void Start()
    {
        Cursor.visible = false;
        _name.text = SessionInfo.SessionName;
        GameMaster.OnTimeChanged += UpdateTime;
    }
    private void Update()
    {
        if (Launcher.Chars.Count >= GameMaster.Info.RequiredPlayers)
        {
            _buttonStartGame.interactable = true;
        }
        else
            _buttonStartGame.interactable = false;
    }

    public void ShowStartGameButton(Player player)
    {
        if (player.HasStateAuthority)
        {
            _buttonStartGame.gameObject.SetActive(true);
            _buttonStartGame.onClick.AddListener(() => OnPlayerStartedGame(player));
        }
    }

    private void OnPlayerStartedGame(Player p)
    {
        _buttonStartGame.gameObject.SetActive(false);
        _GM.StartGame();
    }

    public void ChangePlayerAmmo(int ammo, int maxAmmo)
    {
        _ammo.text = $"{ammo}/{maxAmmo}";
    }

    public void UpdateTime(int time)
    {
        string text = TimeSpan.FromSeconds(time).ToString("mm':'ss");
        _timer.text = text;
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }

    public void SwitchPlayerMenu()
    {
        if(_menuCanvas.blocksRaycasts == true)
            EnablePlayerMenu(false);
        else
            EnablePlayerMenu(true);
    }

    public void EnablePlayerMenu(bool enable)
    {
        Cursor.visible = enable;
        _inMenu = enable;
        _menuCanvas.alpha = enable ? 1 : 0;
        _menuCanvas.blocksRaycasts = enable;
    }

    public void OnExitPressed()
    {
        OnDisconnect?.Invoke();
    }

    public void FollowPoint(Vector2 pointPos)
    {
        if(_inMenu)
        {
            _playerPointer.gameObject.SetActive(false);
            return;
        }
        if (!_playerPointer.gameObject.activeSelf)
            _playerPointer.gameObject.SetActive(true);
        
        _playerPointer.transform.position = pointPos;
    }
}
