using System;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private Collider2D _entranceCollider;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPlayerColliderEnter(Collider2D collision)
    {
        if (collision == _collider)
            _sprite.enabled = false;

        if (collision == _entranceCollider)
            throw new NotImplementedException();
    }

    public void OnPlayerColliderExit(Collider2D collider)
    {
        if (collider == _collider)
            _sprite.enabled = true;

        if (collider == _entranceCollider)
            throw new NotImplementedException();
    }
}
