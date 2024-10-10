using RPG.Core;

namespace RPG.Control
{
    public interface IRayCastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController player);
    }

}