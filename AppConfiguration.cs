﻿namespace FileCopyingMachine
{
    internal sealed class AppConfiguration
    {
        public List<string>? InitialDirectories { get; set; }
        public string? TargetDirectory { get; set; }
        public string? LogLevel { get; set; }
    }
}
