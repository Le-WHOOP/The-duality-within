using UnityEngine;

public class PasserByController : MonoBehaviour
{
    public enum PasserByDirection {
        Down,
        Left,
        Up,
        Right
    }

    [SerializeField]
    private PasserByDirection _direction;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        // Sets up x and y so the Idle animation displayed matches _direction
        // On several lines for readability
        _animator.SetFloat("x",
            _direction == PasserByDirection.Down || _direction == PasserByDirection.Up ? 0
            : _direction == PasserByDirection.Left ? -1
            : 1);

        _animator.SetFloat("y", 
            _direction == PasserByDirection.Left || _direction == PasserByDirection.Right ? 0
            : _direction == PasserByDirection.Down ? -1 
            : 1);
    }
}
