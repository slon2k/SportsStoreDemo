using System;

namespace SportsStore.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal) TotalItems / PageSize);
    }
}
