namespace QUANLYSANBONG.Models;

public class BlogIndexViewModel
{
    public List<BlogPost> BaiViet { get; set; } = new();
    public List<BlogCategory> DanhMuc { get; set; } = new();
    public List<BlogPost> BaiVietNoiBat { get; set; } = new();
    public string? Keyword { get; set; }
    public int? CategoryId { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class BlogDetailViewModel
{
    public BlogPost BaiViet { get; set; } = new();
    public List<BlogPost> BaiVietLienQuan { get; set; } = new();
}
