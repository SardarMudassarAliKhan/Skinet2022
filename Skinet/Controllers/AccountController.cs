using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities.Identity;
using Skinet.Core.Interfaces;
using Skinet.Dtos;
using Skinet.Errors;
using Skinet.Extensions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,ITokenService tokenService,IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet(nameof(GetCurruntUser))]
        public async Task<ActionResult<UserDto>> GetCurruntUser()
        {
            var user = await _userManager.FindByEmailFromClaimPrincipal(HttpContext.User);
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Emial = user.Email

            };

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmialExistAsync([FromQuery] string Email)
        {
            return await _userManager.FindByEmailAsync(Email)!=null;
        }

        [HttpGet(nameof(GetUserAddress))]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindEmialWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address,AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut(nameof(UpdateUserAddress))]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindEmialWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address,AddressDto>(user.Address));
            return BadRequest("Problem Updating the User");
        }


        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new APIResponce(401));
  
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return Unauthorized(new APIResponce(401));
            return new UserDto
            {
                Emial = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };     
        }
        [HttpPost(nameof(Register))]
        public async Task<ActionResult<UserDto>> Register(RegsiterDto regsiterDto)
        {
            if(CheckEmialExistAsync(regsiterDto.Email).Result.Value)
            {
                return BadRequest(new APIValidationErrorResponce { Errors= new 
                    [] {"Emial is already in use" } });
            }
            var user = new AppUser
            {
                DisplayName = regsiterDto.DisplayName,
                Email = regsiterDto.Email,
                UserName = regsiterDto.Email
            };
            var result = await _userManager.CreateAsync(user, regsiterDto.Password);
            if (!result.Succeeded) return BadRequest(new APIResponce(400));
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Emial = user.Email,
                Token = _tokenService.CreateToken(user)

            };
        }
    }
}
