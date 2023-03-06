using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Transform _parent;

    private List<GameObject> _hearts = new List<GameObject>();

    private int _health;
    private int _healthMax;

    public void UpdateHealth(int health, int maxHealth)
    {
        if(_parent.childCount == 0)
            SetHearts(maxHealth);

        _health = health;
        _healthMax = maxHealth;
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        _hearts.ForEach(p => p.transform.GetChild(0).gameObject.SetActive(true));

        for(int i = _health; i < _healthMax; i++)
        {
            _hearts[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void SetHearts(int count)
    {
        Debug.Log(count);
        for(int i = 0; i < count; i++)
        {
            _hearts.Add(Instantiate(_heartPrefab, _parent));
        }
    }
}
