using System;
using WebApi.Infrastructure;
using System.Linq;
using WebApi.Models;
using System.Drawing;
using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IHeroService
    {
        Hero AddHero(Hero p_hero);
        decimal UpdatePower(int p_herId);
        List<Hero> GetHerosByTrainerId(int p_trainerId);
    }

    public class HeroService : IHeroService
    {
        private readonly Context _context;
        public HeroService(Context p_context)
        {
            _context = p_context;
        }

        public Hero AddHero(Hero p_hero)
        {
            try
            {
                p_hero.CurrentPower = p_hero.StartPower;
                p_hero.StartDate = DateTime.Now;
                p_hero.SuitColors = GetRandomColor();
                _context.Heros.Add(p_hero);
                _context.SaveChanges();
                return p_hero;
            }
            catch (Exception e)
            {
                Logger.Log.Error($"failed to add hero {e.Message}");
                return null;
            }
        }

        public decimal UpdatePower(int p_herId)
        {
            var hero = _context.Heros.SingleOrDefault(x => x.Id == p_herId);
            if (hero != null)
            {
                var random = new Random();
                var p = random.NextDouble() * (0.1 - 0.01) + 1;
                var newPower = hero.CurrentPower * (decimal)p;
                hero.CurrentPower = (Math.Round(newPower, 2));
                _context.SaveChanges();
                return hero.CurrentPower;
            }
            throw new WebApiException(405, "hero not found for training");
        }

        private static Color GetRandomColor()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            return Color.FromKnownColor(randomColorName); ;
        }

        public List<Hero> GetHerosByTrainerId(int p_trainerId)
        {
            return _context.Heros.Where(x => x.TrainerId == p_trainerId).ToList();
        }
    }
}