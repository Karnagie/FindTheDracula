using System.ComponentModel;
using Core.RaycastingEssence;

namespace Core.RayCastingEssence
{
    public interface IRaycastHint
    {
        void Hint();
        void Unhint();
    }
}