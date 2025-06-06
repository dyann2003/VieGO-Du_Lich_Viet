using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Threading.Tasks;

namespace VieGo.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IEmailSender _emailSender;

        public RegisterController(IRegisterService registerService, IEmailSender emailSender)
        {
            _registerService = registerService;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Email(RegisterEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _registerService.IsEmailRegisteredAsync(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email đã được đăng ký.");
                return View(model);
            }

            var code = _registerService.GenerateEmailConfirmationCode(model.Email);
            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Register", new { email = model.Email, code }, Request.Scheme);

           
            string emailBody = $@"
        <p>Vui lòng xác nhận email đăng ký bằng cách nhấn vào <a href='{callbackUrl}'>Nhấn vào đây để lấy mã xác nhận Email của bạn</a>.</p>
        <p>Nếu bạn không thể click vào link, vui lòng sử dụng mã xác nhận sau:</p>
        <h3>{code}</h3>
    ";

            await _emailSender.SendEmailAsync(model.Email, "Xác nhận email đăng ký", emailBody);

            TempData["Message"] = "Chúng tôi đã gửi email xác nhận đến email của bạn.";
            return RedirectToAction(nameof(ConfirmEmail), new { email = model.Email });
        }


        [HttpGet]
        public IActionResult ConfirmEmail(string email, string code)
        {
            var model = new ConfirmEmailModel { Email = email, Code = code };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var valid = await _registerService.ValidateEmailConfirmationCodeAsync(model.Email, model.Code);
            if (!valid)
            {
                ModelState.AddModelError(nameof(model.Code), "Mã xác nhận không hợp lệ hoặc hết hạn.");
                return View(model);
            }

            await _registerService.MarkEmailConfirmedAsync(model.Email);
            return RedirectToAction(nameof(CreatePassword), new { email = model.Email });
        }

        [HttpGet]
        public IActionResult CreatePassword(string email)
        {
            var model = new CreatePasswordModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePassword(CreatePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _registerService.CreateUserWithPasswordAsync(model.Email, model.Password);
            if (!success)
            {
                ModelState.AddModelError("", "Tạo mật khẩu thất bại, vui lòng thử lại.");
                return View(model);
            }

            await _emailSender.SendEmailAsync(model.Email, "Cảm ơn bạn đã đăng ký",
                "Chúc mừng bạn đã tạo tài khoản thành công! Để chào mừng thành viên mới chúng tôi gửi bạn mã code: 'NEWTRAVEL' để bạn sử dụng cho lần đầu đăng ký.");

            return RedirectToAction(nameof(ThankYou));
        }



        [HttpGet]
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
