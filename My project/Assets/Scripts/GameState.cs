using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum GameStates
    {
        Game,
        Cutscene,
        Pauze,
    }

    private GameStates m_CurrentGameState;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentGameState = GameStates.Cutscene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameStates GetCurrentState()
    {
        return m_CurrentGameState;
    }

    public void StartGame()
    {
        m_CurrentGameState = GameStates.Game;
    }

    public void PauzeGame()
    {
        m_CurrentGameState = GameStates.Pauze;
    }

    public void UnPauzeGame()
    {
        m_CurrentGameState = GameStates.Game;
    }
}
