using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] PhotoContent { get; set; }

        public int UserFigureId { get; set; }
        [ForeignKey("UserFigureId")]
        public virtual UserFigure UserFigure { get; set; }
    }
}
