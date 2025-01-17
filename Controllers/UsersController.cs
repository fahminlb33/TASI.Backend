﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TASI.Backend.Domain.Users.Dtos;
using TASI.Backend.Domain.Users.Entities;
using TASI.Backend.Domain.Users.Handlers;
using TASI.Backend.Infrastructure.Resources;

namespace TASI.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            try
            {
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand model)
        {
            try
            {
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand model)
        {
            try
            {
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpGet("profile/{userId?}")]
        public async Task<IActionResult> GetProfile([FromRoute] int? userId)
        {
            try
            {
                return await _mediator.Send(new GetProfileCommand {UserId = userId});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetUsersCommand model)
        {
            try
            {
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand model)
        {
            try
            {
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = nameof(UserRole.SuperAdmin))]
        public async Task<IActionResult> Edit([FromRoute, Required] int userId, [FromBody] EditUserDto body)
        {
            try
            {
                return await _mediator.Send(new EditUserCommand
                {
                    UserId = userId,
                    Body = body
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = nameof(UserRole.SuperAdmin))]
        public async Task<IActionResult> Delete([FromRoute, Required] int userId)
        {
            try
            {
                return await _mediator.Send(new DeleteUserCommand {UserId = userId});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {0}", HttpContext.Request.Path);
                return BadRequest(ErrorMessages.InternalExceptionModel);
            }
        }
    }
}
