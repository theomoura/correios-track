using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorreiosTrack.Models
{
    public class Rastreio
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Status { get; set; }
    }
}