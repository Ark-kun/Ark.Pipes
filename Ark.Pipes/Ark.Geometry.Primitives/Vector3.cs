using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ark.Geometry.Primitives {
#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
    namespace Double {
#else
    using TFloat = System.Single;
    namespace Single {
#endif

#if !PORTABLE1
        [Serializable]
#endif
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector3 : IEquatable<Vector3> {
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat X;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat Y;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat Z;
            private static Vector3 _zero;
            private static Vector3 _one;
            private static Vector3 _unitX;
            private static Vector3 _unitY;
            private static Vector3 _unitZ;
            private static Vector3 _up;
            private static Vector3 _down;
            private static Vector3 _right;
            private static Vector3 _left;
            private static Vector3 _forward;
            private static Vector3 _backward;
            public static Vector3 Zero {
                get {
                    return _zero;
                }
            }
            public static Vector3 One {
                get {
                    return _one;
                }
            }
            public static Vector3 UnitX {
                get {
                    return _unitX;
                }
            }
            public static Vector3 UnitY {
                get {
                    return _unitY;
                }
            }
            public static Vector3 UnitZ {
                get {
                    return _unitZ;
                }
            }
            public static Vector3 Up {
                get {
                    return _up;
                }
            }
            public static Vector3 Down {
                get {
                    return _down;
                }
            }
            public static Vector3 Right {
                get {
                    return _right;
                }
            }
            public static Vector3 Left {
                get {
                    return _left;
                }
            }
            public static Vector3 Forward {
                get {
                    return _forward;
                }
            }
            public static Vector3 Backward {
                get {
                    return _backward;
                }
            }
            public Vector3(TFloat x, TFloat y, TFloat z) {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public Vector3(TFloat value) {
                this.X = this.Y = this.Z = value;
            }

            public Vector3(Vector2 value, TFloat z) {
                this.X = value.X;
                this.Y = value.Y;
                this.Z = z;
            }

            public override string ToString() {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}", new object[] { this.X.ToString(currentCulture), this.Y.ToString(currentCulture), this.Z.ToString(currentCulture) });
            }

            public bool Equals(Vector3 other) {
                return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
            }

            public override bool Equals(object obj) {
                bool flag = false;
                if (obj is Vector3) {
                    flag = this.Equals((Vector3)obj);
                }
                return flag;
            }

            public override int GetHashCode() {
                return ((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode());
            }

            public TFloat Length() {
                TFloat num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
                return (TFloat)Math.Sqrt((double)num);
            }

            public TFloat LengthSquared() {
                return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
            }

            public static TFloat Distance(Vector3 value1, Vector3 value2) {
                TFloat num3 = value1.X - value2.X;
                TFloat num2 = value1.Y - value2.Y;
                TFloat num = value1.Z - value2.Z;
                TFloat num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
                return (TFloat)Math.Sqrt((double)num4);
            }

            public static void Distance(ref Vector3 value1, ref Vector3 value2, out TFloat result) {
                TFloat num3 = value1.X - value2.X;
                TFloat num2 = value1.Y - value2.Y;
                TFloat num = value1.Z - value2.Z;
                TFloat num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
                result = (TFloat)Math.Sqrt((double)num4);
            }

            public static TFloat DistanceSquared(Vector3 value1, Vector3 value2) {
                TFloat num3 = value1.X - value2.X;
                TFloat num2 = value1.Y - value2.Y;
                TFloat num = value1.Z - value2.Z;
                return (((num3 * num3) + (num2 * num2)) + (num * num));
            }

            public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out TFloat result) {
                TFloat num3 = value1.X - value2.X;
                TFloat num2 = value1.Y - value2.Y;
                TFloat num = value1.Z - value2.Z;
                result = ((num3 * num3) + (num2 * num2)) + (num * num);
            }

            public static TFloat Dot(Vector3 vector1, Vector3 vector2) {
                return (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z));
            }

            public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out TFloat result) {
                result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
            }

            public void Normalize() {
                TFloat num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
                TFloat num = 1f / ((TFloat)Math.Sqrt((double)num2));
                this.X *= num;
                this.Y *= num;
                this.Z *= num;
            }

            public static Vector3 Normalize(Vector3 value) {
                Vector3 vector;
                TFloat num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
                TFloat num = 1f / ((TFloat)Math.Sqrt((double)num2));
                vector.X = value.X * num;
                vector.Y = value.Y * num;
                vector.Z = value.Z * num;
                return vector;
            }

            public static void Normalize(ref Vector3 value, out Vector3 result) {
                TFloat num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
                TFloat num = 1f / ((TFloat)Math.Sqrt((double)num2));
                result.X = value.X * num;
                result.Y = value.Y * num;
                result.Z = value.Z * num;
            }

            public static Vector3 Cross(Vector3 vector1, Vector3 vector2) {
                Vector3 vector;
                vector.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
                vector.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
                vector.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
                return vector;
            }

            public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result) {
                TFloat num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
                TFloat num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
                TFloat num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
                result.X = num3;
                result.Y = num2;
                result.Z = num;
            }

            public static Vector3 Reflect(Vector3 vector, Vector3 normal) {
                Vector3 vector2;
                TFloat num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
                vector2.X = vector.X - ((2f * num) * normal.X);
                vector2.Y = vector.Y - ((2f * num) * normal.Y);
                vector2.Z = vector.Z - ((2f * num) * normal.Z);
                return vector2;
            }

            public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result) {
                TFloat num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
                result.X = vector.X - ((2f * num) * normal.X);
                result.Y = vector.Y - ((2f * num) * normal.Y);
                result.Z = vector.Z - ((2f * num) * normal.Z);
            }

            public static Vector3 Min(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = (value1.X < value2.X) ? value1.X : value2.X;
                vector.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
                vector.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
                return vector;
            }

            public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = (value1.X < value2.X) ? value1.X : value2.X;
                result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
                result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
            }

            public static Vector3 Max(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = (value1.X > value2.X) ? value1.X : value2.X;
                vector.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
                vector.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
                return vector;
            }

            public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = (value1.X > value2.X) ? value1.X : value2.X;
                result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
                result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
            }

            public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max) {
                Vector3 vector;
                TFloat x = value1.X;
                x = (x > max.X) ? max.X : x;
                x = (x < min.X) ? min.X : x;
                TFloat y = value1.Y;
                y = (y > max.Y) ? max.Y : y;
                y = (y < min.Y) ? min.Y : y;
                TFloat z = value1.Z;
                z = (z > max.Z) ? max.Z : z;
                z = (z < min.Z) ? min.Z : z;
                vector.X = x;
                vector.Y = y;
                vector.Z = z;
                return vector;
            }

            public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result) {
                TFloat x = value1.X;
                x = (x > max.X) ? max.X : x;
                x = (x < min.X) ? min.X : x;
                TFloat y = value1.Y;
                y = (y > max.Y) ? max.Y : y;
                y = (y < min.Y) ? min.Y : y;
                TFloat z = value1.Z;
                z = (z > max.Z) ? max.Z : z;
                z = (z < min.Z) ? min.Z : z;
                result.X = x;
                result.Y = y;
                result.Z = z;
            }

            public static Vector3 Lerp(Vector3 value1, Vector3 value2, TFloat amount) {
                Vector3 vector;
                vector.X = value1.X + ((value2.X - value1.X) * amount);
                vector.Y = value1.Y + ((value2.Y - value1.Y) * amount);
                vector.Z = value1.Z + ((value2.Z - value1.Z) * amount);
                return vector;
            }

            public static void Lerp(ref Vector3 value1, ref Vector3 value2, TFloat amount, out Vector3 result) {
                result.X = value1.X + ((value2.X - value1.X) * amount);
                result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
                result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
            }

            public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, TFloat amount1, TFloat amount2) {
                Vector3 vector;
                vector.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
                vector.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
                vector.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
                return vector;
            }

            public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, TFloat amount1, TFloat amount2, out Vector3 result) {
                result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
                result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
                result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
            }

            public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, TFloat amount) {
                Vector3 vector;
                amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
                amount = (amount * amount) * (3f - (2f * amount));
                vector.X = value1.X + ((value2.X - value1.X) * amount);
                vector.Y = value1.Y + ((value2.Y - value1.Y) * amount);
                vector.Z = value1.Z + ((value2.Z - value1.Z) * amount);
                return vector;
            }

            public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, TFloat amount, out Vector3 result) {
                amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
                amount = (amount * amount) * (3f - (2f * amount));
                result.X = value1.X + ((value2.X - value1.X) * amount);
                result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
                result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
            }

            public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, TFloat amount) {
                Vector3 vector;
                TFloat num = amount * amount;
                TFloat num2 = amount * num;
                vector.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
                vector.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
                vector.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
                return vector;
            }

            public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, TFloat amount, out Vector3 result) {
                TFloat num = amount * amount;
                TFloat num2 = amount * num;
                result.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
                result.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
                result.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
            }

            public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, TFloat amount) {
                Vector3 vector;
                TFloat num = amount * amount;
                TFloat num2 = amount * num;
                TFloat num6 = ((2f * num2) - (3f * num)) + 1f;
                TFloat num5 = (-2f * num2) + (3f * num);
                TFloat num4 = (num2 - (2f * num)) + amount;
                TFloat num3 = num2 - num;
                vector.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
                vector.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
                vector.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
                return vector;
            }

            public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, TFloat amount, out Vector3 result) {
                TFloat num = amount * amount;
                TFloat num2 = amount * num;
                TFloat num6 = ((2f * num2) - (3f * num)) + 1f;
                TFloat num5 = (-2f * num2) + (3f * num);
                TFloat num4 = (num2 - (2f * num)) + amount;
                TFloat num3 = num2 - num;
                result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
                result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
                result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
            }

            public static Vector3 Transform(Vector3 position, Matrix matrix) {
                Vector3 vector;
                TFloat num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
                TFloat num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
                TFloat num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
                vector.X = num3;
                vector.Y = num2;
                vector.Z = num;
                return vector;
            }

            public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result) {
                TFloat num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
                TFloat num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
                TFloat num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
                result.X = num3;
                result.Y = num2;
                result.Z = num;
            }

            public static Vector3 TransformNormal(Vector3 normal, Matrix matrix) {
                Vector3 vector;
                TFloat num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
                TFloat num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
                TFloat num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
                vector.X = num3;
                vector.Y = num2;
                vector.Z = num;
                return vector;
            }

            public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result) {
                TFloat num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
                TFloat num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
                TFloat num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
                result.X = num3;
                result.Y = num2;
                result.Z = num;
            }

            public static Vector3 Transform(Vector3 value, Quaternion rotation) {
                Vector3 vector;
                TFloat num12 = rotation.X + rotation.X;
                TFloat num2 = rotation.Y + rotation.Y;
                TFloat num = rotation.Z + rotation.Z;
                TFloat num11 = rotation.W * num12;
                TFloat num10 = rotation.W * num2;
                TFloat num9 = rotation.W * num;
                TFloat num8 = rotation.X * num12;
                TFloat num7 = rotation.X * num2;
                TFloat num6 = rotation.X * num;
                TFloat num5 = rotation.Y * num2;
                TFloat num4 = rotation.Y * num;
                TFloat num3 = rotation.Z * num;
                TFloat num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
                TFloat num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
                TFloat num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
                vector.X = num15;
                vector.Y = num14;
                vector.Z = num13;
                return vector;
            }

            public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result) {
                TFloat num12 = rotation.X + rotation.X;
                TFloat num2 = rotation.Y + rotation.Y;
                TFloat num = rotation.Z + rotation.Z;
                TFloat num11 = rotation.W * num12;
                TFloat num10 = rotation.W * num2;
                TFloat num9 = rotation.W * num;
                TFloat num8 = rotation.X * num12;
                TFloat num7 = rotation.X * num2;
                TFloat num6 = rotation.X * num;
                TFloat num5 = rotation.Y * num2;
                TFloat num4 = rotation.Y * num;
                TFloat num3 = rotation.Z * num;
                TFloat num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
                TFloat num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
                TFloat num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
                result.X = num15;
                result.Y = num14;
                result.Z = num13;
            }

            public static void Transform(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (destinationArray.Length < sourceArray.Length) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                for (int i = 0; i < sourceArray.Length; i++) {
                    TFloat x = sourceArray[i].X;
                    TFloat y = sourceArray[i].Y;
                    TFloat z = sourceArray[i].Z;
                    destinationArray[i].X = (((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31)) + matrix.M41;
                    destinationArray[i].Y = (((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32)) + matrix.M42;
                    destinationArray[i].Z = (((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33)) + matrix.M43;
                }
            }

            [SuppressMessage("Microsoft.Usage", "CA2233")]
            public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (sourceArray.Length < (sourceIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughSourceSize");
                }
                if (destinationArray.Length < (destinationIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                while (length > 0) {
                    TFloat x = sourceArray[sourceIndex].X;
                    TFloat y = sourceArray[sourceIndex].Y;
                    TFloat z = sourceArray[sourceIndex].Z;
                    destinationArray[destinationIndex].X = (((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31)) + matrix.M41;
                    destinationArray[destinationIndex].Y = (((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32)) + matrix.M42;
                    destinationArray[destinationIndex].Z = (((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33)) + matrix.M43;
                    sourceIndex++;
                    destinationIndex++;
                    length--;
                }
            }

            public static void TransformNormal(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (destinationArray.Length < sourceArray.Length) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                for (int i = 0; i < sourceArray.Length; i++) {
                    TFloat x = sourceArray[i].X;
                    TFloat y = sourceArray[i].Y;
                    TFloat z = sourceArray[i].Z;
                    destinationArray[i].X = ((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31);
                    destinationArray[i].Y = ((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32);
                    destinationArray[i].Z = ((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33);
                }
            }

            [SuppressMessage("Microsoft.Usage", "CA2233")]
            public static void TransformNormal(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (sourceArray.Length < (sourceIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughSourceSize");
                }
                if (destinationArray.Length < (destinationIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                while (length > 0) {
                    TFloat x = sourceArray[sourceIndex].X;
                    TFloat y = sourceArray[sourceIndex].Y;
                    TFloat z = sourceArray[sourceIndex].Z;
                    destinationArray[destinationIndex].X = ((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31);
                    destinationArray[destinationIndex].Y = ((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32);
                    destinationArray[destinationIndex].Z = ((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33);
                    sourceIndex++;
                    destinationIndex++;
                    length--;
                }
            }

            public static void Transform(Vector3[] sourceArray, ref Quaternion rotation, Vector3[] destinationArray) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (destinationArray.Length < sourceArray.Length) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                TFloat num16 = rotation.X + rotation.X;
                TFloat num6 = rotation.Y + rotation.Y;
                TFloat num2 = rotation.Z + rotation.Z;
                TFloat num15 = rotation.W * num16;
                TFloat num14 = rotation.W * num6;
                TFloat num13 = rotation.W * num2;
                TFloat num12 = rotation.X * num16;
                TFloat num11 = rotation.X * num6;
                TFloat num10 = rotation.X * num2;
                TFloat num9 = rotation.Y * num6;
                TFloat num8 = rotation.Y * num2;
                TFloat num7 = rotation.Z * num2;
                TFloat num25 = (1f - num9) - num7;
                TFloat num24 = num11 - num13;
                TFloat num23 = num10 + num14;
                TFloat num22 = num11 + num13;
                TFloat num21 = (1f - num12) - num7;
                TFloat num20 = num8 - num15;
                TFloat num19 = num10 - num14;
                TFloat num18 = num8 + num15;
                TFloat num17 = (1f - num12) - num9;
                for (int i = 0; i < sourceArray.Length; i++) {
                    TFloat x = sourceArray[i].X;
                    TFloat y = sourceArray[i].Y;
                    TFloat z = sourceArray[i].Z;
                    destinationArray[i].X = ((x * num25) + (y * num24)) + (z * num23);
                    destinationArray[i].Y = ((x * num22) + (y * num21)) + (z * num20);
                    destinationArray[i].Z = ((x * num19) + (y * num18)) + (z * num17);
                }
            }

            [SuppressMessage("Microsoft.Usage", "CA2233")]
            public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector3[] destinationArray, int destinationIndex, int length) {
                if (sourceArray == null) {
                    throw new ArgumentNullException("sourceArray");
                }
                if (destinationArray == null) {
                    throw new ArgumentNullException("destinationArray");
                }
                if (sourceArray.Length < (sourceIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughSourceSize");
                }
                if (destinationArray.Length < (destinationIndex + length)) {
                    throw new ArgumentException("FrameworkResources.NotEnoughTargetSize");
                }
                TFloat num15 = rotation.X + rotation.X;
                TFloat num5 = rotation.Y + rotation.Y;
                TFloat num = rotation.Z + rotation.Z;
                TFloat num14 = rotation.W * num15;
                TFloat num13 = rotation.W * num5;
                TFloat num12 = rotation.W * num;
                TFloat num11 = rotation.X * num15;
                TFloat num10 = rotation.X * num5;
                TFloat num9 = rotation.X * num;
                TFloat num8 = rotation.Y * num5;
                TFloat num7 = rotation.Y * num;
                TFloat num6 = rotation.Z * num;
                TFloat num24 = (1f - num8) - num6;
                TFloat num23 = num10 - num12;
                TFloat num22 = num9 + num13;
                TFloat num21 = num10 + num12;
                TFloat num20 = (1f - num11) - num6;
                TFloat num19 = num7 - num14;
                TFloat num18 = num9 - num13;
                TFloat num17 = num7 + num14;
                TFloat num16 = (1f - num11) - num8;
                while (length > 0) {
                    TFloat x = sourceArray[sourceIndex].X;
                    TFloat y = sourceArray[sourceIndex].Y;
                    TFloat z = sourceArray[sourceIndex].Z;
                    destinationArray[destinationIndex].X = ((x * num24) + (y * num23)) + (z * num22);
                    destinationArray[destinationIndex].Y = ((x * num21) + (y * num20)) + (z * num19);
                    destinationArray[destinationIndex].Z = ((x * num18) + (y * num17)) + (z * num16);
                    sourceIndex++;
                    destinationIndex++;
                    length--;
                }
            }

            public static Vector3 Negate(Vector3 value) {
                Vector3 vector;
                vector.X = -value.X;
                vector.Y = -value.Y;
                vector.Z = -value.Z;
                return vector;
            }

            public static void Negate(ref Vector3 value, out Vector3 result) {
                result.X = -value.X;
                result.Y = -value.Y;
                result.Z = -value.Z;
            }

            public static Vector3 Add(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X + value2.X;
                vector.Y = value1.Y + value2.Y;
                vector.Z = value1.Z + value2.Z;
                return vector;
            }

            public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = value1.X + value2.X;
                result.Y = value1.Y + value2.Y;
                result.Z = value1.Z + value2.Z;
            }

            public static Vector3 Subtract(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X - value2.X;
                vector.Y = value1.Y - value2.Y;
                vector.Z = value1.Z - value2.Z;
                return vector;
            }

            public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = value1.X - value2.X;
                result.Y = value1.Y - value2.Y;
                result.Z = value1.Z - value2.Z;
            }

            public static Vector3 Multiply(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X * value2.X;
                vector.Y = value1.Y * value2.Y;
                vector.Z = value1.Z * value2.Z;
                return vector;
            }

            public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = value1.X * value2.X;
                result.Y = value1.Y * value2.Y;
                result.Z = value1.Z * value2.Z;
            }

            public static Vector3 Multiply(Vector3 value1, TFloat scaleFactor) {
                Vector3 vector;
                vector.X = value1.X * scaleFactor;
                vector.Y = value1.Y * scaleFactor;
                vector.Z = value1.Z * scaleFactor;
                return vector;
            }

            public static void Multiply(ref Vector3 value1, TFloat scaleFactor, out Vector3 result) {
                result.X = value1.X * scaleFactor;
                result.Y = value1.Y * scaleFactor;
                result.Z = value1.Z * scaleFactor;
            }

            public static Vector3 Divide(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X / value2.X;
                vector.Y = value1.Y / value2.Y;
                vector.Z = value1.Z / value2.Z;
                return vector;
            }

            public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
                result.X = value1.X / value2.X;
                result.Y = value1.Y / value2.Y;
                result.Z = value1.Z / value2.Z;
            }

            public static Vector3 Divide(Vector3 value1, TFloat value2) {
                Vector3 vector;
                TFloat num = 1f / value2;
                vector.X = value1.X * num;
                vector.Y = value1.Y * num;
                vector.Z = value1.Z * num;
                return vector;
            }

            public static void Divide(ref Vector3 value1, TFloat value2, out Vector3 result) {
                TFloat num = 1f / value2;
                result.X = value1.X * num;
                result.Y = value1.Y * num;
                result.Z = value1.Z * num;
            }

            public static Vector3 operator -(Vector3 value) {
                Vector3 vector;
                vector.X = -value.X;
                vector.Y = -value.Y;
                vector.Z = -value.Z;
                return vector;
            }

            public static bool operator ==(Vector3 value1, Vector3 value2) {
                return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
            }

            public static bool operator !=(Vector3 value1, Vector3 value2) {
                if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                    return !(value1.Z == value2.Z);
                }
                return true;
            }

            public static Vector3 operator +(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X + value2.X;
                vector.Y = value1.Y + value2.Y;
                vector.Z = value1.Z + value2.Z;
                return vector;
            }

            public static Vector3 operator -(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X - value2.X;
                vector.Y = value1.Y - value2.Y;
                vector.Z = value1.Z - value2.Z;
                return vector;
            }

            public static Vector3 operator *(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X * value2.X;
                vector.Y = value1.Y * value2.Y;
                vector.Z = value1.Z * value2.Z;
                return vector;
            }

            public static Vector3 operator *(Vector3 value, TFloat scaleFactor) {
                Vector3 vector;
                vector.X = value.X * scaleFactor;
                vector.Y = value.Y * scaleFactor;
                vector.Z = value.Z * scaleFactor;
                return vector;
            }

            public static Vector3 operator *(TFloat scaleFactor, Vector3 value) {
                Vector3 vector;
                vector.X = value.X * scaleFactor;
                vector.Y = value.Y * scaleFactor;
                vector.Z = value.Z * scaleFactor;
                return vector;
            }

            public static Vector3 operator /(Vector3 value1, Vector3 value2) {
                Vector3 vector;
                vector.X = value1.X / value2.X;
                vector.Y = value1.Y / value2.Y;
                vector.Z = value1.Z / value2.Z;
                return vector;
            }

            public static Vector3 operator /(Vector3 value, TFloat divider) {
                Vector3 vector;
                TFloat num = 1f / divider;
                vector.X = value.X * num;
                vector.Y = value.Y * num;
                vector.Z = value.Z * num;
                return vector;
            }

            static Vector3() {
                _zero = new Vector3();
                _one = new Vector3(1f, 1f, 1f);
                _unitX = new Vector3(1f, 0f, 0f);
                _unitY = new Vector3(0f, 1f, 0f);
                _unitZ = new Vector3(0f, 0f, 1f);
                _up = new Vector3(0f, 1f, 0f);
                _down = new Vector3(0f, -1f, 0f);
                _right = new Vector3(1f, 0f, 0f);
                _left = new Vector3(-1f, 0f, 0f);
                _forward = new Vector3(0f, 0f, -1f);
                _backward = new Vector3(0f, 0f, 1f);
            }
        }
    }
}