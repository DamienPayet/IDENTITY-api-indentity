using System;

namespace Di2P1G3.Dependency.Injection
{
    internal record DiServiceDescriptor
    {
        public DiServiceDescriptor(Type @interface, Type implementation, InstanceLifetime lifetime, object[] args = null,
            object instance = null)
        {
            Interface = @interface;
            Implementation = implementation;
            ConstructorArguments = args;
            Lifetime = lifetime;
            Instance = instance;
        }

        /// <summary>
        /// Interface de l'implémentation.
        /// </summary>
        public Type Interface { get; set; }

        /// <summary>
        /// Implémentation de l'interface.
        /// </summary>
        public Type Implementation { get; set; }

        /// <summary>
        /// Arguments potentiels du constructeur.
        /// </summary>
        public object[] ConstructorArguments { get; set; }

        /// <summary>
        /// Cycle de vie.
        /// </summary>
        public InstanceLifetime Lifetime { get; set; }

        /// <summary>
        /// Instance de l'objet si c'est un singleton.
        /// </summary>
        public object Instance { get; set; }
    }
}