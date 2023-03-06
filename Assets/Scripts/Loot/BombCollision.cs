using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class BombCollision : NetworkBehaviour
{
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private SpriteRenderer _circleSprite;

    private HashSet<Zombie> _zombieList = new HashSet<Zombie>();
    private bool _isActivated = false;

    public void SetColliderRadius(float radius)
    {
        _circleCollider.radius = radius;
        _circleSprite.transform.localScale = Vector3.one * (0.8f / 2 * radius);
        _isActivated = false;
    }

    public void ActivateBomb(Player player)
    {
        _isActivated = true;
        foreach (var zombie in _zombieList)
        {
            zombie.DespawnObject();
            player.Score += zombie.Score;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out var zombie) && !_isActivated)
        {
            _zombieList.Add(zombie);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out var zombie) && !_isActivated)
        {
            _zombieList.Remove(zombie);
        }
    }
}