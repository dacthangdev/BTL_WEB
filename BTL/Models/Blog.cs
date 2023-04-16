using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class Blog
{
    public int Id { get; set; }
    [DisplayName("Tác Giả")]
    public string TacGia { get; set; } = null!;
    [DisplayName("Tiêu đề")]
    public string TieuDe { get; set; } = null!;
    [DisplayName("Nội dung")]
    public string NoiDung { get; set; } = null!;
    [DisplayName("Ảnh")]
    public string Anh { get; set; } = null!;
    [DisplayName("Bình Luận")]
    public DateTime NgayDang { get; set; }

    public virtual ICollection<CommentBlog> CommentBlogs { get; } = new List<CommentBlog>();
}
