using System;

namespace Core.RaycastingEssence
{
    public interface IRaycastTarget
    {
        Action OnStartClick { get; }
        Action OnEndClick { get; }
    }
}