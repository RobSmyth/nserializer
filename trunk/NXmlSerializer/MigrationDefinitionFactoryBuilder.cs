using NDependencyInjection.interfaces;
using NSerializer.Framework.Document;
using NSerializer.Migration;


namespace NSerializer
{
    public class MigrationDefinitionFactoryBuilder : ISubsystemBuilder
    {
        private readonly IMetaData metaData;

        public MigrationDefinitionFactoryBuilder(IMetaData metaData)
        {
            this.metaData = metaData;
        }

        public void Build(ISystemDefinition system)
        {
            system.HasInstance(metaData)
                .Provides<IMetaData>()
                .Provides<MetaData>();

            system.HasSingleton<MigrationDefinitionFactory>()
                .Provides<MigrationDefinitionFactory>();
        }
    }
}