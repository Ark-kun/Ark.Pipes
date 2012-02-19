using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Ark.Pipes;
using Ark.Xna;
using Ark.Xna.Transforms;

namespace Ark.Shohou {
    public class Frame {
        public IInvertibleTransform<Vector3> Transform { get; set; }
    }

    public class DynamicFrame {
        private IInvertibleTransform<Vector3> _transform;
        private DynamicFrame _parent;

        public DynamicFrame Parent {
            get { return _parent; }
            set { _parent = value; }
        }

        public DynamicFrame()
            : this(null, Transform<Vector3>.Identity) {
        }

        public DynamicFrame(DynamicFrame parent)
            : this(parent, Transform<Vector3>.Identity) {
        }

        public DynamicFrame(IInvertibleTransform<Vector3> transform)
            : this(null, transform) {
        }

        public DynamicFrame(DynamicFrame parent, IInvertibleTransform<Vector3> transform) {
            _parent = parent;
            _transform = transform;
        }

        public IInvertibleTransform<Vector3> Transform {
            get { return _transform; }
            set { _transform = value; }
        }

        public IInvertibleTransform<Vector3> GetAbsoluteTransform() {
            DynamicFrame frame = this;
            var transforms = new List<IInvertibleTransform<Vector3>>();
            transforms.Add(_transform);
            while (frame._parent != null) {
                frame = frame._parent;
                transforms.Add(frame._transform);
            }
            return new InvertibleTransformStack<Vector3>(transforms);
        }

        public static DynamicFrame FindCommonAncestor(DynamicFrame src, DynamicFrame dst) {
            if (src == dst)
                return src;
            var srcFrames = new HashSet<DynamicFrame>();
            srcFrames.Add(src);
            var dstFrames = new HashSet<DynamicFrame>();
            dstFrames.Add(dst);
            while (src != null || dst != null) {
                if (src != null && (src = src.Parent) != null) {
                    srcFrames.Add(src);
                    if (dstFrames.Contains(src))
                        return src;
                }
                if (dst != null && (dst = dst.Parent) != null) {
                    dstFrames.Add(dst);
                    if (srcFrames.Contains(dst))
                        return dst;
                }
            }
            return null;
        }


        public static IInvertibleTransform<Vector3> CreateRelativeTransform(DynamicFrame src, DynamicFrame dst) {
            var ancestor = FindCommonAncestor(src, dst);
            if (ancestor == null)
                throw new ArgumentException("Frames have no common ancestor");

            var srcPath = Functional.For(src, f => f != ancestor, f => f.Parent).Select(f => f.Transform.Inverse);
            var dstPath = Functional.For(dst, f => f != ancestor, f => f.Parent).Select(f => f.Transform);

            return new InvertibleTransformStack<Vector3>(srcPath.Concat(dstPath.Reverse()));
        }
    }
}
