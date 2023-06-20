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
        StartTime = Time.time;
    }

    public void Tick()
    {
        if (_breadcrumb.IsActive)
        {
            // Look at the breadcrumb
            _animal.transform.LookAt(
                new Vector3(
                    _breadcrumb.transform.position.x, 
                    0, 
                    _breadcrumb.transform.position.z));
        }
    }

    public void OnExit()
    {
        Quaternion lastSavedRotation = _animal.transform.rotation;
        _breadcrumb.IsActive = false;
        _animal.transform.rotation = lastSavedRotation;
    }
}