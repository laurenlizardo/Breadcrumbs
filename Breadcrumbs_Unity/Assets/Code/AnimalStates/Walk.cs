using UnityEngine;

public class Walk : IState
{
    private readonly Animal _animal;
    private readonly Breadcrumb _breadcrumb;

    public Walk(Animal animal, Breadcrumb breadcrumb)
    {
        _animal = animal;
        _breadcrumb = breadcrumb;
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
            // Look at the breadcrumb the duration of the walk
            _animal.transform.LookAt(new Vector3(_breadcrumb.transform.position.x, 0, _breadcrumb.transform.position.z));
            
            // Start the NavMeshAgent
            _animal.NavMeshAgent.isStopped = false;
            _animal.NavMeshAgent.SetDestination(_breadcrumb.transform.position);
        }
    }

    public void OnExit()
    {
        // Stop the NavMeshAgent
        _animal.NavMeshAgent.velocity = Vector3.zero;
        _animal.NavMeshAgent.isStopped = true;
        _animal.transform.position = _animal.transform.position;
    }
}