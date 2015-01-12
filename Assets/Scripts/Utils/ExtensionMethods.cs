using UnityEngine;
using System;

public static class ExtensionMethods
{
    public static void RemoveChildren(this Transform transform)
    {
        Transform[] children = new Transform[transform.childCount];

        for (int childIndex = 0; childIndex < transform.childCount; ++childIndex)
        {
            children[childIndex] = transform.GetChild(childIndex);
        }

        foreach (Transform child in children)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static float PixelRation(this Camera camera)
    {
        return (camera.orthographicSize * 2) / camera.pixelHeight;
    }

    public static float PixelsToWorld(this Camera camera, int pixels)
    {
        float world;

        world = pixels * PixelRation(camera);

        MS.Debug.Core.Log("Converting " + pixels + " pixels to " + world + " world units.");

        return world;
    }
}

