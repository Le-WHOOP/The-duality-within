public class InteractableBrewingStand : InteractableController
{
    public InteractableBrewingStand() : base(Personnality.Jekyll) { }

    public override void Interact(CityPlayerController player)
    {
        if (Checklist.Instance.IsChecklistComplete())
        {
            base.Interact(player);
            GameController.Instance.EndGame();
        }
    }
}
