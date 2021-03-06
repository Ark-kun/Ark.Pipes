﻿using System;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

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
        public struct Matrix : IEquatable<Matrix> {
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M11;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M12;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M13;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M14;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M21;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M22;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M23;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M24;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M31;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M32;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M33;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M34;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M41;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M42;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M43;
            [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public TFloat M44;
            private static Matrix _identity;
            public static Matrix Identity {
                get {
                    return _identity;
                }
            }
            public Vector3 Up {
                get {
                    Vector3 vector;
                    vector.X = this.M21;
                    vector.Y = this.M22;
                    vector.Z = this.M23;
                    return vector;
                }
                set {
                    this.M21 = value.X;
                    this.M22 = value.Y;
                    this.M23 = value.Z;
                }
            }
            public Vector3 Down {
                get {
                    Vector3 vector;
                    vector.X = -this.M21;
                    vector.Y = -this.M22;
                    vector.Z = -this.M23;
                    return vector;
                }
                set {
                    this.M21 = -value.X;
                    this.M22 = -value.Y;
                    this.M23 = -value.Z;
                }
            }
            public Vector3 Right {
                get {
                    Vector3 vector;
                    vector.X = this.M11;
                    vector.Y = this.M12;
                    vector.Z = this.M13;
                    return vector;
                }
                set {
                    this.M11 = value.X;
                    this.M12 = value.Y;
                    this.M13 = value.Z;
                }
            }
            public Vector3 Left {
                get {
                    Vector3 vector;
                    vector.X = -this.M11;
                    vector.Y = -this.M12;
                    vector.Z = -this.M13;
                    return vector;
                }
                set {
                    this.M11 = -value.X;
                    this.M12 = -value.Y;
                    this.M13 = -value.Z;
                }
            }
            public Vector3 Forward {
                get {
                    Vector3 vector;
                    vector.X = -this.M31;
                    vector.Y = -this.M32;
                    vector.Z = -this.M33;
                    return vector;
                }
                set {
                    this.M31 = -value.X;
                    this.M32 = -value.Y;
                    this.M33 = -value.Z;
                }
            }
            public Vector3 Backward {
                get {
                    Vector3 vector;
                    vector.X = this.M31;
                    vector.Y = this.M32;
                    vector.Z = this.M33;
                    return vector;
                }
                set {
                    this.M31 = value.X;
                    this.M32 = value.Y;
                    this.M33 = value.Z;
                }
            }
            public Vector3 Translation {
                get {
                    Vector3 vector;
                    vector.X = this.M41;
                    vector.Y = this.M42;
                    vector.Z = this.M43;
                    return vector;
                }
                set {
                    this.M41 = value.X;
                    this.M42 = value.Y;
                    this.M43 = value.Z;
                }
            }
            [SuppressMessage("Microsoft.Design", "CA1025")]
            public Matrix(TFloat m11, TFloat m12, TFloat m13, TFloat m14, TFloat m21, TFloat m22, TFloat m23, TFloat m24, TFloat m31, TFloat m32, TFloat m33, TFloat m34, TFloat m41, TFloat m42, TFloat m43, TFloat m44) {
                this.M11 = m11;
                this.M12 = m12;
                this.M13 = m13;
                this.M14 = m14;
                this.M21 = m21;
                this.M22 = m22;
                this.M23 = m23;
                this.M24 = m24;
                this.M31 = m31;
                this.M32 = m32;
                this.M33 = m33;
                this.M34 = m34;
                this.M41 = m41;
                this.M42 = m42;
                this.M43 = m43;
                this.M44 = m44;
            }

            public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3? cameraForwardVector) {
                Matrix matrix;
                Vector3 vector;
                Vector3 vector2;
                Vector3 vector3;
                vector.X = objectPosition.X - cameraPosition.X;
                vector.Y = objectPosition.Y - cameraPosition.Y;
                vector.Z = objectPosition.Z - cameraPosition.Z;
                TFloat num = vector.LengthSquared();
                if (num < 0.0001f) {
                    vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
                } else {
                    Vector3.Multiply(ref vector, (TFloat)(1f / ((TFloat)Math.Sqrt((double)num))), out vector);
                }
                Vector3.Cross(ref cameraUpVector, ref vector, out vector3);
                vector3.Normalize();
                Vector3.Cross(ref vector, ref vector3, out vector2);
                matrix.M11 = vector3.X;
                matrix.M12 = vector3.Y;
                matrix.M13 = vector3.Z;
                matrix.M14 = 0f;
                matrix.M21 = vector2.X;
                matrix.M22 = vector2.Y;
                matrix.M23 = vector2.Z;
                matrix.M24 = 0f;
                matrix.M31 = vector.X;
                matrix.M32 = vector.Y;
                matrix.M33 = vector.Z;
                matrix.M34 = 0f;
                matrix.M41 = objectPosition.X;
                matrix.M42 = objectPosition.Y;
                matrix.M43 = objectPosition.Z;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result) {
                Vector3 vector;
                Vector3 vector2;
                Vector3 vector3;
                vector.X = objectPosition.X - cameraPosition.X;
                vector.Y = objectPosition.Y - cameraPosition.Y;
                vector.Z = objectPosition.Z - cameraPosition.Z;
                TFloat num = vector.LengthSquared();
                if (num < 0.0001f) {
                    vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
                } else {
                    Vector3.Multiply(ref vector, (TFloat)(1f / ((TFloat)Math.Sqrt((double)num))), out vector);
                }
                Vector3.Cross(ref cameraUpVector, ref vector, out vector3);
                vector3.Normalize();
                Vector3.Cross(ref vector, ref vector3, out vector2);
                result.M11 = vector3.X;
                result.M12 = vector3.Y;
                result.M13 = vector3.Z;
                result.M14 = 0f;
                result.M21 = vector2.X;
                result.M22 = vector2.Y;
                result.M23 = vector2.Z;
                result.M24 = 0f;
                result.M31 = vector.X;
                result.M32 = vector.Y;
                result.M33 = vector.Z;
                result.M34 = 0f;
                result.M41 = objectPosition.X;
                result.M42 = objectPosition.Y;
                result.M43 = objectPosition.Z;
                result.M44 = 1f;
            }

            public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector) {
                TFloat num;
                Vector3 vector;
                Matrix matrix;
                Vector3 vector2;
                Vector3 vector3;
                vector2.X = objectPosition.X - cameraPosition.X;
                vector2.Y = objectPosition.Y - cameraPosition.Y;
                vector2.Z = objectPosition.Z - cameraPosition.Z;
                TFloat num2 = vector2.LengthSquared();
                if (num2 < 0.0001f) {
                    vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
                } else {
                    Vector3.Multiply(ref vector2, (TFloat)(1f / ((TFloat)Math.Sqrt((double)num2))), out vector2);
                }
                Vector3 vector4 = rotateAxis;
                Vector3.Dot(ref rotateAxis, ref vector2, out num);
                if (Math.Abs(num) > 0.9982547f) {
                    if (objectForwardVector.HasValue) {
                        vector = objectForwardVector.Value;
                        Vector3.Dot(ref rotateAxis, ref vector, out num);
                        if (Math.Abs(num) > 0.9982547f) {
                            num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                            vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                        }
                    } else {
                        num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                        vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                    }
                    Vector3.Cross(ref rotateAxis, ref vector, out vector3);
                    vector3.Normalize();
                    Vector3.Cross(ref vector3, ref rotateAxis, out vector);
                    vector.Normalize();
                } else {
                    Vector3.Cross(ref rotateAxis, ref vector2, out vector3);
                    vector3.Normalize();
                    Vector3.Cross(ref vector3, ref vector4, out vector);
                    vector.Normalize();
                }
                matrix.M11 = vector3.X;
                matrix.M12 = vector3.Y;
                matrix.M13 = vector3.Z;
                matrix.M14 = 0f;
                matrix.M21 = vector4.X;
                matrix.M22 = vector4.Y;
                matrix.M23 = vector4.Z;
                matrix.M24 = 0f;
                matrix.M31 = vector.X;
                matrix.M32 = vector.Y;
                matrix.M33 = vector.Z;
                matrix.M34 = 0f;
                matrix.M41 = objectPosition.X;
                matrix.M42 = objectPosition.Y;
                matrix.M43 = objectPosition.Z;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result) {
                TFloat num;
                Vector3 vector;
                Vector3 vector2;
                Vector3 vector3;
                vector2.X = objectPosition.X - cameraPosition.X;
                vector2.Y = objectPosition.Y - cameraPosition.Y;
                vector2.Z = objectPosition.Z - cameraPosition.Z;
                TFloat num2 = vector2.LengthSquared();
                if (num2 < 0.0001f) {
                    vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
                } else {
                    Vector3.Multiply(ref vector2, (TFloat)(1f / ((TFloat)Math.Sqrt((double)num2))), out vector2);
                }
                Vector3 vector4 = rotateAxis;
                Vector3.Dot(ref rotateAxis, ref vector2, out num);
                if (Math.Abs(num) > 0.9982547f) {
                    if (objectForwardVector.HasValue) {
                        vector = objectForwardVector.Value;
                        Vector3.Dot(ref rotateAxis, ref vector, out num);
                        if (Math.Abs(num) > 0.9982547f) {
                            num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                            vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                        }
                    } else {
                        num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                        vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                    }
                    Vector3.Cross(ref rotateAxis, ref vector, out vector3);
                    vector3.Normalize();
                    Vector3.Cross(ref vector3, ref rotateAxis, out vector);
                    vector.Normalize();
                } else {
                    Vector3.Cross(ref rotateAxis, ref vector2, out vector3);
                    vector3.Normalize();
                    Vector3.Cross(ref vector3, ref vector4, out vector);
                    vector.Normalize();
                }
                result.M11 = vector3.X;
                result.M12 = vector3.Y;
                result.M13 = vector3.Z;
                result.M14 = 0f;
                result.M21 = vector4.X;
                result.M22 = vector4.Y;
                result.M23 = vector4.Z;
                result.M24 = 0f;
                result.M31 = vector.X;
                result.M32 = vector.Y;
                result.M33 = vector.Z;
                result.M34 = 0f;
                result.M41 = objectPosition.X;
                result.M42 = objectPosition.Y;
                result.M43 = objectPosition.Z;
                result.M44 = 1f;
            }

            public static Matrix CreateTranslation(Vector3 position) {
                Matrix matrix;
                matrix.M11 = 1f;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = 1f;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = 1f;
                matrix.M34 = 0f;
                matrix.M41 = position.X;
                matrix.M42 = position.Y;
                matrix.M43 = position.Z;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateTranslation(ref Vector3 position, out Matrix result) {
                result.M11 = 1f;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = 1f;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = 1f;
                result.M34 = 0f;
                result.M41 = position.X;
                result.M42 = position.Y;
                result.M43 = position.Z;
                result.M44 = 1f;
            }

            public static Matrix CreateTranslation(TFloat xPosition, TFloat yPosition, TFloat zPosition) {
                Matrix matrix;
                matrix.M11 = 1f;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = 1f;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = 1f;
                matrix.M34 = 0f;
                matrix.M41 = xPosition;
                matrix.M42 = yPosition;
                matrix.M43 = zPosition;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateTranslation(TFloat xPosition, TFloat yPosition, TFloat zPosition, out Matrix result) {
                result.M11 = 1f;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = 1f;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = 1f;
                result.M34 = 0f;
                result.M41 = xPosition;
                result.M42 = yPosition;
                result.M43 = zPosition;
                result.M44 = 1f;
            }

            public static Matrix CreateScale(TFloat xScale, TFloat yScale, TFloat zScale) {
                Matrix matrix;
                TFloat num3 = xScale;
                TFloat num2 = yScale;
                TFloat num = zScale;
                matrix.M11 = num3;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = num2;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = num;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateScale(TFloat xScale, TFloat yScale, TFloat zScale, out Matrix result) {
                TFloat num3 = xScale;
                TFloat num2 = yScale;
                TFloat num = zScale;
                result.M11 = num3;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = num2;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = num;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateScale(Vector3 scales) {
                Matrix matrix;
                TFloat x = scales.X;
                TFloat y = scales.Y;
                TFloat z = scales.Z;
                matrix.M11 = x;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = y;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = z;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateScale(ref Vector3 scales, out Matrix result) {
                TFloat x = scales.X;
                TFloat y = scales.Y;
                TFloat z = scales.Z;
                result.M11 = x;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = y;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = z;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateScale(TFloat scale) {
                Matrix matrix;
                TFloat num = scale;
                matrix.M11 = num;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = num;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = num;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateScale(TFloat scale, out Matrix result) {
                TFloat num = scale;
                result.M11 = num;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = num;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = num;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateRotationX(TFloat radians) {
                Matrix matrix;
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                matrix.M11 = 1f;
                matrix.M12 = 0f;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = num2;
                matrix.M23 = num;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = -num;
                matrix.M33 = num2;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateRotationX(TFloat radians, out Matrix result) {
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                result.M11 = 1f;
                result.M12 = 0f;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = num2;
                result.M23 = num;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = -num;
                result.M33 = num2;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateRotationY(TFloat radians) {
                Matrix matrix;
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                matrix.M11 = num2;
                matrix.M12 = 0f;
                matrix.M13 = -num;
                matrix.M14 = 0f;
                matrix.M21 = 0f;
                matrix.M22 = 1f;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = num;
                matrix.M32 = 0f;
                matrix.M33 = num2;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateRotationY(TFloat radians, out Matrix result) {
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                result.M11 = num2;
                result.M12 = 0f;
                result.M13 = -num;
                result.M14 = 0f;
                result.M21 = 0f;
                result.M22 = 1f;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = num;
                result.M32 = 0f;
                result.M33 = num2;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateRotationZ(TFloat radians) {
                Matrix matrix;
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                matrix.M11 = num2;
                matrix.M12 = num;
                matrix.M13 = 0f;
                matrix.M14 = 0f;
                matrix.M21 = -num;
                matrix.M22 = num2;
                matrix.M23 = 0f;
                matrix.M24 = 0f;
                matrix.M31 = 0f;
                matrix.M32 = 0f;
                matrix.M33 = 1f;
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateRotationZ(TFloat radians, out Matrix result) {
                TFloat num2 = (TFloat)Math.Cos((double)radians);
                TFloat num = (TFloat)Math.Sin((double)radians);
                result.M11 = num2;
                result.M12 = num;
                result.M13 = 0f;
                result.M14 = 0f;
                result.M21 = -num;
                result.M22 = num2;
                result.M23 = 0f;
                result.M24 = 0f;
                result.M31 = 0f;
                result.M32 = 0f;
                result.M33 = 1f;
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateFromAxisAngle(Vector3 axis, TFloat angle) {
                Matrix matrix;
                TFloat x = axis.X;
                TFloat y = axis.Y;
                TFloat z = axis.Z;
                TFloat num2 = (TFloat)Math.Sin((double)angle);
                TFloat num = (TFloat)Math.Cos((double)angle);
                TFloat num11 = x * x;
                TFloat num10 = y * y;
                TFloat num9 = z * z;
                TFloat num8 = x * y;
                TFloat num7 = x * z;
                TFloat num6 = y * z;
                matrix.M11 = num11 + (num * (1f - num11));
                matrix.M12 = (num8 - (num * num8)) + (num2 * z);
                matrix.M13 = (num7 - (num * num7)) - (num2 * y);
                matrix.M14 = 0f;
                matrix.M21 = (num8 - (num * num8)) - (num2 * z);
                matrix.M22 = num10 + (num * (1f - num10));
                matrix.M23 = (num6 - (num * num6)) + (num2 * x);
                matrix.M24 = 0f;
                matrix.M31 = (num7 - (num * num7)) + (num2 * y);
                matrix.M32 = (num6 - (num * num6)) - (num2 * x);
                matrix.M33 = num9 + (num * (1f - num9));
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateFromAxisAngle(ref Vector3 axis, TFloat angle, out Matrix result) {
                TFloat x = axis.X;
                TFloat y = axis.Y;
                TFloat z = axis.Z;
                TFloat num2 = (TFloat)Math.Sin((double)angle);
                TFloat num = (TFloat)Math.Cos((double)angle);
                TFloat num11 = x * x;
                TFloat num10 = y * y;
                TFloat num9 = z * z;
                TFloat num8 = x * y;
                TFloat num7 = x * z;
                TFloat num6 = y * z;
                result.M11 = num11 + (num * (1f - num11));
                result.M12 = (num8 - (num * num8)) + (num2 * z);
                result.M13 = (num7 - (num * num7)) - (num2 * y);
                result.M14 = 0f;
                result.M21 = (num8 - (num * num8)) - (num2 * z);
                result.M22 = num10 + (num * (1f - num10));
                result.M23 = (num6 - (num * num6)) + (num2 * x);
                result.M24 = 0f;
                result.M31 = (num7 - (num * num7)) + (num2 * y);
                result.M32 = (num6 - (num * num6)) - (num2 * x);
                result.M33 = num9 + (num * (1f - num9));
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreatePerspectiveFieldOfView(TFloat fieldOfView, TFloat aspectRatio, TFloat nearPlaneDistance, TFloat farPlaneDistance) {
                Matrix matrix;
                if ((fieldOfView <= 0f) || (fieldOfView >= 3.141593f)) {
                    throw new ArgumentOutOfRangeException("fieldOfView", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.OutRangeFieldOfView", new object[] { "fieldOfView" }));
                }
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                TFloat num = 1f / ((TFloat)Math.Tan((double)(fieldOfView * 0.5f)));
                TFloat num9 = num / aspectRatio;
                matrix.M11 = num9;
                matrix.M12 = matrix.M13 = matrix.M14 = 0f;
                matrix.M22 = num;
                matrix.M21 = matrix.M23 = matrix.M24 = 0f;
                matrix.M31 = matrix.M32 = 0f;
                matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                matrix.M34 = -1f;
                matrix.M41 = matrix.M42 = matrix.M44 = 0f;
                matrix.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
                return matrix;
            }

            public static void CreatePerspectiveFieldOfView(TFloat fieldOfView, TFloat aspectRatio, TFloat nearPlaneDistance, TFloat farPlaneDistance, out Matrix result) {
                if ((fieldOfView <= 0f) || (fieldOfView >= 3.141593f)) {
                    throw new ArgumentOutOfRangeException("fieldOfView", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.OutRangeFieldOfView", new object[] { "fieldOfView" }));
                }
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                TFloat num = 1f / ((TFloat)Math.Tan((double)(fieldOfView * 0.5f)));
                TFloat num9 = num / aspectRatio;
                result.M11 = num9;
                result.M12 = result.M13 = result.M14 = 0f;
                result.M22 = num;
                result.M21 = result.M23 = result.M24 = 0f;
                result.M31 = result.M32 = 0f;
                result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                result.M34 = -1f;
                result.M41 = result.M42 = result.M44 = 0f;
                result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            }

            public static Matrix CreatePerspective(TFloat width, TFloat height, TFloat nearPlaneDistance, TFloat farPlaneDistance) {
                Matrix matrix;
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                matrix.M11 = (2f * nearPlaneDistance) / width;
                matrix.M12 = matrix.M13 = matrix.M14 = 0f;
                matrix.M22 = (2f * nearPlaneDistance) / height;
                matrix.M21 = matrix.M23 = matrix.M24 = 0f;
                matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                matrix.M31 = matrix.M32 = 0f;
                matrix.M34 = -1f;
                matrix.M41 = matrix.M42 = matrix.M44 = 0f;
                matrix.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
                return matrix;
            }

            public static void CreatePerspective(TFloat width, TFloat height, TFloat nearPlaneDistance, TFloat farPlaneDistance, out Matrix result) {
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                result.M11 = (2f * nearPlaneDistance) / width;
                result.M12 = result.M13 = result.M14 = 0f;
                result.M22 = (2f * nearPlaneDistance) / height;
                result.M21 = result.M23 = result.M24 = 0f;
                result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                result.M31 = result.M32 = 0f;
                result.M34 = -1f;
                result.M41 = result.M42 = result.M44 = 0f;
                result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            }

            public static Matrix CreatePerspectiveOffCenter(TFloat left, TFloat right, TFloat bottom, TFloat top, TFloat nearPlaneDistance, TFloat farPlaneDistance) {
                Matrix matrix;
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                matrix.M11 = (2f * nearPlaneDistance) / (right - left);
                matrix.M12 = matrix.M13 = matrix.M14 = 0f;
                matrix.M22 = (2f * nearPlaneDistance) / (top - bottom);
                matrix.M21 = matrix.M23 = matrix.M24 = 0f;
                matrix.M31 = (left + right) / (right - left);
                matrix.M32 = (top + bottom) / (top - bottom);
                matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                matrix.M34 = -1f;
                matrix.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
                matrix.M41 = matrix.M42 = matrix.M44 = 0f;
                return matrix;
            }

            public static void CreatePerspectiveOffCenter(TFloat left, TFloat right, TFloat bottom, TFloat top, TFloat nearPlaneDistance, TFloat farPlaneDistance, out Matrix result) {
                if (nearPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "nearPlaneDistance" }));
                }
                if (farPlaneDistance <= 0f) {
                    throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format(CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", new object[] { "farPlaneDistance" }));
                }
                if (nearPlaneDistance >= farPlaneDistance) {
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "FrameworkResources.OppositePlanes");
                }
                result.M11 = (2f * nearPlaneDistance) / (right - left);
                result.M12 = result.M13 = result.M14 = 0f;
                result.M22 = (2f * nearPlaneDistance) / (top - bottom);
                result.M21 = result.M23 = result.M24 = 0f;
                result.M31 = (left + right) / (right - left);
                result.M32 = (top + bottom) / (top - bottom);
                result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
                result.M34 = -1f;
                result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
                result.M41 = result.M42 = result.M44 = 0f;
            }

            public static Matrix CreateOrthographic(TFloat width, TFloat height, TFloat zNearPlane, TFloat zFarPlane) {
                Matrix matrix;
                matrix.M11 = 2f / width;
                matrix.M12 = matrix.M13 = matrix.M14 = 0f;
                matrix.M22 = 2f / height;
                matrix.M21 = matrix.M23 = matrix.M24 = 0f;
                matrix.M33 = 1f / (zNearPlane - zFarPlane);
                matrix.M31 = matrix.M32 = matrix.M34 = 0f;
                matrix.M41 = matrix.M42 = 0f;
                matrix.M43 = zNearPlane / (zNearPlane - zFarPlane);
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateOrthographic(TFloat width, TFloat height, TFloat zNearPlane, TFloat zFarPlane, out Matrix result) {
                result.M11 = 2f / width;
                result.M12 = result.M13 = result.M14 = 0f;
                result.M22 = 2f / height;
                result.M21 = result.M23 = result.M24 = 0f;
                result.M33 = 1f / (zNearPlane - zFarPlane);
                result.M31 = result.M32 = result.M34 = 0f;
                result.M41 = result.M42 = 0f;
                result.M43 = zNearPlane / (zNearPlane - zFarPlane);
                result.M44 = 1f;
            }

            public static Matrix CreateOrthographicOffCenter(TFloat left, TFloat right, TFloat bottom, TFloat top, TFloat zNearPlane, TFloat zFarPlane) {
                Matrix matrix;
                matrix.M11 = 2f / (right - left);
                matrix.M12 = matrix.M13 = matrix.M14 = 0f;
                matrix.M22 = 2f / (top - bottom);
                matrix.M21 = matrix.M23 = matrix.M24 = 0f;
                matrix.M33 = 1f / (zNearPlane - zFarPlane);
                matrix.M31 = matrix.M32 = matrix.M34 = 0f;
                matrix.M41 = (left + right) / (left - right);
                matrix.M42 = (top + bottom) / (bottom - top);
                matrix.M43 = zNearPlane / (zNearPlane - zFarPlane);
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateOrthographicOffCenter(TFloat left, TFloat right, TFloat bottom, TFloat top, TFloat zNearPlane, TFloat zFarPlane, out Matrix result) {
                result.M11 = 2f / (right - left);
                result.M12 = result.M13 = result.M14 = 0f;
                result.M22 = 2f / (top - bottom);
                result.M21 = result.M23 = result.M24 = 0f;
                result.M33 = 1f / (zNearPlane - zFarPlane);
                result.M31 = result.M32 = result.M34 = 0f;
                result.M41 = (left + right) / (left - right);
                result.M42 = (top + bottom) / (bottom - top);
                result.M43 = zNearPlane / (zNearPlane - zFarPlane);
                result.M44 = 1f;
            }

            public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector) {
                Matrix matrix;
                Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
                Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
                Vector3 vector3 = Vector3.Cross(vector, vector2);
                matrix.M11 = vector2.X;
                matrix.M12 = vector3.X;
                matrix.M13 = vector.X;
                matrix.M14 = 0f;
                matrix.M21 = vector2.Y;
                matrix.M22 = vector3.Y;
                matrix.M23 = vector.Y;
                matrix.M24 = 0f;
                matrix.M31 = vector2.Z;
                matrix.M32 = vector3.Z;
                matrix.M33 = vector.Z;
                matrix.M34 = 0f;
                matrix.M41 = -Vector3.Dot(vector2, cameraPosition);
                matrix.M42 = -Vector3.Dot(vector3, cameraPosition);
                matrix.M43 = -Vector3.Dot(vector, cameraPosition);
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result) {
                Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
                Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
                Vector3 vector3 = Vector3.Cross(vector, vector2);
                result.M11 = vector2.X;
                result.M12 = vector3.X;
                result.M13 = vector.X;
                result.M14 = 0f;
                result.M21 = vector2.Y;
                result.M22 = vector3.Y;
                result.M23 = vector.Y;
                result.M24 = 0f;
                result.M31 = vector2.Z;
                result.M32 = vector3.Z;
                result.M33 = vector.Z;
                result.M34 = 0f;
                result.M41 = -Vector3.Dot(vector2, cameraPosition);
                result.M42 = -Vector3.Dot(vector3, cameraPosition);
                result.M43 = -Vector3.Dot(vector, cameraPosition);
                result.M44 = 1f;
            }

            public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up) {
                Matrix matrix;
                Vector3 vector = Vector3.Normalize(-forward);
                Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
                Vector3 vector3 = Vector3.Cross(vector, vector2);
                matrix.M11 = vector2.X;
                matrix.M12 = vector2.Y;
                matrix.M13 = vector2.Z;
                matrix.M14 = 0f;
                matrix.M21 = vector3.X;
                matrix.M22 = vector3.Y;
                matrix.M23 = vector3.Z;
                matrix.M24 = 0f;
                matrix.M31 = vector.X;
                matrix.M32 = vector.Y;
                matrix.M33 = vector.Z;
                matrix.M34 = 0f;
                matrix.M41 = position.X;
                matrix.M42 = position.Y;
                matrix.M43 = position.Z;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result) {
                Vector3 vector = Vector3.Normalize(-forward);
                Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
                Vector3 vector3 = Vector3.Cross(vector, vector2);
                result.M11 = vector2.X;
                result.M12 = vector2.Y;
                result.M13 = vector2.Z;
                result.M14 = 0f;
                result.M21 = vector3.X;
                result.M22 = vector3.Y;
                result.M23 = vector3.Z;
                result.M24 = 0f;
                result.M31 = vector.X;
                result.M32 = vector.Y;
                result.M33 = vector.Z;
                result.M34 = 0f;
                result.M41 = position.X;
                result.M42 = position.Y;
                result.M43 = position.Z;
                result.M44 = 1f;
            }

            public static Matrix CreateFromQuaternion(Quaternion quaternion) {
                Matrix matrix;
                TFloat num9 = quaternion.X * quaternion.X;
                TFloat num8 = quaternion.Y * quaternion.Y;
                TFloat num7 = quaternion.Z * quaternion.Z;
                TFloat num6 = quaternion.X * quaternion.Y;
                TFloat num5 = quaternion.Z * quaternion.W;
                TFloat num4 = quaternion.Z * quaternion.X;
                TFloat num3 = quaternion.Y * quaternion.W;
                TFloat num2 = quaternion.Y * quaternion.Z;
                TFloat num = quaternion.X * quaternion.W;
                matrix.M11 = 1f - (2f * (num8 + num7));
                matrix.M12 = 2f * (num6 + num5);
                matrix.M13 = 2f * (num4 - num3);
                matrix.M14 = 0f;
                matrix.M21 = 2f * (num6 - num5);
                matrix.M22 = 1f - (2f * (num7 + num9));
                matrix.M23 = 2f * (num2 + num);
                matrix.M24 = 0f;
                matrix.M31 = 2f * (num4 + num3);
                matrix.M32 = 2f * (num2 - num);
                matrix.M33 = 1f - (2f * (num8 + num9));
                matrix.M34 = 0f;
                matrix.M41 = 0f;
                matrix.M42 = 0f;
                matrix.M43 = 0f;
                matrix.M44 = 1f;
                return matrix;
            }

            public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result) {
                TFloat num9 = quaternion.X * quaternion.X;
                TFloat num8 = quaternion.Y * quaternion.Y;
                TFloat num7 = quaternion.Z * quaternion.Z;
                TFloat num6 = quaternion.X * quaternion.Y;
                TFloat num5 = quaternion.Z * quaternion.W;
                TFloat num4 = quaternion.Z * quaternion.X;
                TFloat num3 = quaternion.Y * quaternion.W;
                TFloat num2 = quaternion.Y * quaternion.Z;
                TFloat num = quaternion.X * quaternion.W;
                result.M11 = 1f - (2f * (num8 + num7));
                result.M12 = 2f * (num6 + num5);
                result.M13 = 2f * (num4 - num3);
                result.M14 = 0f;
                result.M21 = 2f * (num6 - num5);
                result.M22 = 1f - (2f * (num7 + num9));
                result.M23 = 2f * (num2 + num);
                result.M24 = 0f;
                result.M31 = 2f * (num4 + num3);
                result.M32 = 2f * (num2 - num);
                result.M33 = 1f - (2f * (num8 + num9));
                result.M34 = 0f;
                result.M41 = 0f;
                result.M42 = 0f;
                result.M43 = 0f;
                result.M44 = 1f;
            }

            public static Matrix CreateFromYawPitchRoll(TFloat yaw, TFloat pitch, TFloat roll) {
                Matrix matrix;
                Quaternion quaternion;
                Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out quaternion);
                CreateFromQuaternion(ref quaternion, out matrix);
                return matrix;
            }

            public static void CreateFromYawPitchRoll(TFloat yaw, TFloat pitch, TFloat roll, out Matrix result) {
                Quaternion quaternion;
                Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out quaternion);
                CreateFromQuaternion(ref quaternion, out result);
            }

            //    public static Matrix CreateShadow(Vector3 lightDirection, Plane plane) {
            //        Matrix matrix;
            //        Plane plane2;
            //        Plane.Normalize(ref plane, out plane2);
            //        TFloat num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
            //        TFloat num5 = -plane2.Normal.X;
            //        TFloat num4 = -plane2.Normal.Y;
            //        TFloat num3 = -plane2.Normal.Z;
            //        TFloat num2 = -plane2.D;
            //        matrix.M11 = (num5 * lightDirection.X) + num;
            //        matrix.M21 = num4 * lightDirection.X;
            //        matrix.M31 = num3 * lightDirection.X;
            //        matrix.M41 = num2 * lightDirection.X;
            //        matrix.M12 = num5 * lightDirection.Y;
            //        matrix.M22 = (num4 * lightDirection.Y) + num;
            //        matrix.M32 = num3 * lightDirection.Y;
            //        matrix.M42 = num2 * lightDirection.Y;
            //        matrix.M13 = num5 * lightDirection.Z;
            //        matrix.M23 = num4 * lightDirection.Z;
            //        matrix.M33 = (num3 * lightDirection.Z) + num;
            //        matrix.M43 = num2 * lightDirection.Z;
            //        matrix.M14 = 0f;
            //        matrix.M24 = 0f;
            //        matrix.M34 = 0f;
            //        matrix.M44 = num;
            //        return matrix;
            //    }

            //    public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result) {
            //        Plane plane2;
            //        Plane.Normalize(ref plane, out plane2);
            //        TFloat num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
            //        TFloat num5 = -plane2.Normal.X;
            //        TFloat num4 = -plane2.Normal.Y;
            //        TFloat num3 = -plane2.Normal.Z;
            //        TFloat num2 = -plane2.D;
            //        result.M11 = (num5 * lightDirection.X) + num;
            //        result.M21 = num4 * lightDirection.X;
            //        result.M31 = num3 * lightDirection.X;
            //        result.M41 = num2 * lightDirection.X;
            //        result.M12 = num5 * lightDirection.Y;
            //        result.M22 = (num4 * lightDirection.Y) + num;
            //        result.M32 = num3 * lightDirection.Y;
            //        result.M42 = num2 * lightDirection.Y;
            //        result.M13 = num5 * lightDirection.Z;
            //        result.M23 = num4 * lightDirection.Z;
            //        result.M33 = (num3 * lightDirection.Z) + num;
            //        result.M43 = num2 * lightDirection.Z;
            //        result.M14 = 0f;
            //        result.M24 = 0f;
            //        result.M34 = 0f;
            //        result.M44 = num;
            //    }

            //    public static Matrix CreateReflection(Plane value) {
            //        Matrix matrix;
            //        value.Normalize();
            //        TFloat x = value.Normal.X;
            //        TFloat y = value.Normal.Y;
            //        TFloat z = value.Normal.Z;
            //        TFloat num3 = -2f * x;
            //        TFloat num2 = -2f * y;
            //        TFloat num = -2f * z;
            //        matrix.M11 = (num3 * x) + 1f;
            //        matrix.M12 = num2 * x;
            //        matrix.M13 = num * x;
            //        matrix.M14 = 0f;
            //        matrix.M21 = num3 * y;
            //        matrix.M22 = (num2 * y) + 1f;
            //        matrix.M23 = num * y;
            //        matrix.M24 = 0f;
            //        matrix.M31 = num3 * z;
            //        matrix.M32 = num2 * z;
            //        matrix.M33 = (num * z) + 1f;
            //        matrix.M34 = 0f;
            //        matrix.M41 = num3 * value.D;
            //        matrix.M42 = num2 * value.D;
            //        matrix.M43 = num * value.D;
            //        matrix.M44 = 1f;
            //        return matrix;
            //    }

            //    public static void CreateReflection(ref Plane value, out Matrix result) {
            //        Plane plane;
            //        Plane.Normalize(ref value, out plane);
            //        value.Normalize();
            //        TFloat x = plane.Normal.X;
            //        TFloat y = plane.Normal.Y;
            //        TFloat z = plane.Normal.Z;
            //        TFloat num3 = -2f * x;
            //        TFloat num2 = -2f * y;
            //        TFloat num = -2f * z;
            //        result.M11 = (num3 * x) + 1f;
            //        result.M12 = num2 * x;
            //        result.M13 = num * x;
            //        result.M14 = 0f;
            //        result.M21 = num3 * y;
            //        result.M22 = (num2 * y) + 1f;
            //        result.M23 = num * y;
            //        result.M24 = 0f;
            //        result.M31 = num3 * z;
            //        result.M32 = num2 * z;
            //        result.M33 = (num * z) + 1f;
            //        result.M34 = 0f;
            //        result.M41 = num3 * plane.D;
            //        result.M42 = num2 * plane.D;
            //        result.M43 = num * plane.D;
            //        result.M44 = 1f;
            //    }

            //    public unsafe bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
            //{
            //    bool flag = true;
            //    fixed (TFloat* numRef = &scale.X)
            //    {
            //        uint num;
            //        uint num3;
            //        uint num4;
            //        VectorBasis basis2;
            //        Vector3** vectorPtr = (Vector3**) &basis2;
            //        Matrix identity = Identity;
            //        CanonicalBasis basis = new CanonicalBasis();
            //        Vector3* vectorPtr2 = &basis.Row0;
            //        basis.Row0 = new Vector3(1f, 0f, 0f);
            //        basis.Row1 = new Vector3(0f, 1f, 0f);
            //        basis.Row2 = new Vector3(0f, 0f, 1f);
            //        translation.X = this.M41;
            //        translation.Y = this.M42;
            //        translation.Z = this.M43;
            //        *((IntPtr*) vectorPtr) = &identity.M11;
            //        *((IntPtr*) (vectorPtr + 1)) = (IntPtr) &identity.M21;
            //        *((IntPtr*) (vectorPtr + 2)) = (IntPtr) &identity.M31;
            //        *(*((IntPtr*) vectorPtr)) = new Vector3(this.M11, this.M12, this.M13);
            //        *(*((IntPtr*) (vectorPtr + 1))) = new Vector3(this.M21, this.M22, this.M23);
            //        *(*((IntPtr*) (vectorPtr + 2))) = new Vector3(this.M31, this.M32, this.M33);
            //        scale.X = *(((IntPtr*) vectorPtr)).Length();
            //        scale.Y = *(((IntPtr*) (vectorPtr + 1))).Length();
            //        scale.Z = *(((IntPtr*) (vectorPtr + 2))).Length();
            //        TFloat num11 = numRef[0];
            //        TFloat num10 = numRef[4];
            //        TFloat num7 = numRef[8];
            //        if (num11 < num10)
            //        {
            //            if (num10 < num7)
            //            {
            //                num = 2;
            //                num3 = 1;
            //                num4 = 0;
            //            }
            //            else
            //            {
            //                num = 1;
            //                if (num11 < num7)
            //                {
            //                    num3 = 2;
            //                    num4 = 0;
            //                }
            //                else
            //                {
            //                    num3 = 0;
            //                    num4 = 2;
            //                }
            //            }
            //        }
            //        else if (num11 < num7)
            //        {
            //            num = 2;
            //            num3 = 0;
            //            num4 = 1;
            //        }
            //        else
            //        {
            //            num = 0;
            //            if (num10 < num7)
            //            {
            //                num3 = 2;
            //                num4 = 1;
            //            }
            //            else
            //            {
            //                num3 = 1;
            //                num4 = 2;
            //            }
            //        }
            //        if (numRef[num * 4] < 0.0001f)
            //        {
            //            *(*((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))) = vectorPtr2[(int) (num * sizeof(Vector3))];
            //        }
            //        *(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))).Normalize();
            //        if (numRef[num3 * 4] < 0.0001f)
            //        {
            //            uint num5;
            //            TFloat num9 = Math.Abs(*(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))).X);
            //            TFloat num8 = Math.Abs(*(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))).Y);
            //            TFloat num6 = Math.Abs(*(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))).Z);
            //            if (num9 < num8)
            //            {
            //                if (num8 < num6)
            //                {
            //                    num5 = 0;
            //                }
            //                else if (num9 < num6)
            //                {
            //                    num5 = 0;
            //                }
            //                else
            //                {
            //                    num5 = 2;
            //                }
            //            }
            //            else if (num9 < num6)
            //            {
            //                num5 = 1;
            //            }
            //            else if (num8 < num6)
            //            {
            //                num5 = 1;
            //            }
            //            else
            //            {
            //                num5 = 2;
            //            }
            //            Vector3.Cross(*(((IntPtr*) (vectorPtr + (num3 * sizeof(Vector3*))))),*(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))), out (Vector3) ref (vectorPtr2 + (num5 * sizeof(Vector3))));
            //        }
            //        *(((IntPtr*) (vectorPtr + (num3 * sizeof(Vector3*))))).Normalize();
            //        if (numRef[num4 * 4] < 0.0001f)
            //        {
            //            Vector3.Cross(ref (Vector3) ref *(((IntPtr*) (vectorPtr + (num4 * sizeof(Vector3*))))), ref (Vector3) ref *(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))), out (Vector3) ref *(((IntPtr*) (vectorPtr + (num3 * sizeof(Vector3*))))));
            //        }
            //        *(((IntPtr*) (vectorPtr + (num4 * sizeof(Vector3*))))).Normalize();
            //        TFloat num2 = identity.Determinant();
            //        if (num2 < 0f)
            //        {
            //            numRef[num * 4] = -numRef[num * 4];
            //            *(*((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))) = -*(*(((IntPtr*) (vectorPtr + (num * sizeof(Vector3*))))));
            //            num2 = -num2;
            //        }
            //        num2--;
            //        num2 *= num2;
            //        if (0.0001f < num2)
            //        {
            //            rotation = Quaternion.Identity;
            //            flag = false;
            //        }
            //        else
            //        {
            //            Quaternion.CreateFromRotationMatrix(ref identity, out rotation);
            //        }
            //    }
            //    return flag;
            //}

            public static Matrix Transform(Matrix value, Quaternion rotation) {
                Matrix matrix;
                TFloat num21 = rotation.X + rotation.X;
                TFloat num11 = rotation.Y + rotation.Y;
                TFloat num10 = rotation.Z + rotation.Z;
                TFloat num20 = rotation.W * num21;
                TFloat num19 = rotation.W * num11;
                TFloat num18 = rotation.W * num10;
                TFloat num17 = rotation.X * num21;
                TFloat num16 = rotation.X * num11;
                TFloat num15 = rotation.X * num10;
                TFloat num14 = rotation.Y * num11;
                TFloat num13 = rotation.Y * num10;
                TFloat num12 = rotation.Z * num10;
                TFloat num9 = (1f - num14) - num12;
                TFloat num8 = num16 - num18;
                TFloat num7 = num15 + num19;
                TFloat num6 = num16 + num18;
                TFloat num5 = (1f - num17) - num12;
                TFloat num4 = num13 - num20;
                TFloat num3 = num15 - num19;
                TFloat num2 = num13 + num20;
                TFloat num = (1f - num17) - num14;
                matrix.M11 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
                matrix.M12 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
                matrix.M13 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
                matrix.M14 = value.M14;
                matrix.M21 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
                matrix.M22 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
                matrix.M23 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
                matrix.M24 = value.M24;
                matrix.M31 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
                matrix.M32 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
                matrix.M33 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
                matrix.M34 = value.M34;
                matrix.M41 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
                matrix.M42 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
                matrix.M43 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
                matrix.M44 = value.M44;
                return matrix;
            }

            public static void Transform(ref Matrix value, ref Quaternion rotation, out Matrix result) {
                TFloat num21 = rotation.X + rotation.X;
                TFloat num11 = rotation.Y + rotation.Y;
                TFloat num10 = rotation.Z + rotation.Z;
                TFloat num20 = rotation.W * num21;
                TFloat num19 = rotation.W * num11;
                TFloat num18 = rotation.W * num10;
                TFloat num17 = rotation.X * num21;
                TFloat num16 = rotation.X * num11;
                TFloat num15 = rotation.X * num10;
                TFloat num14 = rotation.Y * num11;
                TFloat num13 = rotation.Y * num10;
                TFloat num12 = rotation.Z * num10;
                TFloat num9 = (1f - num14) - num12;
                TFloat num8 = num16 - num18;
                TFloat num7 = num15 + num19;
                TFloat num6 = num16 + num18;
                TFloat num5 = (1f - num17) - num12;
                TFloat num4 = num13 - num20;
                TFloat num3 = num15 - num19;
                TFloat num2 = num13 + num20;
                TFloat num = (1f - num17) - num14;
                TFloat num37 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
                TFloat num36 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
                TFloat num35 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
                TFloat num34 = value.M14;
                TFloat num33 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
                TFloat num32 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
                TFloat num31 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
                TFloat num30 = value.M24;
                TFloat num29 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
                TFloat num28 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
                TFloat num27 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
                TFloat num26 = value.M34;
                TFloat num25 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
                TFloat num24 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
                TFloat num23 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
                TFloat num22 = value.M44;
                result.M11 = num37;
                result.M12 = num36;
                result.M13 = num35;
                result.M14 = num34;
                result.M21 = num33;
                result.M22 = num32;
                result.M23 = num31;
                result.M24 = num30;
                result.M31 = num29;
                result.M32 = num28;
                result.M33 = num27;
                result.M34 = num26;
                result.M41 = num25;
                result.M42 = num24;
                result.M43 = num23;
                result.M44 = num22;
            }

            public override string ToString() {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return ("{ " + string.Format(currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new object[] { this.M11.ToString(currentCulture), this.M12.ToString(currentCulture), this.M13.ToString(currentCulture), this.M14.ToString(currentCulture) }) + string.Format(currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new object[] { this.M21.ToString(currentCulture), this.M22.ToString(currentCulture), this.M23.ToString(currentCulture), this.M24.ToString(currentCulture) }) + string.Format(currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new object[] { this.M31.ToString(currentCulture), this.M32.ToString(currentCulture), this.M33.ToString(currentCulture), this.M34.ToString(currentCulture) }) + string.Format(currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new object[] { this.M41.ToString(currentCulture), this.M42.ToString(currentCulture), this.M43.ToString(currentCulture), this.M44.ToString(currentCulture) }) + "}");
            }

            public bool Equals(Matrix other) {
                return ((((((this.M11 == other.M11) && (this.M22 == other.M22)) && ((this.M33 == other.M33) && (this.M44 == other.M44))) && (((this.M12 == other.M12) && (this.M13 == other.M13)) && ((this.M14 == other.M14) && (this.M21 == other.M21)))) && ((((this.M23 == other.M23) && (this.M24 == other.M24)) && ((this.M31 == other.M31) && (this.M32 == other.M32))) && (((this.M34 == other.M34) && (this.M41 == other.M41)) && (this.M42 == other.M42)))) && (this.M43 == other.M43));
            }

            public override bool Equals(object obj) {
                bool flag = false;
                if (obj is Matrix) {
                    flag = this.Equals((Matrix)obj);
                }
                return flag;
            }

            public override int GetHashCode() {
                return (((((((((((((((this.M11.GetHashCode() + this.M12.GetHashCode()) + this.M13.GetHashCode()) + this.M14.GetHashCode()) + this.M21.GetHashCode()) + this.M22.GetHashCode()) + this.M23.GetHashCode()) + this.M24.GetHashCode()) + this.M31.GetHashCode()) + this.M32.GetHashCode()) + this.M33.GetHashCode()) + this.M34.GetHashCode()) + this.M41.GetHashCode()) + this.M42.GetHashCode()) + this.M43.GetHashCode()) + this.M44.GetHashCode());
            }

            public static Matrix Transpose(Matrix matrix) {
                Matrix matrix2;
                matrix2.M11 = matrix.M11;
                matrix2.M12 = matrix.M21;
                matrix2.M13 = matrix.M31;
                matrix2.M14 = matrix.M41;
                matrix2.M21 = matrix.M12;
                matrix2.M22 = matrix.M22;
                matrix2.M23 = matrix.M32;
                matrix2.M24 = matrix.M42;
                matrix2.M31 = matrix.M13;
                matrix2.M32 = matrix.M23;
                matrix2.M33 = matrix.M33;
                matrix2.M34 = matrix.M43;
                matrix2.M41 = matrix.M14;
                matrix2.M42 = matrix.M24;
                matrix2.M43 = matrix.M34;
                matrix2.M44 = matrix.M44;
                return matrix2;
            }

            public static void Transpose(ref Matrix matrix, out Matrix result) {
                TFloat num16 = matrix.M11;
                TFloat num15 = matrix.M12;
                TFloat num14 = matrix.M13;
                TFloat num13 = matrix.M14;
                TFloat num12 = matrix.M21;
                TFloat num11 = matrix.M22;
                TFloat num10 = matrix.M23;
                TFloat num9 = matrix.M24;
                TFloat num8 = matrix.M31;
                TFloat num7 = matrix.M32;
                TFloat num6 = matrix.M33;
                TFloat num5 = matrix.M34;
                TFloat num4 = matrix.M41;
                TFloat num3 = matrix.M42;
                TFloat num2 = matrix.M43;
                TFloat num = matrix.M44;
                result.M11 = num16;
                result.M12 = num12;
                result.M13 = num8;
                result.M14 = num4;
                result.M21 = num15;
                result.M22 = num11;
                result.M23 = num7;
                result.M24 = num3;
                result.M31 = num14;
                result.M32 = num10;
                result.M33 = num6;
                result.M34 = num2;
                result.M41 = num13;
                result.M42 = num9;
                result.M43 = num5;
                result.M44 = num;
            }

            public TFloat Determinant() {
                TFloat num22 = this.M11;
                TFloat num21 = this.M12;
                TFloat num20 = this.M13;
                TFloat num19 = this.M14;
                TFloat num12 = this.M21;
                TFloat num11 = this.M22;
                TFloat num10 = this.M23;
                TFloat num9 = this.M24;
                TFloat num8 = this.M31;
                TFloat num7 = this.M32;
                TFloat num6 = this.M33;
                TFloat num5 = this.M34;
                TFloat num4 = this.M41;
                TFloat num3 = this.M42;
                TFloat num2 = this.M43;
                TFloat num = this.M44;
                TFloat num18 = (num6 * num) - (num5 * num2);
                TFloat num17 = (num7 * num) - (num5 * num3);
                TFloat num16 = (num7 * num2) - (num6 * num3);
                TFloat num15 = (num8 * num) - (num5 * num4);
                TFloat num14 = (num8 * num2) - (num6 * num4);
                TFloat num13 = (num8 * num3) - (num7 * num4);
                return ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
            }

            public static Matrix Invert(Matrix matrix) {
                Matrix matrix2;
                TFloat num5 = matrix.M11;
                TFloat num4 = matrix.M12;
                TFloat num3 = matrix.M13;
                TFloat num2 = matrix.M14;
                TFloat num9 = matrix.M21;
                TFloat num8 = matrix.M22;
                TFloat num7 = matrix.M23;
                TFloat num6 = matrix.M24;
                TFloat num17 = matrix.M31;
                TFloat num16 = matrix.M32;
                TFloat num15 = matrix.M33;
                TFloat num14 = matrix.M34;
                TFloat num13 = matrix.M41;
                TFloat num12 = matrix.M42;
                TFloat num11 = matrix.M43;
                TFloat num10 = matrix.M44;
                TFloat num23 = (num15 * num10) - (num14 * num11);
                TFloat num22 = (num16 * num10) - (num14 * num12);
                TFloat num21 = (num16 * num11) - (num15 * num12);
                TFloat num20 = (num17 * num10) - (num14 * num13);
                TFloat num19 = (num17 * num11) - (num15 * num13);
                TFloat num18 = (num17 * num12) - (num16 * num13);
                TFloat num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
                TFloat num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
                TFloat num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
                TFloat num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
                TFloat num = 1f / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
                matrix2.M11 = num39 * num;
                matrix2.M21 = num38 * num;
                matrix2.M31 = num37 * num;
                matrix2.M41 = num36 * num;
                matrix2.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
                matrix2.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
                matrix2.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
                matrix2.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
                TFloat num35 = (num7 * num10) - (num6 * num11);
                TFloat num34 = (num8 * num10) - (num6 * num12);
                TFloat num33 = (num8 * num11) - (num7 * num12);
                TFloat num32 = (num9 * num10) - (num6 * num13);
                TFloat num31 = (num9 * num11) - (num7 * num13);
                TFloat num30 = (num9 * num12) - (num8 * num13);
                matrix2.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
                matrix2.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
                matrix2.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
                matrix2.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
                TFloat num29 = (num7 * num14) - (num6 * num15);
                TFloat num28 = (num8 * num14) - (num6 * num16);
                TFloat num27 = (num8 * num15) - (num7 * num16);
                TFloat num26 = (num9 * num14) - (num6 * num17);
                TFloat num25 = (num9 * num15) - (num7 * num17);
                TFloat num24 = (num9 * num16) - (num8 * num17);
                matrix2.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
                matrix2.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
                matrix2.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
                matrix2.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
                return matrix2;
            }

            public static void Invert(ref Matrix matrix, out Matrix result) {
                TFloat num5 = matrix.M11;
                TFloat num4 = matrix.M12;
                TFloat num3 = matrix.M13;
                TFloat num2 = matrix.M14;
                TFloat num9 = matrix.M21;
                TFloat num8 = matrix.M22;
                TFloat num7 = matrix.M23;
                TFloat num6 = matrix.M24;
                TFloat num17 = matrix.M31;
                TFloat num16 = matrix.M32;
                TFloat num15 = matrix.M33;
                TFloat num14 = matrix.M34;
                TFloat num13 = matrix.M41;
                TFloat num12 = matrix.M42;
                TFloat num11 = matrix.M43;
                TFloat num10 = matrix.M44;
                TFloat num23 = (num15 * num10) - (num14 * num11);
                TFloat num22 = (num16 * num10) - (num14 * num12);
                TFloat num21 = (num16 * num11) - (num15 * num12);
                TFloat num20 = (num17 * num10) - (num14 * num13);
                TFloat num19 = (num17 * num11) - (num15 * num13);
                TFloat num18 = (num17 * num12) - (num16 * num13);
                TFloat num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
                TFloat num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
                TFloat num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
                TFloat num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
                TFloat num = 1f / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
                result.M11 = num39 * num;
                result.M21 = num38 * num;
                result.M31 = num37 * num;
                result.M41 = num36 * num;
                result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
                result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
                result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
                result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
                TFloat num35 = (num7 * num10) - (num6 * num11);
                TFloat num34 = (num8 * num10) - (num6 * num12);
                TFloat num33 = (num8 * num11) - (num7 * num12);
                TFloat num32 = (num9 * num10) - (num6 * num13);
                TFloat num31 = (num9 * num11) - (num7 * num13);
                TFloat num30 = (num9 * num12) - (num8 * num13);
                result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
                result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
                result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
                result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
                TFloat num29 = (num7 * num14) - (num6 * num15);
                TFloat num28 = (num8 * num14) - (num6 * num16);
                TFloat num27 = (num8 * num15) - (num7 * num16);
                TFloat num26 = (num9 * num14) - (num6 * num17);
                TFloat num25 = (num9 * num15) - (num7 * num17);
                TFloat num24 = (num9 * num16) - (num8 * num17);
                result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
                result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
                result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
                result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
            }

            public static Matrix Lerp(Matrix matrix1, Matrix matrix2, TFloat amount) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
                matrix.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
                matrix.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
                matrix.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
                matrix.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
                matrix.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
                matrix.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
                matrix.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
                matrix.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
                matrix.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
                matrix.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
                matrix.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
                matrix.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
                matrix.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
                matrix.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
                matrix.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
                return matrix;
            }

            public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, TFloat amount, out Matrix result) {
                result.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
                result.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
                result.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
                result.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
                result.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
                result.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
                result.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
                result.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
                result.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
                result.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
                result.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
                result.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
                result.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
                result.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
                result.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
                result.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
            }

            public static Matrix Negate(Matrix matrix) {
                Matrix matrix2;
                matrix2.M11 = -matrix.M11;
                matrix2.M12 = -matrix.M12;
                matrix2.M13 = -matrix.M13;
                matrix2.M14 = -matrix.M14;
                matrix2.M21 = -matrix.M21;
                matrix2.M22 = -matrix.M22;
                matrix2.M23 = -matrix.M23;
                matrix2.M24 = -matrix.M24;
                matrix2.M31 = -matrix.M31;
                matrix2.M32 = -matrix.M32;
                matrix2.M33 = -matrix.M33;
                matrix2.M34 = -matrix.M34;
                matrix2.M41 = -matrix.M41;
                matrix2.M42 = -matrix.M42;
                matrix2.M43 = -matrix.M43;
                matrix2.M44 = -matrix.M44;
                return matrix2;
            }

            public static void Negate(ref Matrix matrix, out Matrix result) {
                result.M11 = -matrix.M11;
                result.M12 = -matrix.M12;
                result.M13 = -matrix.M13;
                result.M14 = -matrix.M14;
                result.M21 = -matrix.M21;
                result.M22 = -matrix.M22;
                result.M23 = -matrix.M23;
                result.M24 = -matrix.M24;
                result.M31 = -matrix.M31;
                result.M32 = -matrix.M32;
                result.M33 = -matrix.M33;
                result.M34 = -matrix.M34;
                result.M41 = -matrix.M41;
                result.M42 = -matrix.M42;
                result.M43 = -matrix.M43;
                result.M44 = -matrix.M44;
            }

            public static Matrix Add(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 + matrix2.M11;
                matrix.M12 = matrix1.M12 + matrix2.M12;
                matrix.M13 = matrix1.M13 + matrix2.M13;
                matrix.M14 = matrix1.M14 + matrix2.M14;
                matrix.M21 = matrix1.M21 + matrix2.M21;
                matrix.M22 = matrix1.M22 + matrix2.M22;
                matrix.M23 = matrix1.M23 + matrix2.M23;
                matrix.M24 = matrix1.M24 + matrix2.M24;
                matrix.M31 = matrix1.M31 + matrix2.M31;
                matrix.M32 = matrix1.M32 + matrix2.M32;
                matrix.M33 = matrix1.M33 + matrix2.M33;
                matrix.M34 = matrix1.M34 + matrix2.M34;
                matrix.M41 = matrix1.M41 + matrix2.M41;
                matrix.M42 = matrix1.M42 + matrix2.M42;
                matrix.M43 = matrix1.M43 + matrix2.M43;
                matrix.M44 = matrix1.M44 + matrix2.M44;
                return matrix;
            }

            public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result) {
                result.M11 = matrix1.M11 + matrix2.M11;
                result.M12 = matrix1.M12 + matrix2.M12;
                result.M13 = matrix1.M13 + matrix2.M13;
                result.M14 = matrix1.M14 + matrix2.M14;
                result.M21 = matrix1.M21 + matrix2.M21;
                result.M22 = matrix1.M22 + matrix2.M22;
                result.M23 = matrix1.M23 + matrix2.M23;
                result.M24 = matrix1.M24 + matrix2.M24;
                result.M31 = matrix1.M31 + matrix2.M31;
                result.M32 = matrix1.M32 + matrix2.M32;
                result.M33 = matrix1.M33 + matrix2.M33;
                result.M34 = matrix1.M34 + matrix2.M34;
                result.M41 = matrix1.M41 + matrix2.M41;
                result.M42 = matrix1.M42 + matrix2.M42;
                result.M43 = matrix1.M43 + matrix2.M43;
                result.M44 = matrix1.M44 + matrix2.M44;
            }

            public static Matrix Subtract(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 - matrix2.M11;
                matrix.M12 = matrix1.M12 - matrix2.M12;
                matrix.M13 = matrix1.M13 - matrix2.M13;
                matrix.M14 = matrix1.M14 - matrix2.M14;
                matrix.M21 = matrix1.M21 - matrix2.M21;
                matrix.M22 = matrix1.M22 - matrix2.M22;
                matrix.M23 = matrix1.M23 - matrix2.M23;
                matrix.M24 = matrix1.M24 - matrix2.M24;
                matrix.M31 = matrix1.M31 - matrix2.M31;
                matrix.M32 = matrix1.M32 - matrix2.M32;
                matrix.M33 = matrix1.M33 - matrix2.M33;
                matrix.M34 = matrix1.M34 - matrix2.M34;
                matrix.M41 = matrix1.M41 - matrix2.M41;
                matrix.M42 = matrix1.M42 - matrix2.M42;
                matrix.M43 = matrix1.M43 - matrix2.M43;
                matrix.M44 = matrix1.M44 - matrix2.M44;
                return matrix;
            }

            public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result) {
                result.M11 = matrix1.M11 - matrix2.M11;
                result.M12 = matrix1.M12 - matrix2.M12;
                result.M13 = matrix1.M13 - matrix2.M13;
                result.M14 = matrix1.M14 - matrix2.M14;
                result.M21 = matrix1.M21 - matrix2.M21;
                result.M22 = matrix1.M22 - matrix2.M22;
                result.M23 = matrix1.M23 - matrix2.M23;
                result.M24 = matrix1.M24 - matrix2.M24;
                result.M31 = matrix1.M31 - matrix2.M31;
                result.M32 = matrix1.M32 - matrix2.M32;
                result.M33 = matrix1.M33 - matrix2.M33;
                result.M34 = matrix1.M34 - matrix2.M34;
                result.M41 = matrix1.M41 - matrix2.M41;
                result.M42 = matrix1.M42 - matrix2.M42;
                result.M43 = matrix1.M43 - matrix2.M43;
                result.M44 = matrix1.M44 - matrix2.M44;
            }

            public static Matrix Multiply(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
                matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
                matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
                matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
                matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
                matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
                matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
                matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
                matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
                matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
                matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
                matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
                matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
                matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
                matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
                matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
                return matrix;
            }

            public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result) {
                TFloat num16 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
                TFloat num15 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
                TFloat num14 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
                TFloat num13 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
                TFloat num12 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
                TFloat num11 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
                TFloat num10 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
                TFloat num9 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
                TFloat num8 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
                TFloat num7 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
                TFloat num6 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
                TFloat num5 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
                TFloat num4 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
                TFloat num3 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
                TFloat num2 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
                TFloat num = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
                result.M11 = num16;
                result.M12 = num15;
                result.M13 = num14;
                result.M14 = num13;
                result.M21 = num12;
                result.M22 = num11;
                result.M23 = num10;
                result.M24 = num9;
                result.M31 = num8;
                result.M32 = num7;
                result.M33 = num6;
                result.M34 = num5;
                result.M41 = num4;
                result.M42 = num3;
                result.M43 = num2;
                result.M44 = num;
            }

            public static Matrix Multiply(Matrix matrix1, TFloat scaleFactor) {
                Matrix matrix;
                TFloat num = scaleFactor;
                matrix.M11 = matrix1.M11 * num;
                matrix.M12 = matrix1.M12 * num;
                matrix.M13 = matrix1.M13 * num;
                matrix.M14 = matrix1.M14 * num;
                matrix.M21 = matrix1.M21 * num;
                matrix.M22 = matrix1.M22 * num;
                matrix.M23 = matrix1.M23 * num;
                matrix.M24 = matrix1.M24 * num;
                matrix.M31 = matrix1.M31 * num;
                matrix.M32 = matrix1.M32 * num;
                matrix.M33 = matrix1.M33 * num;
                matrix.M34 = matrix1.M34 * num;
                matrix.M41 = matrix1.M41 * num;
                matrix.M42 = matrix1.M42 * num;
                matrix.M43 = matrix1.M43 * num;
                matrix.M44 = matrix1.M44 * num;
                return matrix;
            }

            public static void Multiply(ref Matrix matrix1, TFloat scaleFactor, out Matrix result) {
                TFloat num = scaleFactor;
                result.M11 = matrix1.M11 * num;
                result.M12 = matrix1.M12 * num;
                result.M13 = matrix1.M13 * num;
                result.M14 = matrix1.M14 * num;
                result.M21 = matrix1.M21 * num;
                result.M22 = matrix1.M22 * num;
                result.M23 = matrix1.M23 * num;
                result.M24 = matrix1.M24 * num;
                result.M31 = matrix1.M31 * num;
                result.M32 = matrix1.M32 * num;
                result.M33 = matrix1.M33 * num;
                result.M34 = matrix1.M34 * num;
                result.M41 = matrix1.M41 * num;
                result.M42 = matrix1.M42 * num;
                result.M43 = matrix1.M43 * num;
                result.M44 = matrix1.M44 * num;
            }

            public static Matrix Divide(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 / matrix2.M11;
                matrix.M12 = matrix1.M12 / matrix2.M12;
                matrix.M13 = matrix1.M13 / matrix2.M13;
                matrix.M14 = matrix1.M14 / matrix2.M14;
                matrix.M21 = matrix1.M21 / matrix2.M21;
                matrix.M22 = matrix1.M22 / matrix2.M22;
                matrix.M23 = matrix1.M23 / matrix2.M23;
                matrix.M24 = matrix1.M24 / matrix2.M24;
                matrix.M31 = matrix1.M31 / matrix2.M31;
                matrix.M32 = matrix1.M32 / matrix2.M32;
                matrix.M33 = matrix1.M33 / matrix2.M33;
                matrix.M34 = matrix1.M34 / matrix2.M34;
                matrix.M41 = matrix1.M41 / matrix2.M41;
                matrix.M42 = matrix1.M42 / matrix2.M42;
                matrix.M43 = matrix1.M43 / matrix2.M43;
                matrix.M44 = matrix1.M44 / matrix2.M44;
                return matrix;
            }

            public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result) {
                result.M11 = matrix1.M11 / matrix2.M11;
                result.M12 = matrix1.M12 / matrix2.M12;
                result.M13 = matrix1.M13 / matrix2.M13;
                result.M14 = matrix1.M14 / matrix2.M14;
                result.M21 = matrix1.M21 / matrix2.M21;
                result.M22 = matrix1.M22 / matrix2.M22;
                result.M23 = matrix1.M23 / matrix2.M23;
                result.M24 = matrix1.M24 / matrix2.M24;
                result.M31 = matrix1.M31 / matrix2.M31;
                result.M32 = matrix1.M32 / matrix2.M32;
                result.M33 = matrix1.M33 / matrix2.M33;
                result.M34 = matrix1.M34 / matrix2.M34;
                result.M41 = matrix1.M41 / matrix2.M41;
                result.M42 = matrix1.M42 / matrix2.M42;
                result.M43 = matrix1.M43 / matrix2.M43;
                result.M44 = matrix1.M44 / matrix2.M44;
            }

            public static Matrix Divide(Matrix matrix1, TFloat divider) {
                Matrix matrix;
                TFloat num = 1f / divider;
                matrix.M11 = matrix1.M11 * num;
                matrix.M12 = matrix1.M12 * num;
                matrix.M13 = matrix1.M13 * num;
                matrix.M14 = matrix1.M14 * num;
                matrix.M21 = matrix1.M21 * num;
                matrix.M22 = matrix1.M22 * num;
                matrix.M23 = matrix1.M23 * num;
                matrix.M24 = matrix1.M24 * num;
                matrix.M31 = matrix1.M31 * num;
                matrix.M32 = matrix1.M32 * num;
                matrix.M33 = matrix1.M33 * num;
                matrix.M34 = matrix1.M34 * num;
                matrix.M41 = matrix1.M41 * num;
                matrix.M42 = matrix1.M42 * num;
                matrix.M43 = matrix1.M43 * num;
                matrix.M44 = matrix1.M44 * num;
                return matrix;
            }

            public static void Divide(ref Matrix matrix1, TFloat divider, out Matrix result) {
                TFloat num = 1f / divider;
                result.M11 = matrix1.M11 * num;
                result.M12 = matrix1.M12 * num;
                result.M13 = matrix1.M13 * num;
                result.M14 = matrix1.M14 * num;
                result.M21 = matrix1.M21 * num;
                result.M22 = matrix1.M22 * num;
                result.M23 = matrix1.M23 * num;
                result.M24 = matrix1.M24 * num;
                result.M31 = matrix1.M31 * num;
                result.M32 = matrix1.M32 * num;
                result.M33 = matrix1.M33 * num;
                result.M34 = matrix1.M34 * num;
                result.M41 = matrix1.M41 * num;
                result.M42 = matrix1.M42 * num;
                result.M43 = matrix1.M43 * num;
                result.M44 = matrix1.M44 * num;
            }

            public static Matrix operator -(Matrix matrix1) {
                Matrix matrix;
                matrix.M11 = -matrix1.M11;
                matrix.M12 = -matrix1.M12;
                matrix.M13 = -matrix1.M13;
                matrix.M14 = -matrix1.M14;
                matrix.M21 = -matrix1.M21;
                matrix.M22 = -matrix1.M22;
                matrix.M23 = -matrix1.M23;
                matrix.M24 = -matrix1.M24;
                matrix.M31 = -matrix1.M31;
                matrix.M32 = -matrix1.M32;
                matrix.M33 = -matrix1.M33;
                matrix.M34 = -matrix1.M34;
                matrix.M41 = -matrix1.M41;
                matrix.M42 = -matrix1.M42;
                matrix.M43 = -matrix1.M43;
                matrix.M44 = -matrix1.M44;
                return matrix;
            }

            public static bool operator ==(Matrix matrix1, Matrix matrix2) {
                return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
            }

            public static bool operator !=(Matrix matrix1, Matrix matrix2) {
                if (((((matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12)) && ((matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14))) && (((matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)))) && ((((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34))) && (((matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42)) && (matrix1.M43 == matrix2.M43)))) {
                    return !(matrix1.M44 == matrix2.M44);
                }
                return true;
            }

            public static Matrix operator +(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 + matrix2.M11;
                matrix.M12 = matrix1.M12 + matrix2.M12;
                matrix.M13 = matrix1.M13 + matrix2.M13;
                matrix.M14 = matrix1.M14 + matrix2.M14;
                matrix.M21 = matrix1.M21 + matrix2.M21;
                matrix.M22 = matrix1.M22 + matrix2.M22;
                matrix.M23 = matrix1.M23 + matrix2.M23;
                matrix.M24 = matrix1.M24 + matrix2.M24;
                matrix.M31 = matrix1.M31 + matrix2.M31;
                matrix.M32 = matrix1.M32 + matrix2.M32;
                matrix.M33 = matrix1.M33 + matrix2.M33;
                matrix.M34 = matrix1.M34 + matrix2.M34;
                matrix.M41 = matrix1.M41 + matrix2.M41;
                matrix.M42 = matrix1.M42 + matrix2.M42;
                matrix.M43 = matrix1.M43 + matrix2.M43;
                matrix.M44 = matrix1.M44 + matrix2.M44;
                return matrix;
            }

            public static Matrix operator -(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 - matrix2.M11;
                matrix.M12 = matrix1.M12 - matrix2.M12;
                matrix.M13 = matrix1.M13 - matrix2.M13;
                matrix.M14 = matrix1.M14 - matrix2.M14;
                matrix.M21 = matrix1.M21 - matrix2.M21;
                matrix.M22 = matrix1.M22 - matrix2.M22;
                matrix.M23 = matrix1.M23 - matrix2.M23;
                matrix.M24 = matrix1.M24 - matrix2.M24;
                matrix.M31 = matrix1.M31 - matrix2.M31;
                matrix.M32 = matrix1.M32 - matrix2.M32;
                matrix.M33 = matrix1.M33 - matrix2.M33;
                matrix.M34 = matrix1.M34 - matrix2.M34;
                matrix.M41 = matrix1.M41 - matrix2.M41;
                matrix.M42 = matrix1.M42 - matrix2.M42;
                matrix.M43 = matrix1.M43 - matrix2.M43;
                matrix.M44 = matrix1.M44 - matrix2.M44;
                return matrix;
            }

            public static Matrix operator *(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
                matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
                matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
                matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
                matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
                matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
                matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
                matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
                matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
                matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
                matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
                matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
                matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
                matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
                matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
                matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
                return matrix;
            }

            public static Matrix operator *(Matrix matrix, TFloat scaleFactor) {
                Matrix matrix2;
                TFloat num = scaleFactor;
                matrix2.M11 = matrix.M11 * num;
                matrix2.M12 = matrix.M12 * num;
                matrix2.M13 = matrix.M13 * num;
                matrix2.M14 = matrix.M14 * num;
                matrix2.M21 = matrix.M21 * num;
                matrix2.M22 = matrix.M22 * num;
                matrix2.M23 = matrix.M23 * num;
                matrix2.M24 = matrix.M24 * num;
                matrix2.M31 = matrix.M31 * num;
                matrix2.M32 = matrix.M32 * num;
                matrix2.M33 = matrix.M33 * num;
                matrix2.M34 = matrix.M34 * num;
                matrix2.M41 = matrix.M41 * num;
                matrix2.M42 = matrix.M42 * num;
                matrix2.M43 = matrix.M43 * num;
                matrix2.M44 = matrix.M44 * num;
                return matrix2;
            }

            public static Matrix operator *(TFloat scaleFactor, Matrix matrix) {
                Matrix matrix2;
                TFloat num = scaleFactor;
                matrix2.M11 = matrix.M11 * num;
                matrix2.M12 = matrix.M12 * num;
                matrix2.M13 = matrix.M13 * num;
                matrix2.M14 = matrix.M14 * num;
                matrix2.M21 = matrix.M21 * num;
                matrix2.M22 = matrix.M22 * num;
                matrix2.M23 = matrix.M23 * num;
                matrix2.M24 = matrix.M24 * num;
                matrix2.M31 = matrix.M31 * num;
                matrix2.M32 = matrix.M32 * num;
                matrix2.M33 = matrix.M33 * num;
                matrix2.M34 = matrix.M34 * num;
                matrix2.M41 = matrix.M41 * num;
                matrix2.M42 = matrix.M42 * num;
                matrix2.M43 = matrix.M43 * num;
                matrix2.M44 = matrix.M44 * num;
                return matrix2;
            }

            public static Matrix operator /(Matrix matrix1, Matrix matrix2) {
                Matrix matrix;
                matrix.M11 = matrix1.M11 / matrix2.M11;
                matrix.M12 = matrix1.M12 / matrix2.M12;
                matrix.M13 = matrix1.M13 / matrix2.M13;
                matrix.M14 = matrix1.M14 / matrix2.M14;
                matrix.M21 = matrix1.M21 / matrix2.M21;
                matrix.M22 = matrix1.M22 / matrix2.M22;
                matrix.M23 = matrix1.M23 / matrix2.M23;
                matrix.M24 = matrix1.M24 / matrix2.M24;
                matrix.M31 = matrix1.M31 / matrix2.M31;
                matrix.M32 = matrix1.M32 / matrix2.M32;
                matrix.M33 = matrix1.M33 / matrix2.M33;
                matrix.M34 = matrix1.M34 / matrix2.M34;
                matrix.M41 = matrix1.M41 / matrix2.M41;
                matrix.M42 = matrix1.M42 / matrix2.M42;
                matrix.M43 = matrix1.M43 / matrix2.M43;
                matrix.M44 = matrix1.M44 / matrix2.M44;
                return matrix;
            }

            public static Matrix operator /(Matrix matrix1, TFloat divider) {
                Matrix matrix;
                TFloat num = 1f / divider;
                matrix.M11 = matrix1.M11 * num;
                matrix.M12 = matrix1.M12 * num;
                matrix.M13 = matrix1.M13 * num;
                matrix.M14 = matrix1.M14 * num;
                matrix.M21 = matrix1.M21 * num;
                matrix.M22 = matrix1.M22 * num;
                matrix.M23 = matrix1.M23 * num;
                matrix.M24 = matrix1.M24 * num;
                matrix.M31 = matrix1.M31 * num;
                matrix.M32 = matrix1.M32 * num;
                matrix.M33 = matrix1.M33 * num;
                matrix.M34 = matrix1.M34 * num;
                matrix.M41 = matrix1.M41 * num;
                matrix.M42 = matrix1.M42 * num;
                matrix.M43 = matrix1.M43 * num;
                matrix.M44 = matrix1.M44 * num;
                return matrix;
            }

            static Matrix() {
                _identity = new Matrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
            }
            // Nested Types
            [StructLayout(LayoutKind.Sequential)]
            private struct CanonicalBasis {
                public Vector3 Row0;
                public Vector3 Row1;
                public Vector3 Row2;
            }

            //[StructLayout(LayoutKind.Sequential)]
            //private struct VectorBasis {
            //    public unsafe Vector3* Element0;
            //    public unsafe Vector3* Element1;
            //    public unsafe Vector3* Element2;
            //}
        }
    }
}