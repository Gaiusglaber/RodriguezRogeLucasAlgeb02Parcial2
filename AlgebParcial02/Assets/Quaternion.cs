using System;
using UnityEngine.Internal;
using UnityEngine;

[Serializable]
public struct Quaternioncito : IEquatable<Quaternioncito>
{
	const float radToDeg = (float)(180.0 / Math.PI);
	const float degToRad = (float)(Math.PI / 180.0);

	public const float kEpsilon = 1E-06f; // should probably be used in the 0 tests in LookRotation or Slerp
	public Vector3 xyz
	{
		set
		{
			x = value.x;
			y = value.y;
			z = value.z;
		}
		get
		{
			return new Vector3(x, y, z);
		}
	}

	public float x;

	public float y;
	
	public float z;
	
	public float w;
	public static Quaternioncito identity
	{
		get
		{
			return new Quaternioncito(0f, 0f, 0f, 1f);
		}
	}
	public Vector3 eulerAngles
	{
		get
		{
			return Quaternioncito.ToEulerRad(this) * radToDeg;
		}
		set
		{
			this = Quaternioncito.FromEulerRad(value * degToRad);
		}
	}
	public float Length
	{
		get
		{
			return (float)System.Math.Sqrt(x * x + y * y + z * z + w * w);
		}
	}
	public float LengthSquared
	{
		get
		{
			return x * x + y * y + z * z + w * w;
		}
	}
	public Quaternioncito(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}
	public Quaternioncito(Vector3 v, float w)
	{
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
		this.w = w;
	}
	public void Set(float new_x, float new_y, float new_z, float new_w)
	{
		this.x = new_x;
		this.y = new_y;
		this.z = new_z;
		this.w = new_w;
	}
	public void Normalize()
	{
		float scale = 1.0f / this.Length;
		xyz *= scale;
		w *= scale;
	}
	public static Quaternioncito Normalize(Quaternioncito q)
	{
		Quaternioncito result;
		Normalize(ref q, out result);
		return result;
	}
	public static void Normalize(ref Quaternioncito q, out Quaternioncito result)
	{
		float scale = 1.0f / q.Length;
		result = new Quaternioncito(q.xyz * scale, q.w * scale);
	}
	public static float Dot(Quaternioncito a, Quaternioncito b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
	}
	public static Quaternioncito AngleAxis(float angle, Vector3 axis)
	{
		return Quaternioncito.AngleAxis(angle, ref axis);
	}
	private static Quaternioncito AngleAxis(float degress, ref Vector3 axis)
	{
		if (axis.sqrMagnitude == 0.0f)
			return identity;

		Quaternioncito result = identity;
		var radians = degress * degToRad;
		radians *= 0.5f;
		axis.Normalize();
		axis = axis * (float)System.Math.Sin(radians);
		result.x = axis.x;
		result.y = axis.y;
		result.z = axis.z;
		result.w = (float)System.Math.Cos(radians);

		return Normalize(result);
	}
	public void ToAngleAxis(out float angle, out Vector3 axis)
	{
		Quaternioncito.ToAxisAngleRad(this, out axis, out angle);
		angle *= radToDeg;
	}
	public static Quaternioncito FromToRotation(Vector3 fromDirection, Vector3 toDirection)
	{
		return RotateTowards(LookRotation(fromDirection), LookRotation(toDirection), float.MaxValue);
	}
	public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
	{
		this = Quaternioncito.FromToRotation(fromDirection, toDirection);
	}
	public static Quaternioncito LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
	{
		return Quaternioncito.LookRotation(ref forward, ref upwards);
	}
	public static Quaternioncito LookRotation(Vector3 forward)
	{
		Vector3 up = Vector3.up;
		return Quaternioncito.LookRotation(ref forward, ref up);
	}
	private static Quaternioncito LookRotation(ref Vector3 forward, ref Vector3 up)
	{

		forward = Vector3.Normalize(forward);
		Vector3 right = Vector3.Normalize(Vector3.Cross(up, forward));
		up = Vector3.Cross(forward, right);
		var m00 = right.x;
		var m01 = right.y;
		var m02 = right.z;
		var m10 = up.x;
		var m11 = up.y;
		var m12 = up.z;
		var m20 = forward.x;
		var m21 = forward.y;
		var m22 = forward.z;


		float num8 = (m00 + m11) + m22;
		var quaternion = new Quaternioncito();
		if (num8 > 0f)
		{
			var num = (float)System.Math.Sqrt(num8 + 1f);
			quaternion.w = num * 0.5f;
			num = 0.5f / num;
			quaternion.x = (m12 - m21) * num;
			quaternion.y = (m20 - m02) * num;
			quaternion.z = (m01 - m10) * num;
			return quaternion;
		}
		if ((m00 >= m11) && (m00 >= m22))
		{
			var num7 = (float)System.Math.Sqrt(((1f + m00) - m11) - m22);
			var num4 = 0.5f / num7;
			quaternion.x = 0.5f * num7;
			quaternion.y = (m01 + m10) * num4;
			quaternion.z = (m02 + m20) * num4;
			quaternion.w = (m12 - m21) * num4;
			return quaternion;
		}
		if (m11 > m22)
		{
			var num6 = (float)System.Math.Sqrt(((1f + m11) - m00) - m22);
			var num3 = 0.5f / num6;
			quaternion.x = (m10 + m01) * num3;
			quaternion.y = 0.5f * num6;
			quaternion.z = (m21 + m12) * num3;
			quaternion.w = (m20 - m02) * num3;
			return quaternion;
		}
		var num5 = (float)System.Math.Sqrt(((1f + m22) - m00) - m11);
		var num2 = 0.5f / num5;
		quaternion.x = (m20 + m02) * num2;
		quaternion.y = (m21 + m12) * num2;
		quaternion.z = 0.5f * num5;
		quaternion.w = (m01 - m10) * num2;
		return quaternion;
	}
	public void SetLookRotation(Vector3 view)
	{
		Vector3 up = Vector3.up;
		this.SetLookRotation(view, up);
	}
	public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
	{
		this = Quaternioncito.LookRotation(view, up);
	}
	public static Quaternioncito Slerp(Quaternioncito a, Quaternioncito b, float t)
	{
		return Quaternioncito.Slerp(ref a, ref b, t);
	}
	private static Quaternioncito Slerp(ref Quaternioncito a, ref Quaternioncito b, float t)
	{
		if (t > 1) t = 1;
		if (t < 0) t = 0;
		return SlerpUnclamped(ref a, ref b, t);
	}
	public static Quaternioncito SlerpUnclamped(Quaternioncito a, Quaternioncito b, float t)
	{
		return Quaternioncito.SlerpUnclamped(ref a, ref b, t);
	}
	private static Quaternioncito SlerpUnclamped(ref Quaternioncito a, ref Quaternioncito b, float t)
	{
		// if either input is zero, return the other.
		if (a.LengthSquared == 0.0f)
		{
			if (b.LengthSquared == 0.0f)
			{
				return identity;
			}
			return b;
		}
		else if (b.LengthSquared == 0.0f)
		{
			return a;
		}


		float cosHalfAngle = a.w * b.w + Vector3.Dot(a.xyz, b.xyz);

		if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
		{
			// angle = 0.0f, so just return one input.
			return a;
		}
		else if (cosHalfAngle < 0.0f)
		{
			b.xyz = -b.xyz;
			b.w = -b.w;
			cosHalfAngle = -cosHalfAngle;
		}

		float blendA;
		float blendB;
		if (cosHalfAngle < 0.99f)
		{
			// do proper slerp for big angles
			float halfAngle = (float)System.Math.Acos(cosHalfAngle);
			float sinHalfAngle = (float)System.Math.Sin(halfAngle);
			float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
			blendA = (float)System.Math.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
			blendB = (float)System.Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
		}
		else
		{
			// do lerp if angle is really small.
			blendA = 1.0f - t;
			blendB = t;
		}

		Quaternioncito result = new Quaternioncito(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
		if (result.LengthSquared > 0.0f)
			return Normalize(result);
		else
			return identity;
	}
	public static Quaternioncito Lerp(Quaternioncito a, Quaternioncito b, float t)
	{
		if (t > 1) t = 1;
		if (t < 0) t = 0;
		return Slerp(ref a, ref b, t); // TODO: use lerp not slerp, "Because quaternion works in 4D. Rotation in 4D are linear" ???
	}
	public static Quaternioncito LerpUnclamped(Quaternioncito a, Quaternioncito b, float t)
	{
		return Slerp(ref a, ref b, t);
	}
	public static Quaternioncito RotateTowards(Quaternioncito from, Quaternioncito to, float maxDegreesDelta)
	{
		float num = Quaternioncito.Angle(from, to);
		if (num == 0f)
		{
			return to;
		}
		float t = Math.Min(1f, maxDegreesDelta / num);
		return Quaternioncito.SlerpUnclamped(from, to, t);
	}
	public static Quaternioncito Inverse(Quaternioncito rotation)
	{
		float lengthSq = rotation.LengthSquared;
		if (lengthSq != 0.0)
		{
			float i = 1.0f / lengthSq;
			return new Quaternioncito(rotation.xyz * -i, rotation.w * i);
		}
		return rotation;
	}
	public override string ToString()
	{
		return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", this.x, this.y, this.z, this.w);
	}
	public string ToString(string format)
	{
		return string.Format("({0}, {1}, {2}, {3})", this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format));
	}
	public static float Angle(Quaternioncito a, Quaternioncito b)
	{
		float f = Quaternioncito.Dot(a, b);
		return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * radToDeg;
	}
	public static Quaternioncito Euler(float x, float y, float z)
	{
		return Quaternioncito.FromEulerRad(new Vector3((float)x, (float)y, (float)z) * degToRad);
	}
	public static Quaternioncito Euler(Vector3 euler)
	{
		return Quaternioncito.FromEulerRad(euler * degToRad);
	}
	private static Vector3 ToEulerRad(Quaternioncito rotation)
	{
		float sqw = rotation.w * rotation.w;
		float sqx = rotation.x * rotation.x;
		float sqy = rotation.y * rotation.y;
		float sqz = rotation.z * rotation.z;
		float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
		float test = rotation.x * rotation.w - rotation.y * rotation.z;
		Vector3 v;

		if (test > 0.4995f * unit)
		{ // singularity at north pole
			v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = Mathf.PI / 2;
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		if (test < -0.4995f * unit)
		{ // singularity at south pole
			v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = -Mathf.PI / 2;
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		Quaternioncito q = new Quaternioncito(rotation.w, rotation.z, rotation.x, rotation.y);
		v.y = (float)System.Math.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
		v.x = (float)System.Math.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
		v.z = (float)System.Math.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
		return NormalizeAngles(v * Mathf.Rad2Deg);
	}
	private static Vector3 NormalizeAngles(Vector3 angles)
	{
		angles.x = NormalizeAngle(angles.x);
		angles.y = NormalizeAngle(angles.y);
		angles.z = NormalizeAngle(angles.z);
		return angles;
	}
	private static float NormalizeAngle(float angle)
	{
		while (angle > 360)
			angle -= 360;
		while (angle < 0)
			angle += 360;
		return angle;
	}
	private static Quaternioncito FromEulerRad(Vector3 euler)
	{
		var yaw = euler.x;
		var pitch = euler.y;
		var roll = euler.z;
		float rollOver2 = roll * 0.5f;
		float sinRollOver2 = (float)System.Math.Sin((float)rollOver2);
		float cosRollOver2 = (float)System.Math.Cos((float)rollOver2);
		float pitchOver2 = pitch * 0.5f;
		float sinPitchOver2 = (float)System.Math.Sin((float)pitchOver2);
		float cosPitchOver2 = (float)System.Math.Cos((float)pitchOver2);
		float yawOver2 = yaw * 0.5f;
		float sinYawOver2 = (float)System.Math.Sin((float)yawOver2);
		float cosYawOver2 = (float)System.Math.Cos((float)yawOver2);
		Quaternioncito result;
		result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
		result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
		result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
		result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
		return result;

	}
	private static void ToAxisAngleRad(Quaternioncito q, out Vector3 axis, out float angle)
	{
		if (System.Math.Abs(q.w) > 1.0f)
			q.Normalize();
		angle = 2.0f * (float)System.Math.Acos(q.w); // angle
		float den = (float)System.Math.Sqrt(1.0 - q.w * q.w);
		if (den > 0.0001f)
		{
			axis = q.xyz / den;
		}
		else
		{
			axis = new Vector3(1, 0, 0);
		}
	}
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
	}
	public override bool Equals(object other)
	{
		if (!(other is Quaternioncito))
		{
			return false;
		}
		Quaternioncito quaternion = (Quaternioncito)other;
		return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
	}
	public bool Equals(Quaternioncito other)
	{
		return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
	}
	public static Quaternioncito operator *(Quaternioncito lhs, Quaternioncito rhs)
	{
		return new Quaternioncito(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
	}
	public static Vector3 operator *(Quaternioncito rotation, Vector3 point)
	{
		float num = rotation.x * 2f;
		float num2 = rotation.y * 2f;
		float num3 = rotation.z * 2f;
		float num4 = rotation.x * num;
		float num5 = rotation.y * num2;
		float num6 = rotation.z * num3;
		float num7 = rotation.x * num2;
		float num8 = rotation.x * num3;
		float num9 = rotation.y * num3;
		float num10 = rotation.w * num;
		float num11 = rotation.w * num2;
		float num12 = rotation.w * num3;
		Vector3 result;
		result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
		result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
		result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
		return result;
	}
	public static bool operator ==(Quaternioncito lhs, Quaternioncito rhs)
	{
		return Quaternioncito.Dot(lhs, rhs) > 0.999999f;
	}
	public static bool operator !=(Quaternioncito lhs, Quaternioncito rhs)
	{
		return Quaternioncito.Dot(lhs, rhs) <= 0.999999f;
	}
	#region CastUnity.Quaternion to Quaternioncito
	public static implicit operator Quaternion(Quaternioncito me)
	{
		return new Quaternion((float)me.x, (float)me.y, (float)me.z, (float)me.w);
	}
	public static implicit operator Quaternioncito(Quaternion other)
	{
		return new Quaternioncito((float)other.x, (float)other.y, (float)other.z, (float)other.w);
	}
	#endregion
}