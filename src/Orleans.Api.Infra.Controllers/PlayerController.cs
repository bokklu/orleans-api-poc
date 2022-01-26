using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Orleans.Api.Application.Interfaces;
using Orleans.Api.Infra.Controllers.Extensions;
using Orleans.Api.Infra.Controllers.Validators;
using Orleans.Api.Models;
using Orleans.Api.Models.Input;

namespace Orleans.Api.Infra.Controllers
{
    [ApiController]
    [Route("player")]
    public class PlayerController : BaseController
    {
        private static readonly UpsertPlayerValidator _upsertPlayerValidator = new UpsertPlayerValidator();
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public Task<IActionResult> UpsertPlayerAsync([FromBody] Player player)
        {
            return HandleRequestAsync(async () =>
            {
                var validationResult = _upsertPlayerValidator.Validate(player);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                throw new Exception();

                await _playerService.CreatePlayerAsync(player.ToDomain());

                return NoContent();
            });
        }

        [HttpGet]
        public Task<IActionResult> GetPlayerAsync(int playerId)
        {
            return HandleRequestAsync(async () =>
            {
                if (playerId <= 0)
                    return BadRequest(new ValidationFailure(nameof(playerId), "Invalid id provided.", playerId));

                var player = await _playerService.GetPlayerAsync(playerId);

                return player == null ? NotFound() : Ok(player);
            });
        }

        [HttpPut("level")]
        public Task<IActionResult> UpdateLevelAsync(int playerId, LevelAction levelAction)
        {
            return HandleRequestAsync(async () =>
            {
                if (playerId <= 0)
                    return BadRequest(new ValidationFailure(nameof(playerId), "Invalid id provided.", playerId));

                if (!Enum.IsDefined(typeof(LevelAction), levelAction))
                    return BadRequest(new ValidationFailure(nameof(levelAction), "Invalid action provided.", levelAction));

                var isFound = await _playerService.UpdateLevelAsync(playerId, (Domain.LevelAction)levelAction);

                return !isFound ? NotFound() : NoContent();
            });
        }
    }
}
