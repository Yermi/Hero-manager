using System;
using System.Linq;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ITrainService
    {
        bool AddTrain(Train p_train);
    }

    public class TrainService : ITrainService
    {
        private readonly Context _context;

        public TrainService(Context p_context)
        {
            _context = p_context;
        }
        public bool AddTrain(Train p_train)
        {
            if (CanAddTrain(p_train.TrainerId, p_train.HeroId, p_train.TrainDate))
            {
                _context.Trains.Add(p_train);
                _context.SaveChanges();
                return true;
            }
            throw new WebApiException(405, "This hero have been already trained 5 times for today");
        }

        private bool CanAddTrain(int p_trainerId, int p_heroId, DateTime p_trainDate)
        {
            return _context.Trains?.Count(x => x.TrainerId == p_trainerId &&
             x.HeroId == p_heroId &&
             x.TrainDate.Date == p_trainDate.Date) < 5;
        }
    }
}