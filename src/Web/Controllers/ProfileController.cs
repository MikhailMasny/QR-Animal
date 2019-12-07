﻿using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public ProfileController(ILogger<ProfileController> logger,
                                 IMediator mediator,
                                 IIdentityService identityService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Profile");

            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

            var qrq = new GetAnimalsQuery();
            var qr = await _mediator.Send(qrq);

            var userAnimals = qr.Where(a => a.UserId == userId);

            return View(userAnimals);
        }
    }
}