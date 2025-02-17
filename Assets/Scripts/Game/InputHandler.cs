using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private KeyCode upKey;
    [SerializeField]
    private KeyCode leftKey;
    [SerializeField]
    private KeyCode downKey;
    [SerializeField]
    private KeyCode rightKey;
    [SerializeField]
    private KeyCode interactKey;

    /// <summary>
    /// The player character currently controlled by these inputs
    /// </summary>
    public PlayerController Player { get; set; }

    /// <summary>
    /// Returns the vertical input value based on key presses.
    /// </summary>
    /// <returns>
    /// A float value representing vertical movement:
    /// 1 for the up key, -1 for the down key, and 0 if neither or both are pressed.
    /// </returns>
    private float GetVerticalAxis()
    {
        return (Input.GetKey(upKey) ? 1 : 0) + (Input.GetKey(downKey) ? -1 : 0);
    }

    /// <summary>
    /// Returns the horizontal input value based on key presses.
    /// </summary>
    /// <returns>
    /// A float value representing vertical movement:
    /// 1 for the right key, -1 for the left key, and 0 if neither or both are pressed.
    /// </returns>
    private float GetHorizontalAxis()
    {
        return (Input.GetKey(leftKey) ? -1 : 0) + (Input.GetKey(rightKey) ? 1 : 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && Player is CityPlayerController player)
            player.Interact();

        Player.Move(GetHorizontalAxis(), GetVerticalAxis());
    }
}
