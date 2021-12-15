using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static long GuestRequestKey = 10000000;
        public static long HostingUnitKey = 20000000;
        public static long OrderKey = 30000000;
        public const int Commission = 10;
        public const int Expiration = 10;







        #region valids
        /// <summary>
        /// checks weather the name is ligeal or throw exeption
        /// </summary>
        /// <param name="_value"></param>
        public static void isValidName(string _value)
        {
            if (_value != null)
            {
                if (_value.Length > 25) throw new MyException("Too long name!! (BL)");
                char current;
                for (int i = 0; i < _value.Length; i++)
                {
                    current = _value[i];

                    if (((current < 65) && (current != 32)) || (current > 122) || ((current > 90) && (current < 96)))
                        throw new MyException("name must contain only letters and spaces! (BL)");
                }
            }
        }

        /// <summary>
        /// checks weather the Id is ligeal or throw exeption
        /// </summary>
        /// <param name="_id"></param>
        public static void isValidId(string _id)
        {
            if (_id != null)
            {
                if (_id.Length != 9) throw new MyException("Id number must have 9 digits! (BL)");
                char current;
                for (int i = 0; i < 9; i++)
                {
                    current = _id[i];
                    if ((current < 48) || (current > 57)) throw new MyException("Id must contain only numbers! (BL)");
                }
            }
        }

        /// <summary>
        /// checks if email is legal or throws exeption
        /// </summary>
        /// <param name="e"></param>
        public static void isValidEmail(string e)
        {
            if (!e.Contains("@"))
            {
                throw new MyException("Email Address is not valid (BL)");
            }
        }

        /// <summary>
        /// checks weather the cell number is ligeal or throw exeption
        /// </summary>
        /// <param name="_number"></param>
        public static void isValidCell(string _number)
        {
            if (_number != null)
            {
                if (_number.Length != 10) throw new Exception("Cell Phone number must have 10 digits! (BL)");
                char current;
                if ((_number[0] != '0') || (_number[1] != '5')) throw new Exception("Cell Phone number must begin with '05'! (BL)");
                for (int i = 0; i < 9; i++)
                {
                    current = _number[i];
                    if ((current < 48) || (current > 57)) throw new Exception("Cell Phone must contain only numbers! (BL)");
                }
            }
        }
        #endregion

    }
}
