using UnityEngine;

public class InteractableIngredient : InteractableController
{
    public enum IngredientType
    {
        ALCOHOL,
        OTHER // plants, salt
    }
    public InteractableIngredient(Personnality availableTo) : base(Personnality.Jekyll) { }

    public IngredientType Type { get; }

    public override void Interact()
    {
        Checklist.Instance.Collect(this);
    }
}
