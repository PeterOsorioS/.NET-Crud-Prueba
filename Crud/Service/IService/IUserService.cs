using Crud.DTO;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service.IService
{
    public interface IUserService
    {
        public IActionResult Create(RegisterDTO user);
        public IActionResult Authentication(LoginDTO userLogin);
    }
}
