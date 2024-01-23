﻿using System.ComponentModel.DataAnnotations;

namespace Models.Interfaces
{
    public interface IIdentity
    {
        [Key]
        public int Id { get; set; }
    }
}
