public class InteractableBrewingStant : InteractableController
{
    public InteractableBrewingStant() : base(Personnality.Jekyll) { }

    public override void Interact(CityPlayerController player)
    {
        if (Checklist.Instance.IsChecklistComplete())
        {
            GameController.Instance.EndGame();
        }
    }
}
