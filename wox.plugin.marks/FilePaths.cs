using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wox.plugin.marks
{
    public class FilePaths
    {
        public string name { get; set; }
        public string path { get; set; }
        /// <summary>
        /// 0 网址，1 目录，2应用
        /// </summary>
        public int type { get; set; }
    }
}
