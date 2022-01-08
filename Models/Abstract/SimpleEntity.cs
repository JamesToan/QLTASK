using System;
using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Abstract
{
    public class SimpleEntity : IBaseEntity
    {

        public int Id { get; set; }

    }
}
