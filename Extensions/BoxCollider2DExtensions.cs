using UnityEngine;

namespace BoilerTools.Extensions
{
    public static class BoxCollider2DExtensions
    {
        /// <summary>
        /// Creates a child 3d box collider version of the 2d collider.
        /// </summary>
        public static BoxCollider To3DBoxCollider(this BoxCollider2D bc2d , float depth = 100)
        {
            // Get old collider bounds 
            Bounds bounds = bc2d.bounds;

            // Create new game object for our obj
            var obj = new GameObject();
            obj.transform.parent = bc2d.transform.parent;

            // Create new collider
            var newcollider = obj.AddComponent<BoxCollider>();

            // Populate
            newcollider.size = new Vector3(bounds.size.x, bounds.size.y, depth);
            newcollider.center = new Vector3(bounds.center.x, bounds.center.y, 0);

            // Return
            return newcollider;

        }
    }
}
