using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_DL.DTOs
{
    public class TxtNaamDTO
    {
        public TxtNaamDTO(int? id, string value, int? frequency)
        {
            Id = id;
            Value = value;
            Frequency = frequency;
        }

        public int? Id { get; set; }
        public string Value { get; set; }
        public int? Frequency { get; set; }
    }
}
