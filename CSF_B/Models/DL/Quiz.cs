using System;
using System.Collections.Generic;

namespace CSF_B.Models.DL
{
    public partial class Quiz
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description1 { get; set; } = null!;
        public string? Description2 { get; set; }
    }
}
