using UnityEngine;

namespace BoilerTools.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroy all children. Todo, time/alloc test foreach & clone destroy speeds. Testing in unity 5.4.2 on PC shows that the For loop is roughly twice as fast as the Foreach counterpart.
        /// </summary>
        public static void DestroyAllChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
