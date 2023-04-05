﻿using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatBanAPIController : ControllerBase
    {
        CsdlwebContext db = new CsdlwebContext();
        [HttpGet]
        public List<DatBan> GetAllDatban()
        {
            return db.DatBans.ToList();
        }
        [HttpPost]
        public bool AddDatBan(string name, string email, string phone, DateTime date, string time, string person)
        {
            DatBan datBan = new DatBan();
            datBan.Name_KH = name;
            datBan.Email = email;
            datBan.Phone = phone;
            datBan.NgayDat = date;
            datBan.Gio_Nhan = time;
            datBan.Sl = person;         
            try
            {
                db.DatBans.Add(datBan);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
