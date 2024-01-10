using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Selivura
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute { }

    public interface IDependecyProvider { }

    [DefaultExecutionOrder(-1000)]
    public class Injector : Singleton<Injector>
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();
        protected override void Awake()
        {
            base.Awake();

            var providers = FindMonoBehaviours().OfType<IDependecyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            var injectables = FindMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }
        public void Inject(object instance)
        {
            var type = instance.GetType();
            var injectableFields = type.GetFields(bindingFlags).Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                {
                    throw new Exception($"Failed to inject (BRUH) {fieldType.Name} int {type.Name}");
                }

                injectableField.SetValue(instance, resolvedInstance);
            }

            var injectableMethods = type.GetMethods(bindingFlags).Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParams = injectableMethod.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .ToArray();
                var resolvedInstances = requiredParams.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new Exception($"Failed to inject(BRUH) {type.Name}.{injectableMethod.Name}");
                }

                injectableMethod.Invoke(instance, resolvedInstances);
            }
        }
        object Resolve(Type type)
        {
            registry.TryGetValue(type, out var instance);
            return instance;
        }
        static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
        public void RegisterProvider(IDependecyProvider provider)
        {
            var methods = provider.GetType().GetMethods(bindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, null);
                if (providedInstance != null)
                {
                    registry.Add(returnType, providedInstance);
                }
                else
                {
                    throw new Exception($"Provider {provider.GetType().Name} returned null(тут я афигел) for {returnType.Name}");
                }
            }
        }

        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }
    }
}
