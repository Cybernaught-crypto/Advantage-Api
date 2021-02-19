using System.Collections.Generic;
using System.Linq;

namespace Advantage.Api
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int i, int len)
        {

            // [1] page, 10 results. On the first page i = 1 - 1 would give zero. Results in skipping and taking the first 10 (len) result

            Data = data.Skip((i - 1) * len).Take(len).ToList();

            Total = data.Count();
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

    }
}