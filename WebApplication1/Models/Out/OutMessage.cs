﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Out
{
    public class OutMessage
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Content { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public bool Sent { get; set; }
    }
}