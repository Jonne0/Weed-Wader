using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Text scoreText;

    public void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score.ToString("D5");
    }
    public void RestartGame()
    {
        //TODO deactivate all bullets
        this.gameObject.SetActive(false);
        GameManager.Instance.Restart();

        PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.CanMove = true;
    }

    public void BackToStart()
    {
        GameManager.Instance.BackToMenu();
    }
}
