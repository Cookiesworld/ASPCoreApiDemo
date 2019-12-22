using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Dapper;
using Dapper.FluentMap.Mapping;

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

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        NotKnown,
        NotSpecified
    }
}