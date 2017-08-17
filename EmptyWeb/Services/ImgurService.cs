using EmptyWeb.Data;
using Imgur.API;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System.IO;
using System.Threading.Tasks;

namespace EmptyWeb.Services
{
    public class ImgurService
    {
        private readonly GSSPHNDbContext _context;
        private readonly LoggingService _logger;
        private readonly string CLIENT_ID = "20d2359fb178560";
        private readonly string CLIENT_SECRET = "285d1450c3fe69299dbfde9ddc80adbeb83b18db";

        public ImgurService(GSSPHNDbContext context, LoggingService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IImage> UploadImage(Stream stream)
        {
            try
            {
                var client = new ImgurClient(CLIENT_ID, CLIENT_SECRET);
                var endpoint = new ImageEndpoint(client);
                IImage image = await endpoint.UploadImageStreamAsync(stream);
                _logger.WriteLog(string.Format("{0} (ID: {1}, SIZE: {2}, BW: {3}) was uploaded on {4}", image.Name, image.Id, image.Size, image.Bandwidth, image.DateTime));
                return image;
            }
            catch (ImgurException imgurEx)
            {
                _logger.WriteLog(imgurEx.Message);
            }
            return null;
        }

    }
}
