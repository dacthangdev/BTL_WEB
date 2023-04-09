using BTL.Models;

namespace BTL.Respository
{
    public class LoaiMenuRespository : ILoaiMenuRespository
    {
        private readonly CsdlwebContext _context;
        public LoaiMenuRespository(CsdlwebContext context)
        {
            _context = context;
        }
        public LoaiMonAn Add(LoaiMonAn loaiMonAn)
        {
            _context.Add(loaiMonAn);
            _context.SaveChanges();
            return loaiMonAn;
        }

        public LoaiMonAn Delete(string ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoaiMonAn> GetAllLoai()
        {
            return _context.LoaiMonAns;
        }

        public LoaiMonAn GetLoaiMon(string ID)
        {
            return _context.LoaiMonAns.Find(ID);
        }

        public LoaiMonAn Update(LoaiMonAn loaiMonAn)
        {
            _context.Update(loaiMonAn);
            _context.SaveChanges();
            return loaiMonAn;
        }
    }
}
