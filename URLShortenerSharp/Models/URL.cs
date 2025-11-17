using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerSharp.Models
{
    public class URL
    {
        public Guid Id { get; set; }
        public string ShortAddress { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int ClicksCount { get; set; }

        public string FullURL { get; set; }
    }
}
