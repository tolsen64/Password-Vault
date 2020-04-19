using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoycoT_Password_Vault
{
    static class Extensions
    {
        internal static int NewID(this DataTable dt)
        {
            int i = 1;
            while (dt.AsEnumerable().Any(row => int.Parse(row["ID"].ToString()) == i))
            {
                i++;
            }

            return i;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in props)
                table.Columns.Add(prop.Name, prop.PropertyType);

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

    }
}
