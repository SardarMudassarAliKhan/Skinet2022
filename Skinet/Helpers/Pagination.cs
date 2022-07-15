using Skinet.Core.Entities;
using Skinet.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Helpers
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
