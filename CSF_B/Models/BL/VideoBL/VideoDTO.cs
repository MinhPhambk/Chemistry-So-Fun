using System;
using System.Collections.Generic;

namespace CSF_B.Models.BL.VideoBL
{
    public class VideoDTO
    {
        public VideoDTO() { }

        public VideoDTO(int id, string name, string description, string link) 
        {
            this.Id = id;
            this.Name = name;
            this.Description = description ?? string.Empty;
            this.Link = link;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
