﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaHo.M151.DataFormatValidator.Models.StorageModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
