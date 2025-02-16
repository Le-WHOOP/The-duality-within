using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;
    protected Animator _activeAnimator;

    [SerializeField]
    private float _speed = 1;

    [Header("Animators")]
    [SerializeField]
    private Animator _animatorJekyll;
    [SerializeField]
    private Animator _animatorHyde;

    protected Personnality _personnality;

    protected PlayerController(Personnality defaultPersonnality)
    {
        _personnality = defaultPersonnality;
    }

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        DisplayActivePlayer();
    }

    /// <summary>
    /// Displays the player matching _personnality and hides the other.
    /// </summary>
    /// <remarks>
    /// Also updates _animator to display the proper animation in CityPlayerController.
    /// </remarks>
    private void DisplayActivePlayer()
    {
        _animatorJekyll.gameObject.SetActive(_personnality == Personnality.Jekyll);
        _animatorHyde.gameObject.SetActive(_personnality == Personnality.Hyde);
        _activeAnimator = _animatorJekyll.gameObject.activeSelf ? _animatorJekyll : _animatorHyde;
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

    public virtual void SwapPersonnality()
    {
        _personnality = _personnality == Personnality.Hyde ? Personnality.Jekyll : Personnality.Hyde;
        DisplayActivePlayer();
    }
}
