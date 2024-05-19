using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSF_B.Models.BL.VideoBL
{
    public interface IVideoService
    {
        public Task<List<VideoDTO>> GetVideo();
    }
}