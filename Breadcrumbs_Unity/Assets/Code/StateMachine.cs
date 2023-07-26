using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;
    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private static List<Transition> EmptyTransitions = new List<Transition>(0);
    
    public void SetState(IState state)
    {
        // If the state being transitioned to is the same as the current state, do nothing.
        if (state == _currentState)
        {
            return;
        }

        // Finish up the current state then set the new state.
        _currentState?.OnExit();
        _currentState = state;
        
        // Add transition storage here
        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;
        
        // Set up the new state.
        _currentState.OnEnter();
        
        Debug.Log("NEW STATE:" + _currentState.ToString());
    }

    public void Tick()
    {
        // Add transition checks here
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);
        
        _currentState.Tick();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        // Check if the key has a matching value.
        // If it doesn't, add the value to the key.
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        // Add to the local list of transitions a new transition using the "transition to" state and predicate
        transitions.Add(new Transition(to, predicate));
    }

    private class Transition
    {
        // The condition to meet to trigger the transition
        public Func<bool> Condition { get; }
        
        // The state to transition to
        public IState To { get; }

        // Conductor to set its properties
        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
    
    private Transition GetTransition()
    {
        foreach (var transition in _currentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }
}