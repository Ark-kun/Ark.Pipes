using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

using TType = System.Double;

namespace Ark.Borrowed.Net.Microsoft.Xna.Framework._Double {
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEquatable<Quaternion> {
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType X;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType Y;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType Z;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public TType W;
        private static Quaternion _identity;
        public static Quaternion Identity {
            get {
                return _identity;
            }
        }
        public Quaternion(TType x, TType y, TType z, TType w) {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(Vector3 vectorPart, TType scalarPart) {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        public override string ToString() {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", new object[] { this.X.ToString(currentCulture), this.Y.ToString(currentCulture), this.Z.ToString(currentCulture), this.W.ToString(currentCulture) });
        }

        public bool Equals(Quaternion other) {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }

        public override bool Equals(object obj) {
            bool flag = false;
            if (obj is Quaternion) {
                flag = this.Equals((Quaternion)obj);
            }
            return flag;
        }

        public override int GetHashCode() {
            return (((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode()) + this.W.GetHashCode());
        }

        public TType LengthSquared() {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        public TType Length() {
            TType num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return (TType)Math.Sqrt((double)num);
        }

        public void Normalize() {
            TType num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
            this.X *= num;
            this.Y *= num;
            this.Z *= num;
            this.W *= num;
        }

        public static Quaternion Normalize(Quaternion quaternion) {
            Quaternion quaternion2;
            TType num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
            quaternion2.X = quaternion.X * num;
            quaternion2.Y = quaternion.Y * num;
            quaternion2.Z = quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result) {
            TType num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            TType num = 1f / ((TType)Math.Sqrt((double)num2));
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public void Conjugate() {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public static Quaternion Conjugate(Quaternion value) {
            Quaternion quaternion;
            quaternion.X = -value.X;
            quaternion.Y = -value.Y;
            quaternion.Z = -value.Z;
            quaternion.W = value.W;
            return quaternion;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result) {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion Inverse(Quaternion quaternion) {
            Quaternion quaternion2;
            TType num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            TType num = 1f / num2;
            quaternion2.X = -quaternion.X * num;
            quaternion2.Y = -quaternion.Y * num;
            quaternion2.Z = -quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result) {
            TType num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            TType num = 1f / num2;
            result.X = -quaternion.X * num;
            result.Y = -quaternion.Y * num;
            result.Z = -quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, TType angle) {
            Quaternion quaternion;
            TType num2 = angle * 0.5f;
            TType num = (TType)Math.Sin((double)num2);
            TType num3 = (TType)Math.Cos((double)num2);
            quaternion.X = axis.X * num;
            quaternion.Y = axis.Y * num;
            quaternion.Z = axis.Z * num;
            quaternion.W = num3;
            return quaternion;
        }

        public static void CreateFromAxisAngle(ref Vector3 axis, TType angle, out Quaternion result) {
            TType num2 = angle * 0.5f;
            TType num = (TType)Math.Sin((double)num2);
            TType num3 = (TType)Math.Cos((double)num2);
            result.X = axis.X * num;
            result.Y = axis.Y * num;
            result.Z = axis.Z * num;
            result.W = num3;
        }

        public static Quaternion CreateFromYawPitchRoll(TType yaw, TType pitch, TType roll) {
            Quaternion quaternion;
            TType num9 = roll * 0.5f;
            TType num6 = (TType)Math.Sin((double)num9);
            TType num5 = (TType)Math.Cos((double)num9);
            TType num8 = pitch * 0.5f;
            TType num4 = (TType)Math.Sin((double)num8);
            TType num3 = (TType)Math.Cos((double)num8);
            TType num7 = yaw * 0.5f;
            TType num2 = (TType)Math.Sin((double)num7);
            TType num = (TType)Math.Cos((double)num7);
            quaternion.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            quaternion.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            quaternion.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            quaternion.W = ((num * num3) * num5) + ((num2 * num4) * num6);
            return quaternion;
        }

        public static void CreateFromYawPitchRoll(TType yaw, TType pitch, TType roll, out Quaternion result) {
            TType num9 = roll * 0.5f;
            TType num6 = (TType)Math.Sin((double)num9);
            TType num5 = (TType)Math.Cos((double)num9);
            TType num8 = pitch * 0.5f;
            TType num4 = (TType)Math.Sin((double)num8);
            TType num3 = (TType)Math.Cos((double)num8);
            TType num7 = yaw * 0.5f;
            TType num2 = (TType)Math.Sin((double)num7);
            TType num = (TType)Math.Cos((double)num7);
            result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
        }

        public static Quaternion CreateFromRotationMatrix(Matrix matrix) {
            TType num8 = (matrix.M11 + matrix.M22) + matrix.M33;
            Quaternion quaternion = new Quaternion();
            if (num8 > 0f) {
                TType num = (TType)Math.Sqrt((double)(num8 + 1f));
                quaternion.W = num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (matrix.M23 - matrix.M32) * num;
                quaternion.Y = (matrix.M31 - matrix.M13) * num;
                quaternion.Z = (matrix.M12 - matrix.M21) * num;
                return quaternion;
            }
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33)) {
                TType num7 = (TType)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                TType num4 = 0.5f / num7;
                quaternion.X = 0.5f * num7;
                quaternion.Y = (matrix.M12 + matrix.M21) * num4;
                quaternion.Z = (matrix.M13 + matrix.M31) * num4;
                quaternion.W = (matrix.M23 - matrix.M32) * num4;
                return quaternion;
            }
            if (matrix.M22 > matrix.M33) {
                TType num6 = (TType)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                TType num3 = 0.5f / num6;
                quaternion.X = (matrix.M21 + matrix.M12) * num3;
                quaternion.Y = 0.5f * num6;
                quaternion.Z = (matrix.M32 + matrix.M23) * num3;
                quaternion.W = (matrix.M31 - matrix.M13) * num3;
                return quaternion;
            }
            TType num5 = (TType)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
            TType num2 = 0.5f / num5;
            quaternion.X = (matrix.M31 + matrix.M13) * num2;
            quaternion.Y = (matrix.M32 + matrix.M23) * num2;
            quaternion.Z = 0.5f * num5;
            quaternion.W = (matrix.M12 - matrix.M21) * num2;
            return quaternion;
        }

        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result) {
            TType num8 = (matrix.M11 + matrix.M22) + matrix.M33;
            if (num8 > 0f) {
                TType num = (TType)Math.Sqrt((double)(num8 + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            } else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33)) {
                TType num7 = (TType)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                TType num4 = 0.5f / num7;
                result.X = 0.5f * num7;
                result.Y = (matrix.M12 + matrix.M21) * num4;
                result.Z = (matrix.M13 + matrix.M31) * num4;
                result.W = (matrix.M23 - matrix.M32) * num4;
            } else if (matrix.M22 > matrix.M33) {
                TType num6 = (TType)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                TType num3 = 0.5f / num6;
                result.X = (matrix.M21 + matrix.M12) * num3;
                result.Y = 0.5f * num6;
                result.Z = (matrix.M32 + matrix.M23) * num3;
                result.W = (matrix.M31 - matrix.M13) * num3;
            } else {
                TType num5 = (TType)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
                TType num2 = 0.5f / num5;
                result.X = (matrix.M31 + matrix.M13) * num2;
                result.Y = (matrix.M32 + matrix.M23) * num2;
                result.Z = 0.5f * num5;
                result.W = (matrix.M12 - matrix.M21) * num2;
            }
        }

        public static TType Dot(Quaternion quaternion1, Quaternion quaternion2) {
            return ((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W));
        }

        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out TType result) {
            result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
        }

        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, TType amount) {
            TType num2;
            TType num3;
            Quaternion quaternion;
            TType num = amount;
            TType num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f) {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f) {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            } else {
                TType num5 = (TType)Math.Acos((double)num4);
                TType num6 = (TType)(1.0 / Math.Sin((double)num5));
                num3 = ((TType)Math.Sin((double)((1f - num) * num5))) * num6;
                num2 = flag ? ((-(TType)Math.Sin((double)(num * num5))) * num6) : (((TType)Math.Sin(num * num5)) * num6);
            }
            quaternion.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            quaternion.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            quaternion.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            quaternion.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
            return quaternion;
        }

        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, TType amount, out Quaternion result) {
            TType num2;
            TType num3;
            TType num = amount;
            TType num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f) {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f) {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            } else {
                TType num5 = (TType)Math.Acos((double)num4);
                TType num6 = (TType)(1.0 / Math.Sin((double)num5));
                num3 = ((TType)Math.Sin((double)((1f - num) * num5))) * num6;
                num2 = flag ? ((-(TType)Math.Sin(num * num5)) * num6) : (((TType)Math.Sin((double)(num * num5))) * num6);
            }
            result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
        }

        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, TType amount) {
            TType num = amount;
            TType num2 = 1f - num;
            Quaternion quaternion = new Quaternion();
            TType num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f) {
                quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            } else {
                quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            TType num4 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            TType num3 = 1f / ((TType)Math.Sqrt((double)num4));
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }

        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, TType amount, out Quaternion result) {
            TType num = amount;
            TType num2 = 1f - num;
            TType num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f) {
                result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            } else {
                result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            TType num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
            TType num3 = 1f / ((TType)Math.Sqrt((double)num4));
            result.X *= num3;
            result.Y *= num3;
            result.Z *= num3;
            result.W *= num3;
        }

        public static Quaternion Concatenate(Quaternion value1, Quaternion value2) {
            Quaternion quaternion;
            TType x = value2.X;
            TType y = value2.Y;
            TType z = value2.Z;
            TType w = value2.W;
            TType num4 = value1.X;
            TType num3 = value1.Y;
            TType num2 = value1.Z;
            TType num = value1.W;
            TType num12 = (y * num2) - (z * num3);
            TType num11 = (z * num4) - (x * num2);
            TType num10 = (x * num3) - (y * num4);
            TType num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result) {
            TType x = value2.X;
            TType y = value2.Y;
            TType z = value2.Z;
            TType w = value2.W;
            TType num4 = value1.X;
            TType num3 = value1.Y;
            TType num2 = value1.Z;
            TType num = value1.W;
            TType num12 = (y * num2) - (z * num3);
            TType num11 = (z * num4) - (x * num2);
            TType num10 = (x * num3) - (y * num4);
            TType num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }

        public static Quaternion Negate(Quaternion quaternion) {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }

        public static void Negate(ref Quaternion quaternion, out Quaternion result) {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }

        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }

        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result) {
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }

        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result) {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }

        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num4 = quaternion2.X;
            TType num3 = quaternion2.Y;
            TType num2 = quaternion2.Z;
            TType num = quaternion2.W;
            TType num12 = (y * num2) - (z * num3);
            TType num11 = (z * num4) - (x * num2);
            TType num10 = (x * num3) - (y * num4);
            TType num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result) {
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num4 = quaternion2.X;
            TType num3 = quaternion2.Y;
            TType num2 = quaternion2.Z;
            TType num = quaternion2.W;
            TType num12 = (y * num2) - (z * num3);
            TType num11 = (z * num4) - (x * num2);
            TType num10 = (x * num3) - (y * num4);
            TType num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }

        public static Quaternion Multiply(Quaternion quaternion1, TType scaleFactor) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }

        public static void Multiply(ref Quaternion quaternion1, TType scaleFactor, out Quaternion result) {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            TType num5 = 1f / num14;
            TType num4 = -quaternion2.X * num5;
            TType num3 = -quaternion2.Y * num5;
            TType num2 = -quaternion2.Z * num5;
            TType num = quaternion2.W * num5;
            TType num13 = (y * num2) - (z * num3);
            TType num12 = (z * num4) - (x * num2);
            TType num11 = (x * num3) - (y * num4);
            TType num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result) {
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            TType num5 = 1f / num14;
            TType num4 = -quaternion2.X * num5;
            TType num3 = -quaternion2.Y * num5;
            TType num2 = -quaternion2.Z * num5;
            TType num = quaternion2.W * num5;
            TType num13 = (y * num2) - (z * num3);
            TType num12 = (z * num4) - (x * num2);
            TType num11 = (x * num3) - (y * num4);
            TType num10 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num13;
            result.Y = ((y * num) + (num3 * w)) + num12;
            result.Z = ((z * num) + (num2 * w)) + num11;
            result.W = (w * num) - num10;
        }

        public static Quaternion operator -(Quaternion quaternion) {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }

        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2) {
            return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
        }

        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2) {
            if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) {
                return !(quaternion1.W == quaternion2.W);
            }
            return true;
        }

        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num4 = quaternion2.X;
            TType num3 = quaternion2.Y;
            TType num2 = quaternion2.Z;
            TType num = quaternion2.W;
            TType num12 = (y * num2) - (z * num3);
            TType num11 = (z * num4) - (x * num2);
            TType num10 = (x * num3) - (y * num4);
            TType num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static Quaternion operator *(Quaternion quaternion1, TType scaleFactor) {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2) {
            Quaternion quaternion;
            TType x = quaternion1.X;
            TType y = quaternion1.Y;
            TType z = quaternion1.Z;
            TType w = quaternion1.W;
            TType num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            TType num5 = 1f / num14;
            TType num4 = -quaternion2.X * num5;
            TType num3 = -quaternion2.Y * num5;
            TType num2 = -quaternion2.Z * num5;
            TType num = quaternion2.W * num5;
            TType num13 = (y * num2) - (z * num3);
            TType num12 = (z * num4) - (x * num2);
            TType num11 = (x * num3) - (y * num4);
            TType num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;
        }

        static Quaternion() {
            _identity = new Quaternion(0f, 0f, 0f, 1f);
        }
    }
}