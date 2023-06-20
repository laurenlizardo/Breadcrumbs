public interface IState
{
    // Use for setup, initializiations.
    void OnEnter();
    
    // Use in the Update loop
    void Tick();
    
    // Use for resetting values.
    void OnExit();
}