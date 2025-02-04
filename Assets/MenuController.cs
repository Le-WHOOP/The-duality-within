using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameObject activeScreen;

    public void OpenScreen(GameObject screen)
    {
        activeScreen.SetActive(false);
        activeScreen = screen;
        activeScreen.SetActive(true);
    }
}
