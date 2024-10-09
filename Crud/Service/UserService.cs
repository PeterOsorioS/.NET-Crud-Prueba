using AutoMapper;
using Crud.DTO;
using Crud.Helpers;
using Crud.Models;
using Crud.Repository.IRepository;
using Crud.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly JwtUtility _jwtUtility;
        public UserService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IMapper mapper, 
            JwtUtility jwtUtility)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _jwtUtility = jwtUtility;
        }

        public IActionResult Create(RegisterDTO userRegister)
        {
            try
            {
                if (userRegister == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "User cannot be null." });
                }

                var userEmailExist = _userRepository.GetFirstOrDefault(u => u.Email == userRegister.Email);
                if (userEmailExist != null)
                {
                    return new BadRequestObjectResult(new { success = false, message = $"The email: {userRegister.Email} is already in use." });
                }
                // Mappeo dto a modelo
                var newUser = _mapper.Map<User>(userRegister);

                // codificacion contraseña
                newUser.Password = _passwordHasher.HashPassword(newUser, userRegister.Password);
                _userRepository.Add(newUser);
                _userRepository.Save();

                var token =_jwtUtility.CreateToken(newUser);

                return new CreatedResult($"/Auth/register/{newUser.Id}", new { newUser, token });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
           
        }
        public IActionResult Authentication(LoginDTO userLogin)
        {
            try
            {
                if (userLogin == null)
                {
                    return new BadRequestObjectResult("User cannot be null.");
                }

                var user = _userRepository.GetByEmail(userLogin.Email);
                if (user == null)
                {
                    return new UnauthorizedObjectResult(new { success = false, message = "User credentials error." });

                }

                var verificationPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);
                if (verificationPassword == PasswordVerificationResult.Failed)
                {
                    return new UnauthorizedObjectResult(new { success = false, message = "User credentials error." });
                }

                var token = _jwtUtility.CreateToken(user);

                return new OkObjectResult(new { sucess = true, token });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
