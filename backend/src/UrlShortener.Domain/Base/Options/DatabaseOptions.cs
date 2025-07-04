﻿using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Base.Options;
public class DatabaseOptions {

    public const string SectionName = "Database";

    [Required]
    public required string ConnectionString { get; set; }
}
