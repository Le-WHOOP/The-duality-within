using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // TODO Only for test
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private float _speed = 1;

    private Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
    }

    /// <summary>
    /// Moves the player by setting the linear velocity.
    /// </summary>
    /// <remarks>
    /// Values are relatives to one another.
    /// (x, y) = (1, 2) will do the same as (x, y) = (2, 4)
    /// </remarks>
    /// <param name="x">The horizontal movement input.</param>
    /// <param name="y">The vertical movement input.</param>
    public void Move(float x, float y)
    {
        _body.linearVelocity = new Vector2(x, y).normalized * _speed;
    }

    /// <summary>
    /// Attempts to interact with a nearby interactable object, if one is within range.
    /// </summary>
    public void Interact()
    {
        gameController.SwapRoles();
        //throw new NotImplementedException();
    }
}
