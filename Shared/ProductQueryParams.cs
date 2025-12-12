using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }

    

        private int _pageindex = 1;
        public int PageIndex
        {
            get { return _pageindex; }
            set
            {
                _pageindex = (value<=0 ) ? 1 : value;
            }
        }


        private const int MaxPageSize = 10;

        private const int DefaultPageSize = 5;

        private int _pageSize = DefaultPageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value<=0)
                    _pageSize = DefaultPageSize;

                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize;

                else
                    _pageSize = value;

            }
        }
    }
}
