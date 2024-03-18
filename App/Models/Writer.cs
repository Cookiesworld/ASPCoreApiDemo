using System;
using System.ComponentModel.DataAnnotations;

namespace Authors.Models
{
    public class Writer
    {
        public Writer()
        {
        }

        public Writer(string name, DateTime? dateOfBirth, Gender gender)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        NotKnown = 2,
        NotSpecified = 3
    }
}