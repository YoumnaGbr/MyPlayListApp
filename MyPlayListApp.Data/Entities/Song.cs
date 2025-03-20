using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.Entities
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SingerId { get; set; }
        public virtual Singer Singer { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
