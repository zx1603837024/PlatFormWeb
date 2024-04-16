using F2.SystemWork.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.OptionSystem.Service
{
    public interface TUserService{
        Hashtable getTUser(TUserQuery sn);
    }
}
