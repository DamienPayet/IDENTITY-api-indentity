using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Di2P1G3.Dependency.Injection
{
    public sealed class ServiceContainerBuilder
    {
        private static readonly Lazy<ServiceContainerBuilder> _lazy =
            new(() => new ServiceContainerBuilder());

        private readonly IDictionary<Type, DiServiceDescriptor> _serviceDescriptors =
            new Dictionary<Type, DiServiceDescriptor>();

        private ServiceContainerBuilder()
        {
        }

        public static ServiceContainerBuilder Instance => _lazy.Value;

        /// <summary>
        ///     Retourne un service à partir de son type.
        /// </summary>
        /// <typeparam name="T">Le type de service à récupérer.</typeparam>
        /// <returns>L'implémentation du service de type <typeparamref name="T" />.</returns>
        /// <exception cref="Exception">Le service demandé n'a pas d'implémentation.</exception>
        public T GetService<T>()
        {
            try
            {
                _serviceDescriptors.TryGetValue(typeof(T), out var descriptor);
                if (descriptor == null)
                    throw new ApplicationException($"Le service {typeof(T)} n'a pas été enregistré");

                return descriptor.Lifetime switch
                {
                    InstanceLifetime.Transient => descriptor.ConstructorArguments != null
                        ? (T)Activator.CreateInstance(descriptor.Implementation,
                            BindingFlags.Default, descriptor.ConstructorArguments)
                        : (T)Activator.CreateInstance(descriptor.Implementation),
                    InstanceLifetime.Singleton => (T)descriptor.Instance,
                    InstanceLifetime.Scoped => descriptor.ConstructorArguments != null
                        ? (T)Activator.CreateInstance(descriptor.Implementation, BindingFlags.Default,
                            descriptor.ConstructorArguments)
                        : (T)Activator.CreateInstance(descriptor.Implementation),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Enregistre un nouveau service avec un cycle de vie transient.
        /// </summary>
        /// <typeparam name="T">Implémentation du service.</typeparam>
        /// <typeparam name="K">Contrat du service.</typeparam>
        public void RegisterTransient<K, T>(object[] args = null) where T : K =>
            Register<K, T>(InstanceLifetime.Transient, args);

        /// <summary>
        ///     Enregistre un nouveau service avec un cycle de vie unique.
        /// </summary>
        /// <typeparam name="T">Implémentation du service.</typeparam>
        /// <typeparam name="K">Contrat du service.</typeparam>
        public void RegisterSingleton<K, T>(object[] args = null) where T : K =>
            Register<K, T>(InstanceLifetime.Singleton, args);

        /// <summary>
        ///     Enregistre un nouveau service à partir d'un objet.
        ///     Le cycle de vie sera un singleton.
        /// </summary>
        /// <param name="obj">Implémentation de l'interface.</param>
        /// <typeparam name="T">Interface de l'objet.</typeparam>
        public void RegisterSingleton<T>(object obj)
        {
            try
            {
                _serviceDescriptors.Add(typeof(T),
                    new DiServiceDescriptor(typeof(T), obj.GetType(), InstanceLifetime.Singleton, null, obj));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Enregistre un nouveau service avec un cycle de vie limité à la portée.
        /// </summary>
        /// <typeparam name="T">Implémentation du service.</typeparam>
        /// <typeparam name="K">Contrat du service.</typeparam>
        public void RegisterScoped<K, T>(object[] args = null) where T : K, IDisposable =>
            Register<K, T>(InstanceLifetime.Scoped, args);

        /// <summary>
        ///     Enregistre un nouveau service à partir de son interface.
        /// </summary>
        /// <typeparam name="T">Implémentation du service.</typeparam>
        /// <typeparam name="K">Contrat du service.</typeparam>
        private void Register<K, T>(InstanceLifetime lifetime, object[] args) where T : K
        {
            try
            {
                switch (lifetime)
                {
                    case InstanceLifetime.Transient:
                    case InstanceLifetime.Scoped:
                        _serviceDescriptors.Add(typeof(K),
                            new DiServiceDescriptor(typeof(K), typeof(T), lifetime, args));
                        break;
                    case InstanceLifetime.Singleton:
                        var obj = (T)Activator.CreateInstance(typeof(T), BindingFlags.Default, args);
                        _serviceDescriptors.Add(typeof(K),
                            new DiServiceDescriptor(typeof(K), obj.GetType(), InstanceLifetime.Singleton, null, obj));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}