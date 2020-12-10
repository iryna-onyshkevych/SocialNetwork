using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraAccess.Repository.Interfaces
{
    interface IComment
    {
        public void AddComment(ISession session);
    }
}
