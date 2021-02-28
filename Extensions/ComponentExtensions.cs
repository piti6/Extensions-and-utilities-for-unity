using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ComponentExtensions
{
    public static IEnumerable<T> GetComponentsInChildrenExcludeSelf<T>(this T source, bool includeInactive = false) where T : Component
    {
        return source.GetComponentsInChildren<T>(includeInactive: includeInactive)
            .Where(x => x != source);
    }

    public static T GetOrAddComponent<T>(this Component source) where T : Component
    {
        var target = source.GetComponent<T>();
        if (target == null)
        {
            target = source.gameObject.AddComponent<T>();
        }

        return target;
    }

    public static void SetGameObjectActive(this Component component, bool value)
    {
        component.gameObject.SetActive(value);
    }
}