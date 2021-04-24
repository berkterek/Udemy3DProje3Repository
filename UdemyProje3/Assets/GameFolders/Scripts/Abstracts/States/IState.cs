
namespace UdemyProject3.Abstracts.States
{
    public interface IState
    {
        void Tick();
        void OnExit();
        void OnEnter();
        
    }    
}
