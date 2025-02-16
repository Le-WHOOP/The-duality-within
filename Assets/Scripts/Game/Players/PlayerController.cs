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
    protected Personnality _personnality;

    private GameObject _jekyll;
    private Animator _animatorJekyll;
    private GameObject _hyde;
    private Animator _animatorHyde;

    protected Animator _animator;


    // TODO: define base _personnality, right now both players are Jekyll
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        _jekyll = transform.Find("Jekyll").gameObject;
        _animatorJekyll = _jekyll.GetComponent<Animator>();

        _hyde = transform.Find("Hyde").gameObject;
        _animatorHyde = _hyde.GetComponent<Animator>();

        DisplayActivePlayer();
    }

    public void DisplayActivePlayer()
    {
        if (_personnality == Personnality.Hyde)
        {
            _hyde.SetActive(true);
            _jekyll.SetActive(false);
            _animator = _animatorHyde;
        }
        else
        {
            _jekyll.SetActive(true);
            _hyde.SetActive(false);
            _animator = _animatorJekyll;
        }
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
