using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectVisible
{
    public static bool IsVisible(this GameObject obj, Camera c)
    {
        if(!obj.activeSelf) return false;

        Renderer r;
        if(!obj.TryGetComponent<Renderer>(out r)) return false;

        var isInCamera = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(c), r.bounds);
        if(!isInCamera) return false;

        RaycastHit hitInfo;
        Physics.Raycast(obj.transform.position, 
            (c.transform.position - obj.transform.position).normalized,
            out hitInfo,
            Vector3.Distance(c.transform.position, obj.transform.position),
            ~LayerMask.GetMask("Player", "HandCamera", "Ignore Raycast")
            );

        if(hitInfo.collider != null) 
            return false;
        
        return true;
    }
}
