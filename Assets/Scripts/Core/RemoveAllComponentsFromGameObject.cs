using UnityEngine;

namespace Core
{
    [ExecuteInEditMode]
    public class RemoveAllComponentsFromGameObject : MonoBehaviour
    {
        [ContextMenu("Remove1")]
        private void Remove()
        {
            for (int i = 0; i < 10; i++)
            {
                RemoveInChildren(transform);
            }
        }
        
        private void RemoveInChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                DestroyImmediate(child.GetComponent<Collider>());
                RemoveInChildren(child);
            }
        }
    }
}