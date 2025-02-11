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

    public PlayerController EntityController { get; set; }

    private float GetVerticalAxis()
    {
        return (Input.GetKey(upKey) ? 1 : 0) + (Input.GetKey(downKey) ? -1 : 0);
    }

    private float GetHorizontalAxis()
    {
        return (Input.GetKey(leftKey) ? -1 : 0) + (Input.GetKey(rightKey) ? 1 : 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
            EntityController.Interact();

        EntityController.Move(GetHorizontalAxis(), GetVerticalAxis());
    }
}
