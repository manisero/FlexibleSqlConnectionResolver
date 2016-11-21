using System;

namespace FlexibleSqlConnectionResolver.ConnectionResolution
{
    public interface ISqlConnectionResolver : IDisposable
    {
        ISqlConnectionWrapper Resolve();
    }
}
