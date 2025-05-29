using System;

namespace Core.Concretes.DTOs.Panel
{
    public class BrandDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? LogoImagePath { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
} 