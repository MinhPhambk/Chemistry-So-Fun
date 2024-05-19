
using CSF_B.Models.DL;
using Microsoft.EntityFrameworkCore;

namespace CSF_B.Models.BL.VideoBL
{
    public class VideoService : IVideoService
    {
        private readonly AppDBContext dbContext;

        public VideoService(AppDBContext appDbContext)
        {
            dbContext = appDbContext;
        }

        public async Task<List<VideoDTO>> GetVideo()
        {
            List<Video> listVideo = await dbContext.Videos.ToListAsync();
            List<VideoDTO> listVideoDTO = new List<VideoDTO>();

            if (listVideo != null)
            {
                foreach (Video video in listVideo)
                {
                    listVideoDTO.Add(new VideoDTO(video.Id, video.Name, video.Description, video.Link));
                }
            }

            return listVideoDTO;
        }
    }
}
