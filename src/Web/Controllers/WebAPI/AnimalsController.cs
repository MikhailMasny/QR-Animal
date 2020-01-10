using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetQRCode;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Web.Extensions;
using Masny.QRAnimal.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers.WebAPI
{
    /// <summary>
    /// Контроллер управления животными пользователя через API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public AnimalsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получить список всех животных (публичных).
        /// </summary>
        /// <returns>Список животных (json).</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPublicAnimalsAsync()
        {
            var animalsQuery = new GetAnimalsQuery();

            var publicAnimals = await _mediator.Send(animalsQuery);

            if (publicAnimals == null)
            {
                return Ok();
            }

            return Json(publicAnimals);
        }
    }
}
