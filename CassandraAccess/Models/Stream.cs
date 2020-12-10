﻿using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessCassandra.Models
{
    public class Stream
    {
        public string UserName { get; set; }
        public TimeUuid LastUpdatedAt { get; set; }
        public Guid PostId { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }

        private List<Comments> Comments { get; set; }
    }
}
