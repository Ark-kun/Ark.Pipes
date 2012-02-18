using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

using TType = System.Double;

namespace Ark.Borrowed.Net.Microsoft.Xna.Framework._Double {
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3> {
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType X;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType Y;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType Z;
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
        public Vector3(TType x, TType y, TType z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(TType value) {
            this.X = this.Y = this.Z = value;
        }

        public Vector3(Vector2 value, TType z) {
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

        public TType Length() {
            TType num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
            return (TType)Math.Sqrt((double)num);
        }

        public TType LengthSquared() {
            return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
        }

        public static TType Distance(Vector3 value1, Vector3 value2) {
            TType num3 = value1.X - value2.X;
            TType num2 = value1.Y - value2.Y;
            TType num = value1.Z - value2.Z;
            TType num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
            return (TType)Math.Sqrt((double)num4);
        }

        public static void Distance(ref Vector3 value1, ref Vector3 value2, out TType result) {
            TType num3 = value1.X - value2.X;
            TType num2 = value1.Y - value2.Y;
            TType num = value1.Z - value2.Z;
            TType num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
            result = (TType)Math.Sqrt((double)num4);
        }

        public static TType DistanceSquared(Vector3 value1, Vector3 value2) {
            TType num3 = value1.X - value2.X;
            TType num2 = value1.Y - value2.Y;
            TType num = value1.Z - value2.Z;
            return (((num3 * num3) + (num2 * num2)) + (num * num));
        }

        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out TType result) {
            TType num3 = value1.X - value2.X;
            TType num2 = value1.Y - value2.Y;
            TType num = value1.Z - value2.Z;
            result = ((num3 * num3) + (num2 * num2)) + (num * num);
        }

        public static TType Dot(Vector3 vector1, Vector3 vector2) {
            return (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z));
        }

        public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out TType result) {
            result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
        }

        public void Normalize() {
            TType num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
            this.X *= num;
            this.Y *= num;
            this.Z *= num;
        }

        public static Vector3 Normalize(Vector3 value) {
            Vector3 vector;
            TType num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
            vector.X = value.X * num;
            vector.Y = value.Y * num;
            vector.Z = value.Z * num;
            return vector;
        }

        public static void Normalize(ref Vector3 value, out Vector3 result) {
            TType num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
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
            TType num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            TType num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            TType num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
            result.X = num3;
            result.Y = num2;
            result.Z = num;
        }

