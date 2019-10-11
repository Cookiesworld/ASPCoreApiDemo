using System;

namespace Authors.Models
{
    public class Writer
    {
        public Writer(string name, DateTime? dateOfBirth)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}