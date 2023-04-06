using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public static Transform followTarget = null;
    public Vector3 OffSet = Vector3.zero;

    private void LateUpdate()
    {
        if(followTarget == null)
        {
            return;
        }

        transform.position = followTarget.position + OffSet;
    }

    public static void SetFollowTarget(Transform target)
    {
        followTarget = target;
    }
}
