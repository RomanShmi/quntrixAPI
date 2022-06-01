﻿using System.ComponentModel.DataAnnotations;

namespace quntrixAPI
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }= string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
