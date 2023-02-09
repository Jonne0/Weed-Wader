using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void StartHelp()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
