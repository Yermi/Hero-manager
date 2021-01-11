using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ITrainerService
    {
        void AddTrainer(Trainer p_trainer);

        Trainer GetTrainerByCredentials(string p_email, string p_password);
        List<Trainer> GetAllTrainers();
    }

    public class TrainerService : ITrainerService
    {
        private readonly Context _context;
        public TrainerService(Context p_context)
        {
            _context = p_context;
        }
        public void AddTrainer(Trainer p_trainer)
        {
            if (_context.Trainers.Any(x => x.Email == p_trainer.Email))
            {
                throw new WebApiException(400, "Email already have an account");
            }
            _context.Trainers.Add(p_trainer);
            _context.SaveChanges();
        }

        public List<Trainer> GetAllTrainers()
        {
            return _context.Trainers.ToList();
        }

        public Trainer GetTrainerByCredentials(string p_email, string p_password)
        {
            return _context.Trainers.Include(x => x.Heros).AsEnumerable()            
            .Where(
                x => 
                x.Email == p_email &&
                BCEngine.Decrypt(x.Password) == p_password
            ).FirstOrDefault();
        }
    }
}