using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Runtime.Data.Registries
{
    /// <summary>
    /// A registry is a class that keeps a permanent list of objects.
    /// Useful for loading things that are not necessarily in the scene.
    /// </summary>
    /// <typeparam name="T">The type of object contained in the registry</typeparam>
    public class Registry<T> : ScriptableObject where T : Object
    {
        #region Public Fields

        public List<T> Items = new List<T>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets an item from the registry by its hash
        /// </summary>
        /// <param name="itemHash">The hash of the item to search for</param>
        /// <returns>An item with a matching hash</returns>
        public T GetItemByHash(int itemHash)
        {
            return Items.FirstOrDefault(item => item.GetHashCode() == itemHash);
        }

        /// <summary>
        /// Adds an item to the registry, if it is not already in the registry.
        /// </summary>
        /// <param name="item">The item to add</param>
        public virtual void Add(T item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// Returns all items of the specified type
        /// </summary>
        /// <typeparam name="U">The desired type</typeparam>
        /// <returns>An array of items of the desired type</returns>
        public U[] GetAllItemsOfType<U>()
        {
            return Items.Where(item => item is U).Cast<U>().ToArray();
        }

        #endregion
        
        #region Protected Methods

#if UNITY_EDITOR
        [ContextMenu("Get All Items In Project")]
        protected virtual void GetAllProjectItems()
        {
            Items.Clear();
            
            foreach (T item in (T[])Resources.FindObjectsOfTypeAll(typeof(T)))
            {
                if (!EditorUtility.IsPersistent(item)) continue;
                Add(item);
            }
        }
#endif

        #endregion
    }
}