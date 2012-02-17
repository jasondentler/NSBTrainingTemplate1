using EventStore;
using EventStore.Serialization;
using Ninject;

namespace Hospital.CommandHandlers
{
    public static class WireupExtensions
    {
        public static SerializationWireup UsingRegisteredSerializer(this PersistenceWireup wireup, IKernel kernel)
        {
            return wireup.UsingCustomSerialization(kernel.Get<ISerialize>());
        }
    }
}
