using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public ScoreManager ScoreManager;
    public PlayfabManager PlayfabManager;
    public VidPlayer vidPlayer;
    public Transform reloadSystem;

    public void GameOver() { 
        reloadSystem.gameObject.SetActive(false);
        vidPlayer.PlayVideo();

        int score = ScoreManager.GetScore();
        GameOverScreen.Setup(score);
        PlayfabManager.SendLeaderboard(score); 
    }

}