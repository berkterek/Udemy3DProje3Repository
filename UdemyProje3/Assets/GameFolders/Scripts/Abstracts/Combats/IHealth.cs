
namespace UdemyProject3.Abstracts.Combats
{
    public interface IHealth
    {
        bool IsDead { get; }
        void TakeDamage(int damage);
        event System.Action OnDead;
    }    
}

