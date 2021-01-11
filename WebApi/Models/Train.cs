using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Train
    {
        [Key]
        public int TrainId { get; set; }
        
        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        
        [ForeignKey("Hero")]
        public int HeroId { get; set; }
        public DateTime TrainDate { get; set; }
    }
}