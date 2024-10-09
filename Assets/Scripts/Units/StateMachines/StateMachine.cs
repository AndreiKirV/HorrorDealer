using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<string, State> _states = new Dictionary<string, State>();
    private State _currentState;
    private State _startState;

    public State CurrentState => _currentState;

    public void AddState(string key, State state)
    {
        if (!_states.ContainsKey(key))
            _states.Add(key, state);
    }

    public void RemoveStates()
    {
        _states = new Dictionary<string, State>();
    }

    public void ResetStateToStart()
    {
        _currentState?.Exit();
        _currentState = _startState;
        _currentState.Entry();
    }

    public void ResetState()
    {
        _currentState?.Exit();
        _currentState?.Entry();
    }

    public void SetState(string key)
    {
        if (_states.ContainsKey(key) && _currentState == _states[key])
        {
            ResetState();
            return;
        }

        if (_states.TryGetValue(key, out var newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Entry();
        }

        if (_startState == null)
            _startState = _currentState;
    }

    public void OnTriggerEnter(Collider other)
    {
        _currentState?.OnTriggerEnter(other);
    }

    public void OnTriggerExit(Collider other)
    {
        _currentState?.OnTriggerExit(other);
    }

    public void Update()
    {
        _currentState?.Update();
    }
}