﻿#nullable enable
namespace FakeOffice.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        public string LastName { get; set; } = null!;
        public string? FirstName { get; set; } 
        public string? Patronymic { get; set; } 
        public DateTime Birthday { get; set; }  
    }
}
