using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvaApp
{
    public interface ISave
    {
        void SaveText(string filename, string text);
    }
}
