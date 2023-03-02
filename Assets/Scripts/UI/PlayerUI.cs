using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menuCanvas;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private Image _playerPointer;

    private bool _inMenu;

    private void Start()
    {
        Cursor.visible = false;
        _name.text = SessionInfo.SessionName;
        GameMaster.OnTimeChanged += UpdateTime;
    }

    private void Update()
    {
        if (GameMaster.EnableTimer)
        { 
        //    int minutes = GameMaster.CurrentTime / 60;
        //    int seconds = GameMaster.CurrentTime % 60;
        //    string text = minutes < 10 ? $"0{minutes}" : minutes.ToString();
        //    text += ":";
        //    text += seconds < 10 ? $"0{seconds}" : seconds.ToString();

        //    _timer.text = text;
        }
    }

    public void UpdateTime(int time)
    {
        string text = System.TimeSpan.FromSeconds(time).ToString("mm':'ss");
        _timer.text = text;
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
