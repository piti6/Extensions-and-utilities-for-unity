using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject Clone(this GameObject obj, bool activate = false)
    {
        var item = UnityEngine.Object.Instantiate(obj, obj.transform.parent);
        if (activate)
        {
            item.SetActive(true);
        }

        return item;
    }

    public static T Clone<T>(this T obj, bool activate = false) where T : Component
    {
        var item = UnityEngine.Object.Instantiate(obj, obj.transform.parent);
        if (activate)
        {
            item.SetGameObjectActive(true);
        }

        return item;
    }
}