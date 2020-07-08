using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRestService.Requests
{
    public class AlbumRequest
    {
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }
    }
}
