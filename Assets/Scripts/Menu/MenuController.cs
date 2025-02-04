using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private ScreenController activeScreen;

    void Start()
    {
        activeScreen.gameObject.SetActive(true);
    }

    public void OpenScreen(ScreenController screen)
    {
        activeScreen.gameObject.SetActive(false);
        activeScreen = screen;
        activeScreen.gameObject.SetActive(true);
    }
}
