using System;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [Header("Hard references")]
    // Reference to the breadcrumb gameobject
    [SerializeField] private Breadcrumb _breadcrumb;
    
    [Header("Speeds")]
    
    [SerializeField] private float _walkSpeed;
    
    [Header("Distances")]
    
    // The distance that the animal can detect a breadcrumb
    [SerializeField] private float _detectionDistance;
    
    // How far the animal should be from the breadcrumb to eat
    [SerializeField] private float _eatDistance;
    
    [Header("Times")]
    
    // The total reaction time
    [SerializeField] private float _totalReactionTime;
    private float _startReactionTime;   // This will be set to Time.time at the time of checking

    // The time it takes the animal to eat
    [SerializeField] private float _totalEatTime;
    private float _startEatTime;    // This will be set to Time.time at the time of checking
    
    [Header("Animation strings")]
    
    [SerializeField] private string _idleAnimation;
    public string IdleAnimation => _idleAnimation;
    [SerializeField] private string _reactAnimation;
    public string ReactAnimation => _reactAnimation;
    [SerializeField] private string _walkAnimation;
    public string WalkAnimation => _walkAnimation;
    [SerializeField] private string _eatAnimation;
    public string EatAnimation => _eatAnimation;

    // Private variables
    private StateMachine _stateMachine;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    
    // Properties
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        // States
        var idle = new Idle(this);
        var react = new React(this, _breadcrumb);
        var walk = new Walk(this, _breadcrumb, _walkSpeed);
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

    private void Update() => _stateMachine.Tick();

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void ChangeAnimation(string name) => _animator.Play(name);
}