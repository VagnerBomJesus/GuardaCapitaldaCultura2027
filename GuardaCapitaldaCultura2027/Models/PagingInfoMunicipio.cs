﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCapitaldaCultura2027.Models
{
    public class PagingInfoMunicipio
    {
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int NUMERO_ANTES_DEPOIS = 3;

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
