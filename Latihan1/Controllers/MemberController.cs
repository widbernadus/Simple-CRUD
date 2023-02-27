using Latihan1.DAO;
using Latihan1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Latihan1.Controllers
{
    public class MemberController : Controller
    {
        MemberDAO _dao;
        public MemberController()
        {
            _dao = new MemberDAO();
        }
        public IActionResult Index()
        {
            List<MemberModel> members = _dao.GetAllMember();
            ViewBag.members = members;
            return View();
        }

        
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(MemberModel data)
        {
            if (!ModelState.IsValid)
            {
                return Ok("error");
            }

            bool insert = _dao.InsertMember(data);
            if (insert)
            {
                TempData["success_message"] = "Berhasil menyimpan data";
            }
            else
            {
                TempData["error_message"] = "Gagal menyimpan data";
            }

            return Redirect("/Member/Create");
        }

        public IActionResult Edit(int id)
        {
            MemberModel member = _dao.GetMemberById(id);
            return View(member);
        }

        public IActionResult Update(MemberModel memberData)
        {
            if (!ModelState.IsValid)
            {
                return Ok("error");
            }

            bool insert = _dao.UpdateMember(memberData);
            if (insert)
            {
                TempData["success_message"] = "Berhasil memperbarui data";
            }
            else
            {
                TempData["error_message"] = "Gagal memperbarui data";
            }

            return Redirect("/Member/Edit/" + memberData.Id);
        }
    }
}
