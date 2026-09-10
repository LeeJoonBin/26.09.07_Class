using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleSceneController : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void OnEnable() => BindButtonEvents();
    private void OnDisable() => UnBindButtonEvents();
    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(LoadGameScene);
    }

    private void UnBindButtonEvents()
    {
        _startButton.onClick.RemoveListener(LoadGameScene);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
