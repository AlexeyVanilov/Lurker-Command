using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Core {
    public sealed class Transform
    {
        private Vector2 _localPosition;
        private Vector2 _localScale;
        private float _localRotation;

        private Matrix _worldMatrix;
        private bool _isDirty = true;

        private Transform _parent;
        private readonly List<Transform> _children = new();

        public Vector2 LocalPosition
        {
            get => _localPosition;
            set => SetDirty(ref _localPosition, value);
        }

        public Vector2 LocalScale
        {
            get => _localScale;
            set => SetDirty(ref _localScale, value);
        }

        public float LocalRotation
        {
            get => _localRotation;
            set
            {
                if (_localRotation == value) return;
                _localRotation = value;
                SetDirty();
            }
        }

        public Transform Parent
        {
            get => _parent;
            set
            {
                _parent?._children.Remove(this);
                _parent = value;
                _parent?._children.Add(this);
                SetDirty();
            }
        }

        public Vector2 WorldPosition
        {
            get
            {
                UpdateMatrix();
                return new Vector2(_worldMatrix.M41, _worldMatrix.M42);
            }
        }

        public Transform()
        {
            _localPosition = Vector2.Zero;
            _localScale = Vector2.One;
            _localRotation = 0f;
            _worldMatrix = Matrix.Identity;
        }

        private void SetDirty<T>(ref T field, T value) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            SetDirty();
        }

        private void SetDirty()
        {
            if (_isDirty) return;
            _isDirty = true;

            foreach (var child in _children)
            {
                child.SetDirty();
            }
        }

        private void UpdateMatrix()
        {
            if (!_isDirty) return;

            _worldMatrix = Matrix.CreateScale(_localScale.X, _localScale.Y, 1f) *
                           Matrix.CreateRotationZ(_localRotation) *
                           Matrix.CreateTranslation(_localPosition.X, _localPosition.Y, 0f);

            if (_parent != null)
            {
                _parent.UpdateMatrix();
                _worldMatrix *= _parent._worldMatrix;
            }

            _isDirty = false;
        }

        public Matrix GetWorldMatrix()
        {
            UpdateMatrix();
            return _worldMatrix;
        }
    }
}