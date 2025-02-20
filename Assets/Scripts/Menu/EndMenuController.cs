using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _winnerText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    [Header("Transition")]
    [SerializeField]
    private FadeScene _fadeScene;

    void Start()
    {
        _winnerText.text = _winnerText.text.Replace("{winner}", GameResults.Winner.ToString());

        if (GameSettings.SwapRoles) 
        {
            string[] tmp = _descriptionText.text.Split('/');
            _descriptionText.text = tmp[1] + '/' + tmp[0];
        }

        _descriptionText.text = _descriptionText.text.Split('/')[GameResults.Winner - 1];
    }

    public void RestartButton()
    {
        StartCoroutine(_fadeScene.LoadScene("GameScene"));
    }

    public void MenuButton()
    {
        StartCoroutine(_fadeScene.LoadScene("MenuScene"));
    }
}
