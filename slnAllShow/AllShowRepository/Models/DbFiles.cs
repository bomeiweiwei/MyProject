using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models
{
    public partial class DbFiles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public byte[] Contnet { get; set; }
    }
}
