using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace webapi.Helpers
{
    public class QueryBookObjects
    {
        public string? Author{get;set;}=null;
        public string? Title{get;set;}=null;
        public string? SortBy { get; set; } = null;
        public bool IsAscending { get; set; } = true;
        public int PageNumber{get;set;}=1;
        public int PageSize{get;set;}=10;

    }
}