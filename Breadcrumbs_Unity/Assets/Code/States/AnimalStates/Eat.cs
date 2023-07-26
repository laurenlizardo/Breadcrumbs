using UnityEngine;

public class Eat : IState
{
    private readonly Animal _animal;
    private readonly Breadcrumb _breadcrumb;
    public float StartTime;

    public Eat(Animal animal, Breadcrumb breadcrumb)
    {
        _animal = animal;
        _breadcrumb = breadcrumb;
    }

    public void OnEnter()
    {
        // Stop the NavMeshAgent
        _animal.NavMeshAgent.isStopped = true;
        
        // Start animation
        _animal.ChangeAnimation(_animal.EatAnimation);
        
        // Track the moment in time the animal begins to eat
        StartTime = Time.time;
    }

    public void Tick() { }

    public void OnExit()
    {
        if (Time.time - StartTime >= _animal.TotalEatTime)
        {
            _animal.IncrementBreadcrumbs();
        }
        
        Quaternion lastSavedRotation = _animal.transform.rotation;
        _breadcrumb.IsActive = false;
        _animal.transform.rotation = lastSavedRotation;
    }
}