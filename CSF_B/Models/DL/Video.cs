using System;
using System.Collections.Generic;

namespace CSF_B.Models.DL
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Link { get; set; } = null!;
    }
}
