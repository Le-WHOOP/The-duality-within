using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // TODO Only for test
    [SerializeField]
    private GameController gameController;

    //[SerializeField]
    public float _speed;

    private Rigidbody2D _body;

    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
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
    public virtual void Move(float x, float y)
    {
        _body.linearVelocity = new Vector2(x, y).normalized * _speed;
    }
}
