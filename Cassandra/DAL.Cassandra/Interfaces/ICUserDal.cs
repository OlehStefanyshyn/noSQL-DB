using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Interfaces
{
    public interface ICUserDal
    {
        void Check(string check);
        void Follow(string n, string s, string e);
    }
}
