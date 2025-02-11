using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    private Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Move(float x, float y)
    {
        _body.linearVelocity = new Vector2(x, y).normalized * _speed;
    }

    public void Interact()
    {
        throw new NotImplementedException();
    }
}
