using System;
using System.Collections.Generic;
using System.Text;

namespace FertilityPoint.Data.Modules
{
   public partial class County
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
