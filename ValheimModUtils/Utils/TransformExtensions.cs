using UnityEngine;

namespace MilkWyzardStudios.Valheim.Utils
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Extension method that finds the requested transform, but returns its
        /// associated <see cref="GameObject"/>.
        /// </summary>
        /// <param name="t">The transform to search within.</param>
        /// <param name="path">The path within the transform to search.</param>
        /// <returns>The <see cref="GameObject"/> assoicated with the requested transform.</returns>
        public static GameObject FindAsGameObject(this Transform t, string path)
        {
            GameObject obj = null;

            Transform child = t.Find(path);
            if (child != null)
                obj = child.gameObject;

            return obj;
        }
    }
}
