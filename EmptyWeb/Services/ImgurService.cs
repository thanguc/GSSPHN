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
        private static readonly string CLIENT_ID = "f925854f0fc5267";
        private static readonly string CLIENT_SECRET = "003eb20c71c7536671aae7f9d93aa2d4b13e2261";

        public static async Task<IImage> UploadImage(Stream stream)
        {
            try
            {
                var client = new ImgurClient(CLIENT_ID, CLIENT_SECRET);
                var endpoint = new ImageEndpoint(client);
                IImage image = await endpoint.UploadImageStreamAsync(stream);
                //Debug.Write("Image uploaded. Image Url: " + image.Link);
                return image;
            }
            catch (ImgurException)
            {
                //Debug.Write("Error uploading the image to Imgur");
                //Debug.Write(imgurEx.Message);
            }
            return null;
        }


    }
}
