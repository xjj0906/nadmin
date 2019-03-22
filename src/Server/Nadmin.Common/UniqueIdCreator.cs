using Snowflake.Core;

namespace Nadmin.Common
{
    public static class UniqueIdCreator
    {
        public static string Get()
        {
            var worker = new IdWorker(1, 1);
            long id = worker.NextId();
            return id.ToString();
        }
    }
}