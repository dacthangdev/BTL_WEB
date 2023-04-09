using System.ComponentModel.DataAnnotations;

namespace BTL.Models.RegisterViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = null!;

        public byte LoaiUser { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string HoTen { get; set; } = null!;

    }
}
