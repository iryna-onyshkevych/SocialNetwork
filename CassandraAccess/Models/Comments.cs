﻿using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessCassandra.Models
{
    public class Comments
    {
        public TimeUuid CommentId { get; set; }
        public string Comment { get; set; }
        public Guid Commentator { get; set; }
    }
}
