using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;

namespace WebProject
{
    public class Users
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; }

    }
}