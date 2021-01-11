using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/heros")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;
        private readonly ITrainService _trainService;

        public HeroController(
            IHeroService p_heroService,
            ITrainService p_trainService
            )
        {
            _heroService = p_heroService;
            _trainService = p_trainService;
        }

        [HttpGet]
        public List<Hero> GetHerosByTrainer(int trainerId)
        {
            return _heroService.GetHerosByTrainerId(trainerId);
        }

        [HttpPost]
        public Hero AddHero(Hero p_hero)
        {
            return _heroService.AddHero(p_hero);
        }

        [HttpPost]
        [Route("train")]
        public decimal TrainHero(Train train)
        {
            train.TrainDate = DateTime.Now;
            var addTrainRes = _trainService.AddTrain(train);
            if (addTrainRes)
            {
                return _heroService.UpdatePower(train.HeroId);
            }
            throw new WebApiException(405, "error in add train for hero " + train.HeroId);
        }
    }
}