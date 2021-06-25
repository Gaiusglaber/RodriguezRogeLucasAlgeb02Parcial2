using System;
using UnityEngine.Internal;
using UnityEngine;

public partial struct Matrixita4x4 : IEquatable<Matrixita4x4>
{
    public float m00;
    public float m10;
    public float m20;
    public float m30;
    public float m01;
    public float m11;
    public float m21;
    public float m31;
    public float m02;
    public float m12;
    public float m22;
    public float m32;
    public float m03;
    public float m13;
    public float m23;
    public float m33;

    public Matrixita4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
    {
        this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x; this.m03 = column3.x;
        this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y; this.m13 = column3.y;
        this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z; this.m23 = column3.z;
        this.m30 = column0.w; this.m31 = column1.w; this.m32 = column2.w; this.m33 = column3.w;
    }
    public float this[int row, int column]
    {
        get
        {
            return this[row + column * 4];
        }

        set
        {
            this[row + column * 4] = value;
        }
    }
    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return m00;
                case 1: return m10;
                case 2: return m20;
                case 3: return m30;
                case 4: return m01;
                case 5: return m11;
                case 6: return m21;
                case 7: return m31;
                case 8: return m02;
                case 9: return m12;
                case 10: return m22;
                case 11: return m32;
                case 12: return m03;
                case 13: return m13;
                case 14: return m23;
                case 15: return m33;
                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }

        set
        {
            switch (index)
            {
                case 0: 
                    m00 = value;
                    break;
                case 1:
                    m10 = value;
                    break;
                case 2:
                    m20 = value;
                    break;
                case 3:
                    m30 = value;
                    break;
                case 4:
                    m01 = value;
                    break;
                case 5:
                    m11 = value;
                    break;
                case 6:
                    m21 = value;
                    break;
                case 7:
                    m31 = value;
                    break;
                case 8:
                    m02 = value;
                    break;
                case 9:
                    m12 = value;
                    break;
                case 10:
                    m22 = value;
                    break;
                case 11:
                    m32 = value;
                    break;
                case 12:
                    m03 = value;
                    break;
                case 13:
                    m13 = value;
                    break;
                case 14:
                    m23 = value;
                    break;
                case 15:
                    m33 = value;
                    break;

                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }
    }
    public override bool Equals(object other)
    {
        if (!(other is Matrixita4x4)) return false;

        return Equals((Matrixita4x4)other);
    }
    public bool Equals(Matrixita4x4 other)
    {
        return GetColumn(0).Equals(other.GetColumn(0))
            && GetColumn(1).Equals(other.GetColumn(1))
            && GetColumn(2).Equals(other.GetColumn(2))
            && GetColumn(3).Equals(other.GetColumn(3));
    }
    public static Matrixita4x4 operator *(Matrixita4x4 lhs, Matrixita4x4 rhs)
    {
        Matrixita4x4 res;
        res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
        res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
        res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
        res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;

        res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
        res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
        res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
        res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;

        res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
        res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
        res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
        res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;

        res.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
        res.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
        res.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
        res.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;

        return res;
    }
    public static Vector4 operator *(Matrixita4x4 lhs, Vector4 vector)
    {
        Vector4 res;
        res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
        res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
        res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
        res.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
        return res;
    }
    public static bool operator ==(Matrixita4x4 lhs, Matrixita4x4 rhs)
    {
        return lhs.GetColumn(0) == rhs.GetColumn(0)
            && lhs.GetColumn(1) == rhs.GetColumn(1)
            && lhs.GetColumn(2) == rhs.GetColumn(2)
            && lhs.GetColumn(3) == rhs.GetColumn(3);
    }
    public static bool operator !=(Matrixita4x4 lhs, Matrixita4x4 rhs)
    {
        return !(lhs == rhs);
    }
    public Vector4 GetColumn(int index)
    {
        switch (index)
        {
            case 0: return new Vector4(m00, m10, m20, m30);
            case 1: return new Vector4(m01, m11, m21, m31);
            case 2: return new Vector4(m02, m12, m22, m32);
            case 3: return new Vector4(m03, m13, m23, m33);
            default:
                throw new IndexOutOfRangeException("Invalid column index!");
        }
    }
    public Vector4 GetRow(int index)
    {
        switch (index)
        {
            case 0: return new Vector4(m00, m01, m02, m03);
            case 1: return new Vector4(m10, m11, m12, m13);
            case 2: return new Vector4(m20, m21, m22, m23);
            case 3: return new Vector4(m30, m31, m32, m33);
            default:
                throw new IndexOutOfRangeException("Invalid row index!");
        }
    }
    public void SetColumn(int index, Vector4 column)
    {
        this[0, index] = column.x;
        this[1, index] = column.y;
        this[2, index] = column.z;
        this[3, index] = column.w;
    }
    public void SetRow(int index, Vector4 row)
    {
        this[index, 0] = row.x;
        this[index, 1] = row.y;
        this[index, 2] = row.z;
        this[index, 3] = row.w;
    }
    public Vector3 MultiplyPoint(Vector3 point)
    {
        Vector3 res;
        float w;
        res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
        res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
        res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
        w = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;

        w = 1F / w;
        res.x *= w;
        res.y *= w;
        res.z *= w;
        return res;
    }
    public Vector3 MultiplyPoint3x4(Vector3 point)
    {
        Vector3 res;
        res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
        res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
        res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
        return res;
    }
    public Vector3 MultiplyVector(Vector3 vector)
    {
        Vector3 res;
        res.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
        res.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
        res.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
        return res;
    }
    public static Matrixita4x4 Scale(Vector3 vector)
    {
        Matrixita4x4 m;
        m.m00 = vector.x; m.m01 = 0F; m.m02 = 0F; m.m03 = 0F;
        m.m10 = 0F; m.m11 = vector.y; m.m12 = 0F; m.m13 = 0F;
        m.m20 = 0F; m.m21 = 0F; m.m22 = vector.z; m.m23 = 0F;
        m.m30 = 0F; m.m31 = 0F; m.m32 = 0F; m.m33 = 1F;
        return m;
    }
    public static Matrixita4x4 Translate(Vector3 vector)
    {
        Matrixita4x4 m;
        m.m00 = 1F; m.m01 = 0F; m.m02 = 0F; m.m03 = vector.x;
        m.m10 = 0F; m.m11 = 1F; m.m12 = 0F; m.m13 = vector.y;
        m.m20 = 0F; m.m21 = 0F; m.m22 = 1F; m.m23 = vector.z;
        m.m30 = 0F; m.m31 = 0F; m.m32 = 0F; m.m33 = 1F;
        return m;
    }
    public static Matrixita4x4 Rotate(Quaternion q)
    {
        float x = q.x * 2.0F;
        float y = q.y * 2.0F;
        float z = q.z * 2.0F;
        float xx = q.x * x;
        float yy = q.y * y;
        float zz = q.z * z;
        float xy = q.x * y;
        float xz = q.x * z;
        float yz = q.y * z;
        float wx = q.w * x;
        float wy = q.w * y;
        float wz = q.w * z;
        Matrixita4x4 m;
        m.m00 = 1.0f - (yy + zz); m.m10 = xy + wz; m.m20 = xz - wy; m.m30 = 0.0F;
        m.m01 = xy - wz; m.m11 = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31 = 0.0F;
        m.m02 = xz + wy; m.m12 = yz - wx; m.m22 = 1.0f - (xx + yy); m.m32 = 0.0F;
        m.m03 = 0.0F; m.m13 = 0.0F; m.m23 = 0.0F; m.m33 = 1.0F;
        return m;
    }
    static readonly Matrixita4x4 zeroMatrix = new Matrixita4x4(new Vector4(0, 0, 0, 0),
        new Vector4(0, 0, 0, 0),
        new Vector4(0, 0, 0, 0),
        new Vector4(0, 0, 0, 0));
    public static Matrixita4x4 zero { get { return zeroMatrix; } }

    static readonly Matrixita4x4 identityMatrix = new Matrixita4x4(new Vector4(1, 0, 0, 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 1));
    public static Matrixita4x4 identity { get { return identityMatrix; } }
}
