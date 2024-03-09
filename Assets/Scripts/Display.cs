using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    public TextMeshPro gameInfoText; // The TextMeshPro that displays the game info
    private int killCount = 0; // The current kill count
    private int waveNumber = 0; // The current wave number

    public void UpdateWaveNumber(int newWaveNumber)
    {
        waveNumber = newWaveNumber;
        UpdateGameInfoText();
    }

    public void IncrementKillCount()
    {
        killCount++;
        UpdateGameInfoText();
    }

    private void UpdateGameInfoText()
    {
        gameInfoText.text = "Wave: " + waveNumber + "\nKills: " + killCount;
    }

    void update ()
    {
        UpdateGameInfoText();
    }
}