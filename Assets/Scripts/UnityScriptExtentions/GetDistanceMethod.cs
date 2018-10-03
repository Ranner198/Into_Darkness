using UnityEngine;
using System.Collections;

public static class GetDistanceMethod
{
    public static float GetDistance(this Transform pos1, GameObject pos2)
    {
        float distance = -1;
        distance = (pos1.transform.position - pos2.transform.position).magnitude;
        return distance;
    }
    public static float GetDistance(this Transform pos1, Vector3 pos2)
    {
        float distance = -1;
        distance = (pos1.transform.position - pos2).magnitude;
        return distance;
    }

    public static Quaternion GetQuaternionDirection(this Transform pos1, GameObject pos2)
    {
        Vector3 dir = -(pos1.transform.position - pos2.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir * Mathf.Rad2Deg);
        return lookRot;
    }
    public static Quaternion GetQuaternionDirection(this Transform pos1, Vector3 pos2)
    {
        Vector3 dir = -(pos1.transform.position - pos2).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir * Mathf.Rad2Deg);
        return lookRot;
    }
    public static Vector3 GetDirection(this Transform pos1, GameObject pos2)
    {
        Vector3 dir = -(pos1.transform.position - pos2.transform.position).normalized;
        return dir;
    }
    public static Vector3 GetDirection(this Transform pos1, Vector3 pos2)
    {
        Vector3 dir = -(pos1.transform.position - pos2).normalized;
        return dir;
    }
}
