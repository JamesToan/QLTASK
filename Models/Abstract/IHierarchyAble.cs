using System.Collections.Generic;

namespace coreWeb.Models.Abstract
{
    public interface IHierarchyAble<T> where T : IBaseEntity
    {
        T Parent { get; set; }

        List<T> Children { get; set; }
    }
}
