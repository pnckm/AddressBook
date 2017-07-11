using System.Linq;
using AddressBookDomain.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers
{
    public class CallsController : Controller
    {
        private readonly CallsRepository _callsRepo;
        private readonly UsersRepository _userRepo;

        public CallsController(CallsRepository callsRepo, UsersRepository userRepo)
        {
            _callsRepo = callsRepo;
            _userRepo = userRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _userRepo.GetUserByLogin(User.Identity.Name);
            return View(_callsRepo.GetAllUserCalls(user).OrderBy(x => x.Created));
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            var user = _userRepo.GetUserByLogin(User.Identity.Name);
            _callsRepo.Remove(user, _callsRepo.GetCallByid(id));
            return RedirectToAction("Index");
        }
    }
}