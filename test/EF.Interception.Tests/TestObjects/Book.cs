using System;
using System.ComponentModel.DataAnnotations;

namespace EF.Interception.Tests
{
    public class Book : IAuditedEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required]
        public DateTime? ModifiedAt { get; set; }
    }
}