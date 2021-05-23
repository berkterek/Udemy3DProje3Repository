using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Animations;
using UdemyProject3.Combats;
using UdemyProject3.Controllers;
using UnityEngine;

namespace UdemyProject3.Abstracts.Controllers
{
    public interface IEnemyController : IEntityController
    {
        IMover Mover { get; }
        InventoryController Inventory { get; }
        CharacterAnimation Animation { get;}
        public Dead Dead { get; }
        Transform Target { get; set; }
        float Magnitude { get; }
        void FindNearestTarget();
    }
}