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
    private Animator _animator;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
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
        Vector2 movement = new Vector2(x, y).normalized;
        _body.linearVelocity = movement * _speed;

        // Only set the animation direction is the player is trying to move
        if (movement != Vector2.zero) {
            _animator.SetFloat("x", movement.x);
            _animator.SetFloat("y", movement.y);
            _animator.SetBool("isWalking", true);
        }
        // FIXME: why does this throw a NullReferenceException()?
        // > maybe it _is_ _animator causing the issue bcs now I'm getting some "Parameter isWalking does not exist"
        else {
            _animator.SetBool("isWalking", false);
        }
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
