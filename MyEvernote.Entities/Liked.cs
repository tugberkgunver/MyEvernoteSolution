using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    //Bir notun birden fazla beğenisi ve bir user birden fazla note beğenmesi
    //Miras almıyorum diğer özellikleri istemiyorum
    [Table("Likes")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Note Notes { get; set; }
        public virtual EvernoteUser LikedUser { get; set; }
    }
}
