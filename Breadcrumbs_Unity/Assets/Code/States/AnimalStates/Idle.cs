﻿using UnityEngine;

public class Idle : IState
{
    private readonly Animal _animal;

    public Idle(Animal animal)
    {
        _animal = animal;
    }

    public void OnEnter()
    {
        // Start animation
        _animal.ChangeAnimation(_animal.IdleAnimation);
    }

    public void Tick() { }
    
    public void OnExit() {}
}