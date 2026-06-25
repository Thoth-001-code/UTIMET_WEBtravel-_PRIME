using System.Collections.Generic;

namespace QuanLyDuLich.Models.DTOs.Common
{
    public class PagedResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)System.Math.Ceiling((double)TotalCount / PageSize);
        public List<T> Items { get; set; } = new List<T>();
    }
}