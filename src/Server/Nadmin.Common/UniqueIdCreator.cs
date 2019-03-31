using Snowflake.Core;

namespace Nadmin.Common
{
    public static class UniqueIdCreator
    {
        private static readonly IdWorker Worker = new IdWorker(1, 1);

        public static string Get()
        {
            long id = Worker.NextId();
            return id.ToString();
        }
    }
}