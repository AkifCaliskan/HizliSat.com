﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class Image : EntityBase
    {
        public string ImageUrl { get; set; }
        public bool IsCover { get; set; }
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
        public bool Status { get; set; }
    }
}
