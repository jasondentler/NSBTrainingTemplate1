using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using SignalR.Hosting.AspNet;
using SignalR.Infrastructure;

namespace Hospital.Web.App_Start
{
    public class SignalRModule : NinjectModule 
    {
        public override void Load()
        {
            var resolver = new SignalRDependencyResolver(Kernel);
            RegisterDefaults();
            AspNetHost.SetResolver(resolver);
        }

        private void RegisterDefaults()
        {
            var defaults = GetDefaultRegistrationMap();
            defaults
                .SelectMany(kv => kv.Value, (kv, v) => new KeyValuePair<Type, Func<object>>(kv.Key, v))
                .ToList()
                .ForEach(registration => Kernel.Bind(registration.Key)
                                             .ToMethod(ctx => registration.Value()));
        }

        private static Dictionary<Type, IList<Func<object>>> GetDefaultRegistrationMap()
        {
            var def = new DefaultDependencyHack();
            return def.Resolvers;
        }

        /// <summary>
        /// In DefaultDependencyResolver, the defaults are registered from the constructor.
        /// This hack works around the evil problem of DefaultDependencyResolver calling 
        /// virtual members from the constructor.
        /// </summary>
        private class DefaultDependencyHack : DefaultDependencyResolver
        {

            private readonly Dictionary<Type, IList<Func<object>>> _resolvers = new Dictionary<Type, IList<Func<object>>>();

            public Dictionary<Type, IList<Func<object>>> Resolvers { get { return _resolvers; }}

            public override object GetService(Type serviceType)
            {
                IList<Func<object>> activators;
                if (_resolvers.TryGetValue(serviceType, out activators))
                {
                    if (activators.Count == 0)
                    {
                        return null;
                    }
                    if (activators.Count > 1)
                    {
                        throw new InvalidOperationException(String.Format("Multiple activators for type {0} are registered. Please call GetServices instead.", serviceType.FullName));
                    }
                    return activators[0]();
                }
                return null;
            }

            public override IEnumerable<object> GetServices(Type serviceType)
            {
                IList<Func<object>> activators;
                if (_resolvers.TryGetValue(serviceType, out activators))
                {
                    if (activators.Count == 0)
                    {
                        return null;
                    }
                    return activators.Select(r => r()).ToList();
                }
                return null;
            }

            public override void Register(Type serviceType, Func<object> activator)
            {
                IList<Func<object>> activators;
                if (!_resolvers.TryGetValue(serviceType, out activators))
                {
                    activators = new List<Func<object>>();
                    _resolvers.Add(serviceType, activators);
                }
                else
                {
                    activators.Clear();
                }
                activators.Add(activator);
            }

            public override void Register(Type serviceType, IEnumerable<Func<object>> activators)
            {
                IList<Func<object>> list;
                if (!_resolvers.TryGetValue(serviceType, out list))
                {
                    list = new List<Func<object>>();
                    _resolvers.Add(serviceType, list);
                }
                else
                {
                    list.Clear();
                }
                foreach (var a in activators)
                {
                    list.Add(a);
                }
            }

        }

    }
}