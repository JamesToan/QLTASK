using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Abstract
{
    public interface IDateTracking
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
