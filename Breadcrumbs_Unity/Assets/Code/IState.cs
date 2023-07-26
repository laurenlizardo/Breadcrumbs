public interface IState
{
    // Use for setup, initializations.
    void OnEnter();
    
    // Use in the Update loop
    void Tick();
    
    // Use for resetting values.
    void OnExit();
}