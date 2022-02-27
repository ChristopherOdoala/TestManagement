using System;
using System.Collections.Generic;
using System.Text;

namespace TestManagement.Core.ViewModels
{
    public class QueryViewModel
    {
        public int? PageIndex { get; set; } = 1;
        public int? PageTotal { get; set; }
        public int? PageSize { get; set; } = 10;
    }
}
