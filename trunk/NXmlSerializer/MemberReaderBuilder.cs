using NDependencyInjection.interfaces;
using NSerializer.Framework.Readers;


namespace NSerializer
{
    public class MemberReaderBuilder<T> : ISubsystemBuilder
        where T : IMemberReader
    {
        public void Build(ISystemDefinition system)
        {
            system.HasSingleton<T>()
                .Provides<IMemberReader>();
        }
    }
}