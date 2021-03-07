using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Registrations;
using System;
using System.Collections.Generic;

namespace OnlineCouser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly RegistrationService _registrationService;
        private readonly RegistrationCreation _registrationCreation;
        private readonly RegistrationCancelation _registrationCancelation;
        private readonly RegistrationConclusion _registrationConclusion;

        public RegistrationController(
            RegistrationService registrationService, 
            RegistrationCreation registrationCreation, 
            RegistrationCancelation registrationCancelation, 
            RegistrationConclusion registrationConclusion)
        {
            _registrationService = registrationService;
            _registrationCreation = registrationCreation;
            _registrationCancelation = registrationCancelation;
            _registrationConclusion = registrationConclusion;
        }

        [HttpGet]
        public IEnumerable<RegistrationDto> Get()
        {
            return _registrationService.GetAll();
        }

        [HttpPost]
        public IActionResult Add(RegistrationDto model)
        {
            try
            {
                _registrationCreation.Create(model);
            }
            catch (DomainException exception)
            {
                return new JsonResult(exception.ListOfRules);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return Ok();
        }

        [HttpPut("conclusion")]
        public IActionResult Conclusion(ConclusionDto model)
        {
            try
            {
                _registrationConclusion.Conclude(model.RegistrationId, model.ExpectedGrade);
            }
            catch (DomainException exception)
            {
                return new JsonResult(exception.ListOfRules);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return Ok();
        }

        [HttpPut("cancelation")]
        public IActionResult Cancelation(CancelationDto model)
        {
            try
            {
                _registrationCancelation.Cancel(model.RegistrationId);
            }
            catch (DomainException exception)
            {
                return new JsonResult(exception.ListOfRules);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return Ok();
        }
    }
}
