using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityTK;

public static class Util
{

    private static int isPointerOverUICacheFrame;
    private static bool isPointerOverUICache;

    /// <summary>
    /// Returns true if the pointer is currently over a ui object.
    /// </summary>
    /// <param name="go">The game object below the pointer.</param>
    /// <returns>True if the pointer is over any UI element, otherwise false.</returns>
    public static bool IsPointerOverUI()
    {
        GameObject go;
        return IsPointerOverUI(out go);
    }

    /// <summary>
    /// Returns true if the pointer is currently over a ui object.
    /// </summary>
    /// <param name="go">The game object below the pointer.</param>
    /// <returns>True if the pointer is over any UI element, otherwise false.</returns>
    public static bool IsPointerOverUI(out GameObject go)
    {
        go = null;

        if (EventSystem.current == null)
            return false;

        // Cache
        if (Time.frameCount == isPointerOverUICacheFrame)
            return isPointerOverUICache;

        isPointerOverUICacheFrame = Time.frameCount;

        // Check
        if (EventSystem.current.IsPointerOverGameObject())
        {
            List<RaycastResult> rr = ListPool<RaycastResult>.Get();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            }, rr);

            for (int i = 0; i < rr.Count; i++)
            {
                var r = rr[i].gameObject;
                if (r.transform is RectTransform)
                {
                    ListPool<RaycastResult>.Return(rr);

                    go = r.gameObject;
                    isPointerOverUICache = true;

                    return true;
                }
            }

            isPointerOverUICache = false;
            ListPool<RaycastResult>.Return(rr);
        }
        isPointerOverUICache = false;
        return false;
    }

    public static bool IsDone(this NavMeshAgent agent)
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }
}