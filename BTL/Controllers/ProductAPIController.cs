using BTL.Models;
using BTL.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    [Route("/home/menu/api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        CsdlwebContext db = new CsdlwebContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var loaimon = (from p in db.Menus
                           select new Product
                           {
                               IdLoai = p.IdLoai,
                               TenMon = p.TenMon,
                               Gia = p.Gia,
                               Id = p.Id,
                               Anh = p.Anh

                           }).ToList();
            return loaimon;
        }
        [HttpGet("{iD}")]
        public IEnumerable<Product> GetAllProductsByCategory(int iD)
        {
            var loaimon = (from p in db.Menus
                           where p.IdLoai == iD
                           select new Product
                           {
                               IdLoai = p.IdLoai,
                               TenMon = p.TenMon,
                               Gia = p.Gia,
                               Id = p.Id,
                               Anh = p.Anh,
                           }).ToList();
            return loaimon;
        }
    }
}
