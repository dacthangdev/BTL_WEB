using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class CommentBlog
{
    public int Id { get; set; }

    public int IdBlog { get; set; }

    public string IdUser { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual Blog IdBlogNavigation { get; set; } = null!;

    public virtual Tuser IdUserNavigation { get; set; } = null!;
}
