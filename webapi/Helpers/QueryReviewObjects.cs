using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Helpers
{
    public class QueryReviewObjects
    {
        public string? Title{get;set;}
        public bool IsAscendingNewest { get; set; } = true;
    }
}