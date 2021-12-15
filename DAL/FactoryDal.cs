using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        public static IDal GetDal()
        {
            //return new imp_Dal();
            return  Dal_XML_imp.getDal_XML();
        }
    }
}
