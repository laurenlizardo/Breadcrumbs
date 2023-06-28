using UnityEngine;

public class Walk : IState
{
    private readonly Animal _animal;
    private readonly Breadcrumb _breadcrumb;
    private readonly float _moveSpeed;

    public Walk(Animal animal, Breadcrumb breadcrumb, float moveSpeed)
    {
        _animal = animal;
        _breadcrumb = breadcrumb;
        _moveSpeed = moveSpeed;
    }

    public void OnEnter()
    {
        // Start animation
        _animal.ChangeAnimation(_animal.WalkAnimation);
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
        
            // Walk towards the breadcrumb
            _animal.transform.position = Vector3.MoveTowards(
                _animal.transform.position, 
                _breadcrumb.transform.position, 
                Time.deltaTime * _moveSpeed);
        }
    }
    
    public void OnExit() { }
}