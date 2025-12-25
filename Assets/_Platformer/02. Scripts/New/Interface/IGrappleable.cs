using UnityEngine;

namespace Platformer.New
{
    public interface IGrappleable
    {
        void ShootGrapple(Vector2 grapplePosition);

        void CutGrapple();

        bool IsGrappling { get; }
    }
}