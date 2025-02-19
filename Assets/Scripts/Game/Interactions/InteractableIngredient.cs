using UnityEngine;

public class InteractableIngredient : InteractableController
{
    public enum IngredientType
    {
        Alcohol,
        // Plants, salt
        Other,
    }

    [SerializeField]
    private IngredientType _type;

    [SerializeField]
    [Tooltip("Leave empty if this item is not inside a building. If not empty, this item will only be visible if the" +
    "player is currently inside the given building")]
    private InteractableBuilding _building;

    public IngredientType Type => _type;

    public InteractableIngredient() : base(Personnality.Jekyll) { }

    void Start()
    {
        // If this item is inside a specific building, it should only by visible and interactable if the player is
        // inside the same building as the item. This is necessary since multiple buildings share the same interior,
        // but items should only be in one of them.
        if (_building != null)
        {
            gameObject.SetActive(false);
            _building.OnEntered += (_, _) => gameObject.SetActive(true);
            _building.OnExited += (_, _) => gameObject.SetActive(false);
        }
    }

    public override void Interact(CityPlayerController player)
    {
        if (player.GetCurrentPersonnality() == Personnality.Jekyll)
        {
            Checklist.Instance.Collect(this);
        }
    }
}
