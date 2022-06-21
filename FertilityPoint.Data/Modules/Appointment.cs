using System;
using System.Collections.Generic;
using System.Text;

namespace FertilityPoint.Data.Modules
{
   public  class Appointment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
