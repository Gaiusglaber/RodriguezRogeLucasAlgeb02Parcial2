using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Vector3 angle = Vector3.zero;
    public Quaternioncito qx = Quaternioncito.identity;
    public Quaternioncito qy = Quaternioncito.identity;
    public Quaternioncito qz = Quaternioncito.identity;

    private void Update()
    {
        float sinAngleZ = Mathf.Sin(Mathf.Deg2Rad * angle.z * 0.5f);
        float cosAngleZ = Mathf.Cos(Mathf.Deg2Rad * angle.z * 0.5f);
        qz.Set(0, 0, sinAngleZ, cosAngleZ);

        float sinAngleX = Mathf.Sin(Mathf.Deg2Rad * angle.x * 0.5f);
        float cosAngleX = Mathf.Cos(Mathf.Deg2Rad * angle.x * 0.5f);
        qx.Set(sinAngleX,0,0,cosAngleX);

        float sinAngleY = Mathf.Sin(Mathf.Deg2Rad * angle.y * 0.5f);
        float cosAngleY = Mathf.Cos(Mathf.Deg2Rad * angle.y * 0.5f);
        qy.Set(0,sinAngleY,0,cosAngleY);

        transform.rotation = qy * qx * qz;
    }
}
