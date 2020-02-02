using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class HudManager : MonoBehaviour
{
    private ColyseusManager colyseusManager;
    private GameManager gameManager;
    private AudioManager audioManager;
    
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI playerCount;
    public Slider totalHeat;
    public TextMeshProUGUI timeDisplay;

    public Button reconnectButton;
    public Button pauseButton;
    
    // Start is called before the first frame update
    void Start()
    {
        colyseusManager = GameObject.FindObjectOfType<ColyseusManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        
        if (roomName != null)
        {
            roomName.text = "Room ID: ";
        }
        
        if (playerCount != null)
        {
            playerCount.text = "Players: ?";
        }

        if (totalHeat != null)
        {
            totalHeat.value = gameManager.GetOverallHeatLevel();
        }
        
        colyseusManager.LeftRoom += code =>
        {
            playerCount.text = "Players: 0";
            
            roomName.text = "Room ID: [empty]";

            if (reconnectButton != null)
            {
                reconnectButton.gameObject.SetActive(true);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDisplay != null)
        {
            var secondsRemaining = Math.Round(gameManager.timeLeft, 0);
            timeDisplay.text = $"Time Remaining: {secondsRemaining.ToString()}";
        }
        
        if (roomName != null)
        {
            roomName.text = "Room ID: " + colyseusManager.GetRoomId();
        }
        
        if (totalHeat != null)
        {
            totalHeat.value = gameManager.GetOverallHeatLevel();
        }

        if (playerCount != null && colyseusManager.room != null && colyseusManager.room.State != null && colyseusManager.room.State.players != null)
        {
            reconnectButton.gameObject.SetActive(false);

            playerCount.text = "Players: " + colyseusManager.room.State.players.Count;
        }
        else
        {
            reconnectButton.gameObject.SetActive(true);

            playerCount.text = "Players: 0";
        }
    }

    public void TogglePause()
    {
        if (gameManager.localGameState == LocalGameState.Paused)
        {
            if (pauseButton != null)
            {
                pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            }
            gameManager.ResumeGame();
        }
        else
        {
            if (pauseButton != null)
            {
                pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
            }
            gameManager.PauseGame();
        }

        if (audioManager != null)
        {
            if (gameManager.localGameState == LocalGameState.Paused)
            {
                audioManager.audioSource.Stop();
            }
            else
            {
                audioManager.audioSource.Play();
            }
        }
    }
}
