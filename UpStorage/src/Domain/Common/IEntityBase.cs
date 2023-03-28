﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IEntityBase<Tkey>
    {

        public Tkey Id { get; set; }
    }
}
