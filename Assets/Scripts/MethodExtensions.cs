using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class MethodExtensions
{

    /// <summary>
    /// Method to obtain a new Vector3 from an existing one with the data inputted
    /// Example: transform.position = objTransform.position.With(y: newY);
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        float newX = x.HasValue ? x.Value : original.x;
        float newY = y.HasValue ? y.Value : original.y;
        float newZ = z.HasValue ? z.Value : original.z;

        return new Vector3(newX, newY, newZ);
    }

    /// <summary>
    /// Return a random item from the list.
    /// Sampling with replacement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Finds the position closest to the given one.
    /// </summary>
    /// <param name="position">World position.</param>
    /// <param name="otherPositions">Other world positions.</param>
    /// <returns>Closest position.</returns>
    public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
    {
        var closest = Vector3.zero;
        var shortestDistance = Mathf.Infinity;

        foreach (var otherPosition in otherPositions)
        {
            var distance = (position - otherPosition).sqrMagnitude;

            if (distance < shortestDistance)
            {
                closest = otherPosition;
                shortestDistance = distance;
            }
        }

        return closest;
    }

    /// <summary>
    /// Checks whether a game object has a component of type T attached.
    /// </summary>
    /// <param name="gameObject">Game object.</param>
    /// <returns>True when component is attached.</returns>
    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() != null;
    }

    /// <summary>
    /// Attaches a component to the given component's game object.
    /// </summary>
    /// <param name="component">Component.</param>
    /// <returns>Newly attached component.</returns>
    public static T AddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Checks whether a component's game object has a component of type T attached.
    /// </summary>
    /// <param name="component">Component.</param>
    /// <returns>True when component is attached.</returns>
    public static bool HasComponent<T>(this Component component) where T : Component
    {
        return component.GetComponent<T>() != null;
    }

    /// <summary>
    /// Destroy all children of a GameObject recursively
    /// </summary>
    /// <param name="parent"></param>
    public static void DestroyChildren(this GameObject parent)
    {
        Transform[] children = new Transform[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
            children[i] = parent.transform.GetChild(i);
        for (int i = 0; i < children.Length; i++)
            GameObject.Destroy(children[i].gameObject);
    }

    /// <summary>
    /// Toggle the defined game object on and off
    /// </summary>
    /// <param name="gameObject"></param>
    public static void ToggleGameObject(this GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public static bool IsNullOrEmpty(this string str)
    {
        return String.IsNullOrEmpty(str);
    }

    public static string ToHex(this Color color)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(color);
    }

    public static Color ColorFromHexString(this string color)
    {
        if (color.IsNullOrEmpty()) return Color.white;

        Color result = Color.white;
        ColorUtility.TryParseHtmlString(color, out result);
        return result;
    }
}