using ValuteSalute.Domain.Entities;

namespace ValuteSalute.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public PageViewModel
        (
            int count,
            int pageNumber,
            int pageSize
        )
        {
            this.PageNumber = pageNumber;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (this.PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (this.PageNumber < this.TotalPages);
            }
        }
    }
}
