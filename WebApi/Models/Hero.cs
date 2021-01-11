using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace WebApi.Models
{
    public class Hero 
    {
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }
        public Ability Ability { get; set; }
        public DateTime StartDate { get; set; }
        [NotMapped]
        public Color SuitColors { get; set; }
        public decimal StartPower { get; set; }
        public decimal CurrentPower { get; set; }
        
        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
    }


    public enum Ability
    {
        Attacker = 1,
        Defender = 2
    }
}