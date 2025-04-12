using System;
using System.Collections.Generic;
using Cathei.PinInject;
using Cathei.PinInject.Internal;

namespace NestedDIContainer.Unity3d.Tests.PinInject
{
    public class DependencyContainerCopy : DependencyContainer
    {
        // type -> constructor
        // public Dictionary<Type, Func<object>> _builders = new Dictionary<Type, Func<object>>();

        // type -> instance
        private readonly Dictionary<(Type, string), object> _instances = new();

        // direct parent to current container
        private IDependencyContainer _parent;

        public DependencyContainerCopy() : base()
        {
            Reset();
        }

        internal void SetParent(IDependencyContainer parent)
        {
            _parent = parent;
        }

        internal void Reset()
        {
            _parent = null;
            // _builders.Clear();
            _instances.Clear();

            // container itself is always bound
            Bind(typeof(IDependencyContainer), null, this);
        }

        /// <summary>
        /// Resolve given type and name, returns the resolved object.
        /// returns null if there is no binding.
        /// </summary>
        public object Resolve(Type type, string name)
        {
            if (_instances.TryGetValue((type, name), out var instance))
                return instance;

            // failed to resolve dependency
            if (_parent == null)
                return null;

            // tail call would be optimized
            return _parent.Resolve(type, name);
        }

        internal void Bind(Type type, string name, object instance)
        {
            var key = (type, name);

            if (_instances.ContainsKey(key))
            {
                throw new InjectionException(
                    $"Same binding is already provided in this context! Type: {type}, Name: {name}");
            }

            _instances.Add(key, instance);
        }
    }
}