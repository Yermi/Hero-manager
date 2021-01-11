using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure;
using WebApi.Infrastructure.Dto;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/trainers")]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService p_trainerService)
        {
            _trainerService = p_trainerService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public bool Register(Trainer p_registerRequest)
        {
            try
            {
                var encrypted = BCEngine.Encrypt(p_registerRequest.Password);
                p_registerRequest.Password = encrypted;

                p_registerRequest.JoinedCompany = DateTime.Now;
                _trainerService.AddTrainer(p_registerRequest);
                p_registerRequest.Password = null;
                return true;
            }
            catch (System.Exception ex)
            {
                throw new WebApiException(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public Trainer Login(LoginRequest p_loginRequest)
        {
            var trainer = _trainerService.GetTrainerByCredentials(p_loginRequest.Email, p_loginRequest.Password);
            if (trainer != null)
            {
                trainer.Password = null;
                return trainer;
            }
            throw new WebApiException(401, "Email or password are incorrect");
        }

        [Authorize]
        [HttpGet]
        public List<Trainer> GetTrainers()
        {
            return _trainerService.GetAllTrainers();
        }
    }
}