using CassandraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Profiles
{
    public class CommentProfile: CustomUdtMappings
    {
        public CommentProfile()
        {
            For<CassandraDTO.Comment>()
                    .Map(scr => scr.User_Id, "User_Id")
                    .Map(scr => scr.Body, "Body")
                    .Map(scr => scr.Create_Date, "Create_Date")
                    .Map(scr => scr.Modify_Date, "Modify_Date");
        }
    }
}
