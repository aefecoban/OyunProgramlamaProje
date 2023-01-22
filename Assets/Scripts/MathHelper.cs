using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper
{
    public static Vector3 Quadratic(Vector3 s, Vector3 m, Vector3 e, float t)
    {
        Vector3 f = Vector3.Lerp(s, m, t);
        Vector3 l = Vector3.Lerp(m, e, t);
        return Vector3.Lerp(f, l, t);
    }

    public static Vector3 QuadraticList(List<Vector3> x, float t)
    {
        List<Vector3> vector3s = new List<Vector3>();
        for (int i = 0; i < (x.Count - 1); i++)
        {
            vector3s.Add(
                Vector3.Lerp(x[i], x[i + 1], t)
            );
        }
        if (vector3s.Count == 2)
        {
            return Vector3.Lerp(vector3s[0], vector3s[1], t);
        }
        else
        {
            return QuadraticList(vector3s, t);
        }
    }

    public static Vector3 quadraticBezierCurve(Vector3 startPos, Vector3 midPos, Vector3 endPos, float time)
    {
        float u = 1 - time;
        float tt = time * time;
        float uu = u * u;

        Vector3 pos = uu * startPos;
        pos += 2 * u * time * midPos;
        pos += tt * endPos;

        return pos;
    }

    /*
     * //https://forum.unity.com/threads/clamping-angle-between-two-values.853771/#post-7726923
     */

    public static float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }

}
