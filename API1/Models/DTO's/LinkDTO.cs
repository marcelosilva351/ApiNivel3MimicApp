using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Models.DTO_s
{
    public class LinkDTO
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }

        public LinkDTO(string rel, string href, string method)
        {
            Rel = rel;
            Href = href;
            Method = method;
        }
    }
}
