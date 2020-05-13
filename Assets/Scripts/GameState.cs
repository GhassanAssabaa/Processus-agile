using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public enum State { Paused, Playing}

public class GameState : MonoBehaviour
{
    public State state {
        get { return _state; }
    }
    private State _state = State.Playing;
    
    public void Pause()
    {
        _state = State.Paused;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _state = State.Playing;
        Time.timeScale = 1;
    }
}
