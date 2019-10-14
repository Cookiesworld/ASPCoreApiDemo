using System;
using System.ComponentModel.DataAnnotations;

namespace Authors.Models
{
    public class Writer
    {
        public Writer() 
        {
        }

        public Writer(string name, DateTime? dateOfBirth)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        [Key]
        public long Id  { get;set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}