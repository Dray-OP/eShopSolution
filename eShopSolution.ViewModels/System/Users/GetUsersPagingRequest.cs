﻿using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class GetUsersPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
