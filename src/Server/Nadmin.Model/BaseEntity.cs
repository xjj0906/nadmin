using Nadmin.Common;
using SqlSugar;

namespace Nadmin.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public string Id { get; set; }

        public BaseEntity()
        {
            Id = UniqueIdCreator.Get();
        }
    }
}