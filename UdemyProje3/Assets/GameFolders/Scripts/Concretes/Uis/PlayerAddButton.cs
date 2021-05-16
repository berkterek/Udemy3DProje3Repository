using UdemyProject3.Abstracts.Uis;
using UdemyProject3.Managers;

namespace UdemyProject3.Uis
{
    public class PlayerAddButton : MyButton
    {
        protected override void HandleOnButtonClicked()
        {
            GameManager.Instance.IncreasePlayerCount();
        }
    }    
}

