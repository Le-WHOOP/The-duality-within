using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableBuilding : InteractableController
{
    public InteractableBuilding() : base(Personnality.Both) { }

    public override void Interact()
    {
        // TODO Enter the building
        throw new System.NotImplementedException();
    }
}
