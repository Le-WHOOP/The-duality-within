using UnityEngine;

public class SettingsScreenController : ScreenController
{
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }
}