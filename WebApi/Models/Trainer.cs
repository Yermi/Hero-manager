using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime StartTraining { get; set; }
        public DateTime JoinedCompany { get; set; }

        public virtual ICollection<Hero> Heros { get; set; }
    }
}