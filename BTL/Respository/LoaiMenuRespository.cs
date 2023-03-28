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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
