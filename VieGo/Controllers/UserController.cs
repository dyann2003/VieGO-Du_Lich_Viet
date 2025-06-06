using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Threading.Tasks;
using System.Linq;
using Model.DTOs;

namespace VieGo.Controllers
{
  public class UserController : Controller
  {
    private readonly IUserService _userService;
    private readonly ILoginService _loginService;
    public UserController(IUserService userService, ILoginService loginService)
    {
      _userService = userService;
      _loginService = loginService;
    }

    // Index với tìm kiếm & lọc
    public async Task<IActionResult> Index(string searchString, string filter = "All")
    {
      var users = await _userService.GetAllAsync();

      // Lọc search
      if (!string.IsNullOrWhiteSpace(searchString))
      {
        users = users.Where(u =>
          u.UserId.ToString().Contains(searchString) ||
          (!string.IsNullOrEmpty(u.FullName) && u.FullName.Contains(searchString)) ||
          (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(searchString))
        ).ToList();
      }

      // Lọc theo filter
      switch (filter)
      {
        case "Active":
          users = users.Where(u => u.Status == 1).ToList();
          break;
        case "Inactive":
          users = users.Where(u => u.Status == 0).ToList();
          break;
        case "Premium":
          users = users.Where(u => u.UserType == "Premium").ToList();
          break;
        case "Standard":
          users = users.Where(u => u.UserType == "Standard").ToList();
          break;
      }

      // Giữ lại các giá trị tìm kiếm & lọc để dùng trong View nếu cần
      ViewData["CurrentSearch"] = searchString;
      ViewData["CurrentFilter"] = filter;

      return View(users);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
      if (!ModelState.IsValid)
      {
        return View(user);
      }

      var existingEmail = await _userService.GetByEmailAsync(user.Email);
      if (existingEmail != null)
      {
        ModelState.AddModelError("Email", "Email is already in use.");
        return View(user);
      }

      if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
      {
        var existingPhone = await _userService.GetByPhoneAsync(user.PhoneNumber);
        if (existingPhone != null)
        {
          ModelState.AddModelError("PhoneNumber", "Phone number is already in use.");
          return View(user);
        }
      }

      await _userService.AddAsync(user);
      return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
      if (id != user.UserId) return BadRequest();

      if (!ModelState.IsValid) return View(user);

      await _userService.UpdateUserAsync(user);
      return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      await _userService.SoftDeleteUserAsync(id);
      return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login(LoginDTO dto)
    {
      if (!ModelState.IsValid) return View(dto);

      var user = _loginService.Login(dto, out string message);
      if (user == null)
      {
        ModelState.AddModelError(string.Empty, message);
        return View(dto);
      }

      HttpContext.Session.SetInt32("UserId", user.UserId);
      HttpContext.Session.SetInt32("RoleId", user.RoleId ?? 0);
      HttpContext.Session.SetString("FullName", user.FullName);

      if (user.RoleId == 1)
        return RedirectToAction("Index", "User");
      else
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Login");
    }
  }
}

