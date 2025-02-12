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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BuildingController>() is BuildingController buildingController)
            buildingController.OnPlayerColliderEnter(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BuildingController>() is BuildingController buildingController)
            buildingController.OnPlayerColliderExit(collision);
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
