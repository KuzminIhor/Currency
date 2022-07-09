using System;
using System.Collections.Generic;
using System.Threading;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CurrencyApp.Core
{
    public static class ServiceLocator
    {
        private static readonly Queue<string> ThreadVisibilityStack = new Queue<string>();
        private const int MaxThreadVisibilityCount = 200;
        private static IUnityContainer container;
        private static Dictionary<string, IUnityContainer> containers;

        /// <summary>
        /// Limit visibility of registered objects by a thread. For Unit Testing.
        /// </summary>
        public static bool UseThreadVisibility { get; set; } = false;

        /// <summary>Configured Unity Container.</summary>
        public static IUnityContainer Container
        {
            get
            {
                if (ServiceLocator.container == null)
                    ServiceLocator.container = (IUnityContainer)new UnityContainer();
                return ServiceLocator.container;
            }
            set => ServiceLocator.container = value;
        }

        /// <summary>Start the service locator with a Unity Container.</summary>
        /// <param name="unityContainer">A instance of the Unity IUnityContainer interface.</param>
        internal static void Initialize(IUnityContainer unityContainer) => ServiceLocator.container = unityContainer;

        /// <summary>Register singleton</summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TClass"></typeparam>
        public static void RegisterSingleton<TInterface, TClass>()
          where TInterface : class
          where TClass : TInterface
        {
            ServiceLocator.GetContainer().RegisterType<TInterface, TClass>((ITypeLifetimeManager)new ContainerControlledLifetimeManager());
        }

        /// <summary>Register singleton</summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TClass">Class type</typeparam>
        /// <param name="name">Class name</param>
        public static void RegisterSingleton<TInterface, TClass>(string name)
          where TInterface : class
          where TClass : TInterface
        {
            ServiceLocator.GetContainer().RegisterType<TInterface, TClass>(name, (ITypeLifetimeManager)new ContainerControlledLifetimeManager());
        }

        /// <summary>Register singleton</summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <param name="instance">Class instance</param>
        public static void RegisterSingleton<TInterface>(TInterface instance) where TInterface : class => ServiceLocator.GetContainer().RegisterInstance<TInterface>(instance, (IInstanceLifetimeManager)new ContainerControlledLifetimeManager());

        /// <summary>Register singleton</summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <param name="instance">Class instance</param>
        /// <param name="name">Class name</param>
        public static void RegisterSingleton<TInterface>(TInterface instance, string name) where TInterface : class => ServiceLocator.GetContainer().RegisterInstance<TInterface>(name, instance, (IInstanceLifetimeManager)new ContainerControlledLifetimeManager());

        /// <summary>Register singleton</summary>
        /// <param name="from">Interface type</param>
        /// <param name="to">Class type</param>
        public static void RegisterSingleton(Type from, Type to) => ServiceLocator.GetContainer().RegisterType(from, to, (ITypeLifetimeManager)new ContainerControlledLifetimeManager());

        /// <summary>Register singleton</summary>
        /// <param name="from">Interface type</param>
        /// <param name="to">Class type</param>
        /// <param name="name">Class name</param>
        public static void RegisterSingleton(Type from, Type to, string name) => ServiceLocator.GetContainer().RegisterType(from, to, name, (ITypeLifetimeManager)new ContainerControlledLifetimeManager());

        /// <summary>Register type</summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TClass">Class type</typeparam>
        public static void RegisterType<TInterface, TClass>()
          where TInterface : class
          where TClass : TInterface
        {
            ServiceLocator.GetContainer().RegisterType<TInterface, TClass>();
        }

        /// <summary>Register type</summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TClass">Class type</typeparam>
        /// <param name="name">Class name</param>
        public static void RegisterType<TInterface, TClass>(string name)
          where TInterface : class
          where TClass : TInterface
        {
            ServiceLocator.GetContainer().RegisterType<TInterface, TClass>(name);
        }

        /// <summary>Register type</summary>
        /// <param name="from">Interface type</param>
        /// <param name="to">Class type</param>
        public static void RegisterType(Type from, Type to) => ServiceLocator.GetContainer().RegisterType(from, to);

        /// <summary>Register type</summary>
        /// <param name="from">Interface type</param>
        /// <param name="to">Class type</param>
        /// <param name="name">Class name</param>
        public static void RegisterType(Type from, Type to, string name) => ServiceLocator.GetContainer().RegisterType(from, to, name);

        /// <summary>Register factory</summary>
        /// <typeparam name="TInterface">factory interface</typeparam>
        /// <param name="factory">factory</param>
        public static void RegisterType<TInterface>(Func<TInterface> factory) => ServiceLocator.GetContainer().RegisterType(typeof(TInterface), (InjectionMember)new InjectionFactory((Func<IUnityContainer, object>)(f => (object)factory())));

        /// <summary>Register factory</summary>
        /// <typeparam name="TInterface">factory interface</typeparam>
        /// <param name="factory">factory</param>
        /// <param name="name">factory name</param>
        public static void RegisterFactory<TInterface>(
          Func<IUnityContainer, TInterface> factory,
          string name)
        {
            ServiceLocator.GetContainer().RegisterFactory(typeof(TInterface), name, (Func<IUnityContainer, Type, string, object>)((unityContainerMain, type, param) => (object)factory(ServiceLocator.container)));
        }

        /// <summary>
        ///   Retrieves an instance of the specified type from the container.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <returns>Returns an instance of the specified type.</returns>
        public static T Get<T>() => ServiceLocator.GetContainer().Resolve<T>();

        /// <summary>
        ///   Retrieves an instance of the specified named type from the container.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="name">Name the type was registered with.</param>
        /// <returns>Returns an instance of the specified type.</returns>
        public static T Get<T>(string name) => ServiceLocator.GetContainer().Resolve<T>(name);

        /// <summary>
        /// Retrieves all instances of the specified type from the container.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <returns>A list of the specified type.</returns>
        public static IEnumerable<T> GetAll<T>() => ServiceLocator.GetContainer().ResolveAll<T>();

        private static IUnityContainer GetContainer()
        {
            if (!ServiceLocator.UseThreadVisibility)
                return ServiceLocator.Container;
            if (ServiceLocator.containers == null)
                ServiceLocator.containers = new Dictionary<string, IUnityContainer>();
            string name1 = Thread.CurrentThread.Name;
            if (name1 == null || name1.Length <= 2)
            {
                Thread.CurrentThread.Name = string.Format("id_{0}", (object)Guid.NewGuid());
                ServiceLocator.ThreadVisibilityStack.Enqueue(Thread.CurrentThread.Name);
                if (ServiceLocator.ThreadVisibilityStack.Count > 200)
                    ServiceLocator.ThreadVisibilityStack.Dequeue();
            }
            string name2 = Thread.CurrentThread.Name;
            if (ServiceLocator.containers.ContainsKey(name2))
                return ServiceLocator.containers[name2];
            UnityContainer container = new UnityContainer();
            ServiceLocator.containers.Add(name2, (IUnityContainer)container);
            return (IUnityContainer)container;
        }
    }
}