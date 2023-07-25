using UnityEngine;

public class React : IState
{
    private readonly Animal _animal;
    private readonly Breadcrumb _breadcrumb;
    public float StartTime;

    public React(Animal animal, Breadcrumb breadcrumb)
    {
        _animal = animal;
        _breadcrumb = breadcrumb;
    }

    public void OnEnter()
    {
        // Start animation
        _animal.ChangeAnimation(_animal.ReactAnimation);
        
        // Track the moment in time the animal begins to react
        StartTime = Time.time;
    }

    public void Tick()
    {
        // Look at the breadcrumb the duration of the reaction
        if (_breadcrumb.IsActive)
        {
            _animal.transform.LookAt(new Vector3(_breadcrumb.transform.position.x, 0, _breadcrumb.transform.position.z));
        }
    }
    
    public void OnExit() { }
}