using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Abstract
{
    public interface IHasOwner
    {
        long? UserId { get; set; }
    }
}