        public static Vector3 Reflect(Vector3 vector, Vector3 normal) {
            Vector3 vector2;
            TType num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
            vector2.X = vector.X - ((2f * num) * normal.X);
            vector2.Y = vector.Y - ((2f * num) * normal.Y);
            vector2.Z = vector.Z - ((2f * num) * normal.Z);
            return vector2;
        }

        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result) {
            TType num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
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
            TType x = value1.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            TType y = value1.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            TType z = value1.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            vector.X = x;
            vector.Y = y;
            vector.Z = z;
            return vector;
        }

        public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result) {
            TType x = value1.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            TType y = value1.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            TType z = value1.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        public static Vector3 Lerp(Vector3 value1, Vector3 value2, TType amount) {
            Vector3 vector;
            vector.X = value1.X + ((value2.X - value1.X) * amount);
            vector.Y = value1.Y + ((value2.Y - value1.Y) * amount);
            vector.Z = value1.Z + ((value2.Z - value1.Z) * amount);
            return vector;
        }

        public static void Lerp(ref Vector3 value1, ref Vector3 value2, TType amount, out Vector3 result) {
            result.X = value1.X + ((value2.X - value1.X) * amount);
            result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
            result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
        }

        public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, TType amount1, TType amount2) {
            Vector3 vector;
            vector.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
            vector.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
            vector.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
            return vector;
        }

        public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, TType amount1, TType amount2, out Vector3 result) {
            result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
            result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
            result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
        }

        public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, TType amount) {
            Vector3 vector;
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            vector.X = value1.X + ((value2.X - value1.X) * amount);
            vector.Y = value1.Y + ((value2.Y - value1.Y) * amount);
            vector.Z = value1.Z + ((value2.Z - value1.Z) * amount);
            return vector;
        }

        public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, TType amount, out Vector3 result) {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.X = value1.X + ((value2.X - value1.X) * amount);
            result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
            result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
        }

        public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, TType amount) {
            Vector3 vector;
            TType num = amount * amount;
            TType num2 = amount * num;
            vector.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
            vector.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
            vector.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
            return vector;
        }

        public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, TType amount, out Vector3 result) {
            TType num = amount * amount;
            TType num2 = amount * num;
            result.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
            result.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
            result.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
        }

        public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, TType amount) {
            Vector3 vector;
            TType num = amount * amount;
            TType num2 = amount * num;
            TType num6 = ((2f * num2) - (3f * num)) + 1f;
            TType num5 = (-2f * num2) + (3f * num);
            TType num4 = (num2 - (2f * num)) + amount;
            TType num3 = num2 - num;
            vector.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
            vector.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
            vector.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
            return vector;
        }

        public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, TType amount, out Vector3 result) {
            TType num = amount * amount;
            TType num2 = amount * num;
            TType num6 = ((2f * num2) - (3f * num)) + 1f;
            TType num5 = (-2f * num2) + (3f * num);
            TType num4 = (num2 - (2f * num)) + amount;
            TType num3 = num2 - num;
            result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
            result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
            result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
        }

        public static Vector3 Transform(Vector3 position, Matrix matrix) {
            Vector3 vector;
            TType num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
            TType num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
            TType num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
            vector.X = num3;
            vector.Y = num2;
            vector.Z = num;
            return vector;
        }

        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result) {
            TType num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
            TType num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
            TType num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
            result.X = num3;
            result.Y = num2;
            result.Z = num;
        }

        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix) {
            Vector3 vector;
            TType num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
            TType num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
            TType num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
            vector.X = num3;
            vector.Y = num2;
            vector.Z = num;
            return vector;
        }

        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result) {
            TType num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
            TType num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
            TType num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
            result.X = num3;
            result.Y = num2;
            result.Z = num;
        }

        public static Vector3 Transform(Vector3 value, Quaternion rotation) {
            Vector3 vector;
            TType num12 = rotation.X + rotation.X;
            TType num2 = rotation.Y + rotation.Y;
            TType num = rotation.Z + rotation.Z;
            TType num11 = rotation.W * num12;
            TType num10 = rotation.W * num2;
            TType num9 = rotation.W * num;
            TType num8 = rotation.X * num12;
            TType num7 = rotation.X * num2;
            TType num6 = rotation.X * num;
            TType num5 = rotation.Y * num2;
            TType num4 = rotation.Y * num;
            TType num3 = rotation.Z * num;
            TType num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            TType num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            TType num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
            vector.X = num15;
            vector.Y = num14;
            vector.Z = num13;
            return vector;
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result) {
            TType num12 = rotation.X + rotation.X;
            TType num2 = rotation.Y + rotation.Y;
            TType num = rotation.Z + rotation.Z;
            TType num11 = rotation.W * num12;
            TType num10 = rotation.W * num2;
            TType num9 = rotation.W * num;
            TType num8 = rotation.X * num12;
            TType num7 = rotation.X * num2;
            TType num6 = rotation.X * num;
            TType num5 = rotation.Y * num2;
            TType num4 = rotation.Y * num;
            TType num3 = rotation.Z * num;
            TType num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            TType num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            TType num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
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
                TType x = sourceArray[i].X;
                TType y = sourceArray[i].Y;
                TType z = sourceArray[i].Z;
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
                TType x = sourceArray[sourceIndex].X;
                TType y = sourceArray[sourceIndex].Y;
                TType z = sourceArray[sourceIndex].Z;
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
                TType x = sourceArray[i].X;
                TType y = sourceArray[i].Y;
                TType z = sourceArray[i].Z;
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
                TType x = sourceArray[sourceIndex].X;
                TType y = sourceArray[sourceIndex].Y;
                TType z = sourceArray[sourceIndex].Z;
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
            TType num16 = rotation.X + rotation.X;
            TType num6 = rotation.Y + rotation.Y;
            TType num2 = rotation.Z + rotation.Z;
            TType num15 = rotation.W * num16;
            TType num14 = rotation.W * num6;
            TType num13 = rotation.W * num2;
            TType num12 = rotation.X * num16;
            TType num11 = rotation.X * num6;
            TType num10 = rotation.X * num2;
            TType num9 = rotation.Y * num6;
            TType num8 = rotation.Y * num2;
            TType num7 = rotation.Z * num2;
            TType num25 = (1f - num9) - num7;
            TType num24 = num11 - num13;
            TType num23 = num10 + num14;
            TType num22 = num11 + num13;
            TType num21 = (1f - num12) - num7;
            TType num20 = num8 - num15;
            TType num19 = num10 - num14;
            TType num18 = num8 + num15;
            TType num17 = (1f - num12) - num9;
            for (int i = 0; i < sourceArray.Length; i++) {
                TType x = sourceArray[i].X;
                TType y = sourceArray[i].Y;
                TType z = sourceArray[i].Z;
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
            TType num15 = rotation.X + rotation.X;
            TType num5 = rotation.Y + rotation.Y;
            TType num = rotation.Z + rotation.Z;
            TType num14 = rotation.W * num15;
            TType num13 = rotation.W * num5;
            TType num12 = rotation.W * num;
            TType num11 = rotation.X * num15;
            TType num10 = rotation.X * num5;
            TType num9 = rotation.X * num;
            TType num8 = rotation.Y * num5;
            TType num7 = rotation.Y * num;
            TType num6 = rotation.Z * num;
            TType num24 = (1f - num8) - num6;
            TType num23 = num10 - num12;
            TType num22 = num9 + num13;
            TType num21 = num10 + num12;
            TType num20 = (1f - num11) - num6;
            TType num19 = num7 - num14;
            TType num18 = num9 - num13;
            TType num17 = num7 + num14;
            TType num16 = (1f - num11) - num8;
            while (length > 0) {
                TType x = sourceArray[sourceIndex].X;
                TType y = sourceArray[sourceIndex].Y;
                TType z = sourceArray[sourceIndex].Z;
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

        public static Vector3 Multiply(Vector3 value1, TType scaleFactor) {
            Vector3 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            return vector;
        }

        public static void Multiply(ref Vector3 value1, TType scaleFactor, out Vector3 result) {
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

        public static Vector3 Divide(Vector3 value1, TType value2) {
            Vector3 vector;
            TType num = 1f / value2;
            vector.X = value1.X * num;
            vector.Y = value1.Y * num;
            vector.Z = value1.Z * num;
            return vector;
        }

        public static void Divide(ref Vector3 value1, TType value2, out Vector3 result) {
            TType num = 1f / value2;
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

        public static Vector3 operator *(Vector3 value, TType scaleFactor) {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        public static Vector3 operator *(TType scaleFactor, Vector3 value) {
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

        public static Vector3 operator /(Vector3 value, TType divider) {
            Vector3 vector;
            TType num = 1f / divider;
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

