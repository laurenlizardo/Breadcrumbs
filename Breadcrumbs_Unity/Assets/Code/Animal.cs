using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    // States
    // 1. Idle: Default, self-explanatory
    // 2. Surprised: Animal sees breadcrumb
    // 3. In motion: Animal is walking/running towards breadcrumb
    // 4. Eating: Animal is eating breadcrumb
    // 5. Repeat
    
    // Reference to the breadcrumb gameobject
    [SerializeField] private Breadcrumb _breadcrumb;
    
    // The distance that the animal can detect a breadcrumb
    [SerializeField] private float _detectionDistance;
    
    // The total reaction time
    [SerializeField] private float _totalReactionTime;
    private float _startReactionTime;   // This will be set to Time.time at the time of checking
    
    // The distance that the animal stops to eat the breadcrumb
    [SerializeField] private float _eatDistance;
    
    // The time it takes the animal to eat
    [SerializeField] private float _totalEatTime;
    private float _startEatTime;    // This will be set to Time.time at the time of checking

    [SerializeField] private float _moveSpeed;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        
        // States
        var idle = new Idle(this);
        var react = new React(this, _breadcrumb);
        var walk = new Walk(this, _breadcrumb, _moveSpeed);
        var eat = new Eat(this, _breadcrumb);

        // Transitions
        _stateMachine.AddTransition(idle, react, HasDetectedBreadcrumb());
        _stateMachine.AddTransition(react, walk, HasReacted());
        _stateMachine.AddTransition(walk, eat, HasReachedBreadcrumb());
        _stateMachine.AddTransition(eat, idle, HasFinishedEating());
        
        // Conditions
        Func<bool> HasDetectedBreadcrumb() => () =>
            Vector3.Distance(this.transform.position, _breadcrumb.transform.position) <= _detectionDistance;
        Func<bool> HasReacted() => () => Time.time - react.StartTime >= _totalReactionTime ? true : false;
        Func<bool> HasReachedBreadcrumb() => () =>
            Vector3.Distance(this.transform.position, _breadcrumb.transform.position) <= _eatDistance;
        Func<bool> HasFinishedEating() => () => Time.time - eat.StartTime >= _totalEatTime ? true : false;

        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}

