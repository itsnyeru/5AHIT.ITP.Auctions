﻿namespace Forms.User {
    public class Edit {
        [Required]
        [MaxLength(16)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
