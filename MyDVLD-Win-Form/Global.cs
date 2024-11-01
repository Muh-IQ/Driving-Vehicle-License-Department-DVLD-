using MyDVLD_Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Win_Form
{
    public class Global
    {
        public static clsUser CurrentUser = clsUser.FindByUserName("");
    }
}
