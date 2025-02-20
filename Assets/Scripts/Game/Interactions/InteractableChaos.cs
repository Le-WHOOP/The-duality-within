using UnityEngine;

public class InteractableChaos : InteractableController
{
    [HideInInspector]
    public float ChaosValue = 1f;

    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        if (player.GetCurrentPersonnality() == Personnality.Hyde)
        {
            ChaosSystem.Instance.RaiseChaos(this);
        }
    }
}
