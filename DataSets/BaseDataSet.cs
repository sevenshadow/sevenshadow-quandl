using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl.DataSets
{
    public class BaseDataSet
    {
        public static String GetEnumDescription(Enum e)
        {

            FieldInfo fieldInfo = e.GetType().GetField(e.ToString());

            DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (enumAttributes.Length > 0)
            {

                return enumAttributes[0].Description;

            }

            return e.ToString();

        }

    }
}
