﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Core.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
