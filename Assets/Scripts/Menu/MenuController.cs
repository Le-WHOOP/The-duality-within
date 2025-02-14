using System;
using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private ScreenController activeScreen;

    [SerializeField]
    private Animator transition;

    void Start()
    {
        activeScreen.gameObject.SetActive(true);
    }

    public void OpenScreen(ScreenController screen) {
        StartCoroutine(LoadScreen(screen));
    }
    public IEnumerator LoadScreen(ScreenController screen)
    {
        // Start fading animation
        transition.SetTrigger("Start");
        // Wait a bit
        yield return new WaitForSeconds(1);

        // Change the screen
        activeScreen.gameObject.SetActive(false);
        activeScreen = screen;
        activeScreen.gameObject.SetActive(true);

        // Start fading animation
        transition.SetTrigger("End");
        //Wait a bit
        yield return new WaitForSeconds(1);
    }
}
