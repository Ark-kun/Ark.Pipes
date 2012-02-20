using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark.Geometry.Transforms {
    public class ReferenceFrame<T> {
        private IInvertibleTransform<T> _transform;
        private ReferenceFrame<T> _parent;

        public ReferenceFrame<T> Parent {
            get { return _parent; }
            set { _parent = value; }
        }

        public ReferenceFrame()
            : this(null, Transform<T>.Identity) {
        }

        public ReferenceFrame(ReferenceFrame<T> parent)
            : this(parent, Transform<T>.Identity) {
        }

        public ReferenceFrame(IInvertibleTransform<T> transform)
            : this(null, transform) {
        }

        public ReferenceFrame(ReferenceFrame<T> parent, IInvertibleTransform<T> transform) {
            _parent = parent;
            _transform = transform;
        }

        public IInvertibleTransform<T> Transform {
            get { return _transform; }
            set { _transform = value; }
        }

        public IInvertibleTransform<T> GetAbsoluteTransform() {
            ReferenceFrame<T> frame = this;
            var transforms = new List<IInvertibleTransform<T>>();
            transforms.Add(_transform);
            while (frame._parent != null) {
                frame = frame._parent;
                transforms.Add(frame._transform);
            }
            return new InvertibleTransformStack<T>(transforms);
        }

        public static ReferenceFrame<T> FindCommonAncestor(ReferenceFrame<T> src, ReferenceFrame<T> dst) {
            if (src == dst)
                return src;
            var srcFrames = new HashSet<ReferenceFrame<T>>();
            srcFrames.Add(src);
            var dstFrames = new HashSet<ReferenceFrame<T>>();
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


        public static IInvertibleTransform<T> CreateRelativeTransform(ReferenceFrame<T> src, ReferenceFrame<T> dst) {
            var ancestor = FindCommonAncestor(src, dst);
            if (ancestor == null)
                throw new ArgumentException("Frames have no common ancestor");

            var srcPath = Functional.For(src, f => f != ancestor, f => f.Parent).Select(f => f.Transform.Inverse);
            var dstPath = Functional.For(dst, f => f != ancestor, f => f.Parent).Select(f => f.Transform);

            return new InvertibleTransformStack<T>(srcPath.Concat(dstPath.Reverse()));
        }
    }
}
