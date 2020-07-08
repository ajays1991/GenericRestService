using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRestService.Responses
{
    public class AlbumResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }
    }
}
