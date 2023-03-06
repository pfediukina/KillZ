using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameText : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _sprite;
    [SerializeField] private float _lifetime;

    public void ShowGameText(GameText type)
    {
        _sprite.enabled = true;
        StartCoroutine(HideText());
        switch (type)
        {
            case GameText.Intro:
                _sprite.sprite = _sprites[0];
                break;
            case GameText.Survived:
                _sprite.sprite = _sprites[1];
                break;
            case GameText.Dead:
                _sprite.sprite = _sprites[2];
                break;
            case GameText.Empty:
                _sprite.enabled = false;
                break;
        }
    }
    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(_lifetime);
        ShowGameText(GameText.Empty);
    }
}


public enum GameText
{
    Intro,
    Survived,
    Dead,
    Empty
}